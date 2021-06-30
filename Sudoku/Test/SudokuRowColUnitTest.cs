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

namespace Sudoku.Test
{
    using System.Linq;

    using FluentAssertions;

    using Sudoku.Solve;
    using Sudoku.Solve.Tools;

    using Xunit;

    public class SudokuRowColUnitTest
    {
        [Theory]
        [InlineData(0, 0, Orientation.Row,    0, 0)]
        [InlineData(0, 1, Orientation.Row,    0, 1)]
        [InlineData(0, 9, Orientation.Row,    0, 9)]
        [InlineData(9, 0, Orientation.Row,    9, 0)]
        [InlineData(9, 1, Orientation.Row,    9, 1)]
        [InlineData(9, 8, Orientation.Row,    9, 8)]
        [InlineData(9, 9, Orientation.Row,    9, 9)]
        [InlineData(0, 0, Orientation.Column, 0, 0)]
        [InlineData(0, 1, Orientation.Column, 1, 0)]
        [InlineData(0, 9, Orientation.Column, 9, 0)]
        [InlineData(9, 0, Orientation.Column, 0, 9)]
        [InlineData(9, 1, Orientation.Column, 1, 9)]
        [InlineData(9, 8, Orientation.Column, 8, 9)]
        [InlineData(9, 9, Orientation.Column, 9, 9)]
        [InlineData(0, 0, Orientation.X3,     0, 0)]
        [InlineData(0, 2, Orientation.X3,     0, 2)]
        [InlineData(1, 0, Orientation.X3,     0, 3)]
        [InlineData(1, 2, Orientation.X3,     0, 5)]
        [InlineData(2, 0, Orientation.X3,     0, 6)]
        [InlineData(2, 2, Orientation.X3,     0, 8)]
        [InlineData(0, 3, Orientation.X3,     1, 0)]
        [InlineData(0, 5, Orientation.X3,     1, 2)]
        [InlineData(1, 3, Orientation.X3,     1, 3)]
        [InlineData(1, 5, Orientation.X3,     1, 5)]
        [InlineData(2, 3, Orientation.X3,     1, 6)]
        [InlineData(2, 5, Orientation.X3,     1, 8)]
        [InlineData(6, 0, Orientation.X3,     6, 0)]
        [InlineData(6, 2, Orientation.X3,     6, 2)]
        [InlineData(7, 0, Orientation.X3,     6, 3)]
        [InlineData(7, 2, Orientation.X3,     6, 5)]
        [InlineData(8, 0, Orientation.X3,     6, 6)]
        [InlineData(8, 2, Orientation.X3,     6, 8)]
        [InlineData(6, 6, Orientation.X3,     8, 0)]
        [InlineData(6, 8, Orientation.X3,     8, 2)]
        [InlineData(7, 6, Orientation.X3,     8, 3)]
        [InlineData(7, 8, Orientation.X3,     8, 5)]
        [InlineData(8, 6, Orientation.X3,     8, 6)]
        [InlineData(8, 8, Orientation.X3,     8, 8)]
        public void TestConvertToRowCol(int row, int col, Orientation orientation, int toRow, int toCol)
        {
            var toRowCol = orientation.ConvertTo(row, col);

            toRowCol.Should().Be((toRow, toCol));
        }

        [Theory]
        [InlineData(Orientation.Row)]
        [InlineData(Orientation.Column)]
        [InlineData(Orientation.X3)]
        public void TestConvertFromTo(Orientation orientation)
        {
            foreach (var row in LoopExtensions.Rows)
            {
                foreach (var col in LoopExtensions.Cols)
                {
                    var toRowCol = orientation.ConvertTo(row, col);
                    var fromRowCol = orientation.ConvertFrom(toRowCol.row, toRowCol.col);

                    fromRowCol.Should().Be((row, col));
                }
            }
        }
    }
}