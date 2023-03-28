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

using global::Sudoku.Solve.Tools;

public abstract class SolverBase
{
    protected SolverBase(Sudoku sudoku)
    {
        Sudoku = sudoku;
    }

    public Sudoku Sudoku { get; private set; }

    #region subSet

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

    #endregion

    protected int CountPossible(Sudoku.GetSudokuField getDef, int no, int row)
    {
        return GetPossible(getDef, no, row).Count();
    }

    protected IEnumerable<SudokuField> GetPossible(Sudoku.GetSudokuField getDef, int no, int row)
    {
        return EmptyFields(getDef, row)
            .Where(def => def.IsPossible(no));
    }

    #region enumerate empty

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

    protected IEnumerable<SudokuField> EmptyFields()
    {
        var emptyList = new List<SudokuField>();
        for (var row = 0; row < 9; row++)
        {
            for (var col = 0; col < 9; col++)
            {
                var def = Sudoku.GetDef(row, col);
                if (def.IsEmpty)
                {
                    emptyList.Add(def);
                }
            }
        }

        return emptyList;
    }

    protected IEnumerable<SudokuField> EmptyFields(Sudoku.GetSudokuField getDef, int row)
    {
        var emptyList = new List<SudokuField>();
        for (var col = 0; col < 9; col++)
        {
            var def = getDef(row, col);
            if (def.IsEmpty)
            {
                emptyList.Add(def);
            }
        }

        return emptyList;
    }

    public IEnumerable<SudokuField> EmptyFields(IEnumerable<(int row, int col)> rowCols)
    {
        return rowCols
            .Select(rowCol => Sudoku.GetDef(rowCol.row, rowCol.col))
            .Where(def => def.IsEmpty);
    }

    #endregion

    protected SudokuField GetStrongLink(SudokuField field, Orientation orientation, int no)
    {
        var weakLink = GetWeakLink(field, orientation, no).ToList();
        return weakLink.Count == 1 ? weakLink.First() : null;
    }

    protected IEnumerable<SudokuField> GetWeakLink(SudokuField field, Orientation orientation, int no)
    {
        var sameNoFields =
            GetPossible(Sudoku.ToGetDef(orientation), no, Sudoku.ConvertTo(orientation, field.AbsRowCol).row)
                .Where(x => x != field);
        return sameNoFields;
    }

    protected bool IsStrongLink(SudokuField field1, SudokuField field2, int no)
    {
        return GetStrongLink(field1, Orientation.Column, no) == field2 ||
               GetStrongLink(field1, Orientation.Row,    no) == field2 ||
               GetStrongLink(field1, Orientation.X3,     no) == field2;
    }

    public abstract bool Solve();
    public abstract bool Solve(Orientation orientation);
}