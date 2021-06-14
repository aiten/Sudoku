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
    using System.Linq;

    using global::Sudoku.Solve.Tools;

    public abstract class SolverFishBase : SolverBase
    {
        protected SolverFishBase(Sudoku sudoku) : base(sudoku)
        {
        }

        protected abstract void SetNotPossible(SudokuField def, int forNo, char rowcol3, string becauseRow, string becauseCol);

        protected int UpdateFish(Sudoku.GetSudokuField getDef, int fishSize, char rowcol3)
        {
            var changeCount = 0;

            foreach (var no in LoopExtensions.Nos)
            {
                var countPerRow = LoopExtensions.Rows.Select(row => CountPossible(getDef, no, row)).ToArray();
                var rowIdx      = GetRowIdx(fishSize);

                do
                {
                    if (rowIdx.All(row => countPerRow[row] > 0 && countPerRow[row] <= fishSize))
                    {
                        var colIdx = LoopExtensions.Cols.Where(col => rowIdx.Any(row => getDef(row, col).IsEmptyAndPossible(no))).ToArray();

                        if (colIdx.Length == fishSize)
                        {
                            foreach (var row in LoopExtensions.Rows.Where(row => !rowIdx.Contains(row)))
                            {
                                foreach (var def in colIdx
                                    .SelectField(getDef, row)
                                    .Where(def => def.IsEmpty && !def.IsNotPossible(no)))
                                {
                                    SetNotPossible(def, no, rowcol3, $"{string.Join(',', rowIdx.Select(idx => idx + 1))}", $"{string.Join(',', colIdx.Select(idx => idx + 1))}");
                                    changeCount++;
                                }
                            }
                        }
                    }
                } while (GetNext(fishSize - 1, rowIdx));
            }

            return changeCount;
        }

        private int[] GetRowIdx(int count)
        {
            var rows = new int[count];
            for (var i = 0; i < count; i++)
            {
                rows[i] = i;
            }

            return rows;
        }

        private bool GetNext(int idx, int[] rows)
        {
            if (rows[idx] < 8)
            {
                rows[idx]++;
                return true;
            }

            if (idx > 0 && GetNext(idx - 1, rows))
            {
                while (rows[idx - 1] >= 7)
                {
                    if (!GetNext(idx - 1, rows))
                    {
                        return false;
                    }
                }

                rows[idx] = rows[idx - 1] + 1;
                return true;
            }

            return false;
        }
    }
}