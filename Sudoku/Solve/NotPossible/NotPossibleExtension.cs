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

namespace Sudoku.Solve.NotPossible
{
    using System.Collections.Generic;
    using System.Linq;

    public static class NotPossibleExtension
    {
        #region For Display

        const string ColNames = "ABCDEFGHI";
        const string RowNames = "123456789";
        const string X3Names  = "123456789";

        public static string ToUserRowList(this IEnumerable<int> because, Orientation orientation)
        {
            return orientation switch
            {
                Orientation.Row    => string.Join(',', because.Select(idx => RowNames[idx])),
                Orientation.Column => string.Join(',', because.Select(idx => ColNames[idx])),
                Orientation.X3     => string.Join(',', because.Select(idx => X3Names[idx])),
            };
        }

        public static string ToUserNoList(this IEnumerable<int> because)
        {
            return string.Join(',', because);
        }

        public static string ToCellStringUser(this (int Row, int Col) rowCol)
        {
            return $"{RowNames[rowCol.Row]}{ColNames[rowCol.Col]}";
        }

        #endregion

        public static string ToRowList(this IEnumerable<int> because)
        {
            return string.Join(',', because);
        }


        public static string ToNoList(this IEnumerable<int> because)
        {
            return string.Join(',', because);
        }

        public static IEnumerable<int> FromNoList(this string noList)
        {
            return noList.Split(',').Select(int.Parse);
        }

        public static IEnumerable<int> FromRowList(this string noList)
        {
            return noList.Split(',').Select(int.Parse);
        }

        public static string ToCellString(this (int row, int col) rowCol)
        {
            return $"{rowCol.row}{rowCol.col}";
        }

        public static (int Row, int Col) FromCellString(this string rowCol)
        {
            return (rowCol[0] - '0', rowCol[1] - '0');
        }
    }
}