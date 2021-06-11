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
    using global::Sudoku.Solve.Tools;

    public class SolverBlockade2SubSet : SolverBase
    {
        public SolverBlockade2SubSet(Sudoku sudoku) : base(sudoku)
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
            return UpdatePossibleBlockade2SubSet(ToGetDef(orientation), ToChar(orientation)) > 0;
        }

        public int UpdatePossibleBlockade2SubSet(Sudoku.GetSudokuField getDef, char rowcol3)
        {
            var changeCount = 0;

            ForEachEmpty(getDef, (def, row, col) =>
            {
                var used   = new bool[9];
                var notSet = new bool[9];

                used[col] = true;

                if (FindSubSet(getDef, row, col + 1, used, notSet))
                {
                    var reason = ReasonPossible(getDef, used, notSet, row);

                    for (var t = 0; t < 9; t++)
                    {
                        if (!used[t])
                        {
                            var def2 = getDef(row, t);
                            if (def2.IsEmpty)
                            {
                                for (var z = 1; z <= 9; z++)
                                {
                                    if (notSet[z - 1] && def2.IsPossible(z))
                                    {
                                        changeCount++;
                                        def2.SetNotPossibleBlockade2P(z, rowcol3, reason.Possible, reason.Index);
                                    }
                                }
                            }
                        }
                    }
                }
            });

            return changeCount;
        }

        private (string Possible, string Index) ReasonPossible(Sudoku.GetSudokuField getDef, bool[] used, bool[] notSet, int row)
        {
            string reasonPossible = null;
            string reasonIndex    = null;
            for (var col = 0; col < 9; col++)
            {
                if (used[col])
                {
                    var def = getDef(row, col);
                    if (def.IsEmpty)
                    {
                        if (string.IsNullOrEmpty(reasonPossible))
                        {
                            for (var no = 1; no <= 9; no++)
                            {
                                if (notSet[no - 1])
                                {
                                    reasonPossible = reasonPossible.Add(',', no);
                                }
                            }
                        }

                        reasonIndex = reasonIndex.Add(',', col + 1);
                    }
                }
            }

            return (reasonPossible, reasonIndex);
        }
    }
}