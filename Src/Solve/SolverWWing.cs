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

using System;
using System.Collections.Generic;
using System.Linq;

using global::Sudoku.Solve.NotPossible;
using global::Sudoku.Solve.Tools;

// see: https://www.thinkgym.de/r%C3%A4tselarten/sudoku/l%C3%B6sungsstrategien-3/xy-wing/
// see http://hodoku.sourceforge.net/de/tech_wings.php

public class SolverWWing : SolverBase
{
    public SolverWWing(Sudoku sudoku) : base(sudoku)
    {
    }

    public override bool Solve()
    {
        return Solve(Orientation.Column);
    }

    public override bool Solve(Orientation orientation)
    {
        var changeCount = 0;
        var listOf2Possible =
            EmptyFields()
                .Where(def => def.GetPossibleNos().Count() == 2);

        int AsNumber(IEnumerable<int> possible)
        {
            var ret = 0;
            foreach (var p in possible.OrderBy(no => no))
            {
                ret = ret * 10 + p;
            }

            return ret;
        }

        var samePossible = listOf2Possible
            .GroupBy(def => AsNumber(def.GetPossibleNos()), def => def)
            .Where(defs => defs.Count() > 1);

        foreach (var same in samePossible)
        {
            foreach (var pair in same.AllPairs())
            {
                var field1 = pair.Item1;
                var field2 = pair.Item2;

                var strongLink = GetStrongLink(field1, field2);
                if (strongLink.No != null)
                {
                    var possibleNos = field1.GetPossibleNos().ToArray();
                    var excludeNo   = possibleNos[0] == strongLink.No ? possibleNos[1] : possibleNos[0];
                    foreach (var excludeField in EmptyFields(field1.AbsRowCol.IntersectFields(field2.AbsRowCol))
                                 .Where(def => def.IsPossible(excludeNo)))
                    {
                        excludeField.SetNotPossible(excludeNo, new NotPossibleWWing()
                        {
                            ForNo            = excludeNo,
                            PairField1       = field1.AbsRowCol,
                            PairField2       = field2.AbsRowCol,
                            StrongLinkNo     = strongLink.No.Value,
                            StrongLinkField1 = strongLink.link1.AbsRowCol,
                            StrongLinkField2 = strongLink.link2.AbsRowCol
                        });
                        changeCount++;
                    }
                }
            }
        }

        return changeCount > 0;
    }

    (int? No, SudokuField link1, SudokuField link2) GetStrongLink(SudokuField field1, SudokuField field2)
    {
        if (field1.AbsRowCol.IsIntersect(field2.AbsRowCol))
        {
            return new (null,null,null);
        }

        var allOrientations = new[] { Orientation.Column, Orientation.Row, Orientation.X3 };

        foreach (var no in field1.GetPossibleNos())
        {
            foreach (var orientation in allOrientations.CartesianProduct(allOrientations))
            {
                var linked =
                    GetWeakLink(field1, orientation.Item1, no)
                        .CartesianProduct(GetWeakLink(field2, orientation.Item2, no))
                        .Where(a => IsStrongLink(a.Item1, a.Item2, no))
                        .ToList();

                if (linked.Any())
                {
                    return new (no, linked[0].Item1, linked[0].Item2);
                }
            }
        }

        return new(null, null, null); ;
    }
}