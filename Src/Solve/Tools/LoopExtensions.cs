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

namespace Sudoku.Solve.Tools;

using System.Collections.Generic;
using System.Linq;

public static class LoopExtensions
{
    public static readonly IEnumerable<int> Rows = Enumerable.Range(0, 9);
    public static readonly IEnumerable<int> Cols = Enumerable.Range(0, 9);
    public static readonly IEnumerable<int> Nos  = Enumerable.Range(1, 9);

    public static IEnumerable<SudokuField> SelectField(this IEnumerable<int> cols, Sudoku.GetSudokuField getDef, int row)
    {
        return cols.Select(col => getDef(row, col));
    }

    public static IEnumerable<SudokuField> SelectFieldEmpty(this IEnumerable<int> cols, Sudoku.GetSudokuField getDef, int row)
    {
        return cols.SelectField(getDef, row).Where(def => def.IsEmpty);
    }
}