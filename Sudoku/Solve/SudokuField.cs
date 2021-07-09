/*
  This file is part of Sudoku - A library to solve a sudoku.

  Copyright (c) Herbert Aitenbichler

  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
  to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
  and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
  WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
*/

namespace Sudoku.Solve
{
    using System.Collections.Generic;
    using System.Linq;

    using global::Sudoku.Solve.NotPossible;
    using global::Sudoku.Solve.Tools;

    public class SudokuField
    {
        #region Data

        public int AbsRow { get; set; }
        public int AbsCol { get; set; }

        public (int Row, int Col) AbsRowCol => (AbsRow, AbsCol);

        public int No { get; internal set; }

        public bool IsEmpty => No == 0;
        public bool HasNo   => No != 0;

        public string UserNote => _userNote ?? "";

        internal void SetUserNote(string userNote)
        {
            _userNote = userNote;
        }

        private string _userNote;

        #endregion

        #region Calculation Help Variabled

        private readonly bool[]            _mainRulePossible  = new bool[9];
        private readonly NotPossibleBase[] _notPossibleReason = new NotPossibleBase[9];

        private bool[] _notPossible = new bool[9];
        private bool[] _notPossibleUncommitted;

        internal bool[] MainRulePossible => _mainRulePossible;

        #endregion

        #region External Access functions

        internal void InitHelpVar()
        {
            _userNote = UserNote;

            _mainRulePossible.Init(false);
            _notPossible.Init(false);

            _notPossibleUncommitted = null;
        }

        private static int NoToIdx(int no)
        {
            return no - 1;
        }

        public bool IsPossible(int no)
        {
            return IsPossibleMainRule(no) && !_notPossible[NoToIdx(no)];
        }

        public bool IsPossibleMainRule(int no)
        {
            return IsEmpty && _mainRulePossible[NoToIdx(no)];
        }

        public bool IsNotPossible(int no)
        {
            return IsPossibleMainRule(no) && _notPossible[NoToIdx(no)];
        }

        public IEnumerable<int> GetPossibleNos()
        {
            return LoopExtensions.Nos.Where(IsPossible);
        }

        public IEnumerable<int> GetPossibleMainRuleNos()
        {
            return LoopExtensions.Nos.Where(IsPossibleMainRule);
        }

        public IEnumerable<NotPossibleBase> GetNotPossible()
        {
            return LoopExtensions.Nos.Where(IsNotPossible).Select(no => _notPossibleReason[NoToIdx(no)]);
        }

        public IEnumerable<int> GetNotPossibleNos()
        {
            return GetNotPossible().Select(not => not.ForNo);
        }


        public NotPossibleBase GetNotPossible(int no)
        {
            return IsNotPossible(no) ? _notPossibleReason[NoToIdx(no)] : null;
        }

        public int PossibleCount()
        {
            return LoopExtensions.Nos.Count(IsPossible);
        }

        public int MainRulePossibleCount()
        {
            return IsEmpty ? _mainRulePossible.Count(x => x) : 0;
        }

        internal void SetNotPossible(int no, NotPossibleBase reason)
        {
            if (_notPossibleUncommitted == null)
            {
                _notPossibleUncommitted = (bool[])_notPossible.Clone();
            }

            var idx = NoToIdx(no);
            _notPossibleUncommitted[idx] = true;
            _notPossibleReason[idx]      = reason;
        }

        internal bool IsSubSetPossible(SudokuField field)
        {
            return !LoopExtensions.Nos.Any(no => IsPossible(no) != field.IsPossible(no) && !IsPossible(no));
        }

        internal void CommitChanges()
        {
            if (_notPossibleUncommitted != null)
                _notPossible = _notPossibleUncommitted;
            _notPossibleUncommitted = null;
        }

        #endregion
    }
}