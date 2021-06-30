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

    public enum Orientation
    {
        Column,
        Row,
        X3
    }

    public static class OrientationExtensions
    {
        public static string ToOrientationDesc(this Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.Row:    return "row";
                case Orientation.Column: return "col";
                case Orientation.X3:     return "3*3";
                default:                 throw new ArgumentException();
            }
        }

        public static char ToChar(this Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.Row:    return 'R';
                case Orientation.Column: return 'C';
                case Orientation.X3:     return 'X';
                default:                 throw new ArgumentException();
            }
        }

        public static Orientation ToOrientation(this string orientation)
        {
            switch (orientation)
            {
                case "R": return Orientation.Row;
                case "C": return Orientation.Column;
                case "X": return Orientation.X3;
                default:  throw new ArgumentException();
            }
        }

        public static Orientation ToOppositeOrientation(this Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.Row:    return Orientation.Column;
                case Orientation.Column: return Orientation.Row;
                default:                 return orientation;
            }
        }

        public static (int row, int col) ConvertFrom(this Orientation orientation, int row, int col)
        {
            return orientation switch
            {
                Orientation.Row    => Sudoku.ConvertFromRow(row, col),
                Orientation.Column => Sudoku.ConvertFromCol(row, col),
                Orientation.X3     => Sudoku.ConvertFromS3(row, col),
                _                  => throw new ArgumentException()
            };
        }

        public static (int row, int col) ConvertTo(this Orientation orientation, int row, int col)
        {
            return orientation switch
            {
                Orientation.Row    => Sudoku.ConvertToRow(row, col),
                Orientation.Column => Sudoku.ConvertToCol(row, col),
                Orientation.X3     => Sudoku.ConvertToS3(row, col),
                _                  => throw new ArgumentException()
            };
        }
    }
}