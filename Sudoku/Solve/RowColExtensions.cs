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
    using System.Collections.Generic;
    using System.Linq;

    using global::Sudoku.Solve.Tools;

    public static class RowColExtensions
    {
        public static (int Row, int Col) ConvertFrom(this (int Row, int Col) rowCol, Orientation orientation)
        {
            return orientation switch
            {
                Orientation.Row    => Sudoku.ConvertFromRow(rowCol.Row, rowCol.Col),
                Orientation.Column => Sudoku.ConvertFromCol(rowCol.Row, rowCol.Col),
                Orientation.X3     => Sudoku.ConvertFromS3(rowCol.Row, rowCol.Col),
                _                  => throw new ArgumentException()
            };
        }

        public static (int Row, int Col) ConvertTo(this (int Row, int Col) rowCol, Orientation orientation)
        {
            return orientation switch
            {
                Orientation.Row    => Sudoku.ConvertToRow(rowCol.Row, rowCol.Col),
                Orientation.Column => Sudoku.ConvertToCol(rowCol.Row, rowCol.Col),
                Orientation.X3     => Sudoku.ConvertToS3(rowCol.Row, rowCol.Col),
                _                  => throw new ArgumentException()
            };
        }

        public static bool IsIntersect(this (int Row, int Col) field, (int Row, int Col) intersectWith)
        {
            var s31 = Sudoku.ConvertToS3(field.Row,         field.Col);
            var s32 = Sudoku.ConvertToS3(intersectWith.Row, intersectWith.Col);

            return field.Col == intersectWith.Col || field.Row == intersectWith.Row || s31.Row == s32.Row;
        }

        public static IEnumerable<(int Row, int Col)> DependentFields(this (int Row, int Col) field)
        {
            var rowColRowFromX3 = field.ConvertTo(Orientation.X3);

            var dependentFields = LoopExtensions.Rows.Select(row => (row, field.Col)).ToList();
            dependentFields.AddRange(LoopExtensions.Cols.Where(col => col != field.Col).Select(col => (field.Row, col)));
            dependentFields.AddRange(LoopExtensions.Cols.Select(col => (rowColRowFromX3.Row, col).ConvertFrom(Orientation.X3)).Where(rowCol => rowCol.Col != field.Col && rowCol.Row != field.Row));

            return dependentFields;
        }

        public static IEnumerable<(int Row, int Col)> IntersectFields(this (int Row, int Col) field, IEnumerable<(int Row, int Col)> intersectWith)
        {
            var intersectedFields = DependentFields(field);
            foreach (var intersect in intersectWith)
            {
                intersectedFields = intersectedFields.Intersect(field.IntersectFields(intersect));
            }

            return intersectedFields;
        }

        public static IEnumerable<(int Row, int Col)> IntersectFields(this (int Row, int Col) field, (int Row, int Col) intersectWith)
        {
            var intersectedFields = new List<(int Row, int Col)>();

            var rowColRowFromX3 = field.ConvertTo(Orientation.X3);
            var rowColRowWithX3 = intersectWith.ConvertTo(Orientation.X3);

            if (field == intersectWith)
            {
                return DependentFields(field);
            }
            else if (field.Col == intersectWith.Col)
            {
                intersectedFields.AddRange(LoopExtensions.Rows.Select(row => (row, field.Col)));
                if (rowColRowFromX3.Row == rowColRowWithX3.Row)
                {
                    var x3col = rowColRowFromX3.Col % 3;
                    intersectedFields.AddRange(LoopExtensions.Cols.Where(col => col % 3 != x3col).Select(col => (rowColRowFromX3.Row, col).ConvertFrom(Orientation.X3)));
                }
            }
            else if (field.Row == intersectWith.Row)
            {
                intersectedFields.AddRange(LoopExtensions.Cols.Select(col => (field.Row, col)));
                if (rowColRowFromX3.Row == rowColRowWithX3.Row)
                {
                    var x3col = rowColRowFromX3.Col / 3;
                    intersectedFields.AddRange(LoopExtensions.Cols.Where(col => col / 3 != x3col).Select(col => (rowColRowFromX3.Row, col).ConvertFrom(Orientation.X3)));
                }
            }
            else if (rowColRowFromX3.Row == rowColRowWithX3.Row)
            {
                intersectedFields.AddRange(LoopExtensions.Cols.Select(col => (rowColRowFromX3.Row, col).ConvertFrom(Orientation.X3)));
            }
            else
            {
                intersectedFields = field.DependentFields().Intersect(intersectWith.DependentFields()).ToList();
            }

            return intersectedFields;
        }
    }
}