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

// see: https://www.thinkgym.de/r%C3%A4tselarten/sudoku/l%C3%B6sungsstrategien-3/xy-wing/
// see http://hodoku.sourceforge.net/de/tech_wings.php

public class SolverXYWing : SolverBase
{
    public SolverXYWing(Sudoku sudoku) : base(sudoku)
    {
    }

    public override bool Solve()
    {
        return Solve(Orientation.Column);
    }

    private bool IsXYWing(SudokuField main, SudokuField cmp1, SudokuField cmp2, out int wingNo)
    {
        var possibleNosMain = main.GetPossibleNos().ToList();
        var possibleNos1    = cmp1.GetPossibleNos().ToList();
        var possibleNos2    = cmp2.GetPossibleNos().ToList();

        var intersectMainTo1 = possibleNosMain.Intersect(possibleNos1).ToList();
        var intersectMainTo2 = possibleNosMain.Intersect(possibleNos2).ToList();
        var intersect1To2    = possibleNos1.Intersect(possibleNos2).ToList();

        wingNo = intersect1To2.FirstOrDefault();

        return intersectMainTo1.Count == 1 &&
               intersectMainTo2.Count == 1 &&
               intersect1To2.Count == 1 &&
               intersectMainTo1.Single() != intersectMainTo2.Single() &&
               intersect1To2.Single() != intersectMainTo1.Single();
    }

    private int UpdateXYWingField(SudokuField field, int forNo, SudokuField pivot, SudokuField pincer1, SudokuField pincer2)
    {
        if (field.AbsRowCol != pivot.AbsRowCol &&
            field.AbsRowCol != pincer1.AbsRowCol &&
            field.AbsRowCol != pincer2.AbsRowCol &&
            field.IsEmpty && field.IsPossible(forNo))
        {
            field.SetNotPossible(forNo, new NotPossibleXYWing()
            {
                ForNo   = forNo,
                Pivot   = pivot.AbsRowCol,
                Pincer1 = pincer1.AbsRowCol,
                Pincer2 = pincer2.AbsRowCol
            });

            return 1;
        }

        return 0;
    }

    private int UpdateXYWing(SudokuField pivot, Orientation orientation1, IList<SudokuField> list1, Orientation orientation2, IList<SudokuField> list2)
    {
        int count = 0;
        foreach (var pincer1 in list1)
        {
            foreach (var pincer2 in list2)
            {
                if (pincer1.AbsRowCol != pincer2.AbsRowCol)
                {
                    int wingNo;
                    if (IsXYWing(pivot, pincer1, pincer2, out wingNo))
                    {
                        var intersect = pincer1.AbsRowCol
                            .IntersectFields(pincer2.AbsRowCol)
                            .Where(pos => pos != pincer1.AbsRowCol && pos != pincer2.AbsRowCol)
                            .Select(pos => Sudoku.GetDef(pos.Row, pos.Col))
                            .Where(field => field.IsEmpty);
                        foreach (var field in intersect)
                        {
                            count += UpdateXYWingField(field, wingNo, pivot, pincer1, pincer2);
                        }
                    }
                }
            }
        }

        return count;
    }

    public override bool Solve(Orientation orientation)
    {
        var changeCount = 0;
        var listOf2Possible = EmptyFields()
            .Where(def => def.GetPossibleNos().Count() == 2)
            .ToList();


        foreach (var pivot in listOf2Possible)
        {
            var rowColCol = pivot.AbsRowCol.ConvertTo(Orientation.Column);
            var rowColRow = pivot.AbsRowCol.ConvertTo(Orientation.Row);
            var rowColX3  = pivot.AbsRowCol.ConvertTo(Orientation.X3);

            var possible2NotThis = listOf2Possible.Where(field => field.AbsRowCol != pivot.AbsRowCol).ToList();

            var sameRow = possible2NotThis.Where(field => field.AbsRowCol.ConvertTo(Orientation.Row).Row == rowColRow.Row).ToList();
            var sameCol = possible2NotThis.Where(field => field.AbsRowCol.ConvertTo(Orientation.Column).Row == rowColCol.Row).ToList();
            var sameX3  = possible2NotThis.Where(field => field.AbsRowCol.ConvertTo(Orientation.X3).Row == rowColX3.Row).ToList();

            if (sameRow.Count >= 1 && sameCol.Count >= 1)
            {
                changeCount += UpdateXYWing(pivot, Orientation.Row, sameRow, Orientation.Column, sameCol);
            }

            if (sameRow.Count >= 1 && sameX3.Count >= 1)
            {
                changeCount += UpdateXYWing(pivot, Orientation.Row, sameRow, Orientation.X3, sameX3);
            }

            if (sameCol.Count >= 1 && sameX3.Count >= 1)
            {
                changeCount += UpdateXYWing(pivot, Orientation.X3, sameX3, Orientation.Column, sameCol);
            }
        }

        return changeCount > 0;
    }
}