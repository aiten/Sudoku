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

    public static class SudokuExtensions
    {
        public static Solve.Sudoku CreateSudoku(this string[] lines)
        {
            var s = new Solve.Sudoku();

            for (var row = 0; row < 9 && row < lines.Length; row++)
            {
                if (!string.IsNullOrEmpty(lines[row]))
                {
                    var cols = lines[row].Split(',', StringSplitOptions.None);

                    for (var col = 0; col < 9; col++)
                    {
                        if (cols.Length > col && !string.IsNullOrEmpty(cols[col]))
                        {
                            if (cols[col] != " ")
                            {
                                if (!s.Set(row, col, int.Parse(cols[col])))
                                {
                                    throw new ArgumentException("illegal sudoku");
                                }
                            }
                        }
                    }
                }
            }

            s.ClearUndo();
            return s;
        }

        public static IList<string> SmartPrint(this Solve.Sudoku s)
        {
            var lines = new List<string>();
            for (int row = 0; row < 9; row++)
            {
                string GetValue(int row, int col)
                {
                    var no = s.Get(row, col);
                    return no == 0 ? " " : no.ToString();
                }

                var line = GetValue(row, 0);
                for (int col = 1; col < 9; col++)
                {
                    line += "," + GetValue(row, col);
                }

                lines.Add(line);
            }

            return lines;
        }

        public static string[,] SmartPrintInfo(this Solve.Sudoku s)
        {
            var opt = new SudokuOptions();
            opt.Help        = true;
            opt.ShowToolTip = true;

            s.UpdatePossible();
            var info = new string[9, 9];
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    var field = s.GetDef(row, col);
                    if (field.IsEmpty)
                    {
                        var possible    = field.PossibleString();
                        var possibleOpt = field.ToButtonToolTip(opt);
                        if (possible == possibleOpt)
                        {
                            info[row, col] = $"[{possible}]";
                        }
                        else
                        {
                            info[row, col] = $"[{possible}]=>{possibleOpt}";
                        }
                    }
                    else
                    {
                        info[row, col] = $"{field.No}";
                    }
                }
            }

            return info;
        }
    }
}