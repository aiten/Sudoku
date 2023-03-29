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

namespace Sudoku.Solve;

using System.Collections.Generic;
using System.Linq;

using global::Sudoku.Solve.NotPossible;
using global::Sudoku.Solve.Tools;

public class SolverBlockade2 : SolverBase
{
    public SolverBlockade2(Sudoku sudoku) : base(sudoku)
    {
    }

    private readonly bool[] _foundIdx1 = new bool[9];

    public override bool Solve()
    {
        var isCol = Solve(Orientation.Column);
        var isRow = Solve(Orientation.Row);
        var isX3  = Solve(Orientation.X3);

        return isCol || isRow || isX3;
    }

    public override bool Solve(Orientation orientation)
    {
        return UpdatePossibleBlockade2(Sudoku.ToGetDef(orientation), orientation) > 0;
    }

    public int UpdatePossibleBlockade2(Sudoku.GetSudokuField getDef, Orientation orientation)
    {
        var changeCount = 0;

        ForEachEmpty(getDef, (def, row, col) =>
        {
            _foundIdx1.Init(false);

            _foundIdx1[col] = true;
            var foundCount = 1;

            foreach (var col2 in LoopExtensions.Cols.Where(col2 => col2 != col))
            {
                var def2 = getDef(row, col2);
                if (def2.IsEmpty)
                {
                    if (def.IsSubSetPossible(def2))
                    {
                        _foundIdx1[col2] = true;
                        foundCount++;
                    }
                }
            }

            if (foundCount == def.PossibleCount())
            {
                var reasonPossible = new List<int>();
                var reasonIndex    = new List<int>();
                foreach (var col2 in LoopExtensions.Cols.Where(col2 => _foundIdx1[col2]))
                {
                    var def2 = getDef(row, col2);
                    if (def2.IsEmpty)
                    {
                        if (reasonPossible.Count == 0)
                        {
                            reasonPossible.AddRange(def2.GetPossibleNos());
                        }

                        reasonIndex.Add(col2);
                    }
                }

                foreach (var col2 in LoopExtensions.Cols.Where(col2 => !_foundIdx1[col2]))
                {
                    var def2 = getDef(row, col2);
                    if (def2.IsEmpty)
                    {
                        foreach (var no in LoopExtensions.Nos)
                        {
                            if (def.IsPossible(no) && def2.IsPossible(no))
                            {
                                changeCount++;
                                def2.SetNotPossible(no, new NotPossibleBlockade2()
                                {
                                    ForNo       = no,
                                    Orientation = orientation,
                                    BecauseIdx  = reasonIndex,
                                    BecauseNos  = reasonPossible
                                });
                            }
                        }
                    }
                }
            }
        });

        return changeCount;
    }
}