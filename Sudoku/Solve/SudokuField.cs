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
    using System.Drawing;
    using System.Text;

    using global::Sudoku.Solve.Tools;

    public class SudokuField
    {
        #region Data

        public int No { get; internal set; }

        public string UserNote => _userNote ?? "";

        internal void SetUserNote(string userNote)
        {
            _userNote = userNote;
        }

        private string _userNote;

        #endregion

        #region Calculation Help Variabled

        private readonly bool[]   _mainRulePossible  = new bool[9];
        private readonly string[] _notPossibleReason = new string[9];

        private bool[] _notPossible        = new bool[9];
        private bool[] _notPossibleChanged = new bool[9];

        internal bool[] MainRulePossible => _mainRulePossible;

        #endregion

        #region Private Help Functions

        private int PossibleCount(bool[] ar)
        {
            var count = 0;
            if (No == 0)
            {
                for (var x = 0; x < 9; x++)
                    if (ar[x])
                        count++;
            }

            return count;
        }

        #endregion

        #region External Access functions

        internal void InitHelpVar()
        {
            _userNote = UserNote;

            _mainRulePossible.Init(false);
            _notPossible.Init(false);

            _notPossibleChanged = null;
        }

        internal bool IsPossible(int no)
        {
            var idx = no - 1;
            return _mainRulePossible[idx] && !_notPossible[idx];
        }

        internal bool IsNotPossible(int no)
        {
            var idx = no - 1;
            return _notPossible[idx];
        }

        internal bool IsSubSetPossible(SudokuField field)
        {
            for (var z = 1; z <= 9; z++)
            {
                if (IsPossible(z) != field.IsPossible(z))
                {
                    if (!IsPossible(z))
                        return false;
                }
            }

            return true;
        }

        internal void SetNotPossible(int no, string reason)
        {
            if (_notPossibleChanged == null)
            {
                _notPossibleChanged = new bool[9];
                for (var z = 0; z < 9; z++)
                {
                    _notPossibleChanged[z] = _notPossible[z];
                }
            }

            var idx = no - 1;
            _notPossibleChanged[idx] = true;
            _notPossibleReason[idx]  = reason;
        }

        internal void CommitChanges()
        {
            if (_notPossibleChanged != null)
                _notPossible = _notPossibleChanged;
            _notPossibleChanged = null;
        }

        public int PossibleCount()
        {
            int x;
            var ret = 0;
            for (x = 1; x <= 9; x++)
            {
                if (IsPossible(x))
                {
                    ret++;
                }
            }

            return ret;
        }

        public int OnlyPossible()
        {
            int x;
            var ret = 0;
            for (x = 1; x <= 9; x++)
            {
                if (IsPossible(x))
                {
                    if (ret != 0)
                        return 0;
                    ret = x;
                }
            }

            return ret;
        }

        public int MainRulePossibleCount()
        {
            return PossibleCount(_mainRulePossible);
        }

        #endregion

        #region Frame Output

        public Color ToButtonColor(SudokuOptions opt)
        {
            if (No != 0)
                return Color.Green;

            if (opt.Help)
            {
                switch (PossibleCount())
                {
                    default: return Color.Gray;
                    case 0:  return Color.Red;
                    case 1:  return Color.LightGray;
                }
            }

            return Color.Gray;
        }

        private string ToButtonStringMainRuleOnly()
        {
            if (No > 0)
            {
                return No.ToString();
            }

            var str2 = new StringBuilder();

            for (var z = 0; z < 9; z++)
            {
                if (MainRulePossible[z])
                {
                    if (str2.Length > 0)
                        str2.Append(",");
                    str2.Append((z + 1).ToString());
                }
            }

            return str2.ToString();
        }

        public string ToButtonToolTip(SudokuOptions opt)
        {
            if (!opt.ShowToolTip)
                return "";

            if (opt.Help)
            {
                var    ret    = ToButtonString(opt);
                string reason = "";
                if (No == 0)
                {
                    for (var z = 0; z < 9; z++)
                    {
                        if (MainRulePossible[z])
                        {
                            if (_notPossible[z])
                            {
                                reason += '\n';

                                if (_notPossibleReason[z][0] == 'B')
                                {
                                    if (opt.ShowNormalized)
                                    {
                                        reason += _notPossibleReason[z];
                                    }
                                    else
                                    {
                                        reason += _notPossibleReason[z].GetNotPossibleReason();
                                    }
                                }
                                else
                                {
                                    reason = reason + (z + 1).ToString();
                                    reason = reason + ": ";
                                    reason = reason + _notPossibleReason[z];
                                }
                            }
                        }
                    }
                }

                return ret + reason;
            }

            return ToButtonStringMainRuleOnly();
        }


        public string PossibleString()
        {
            int z;
            var str1 = new StringBuilder();

            for (z = 0; z < 9; z++)
            {
                if (IsPossible(z + 1))
                {
                    if (str1.Length > 0)
                        str1.Append(",");
                    str1.Append((z + 1).ToString());
                }
            }

            return str1.ToString();
        }

        public string ToButtonString(SudokuOptions opt)
        {
            if (No > 0)
            {
                return No.ToString();
            }

            if (opt.Help)
            {
                int z;
                var str1 = new StringBuilder();
                var str2 = new StringBuilder();

                for (z = 0; z < 9; z++)
                {
                    if (MainRulePossible[z])
                    {
                        if (_notPossible[z])
                        {
                            if (str1.Length > 0)
                                str1.Append(",");
                            str1.Append((z + 1).ToString());
                        }
                        else
                        {
                            if (str2.Length > 0)
                                str2.Append(",");
                            str2.Append((z + 1).ToString());
                        }
                    }
                }

                if (str1.Length > 0)
                {
                    if (str2.Length > 0)
                        return str2 + " - " + str1;

                    return str1.ToString();
                }

                return str2.ToString();
            }

            return UserNote;
        }

        #endregion
    }
}