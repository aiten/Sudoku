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

    public abstract class SolverFishBase : SolverBase
    {
        protected SolverFishBase(Sudoku sudoku) : base(sudoku)
        {
        }

        protected abstract void SetNotPossible(SudokuField def, int forNo, char rowcol3, string becauseRow, string becauseCol);

        protected int UpdateFish(Sudoku.GetSudokuField getDef, int count, char rowcol3)
        {
            var changeCount = 0;

            for (var no = 1; no <= 9; no++)
            {
                var countPerRow = InitCountsPerRow(getDef, no);
                var rows        = GetRowIdx(count);

                do
                {
                    var countsOk = true;
                    for (var i = 0; i < count; i++)
                    {
                        countsOk &= countPerRow[rows[i]] > 0 && countPerRow[rows[i]] <= count;
                    }

                    if (countsOk)
                    {
                        var countCol = 0;
                        var colIdx   = new int[count];
                        for (var col = 0; col < 9; col++)
                        {
                            var isEmptyAndPossible = false;
                            for (var i = 0; i < count; i++)
                            {
                                isEmptyAndPossible |= getDef(rows[i], col).IsEmptyAndPossible(no);
                            }

                            if (isEmptyAndPossible)
                            {
                                if (countCol < count)
                                {
                                    colIdx[countCol] = col;
                                }

                                countCol++;
                            }
                        }

                        if (countCol == count)
                        {
                            for (var row = 0; row < 9; row++)
                            {
                                if (!rows.Contains(row))
                                {
                                    for (int n = 0; n < countCol; n++)
                                    {
                                        var def = getDef(row, colIdx[n]);
                                        if (def.IsEmpty && !def.IsNotPossible(no))
                                        {
                                            SetNotPossible(def, no, rowcol3, $"{string.Join(',', rows.Select(idx => idx + 1))}", $"{string.Join(',', colIdx.Select(idx => idx + 1))}");
                                            changeCount++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                } while (GetNext(count - 1, rows));
            }

            return changeCount;
        }

        private int[] InitCountsPerRow(Sudoku.GetSudokuField getDef, int no)
        {
            var countPerRow = new int[9];
            for (var row = 0; row < 9; row++)
            {
                countPerRow[row] = CountPossible(getDef, no, row);
            }

            return countPerRow;
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