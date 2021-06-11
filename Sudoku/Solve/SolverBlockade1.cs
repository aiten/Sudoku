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
    public class SolverBlockade1 : SolverBase
    {
        public SolverBlockade1(Sudoku sudoku) : base(sudoku)
        {
        }

        public override bool Solve()
        {
            var isCol = Solve(Orientation.Column);
            var isRow = Solve(Orientation.Row);
            var isX3  = Solve(Orientation.X3);

            return isCol || isRow || isX3;
        }

        public override bool Solve(Orientation orientation)
        {
            return UpdatePossibleBlockade1(ToGetDef(orientation), ToChar(orientation)) > 0;
        }

        public int UpdatePossibleBlockade1(Sudoku.GetSudokuField getDef, char rowcol3)
        {
            var changeCount = 0;

            ForEachEmpty(getDef, (def, row, col) =>
            {
                for (var no = 1; no <= 9; no++)
                {
                    if (def.IsPossible(no))
                    {
                        if (!FindPossible(getDef, no, row, col))
                        {
                            for (var no2 = 1; no2 <= 9; no2++)
                            {
                                if (no2 != no && !def.IsNotPossible(no2))
                                {
                                    changeCount++;
                                    def.SetNotPossibleBlockade1(no2, rowcol3, no);
                                }
                            }
                        }
                    }
                }
            });
            return changeCount;
        }

        private bool FindPossible(Sudoku.GetSudokuField getDef, int no, int row, int ignoreIndex)
        {
            for (var col = 0; col < 9; col++)
            {
                if (col != ignoreIndex)
                {
                    var def = getDef(row, col);
                    if (def.IsEmpty && def.IsPossible(no))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}