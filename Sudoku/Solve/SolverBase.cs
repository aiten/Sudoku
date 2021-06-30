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
    using System.Linq;

    using global::Sudoku.Solve.Tools;

    public abstract class SolverBase
    {
        protected SolverBase(Sudoku sudoku)
        {
            Sudoku = sudoku;
        }

        public Sudoku Sudoku { get; private set; }

        protected bool FindSubSet(Sudoku.GetSudokuField getDef, int row, int col, bool[] use, bool[] noSet)
        {
            // find union with backtracking

            if (col >= 9)
                return false;

            if (IsSubSet(getDef, row, use, noSet))
                return true;

            int z;
            for (z = col; z < 9; z++)
            {
                use[z] = true;
                if (FindSubSet(getDef, row, z + 1, use, noSet))
                {
                    return true;
                }

                use[z] = false;
            }

            return false;
        }

        protected bool IsSubSet(Sudoku.GetSudokuField getDef, int row, bool[] use, bool[] noSet)
        {
            var countUsed     = 0;
            var countPossible = 0;
            var countSet      = 0;

            noSet.Init(false);

            for (var col = 0; col < 9; col++)
            {
                var def = getDef(row, col);
                if (def.IsEmpty)
                {
                    countPossible++;
                    if (use[col])
                    {
                        countUsed++;
                        for (var no = 1; no <= 9; no++)
                        {
                            if (def.IsPossible(no))
                            {
                                if (!noSet[no - 1])
                                {
                                    countSet++;
                                    noSet[no - 1] = true;
                                }
                            }
                        }
                    }
                }
            }

            return countSet == countUsed && countUsed != countPossible && countSet > 1;
        }

        protected int CountPossible(Sudoku.GetSudokuField getDef, int no, int row)
        {
            return LoopExtensions.Cols
                .SelectField(getDef, row)
                .Where(def => def.IsEmpty && def.IsPossible(no))
                .Count();
        }

        protected void ForEachEmpty(Sudoku.GetSudokuField getDef, Action<SudokuField, int, int> action)
        {
            for (var row = 0; row < 9; row++)
            {
                for (var col = 0; col < 9; col++)
                {
                    var def = getDef(row, col);
                    if (def.IsEmpty)
                    {
                        action(def, row, col);
                    }
                }
            }
        }

        public abstract bool Solve();
        public abstract bool Solve(Orientation orientation);
    }
}