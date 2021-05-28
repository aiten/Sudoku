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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using global::Sudoku.Solve.Tools;

    public class Sudoku
    {
        #region Private Data

        private readonly SudokuField[,] _fields      = new SudokuField[9, 9];
        private readonly string[]       _userNoteCol = new string[9];
        private readonly string[]       _userNoteRow = new string[9];

        private int _setCount;

        #endregion

        #region Constructor / Initalisation / properties

        public Sudoku()
        {
            for (var x = 0; x < 9; x++)
            for (var y = 0; y < 9; y++)
                _fields[x, y] = new SudokuField();
        }

        public int  StepCount => _setCount;
        public bool Modified  { get; internal set; }

        #endregion

        #region Get Field Information

        public int Get(int x, int y)
        {
            return GetDef(x, y).No;
        }

        public SudokuField GetDef(int x, int y)
        {
            return _fields[x, y];
        }

        private delegate SudokuField GetSudokuField(int x, int y);

        private SudokuField GetSudokuFieldRow(int x, int y)
        {
            return GetDef(x, y);
        }

        private SudokuField GetSudokuFieldCol(int x, int y)
        {
            return GetDef(y, x);
        }

        private SudokuField GetSudokuFieldS3(int x, int y)
        {
            var x3 = (x / 3) * 3;
            var y3 = (x % 3) * 3;

            var dx = y / 3;
            var dy = y % 3;

            return _fields[x3 + dx, y3 + dy];
        }

        #endregion

        #region Set and Validation

        public bool SetNextPossible(int x, int y)
        {
            var def = GetDef(x, y);
            var no  = def.No;

            if (no == 0)
            {
                var only = def.OnlyPossible();
                if (only > 0 && Set(x, y, only))
                {
                    return true;
                }
            }

            for (var i = 1; i <= 10; i++)
            {
                var newNo = no + i;

                if (newNo > 9) newNo -= 10;
                if (Set(x, y, newNo))
                {
                    return true;
                }
            }

            Clear(x, y);
            return false;
        }

        public bool Set(int x, int y, int no)
        {
            if (no < 0 || no > 9)
                return false;

            if (!CanSet(x, y, no))
            {
                return false;
            }

            var def = GetDef(x, y);
            var old = def.No;

            def.No = no;

            AddUndo(x, y, old);

            if ((old == 0 && no > 0))
            {
                _setCount++;
            }
            else if (old > 0 && no == 0)
            {
                _setCount--;
            }

            return true;
        }

        public bool Clear(int x, int y)
        {
            return Set(x, y, 0);
        }

        private bool CanSet(int idx, int no, GetSudokuField getDef)
        {
            for (var i = 0; i < 9; i++)
            {
                if (getDef(idx, i).No == no)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CanSet(int x, int y, int no)
        {
            return no == 0 ||
                   (CanSet(x,                   no, GetSudokuFieldRow) &&
                    CanSet(y,                   no, GetSudokuFieldCol) &&
                    CanSet((x / 3) * 3 + y / 3, no, GetSudokuFieldS3));
        }

        private bool SetNoUndo(int x, int y, int no)
        {
            var old = UndoAvailable;
            UndoAvailable = false;
            var ret = Set(x, y, no);
            UndoAvailable = old;
            return ret;
        }

        #endregion

        #region Undo / Redo

        private class UndoInfo
        {
            public int X;
            public int Y;
            public int No;
        };

        private readonly List<UndoInfo> _undoList = new List<UndoInfo>();

        public bool UndoAvailable { get; set; } = false;

        public bool Undo()
        {
            if (CanUndo())
            {
                var undo = _undoList.Last();
                SetNoUndo(undo.X, undo.Y, undo.No);
                _undoList.RemoveAt(_undoList.Count - 1);
                return true;
            }

            return false;
        }

        public bool CanUndo()
        {
            return _undoList.Count > 0;
        }

        public void AddUndo(int x, int y, int no)
        {
            if (UndoAvailable)
            {
                _undoList.Add(new UndoInfo
                {
                    X  = x,
                    Y  = y,
                    No = no
                });
                Modified = true;
            }
        }

        public void ClearUndo()
        {
            _undoList.Clear();
        }

        #endregion

        #region UserNote

        public void SetUserNote(int x, int y, string userNote)
        {
            var def = GetDef(x, y);
            if (String.Compare(def.UserNote, userNote, StringComparison.Ordinal) != 0)
                Modified = true;
            def.SetUserNote(userNote);
        }

        public string GetUserNoteRow(int idx)
        {
            return _userNoteRow[idx] ?? "";
        }

        public string GetUserNoteCol(int idx)
        {
            return _userNoteCol[idx] ?? "";
        }

        public void SetUserNoteRow(int idx, string userNote)
        {
            var old = GetUserNoteRow(idx);
            if (String.Compare(old, userNote, StringComparison.Ordinal) != 0)
                Modified = true;
            _userNoteRow[idx] = userNote;
        }

        public void SetUserNoteCol(int idx, string userNote)
        {
            var old = GetUserNoteCol(idx);
            if (String.Compare(old, userNote, StringComparison.Ordinal) != 0)
                Modified = true;
            _userNoteCol[idx] = userNote;
        }

        #endregion

        #region Calc Possibilities

        private readonly bool[] _foundIdx1 = new bool[9];
        private readonly bool[] _foundIdx2 = new bool[9];

        private void CommitChanges()
        {
            for (var x = 0; x < 9; x++)
            {
                for (var y = 0; y < 9; y++)
                {
                    GetSudokuFieldCol(x, y).CommitChanges();
                }
            }
        }

        private int UpdatePossibleBlockade1(GetSudokuField getDef, char rowcol3)
        {
            // set all onlyPossible 
            var changCount = 0;

            for (var z = 0; z < 9; z++)
            {
                for (var x = 0; x < 9; x++)
                {
                    for (var y = 0; y < 9; y++)
                    {
                        var def = getDef(x, y);
                        if (def.No == 0 && def.IsPossible(z + 1))
                        {
                            var foundOther = false;
                            for (var t = 0; t < 9; t++)
                            {
                                if (t != y)
                                {
                                    var def2 = getDef(x, t);
                                    if (def2.No == 0 && def2.IsPossible(z + 1))
                                    {
                                        foundOther = true;
                                        break;
                                    }
                                }
                            }

                            if (!foundOther)
                            {
                                for (var t = 0; t < 9; t++)
                                {
                                    if (t != z && !def.IsNotPossible(t + 1))
                                    {
                                        changCount++;
                                        def.SetNotPossibleBlockade1(t + 1, rowcol3, z + 1);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return changCount;
        }

        private int UpdatePossibleBlockade2(GetSudokuField getDef, char rowcol3)
        {
            // set all onlyPossible 
            var changCount = 0;

            for (var x = 0; x < 9; x++)
            {
                for (var y = 0; y < 9; y++)
                {
                    var def = getDef(x, y);
                    if (def.No == 0)
                    {
                        _foundIdx1.Init(false);

                        _foundIdx1[y] = true;
                        var foundCount = 1;

                        for (var t = 0; t < 9; t++)
                        {
                            if (t != y)
                            {
                                var def2 = getDef(x, t);
                                if (def2.No == 0)
                                {
                                    if (def.IsSubSetPossible(def2))
                                    {
                                        _foundIdx1[t] = true;
                                        foundCount++;
                                    }
                                }
                            }
                        }

                        if (foundCount == def.PossibleCount())
                        {
                            string reasonPossible = null;
                            string reasonIndex    = null;
                            for (var t = 0; t < 9; t++)
                            {
                                if (_foundIdx1[t])
                                {
                                    var def2 = getDef(x, t);
                                    if (def2.No == 0)
                                    {
                                        if (string.IsNullOrEmpty(reasonPossible))
                                        {
                                            reasonPossible = def2.PossibleString();
                                        }

                                        reasonIndex = reasonIndex.Add(',', t + 1);
                                    }
                                }
                            }

                            for (var t = 0; t < 9; t++)
                            {
                                if (!_foundIdx1[t])
                                {
                                    var def2 = getDef(x, t);
                                    if (def2.No == 0)
                                    {
                                        for (var z = 1; z <= 9; z++)
                                        {
                                            if (def.IsPossible(z) && def2.IsPossible(z))
                                            {
                                                changCount++;
                                                def2.SetNotPossibleBlockade2(z, rowcol3, reasonPossible, reasonIndex);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return changCount;
        }

        private int UpdatePossibleBlockade2SubSet(GetSudokuField getDef, char rowcol3)
        {
            // set all onlyPossible 
            var changCount = 0;

            for (var x = 0; x < 9; x++)
            {
                for (var y = 0; y < 9; y++)
                {
                    var def = getDef(x, y);
                    if (def.No == 0)
                    {
                        _foundIdx1.Init(false);
                        _foundIdx1[y] = true;

                        if (FindSubSet(getDef, x, y + 1, _foundIdx1, _foundIdx2))
                        {
                            string reasonPossible = null;
                            string reasonIndex    = null;
                            for (var t = 0; t < 9; t++)
                                if (_foundIdx1[t])
                                {
                                    var def2 = getDef(x, t);
                                    if (def2.No == 0)
                                    {
                                        if (string.IsNullOrEmpty(reasonPossible))
                                        {
                                            for (var z = 1; z <= 9; z++)
                                            {
                                                if (_foundIdx2[z - 1])
                                                {
                                                    reasonPossible = reasonPossible.Add(',', z);
                                                }
                                            }
                                        }

                                        reasonIndex = reasonIndex.Add(',', t + 1);
                                    }
                                }

                            for (var t = 0; t < 9; t++)
                            {
                                if (!_foundIdx1[t])
                                {
                                    var def2 = getDef(x, t);
                                    if (def2.No == 0)
                                    {
                                        for (var z = 1; z <= 9; z++)
                                        {
                                            if (_foundIdx2[z - 1] && def2.IsPossible(z))
                                            {
                                                changCount++;
                                                def2.SetNotPossibleBlockade2P(z, rowcol3, reasonPossible, reasonIndex);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return changCount;
        }

        private int UpdatePossibleBlockade3(GetSudokuField getDef, char rowcol3)
        {
            // set all onlyPossible 
            var changCount = 0;

            for (var x = 0; x < 9; x++)
            {
                for (var y = 0; y < 9; y++)
                {
                    var def = getDef(x, y);
                    if (def.No == 0)
                    {
                        for (var z = 1; z <= 9; z++)
                        {
                            string info = null;

                            if (def.IsPossible(z))
                            {
                                // test all in same sudoku but not in same row/col

                                var s3x = x / 3;
                                var s3y = y / 3;
                                int t;

                                for (t = 0; t < 9; t++)
                                {
                                    var sx   = s3x * 3 + t / 3;
                                    var sy   = s3y * 3 + t % 3;
                                    var def2 = getDef(sx, sy);
                                    if (def2.No == 0)
                                    {
                                        if (x != sx)
                                        {
                                            if (def2.IsPossible(z))
                                            {
                                                // abort with z
                                                break;
                                            }
                                        }
                                        else if (def2.IsPossible(z))
                                        {
                                            info = info.Add(',', (sy + 1));
                                        }
                                    }
                                }

                                if (t >= 9)
                                {
                                    for (t = 0; t < 9; t++)
                                    {
                                        if (t / 3 != y / 3)
                                        {
                                            var def2 = getDef(x, t);
                                            if (def2.No == 0 && def2.IsPossible(z))
                                            {
                                                changCount++;
                                                def2.SetNotPossibleBlockade3(z, rowcol3, info);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return changCount;
        }

        private bool FindSubSet(GetSudokuField getDef, int x, int y, bool[] use, bool[] noSet)
        {
            // find union with backtracking

            if (y >= 9)
                return false;

            if (IsSubSet(getDef, x, use, noSet))
                return true;

            int z;
            for (z = y; z < 9; z++)
            {
                use[z] = true;
                if (FindSubSet(getDef, x, z + 1, use, noSet))
                {
                    return true;
                }

                use[z] = false;
            }

            return false;
        }

        private bool IsSubSet(GetSudokuField getDef, int x, bool[] use, bool[] noSet)
        {
            var countUsed     = 0;
            var countPossible = 0;
            var countSet      = 0;

            noSet.Init(false);

            for (var y = 0; y < 9; y++)
            {
                var def = getDef(x, y);
                if (def.No == 0)
                {
                    countPossible++;
                    if (use[y])
                    {
                        countUsed++;
                        for (var t = 1; t <= 9; t++)
                        {
                            if (def.IsPossible(t))
                            {
                                if (!noSet[t - 1])
                                {
                                    countSet++;
                                    noSet[t - 1] = true;
                                }
                            }
                        }
                    }
                }
            }

            return countSet == countUsed && countUsed != countPossible && countSet > 1;
        }

        private bool HavePossibility()
        {
            // Update with sudoku (mainRule) rules

            for (var x = 0; x < 9; x++)
            {
                for (var y = 0; y < 9; y++)
                {
                    var def = GetDef(x, y);

                    if (def.No == 0 && def.OnlyPossible() > 0)
                        return true;
                }
            }

            return false;
        }

        public void UpdatePossible()
        {
            // Update with sudoku (mainRule) rules

            for (var x = 0; x < 9; x++)
            {
                for (var y = 0; y < 9; y++)
                {
                    var def = GetDef(x, y);
                    def.InitHelpVar();

                    if (def.No == 0)
                    {
                        for (var z = 1; z <= 9; z++)
                        {
                            if (SetNoUndo(x, y, z))
                            {
                                def.MainRulePossible[z - 1] = true;
                            }
                        }

                        SetNoUndo(x, y, 0);
                    }
                }
            }

            CommitChanges();

            const char row = 'R';
            const char col = 'C';
            const char s3  = 'X';

            UpdatePossibleBlockade1(GetSudokuFieldS3, s3);

            CommitChanges();

            while (!HavePossibility())
            {
                UpdatePossibleBlockade1(GetSudokuFieldRow, row);
                UpdatePossibleBlockade1(GetSudokuFieldCol, col);
                UpdatePossibleBlockade1(GetSudokuFieldS3,  s3);

                CommitChanges();

                if (HavePossibility())
                    break;

                var changeCount = 0;

                if (0 < UpdatePossibleBlockade3(GetSudokuFieldRow, row)) changeCount++;
                if (0 < UpdatePossibleBlockade3(GetSudokuFieldCol, col)) changeCount++;

                if (0 < UpdatePossibleBlockade2(GetSudokuFieldRow, row)) changeCount++;
                if (0 < UpdatePossibleBlockade2(GetSudokuFieldCol, col)) changeCount++;
                if (0 < UpdatePossibleBlockade2(GetSudokuFieldS3,  s3)) changeCount++;

                if (0 < UpdatePossibleBlockade2SubSet(GetSudokuFieldRow, row)) changeCount++;
                if (0 < UpdatePossibleBlockade2SubSet(GetSudokuFieldCol, col)) changeCount++;
                if (0 < UpdatePossibleBlockade2SubSet(GetSudokuFieldS3,  s3)) changeCount++;

                CommitChanges();

                if (changeCount == 0)
                    break;
            }
        }

        #endregion

        #region Other public User Functions

        public bool Finish()
        {
            var dummy = 0;
            ClearUndo();
            return _CalcPossibleSolutions(-1, 9, true, ref dummy, new CancellationToken()) == 1;
        }

        #endregion

        #region Calc Possibile Solutions (BackTracking)

        public delegate void FoundSolutionHandler(object sudoku, SudokuEventArgs solutionInfo);

        public event FoundSolutionHandler FoundSolution;

        protected virtual void OnFoundSolution(object sudoku, SudokuEventArgs solutionInfo)
        {
            if (FoundSolution != null)
            {
                FoundSolution(sudoku, solutionInfo);
            }
        }

        private volatile bool   _endCalc    = false;
        private          Sudoku _calcSudoku = null;

        public int CalcPossibleSolutions(CancellationToken cancellationToken)
        {
            OnFoundSolution(this, new SudokuEventArgs(0));

            _endCalc                  = false;
            _calcSudoku               = this.Clone();
            _calcSudoku.FoundSolution = FoundSolution;
            var possibleSolutions = 0;
            var ret               = _calcSudoku._CalcPossibleSolutions(-1, 9, false, ref possibleSolutions, cancellationToken);
            _calcSudoku = null;
            OnFoundSolution(this, new SudokuEventArgs(ret, true));
            return ret;
        }

        public void StopPossibleSolutions()
        {
            _endCalc = true;
            if (_calcSudoku != null)
                _calcSudoku.StopPossibleSolutions();
        }

        private int _CalcPossibleSolutions(int x, int y, bool calcEnd, ref int possibleSolutions, CancellationToken cancellationToken)
        {
            if (!calcEnd && _endCalc)
                return -1;

            if (StepCount >= 9 * 9)
            {
                possibleSolutions = possibleSolutions > 1 ? possibleSolutions : 1;

                if (!cancellationToken.IsCancellationRequested)
                {
                    OnFoundSolution(this, new SudokuEventArgs(possibleSolutions));
                }

                return 1;
            }


            var results = 0;
            int myx;
            int myy;
            y++;
            if (y >= 9)
            {
                x++;
                y = 0;
            }

            if (x > 8)
            {
                possibleSolutions = possibleSolutions > 1 ? possibleSolutions : 1;
                return 1;
            }

            for (myx = x; myx < 9; myx++)
            {
                for (myy = y; myy < 9; myy++)
                {
                    var no = Get(myx, myy);

                    if (no == 0)
                    {
                        //int old = no;
                        for (var newNo = 1; newNo <= 9; newNo++)
                        {
                            if (SetNoUndo(myx, myy, newNo))
                            {
                                if (cancellationToken.IsCancellationRequested)
                                {
                                    return -1;
                                }

                                var res = _CalcPossibleSolutions(myx, myy, calcEnd, ref possibleSolutions, cancellationToken);
                                if (res < 0)
                                {
                                    if (!calcEnd)
                                        SetNoUndo(myx, myy, 0);
                                    return res; // abort
                                }

                                if (res > 0)
                                {
                                    if (calcEnd)
                                    {
                                        return 1;
                                    }

                                    results           += res;
                                    possibleSolutions =  possibleSolutions > results ? possibleSolutions : results;
                                }
                            }
                        }

                        SetNoUndo(myx, myy, 0);
                        return results;
                    }
                }

                y = 0;
            }

            return 0;
        }

        #endregion
    }
}