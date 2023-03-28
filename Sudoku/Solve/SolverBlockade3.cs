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

using global::Sudoku.Solve.NotPossible;

public class SolverBlockade3 : SolverBase
{
    public SolverBlockade3(Sudoku sudoku) : base(sudoku)
    {
    }

    public override bool Solve()
    {
        var isCol = Solve(Orientation.Column);
        var isRow = Solve(Orientation.Row);

        return isCol || isRow;
    }

    public override bool Solve(Orientation orientation)
    {
        return UpdatePossibleBlockade3(Sudoku.ToGetDef(orientation), orientation) > 0;
    }

    private int UpdatePossibleBlockade3(Sudoku.GetSudokuField getDef, Orientation orientation)
    {
        var changeCount = 0;

        ForEachEmpty(getDef, (def, row, col) =>
        {
            for (var no = 1; no <= 9; no++)
            {
                var colIdx = new List<int>();

                if (def.IsPossible(no))
                {
                    // test all in same sudoku but not in same row/col

                    var s3Row = row / 3;
                    var s3Col = col / 3;
                    int t;

                    for (t = 0; t < 9; t++)
                    {
                        var sRow = s3Row * 3 + t / 3;
                        var sCol = s3Col * 3 + t % 3;
                        var def2 = getDef(sRow, sCol);
                        if (def2.IsEmpty)
                        {
                            if (row != sRow)
                            {
                                if (def2.IsPossible(no))
                                {
                                    // abort with z
                                    break;
                                }
                            }
                            else if (def2.IsPossible(no))
                            {
                                colIdx.Add(sCol);
                            }
                        }
                    }

                    if (t >= 9)
                    {
                        for (t = 0; t < 9; t++)
                        {
                            if (t / 3 != col / 3)
                            {
                                var def2 = getDef(row, t);
                                if (def2.IsEmpty && def2.IsPossible(no))
                                {
                                    changeCount++;
                                    def2.SetNotPossible(no, new NotPossibleBlockade3()
                                    {
                                        ForNo       = no,
                                        Orientation = orientation,
                                        BecauseIdx  = colIdx
                                    });
                                }
                            }
                        }
                    }
                }
            }
        });

        return changeCount;
    }
}