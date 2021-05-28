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

            for (var y = 0; y < 9 && y < lines.Length; y++)
            {
                if (!string.IsNullOrEmpty(lines[y]))
                {
                    var cols = lines[y].Split(',', StringSplitOptions.None);

                    for (var x = 0; x < 9; x++)
                    {
                        if (cols.Length > x && !string.IsNullOrEmpty(cols[x]))
                        {
                            if (cols[x] != " ")
                            {
                                if (!s.Set(x, y, int.Parse(cols[x])))
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
            for (int y = 0; y < 9; y++)
            {
                string GetValue(int x, int y)
                {
                    var no = s.Get(x, y);
                    return no == 0 ? " " : no.ToString();
                }

                var line = GetValue(0, y);
                for (int x = 1; x < 9; x++)
                {
                    line += "," + GetValue(x, y);
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
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    var field = s.GetDef(x, y);
                    if (field.No == 0)
                    {
                        var possible    = field.PossibleString();
                        var possibleOpt = field.ToButtonToolTip(opt);
                        if (possible == possibleOpt)
                        {
                            info[x, y] = $"[{possible}]";
                        }
                        else
                        {
                            info[x, y] = $"[{possible}]=>{possibleOpt}";
                        }
                    }
                    else
                    {
                        info[x, y] = $"{field.No}";
                    }
                }
            }

            return info;
        }
    }
}