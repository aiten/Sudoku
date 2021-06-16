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
    using System.Collections.Generic;
    using System.Linq;

    using global::Sudoku.Solve.NotPossible;
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
            return UpdatePossibleBlockade2SubSet(ToGetDef(orientation), orientation) > 0;
        }

        public int UpdatePossibleBlockade2SubSet(Sudoku.GetSudokuField getDef, Orientation orientation)
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

                    foreach (var def2 in LoopExtensions.Cols
                        .Where(col2 => !used[col2])
                        .SelectFieldEmpty(getDef, row))
                    {
                        foreach (var no in LoopExtensions.Nos)
                        {
                            if (notSet[no - 1] && def2.IsPossible(no))
                            {
                                changeCount++;
                                def2.SetNotPossible(no, new NotPossibleBlockade2SubSet()
                                {
                                    ForNo       = no,
                                    Orientation = orientation,
                                    BecauseIdx  = reason.Index,
                                    BecauseNos  = reason.Possible
                                });
                            }
                        }
                    }
                }
            });

            return changeCount;
        }

        private (IEnumerable<int> Possible, IEnumerable<int> Index) ReasonPossible(Sudoku.GetSudokuField getDef, bool[] used, bool[] notSet, int row)
        {
            var reasonPossible = new List<int>();
            var reasonIndex    = new List<int>();
            foreach (var col in LoopExtensions.Cols
                .Where(col => used[col]))
            {
                var def = getDef(row, col);
                if (def.IsEmpty)
                {
                    if (reasonPossible.Count == 0)
                    {
                        foreach (var no in LoopExtensions.Nos)
                        {
                            if (notSet[no - 1])
                            {
                                reasonPossible.Add(no);
                            }
                        }
                    }

                    reasonIndex.Add(col);
                }
            }

            return (reasonPossible, reasonIndex);
        }
    }
}