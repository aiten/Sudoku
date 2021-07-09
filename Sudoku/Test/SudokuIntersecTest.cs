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

    using Xunit;

    public class SudokuIntersectTest
    {
        [Theory]
        [InlineData(0, 0, 3, 1)]
        [InlineData(0, 0, 3, 2)]
        [InlineData(0, 0, 3, 3)]
        [InlineData(0, 0, 3, 4)]
        [InlineData(0, 0, 3, 5)]
        [InlineData(0, 0, 3, 6)]
        [InlineData(0, 0, 3, 7)]
        [InlineData(0, 0, 3, 8)]
        [InlineData(0, 0, 1, 8)]
        [InlineData(0, 0, 2, 8)]
        [InlineData(0, 0, 3, 8)]
        [InlineData(0, 0, 4, 8)]
        [InlineData(0, 0, 5, 8)]
        [InlineData(0, 0, 6, 8)]
        [InlineData(0, 0, 7, 8)]
        [InlineData(0, 0, 8, 8)]
        public void NoIntersectTest(int row1, int col1, int row2, int col2)
        {
            var intersect = (row1, col1).IntersectFields((row2, col2));
            intersect.Should().HaveCount(0);
        }

        [Theory]
        [InlineData(0, 0, 3)]
        [InlineData(0, 0, 4)]
        [InlineData(0, 0, 5)]
        [InlineData(0, 0, 6)]
        [InlineData(0, 0, 7)]
        [InlineData(0, 0, 8)]
        [InlineData(1, 0, 3)]
        [InlineData(1, 0, 4)]
        [InlineData(1, 0, 5)]
        [InlineData(1, 0, 6)]
        [InlineData(1, 0, 7)]
        [InlineData(1, 0, 8)]
        [InlineData(2, 3, 0)]
        [InlineData(2, 3, 1)]
        [InlineData(2, 3, 2)]
        [InlineData(2, 3, 6)]
        [InlineData(2, 3, 7)]
        [InlineData(2, 3, 8)]
        public void IntersectOnlySameColumnTest(int col, int row1, int row2)
        {
            var intersect = (row1, col).IntersectFields((row2, col)).ToList();
            intersect.Should().HaveCount(9);
            intersect.Should().OnlyContain(f => f.Col == col);
        }

        [Theory]
        [InlineData(0, 0, 1)]
        [InlineData(0, 0, 2)]
        [InlineData(1, 1, 0)]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 0)]
        [InlineData(2, 2, 1)]
        public void IntersectOnlySameColumnAndX3Test(int col, int row1, int row2)
        {
            var intersect = (row1, col).IntersectFields((row2, col)).ToList();
            var rowColX3  = (row1, col).ConvertTo(Orientation.X3);
            intersect.Should().HaveCount(15);
            intersect.Distinct().Should().HaveCount(15);
            intersect.Should().OnlyContain(f => f.Col == col || f.ConvertTo(Orientation.X3).Row == rowColX3.Row);
        }

        [Theory]
        [InlineData(0, 0, 3)]
        [InlineData(0, 0, 4)]
        [InlineData(0, 0, 5)]
        [InlineData(0, 0, 6)]
        [InlineData(0, 0, 7)]
        [InlineData(0, 0, 8)]
        [InlineData(1, 0, 3)]
        [InlineData(1, 0, 4)]
        [InlineData(1, 0, 5)]
        [InlineData(1, 0, 6)]
        [InlineData(1, 0, 7)]
        [InlineData(1, 0, 8)]
        [InlineData(2, 3, 0)]
        [InlineData(2, 3, 1)]
        [InlineData(2, 3, 2)]
        [InlineData(2, 3, 6)]
        [InlineData(2, 3, 7)]
        [InlineData(2, 3, 8)]
        public void IntersectOnlySameRowTest(int row, int col1, int col2)
        {
            var intersect = (row, col1).IntersectFields((row, col2)).ToList();
            intersect.Should().HaveCount(9);
            intersect.Should().OnlyContain(f => f.Row == row);
        }

        [Theory]
        [InlineData(0, 0, 1)]
        [InlineData(0, 0, 2)]
        [InlineData(1, 1, 0)]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 0)]
        [InlineData(2, 2, 1)]
        public void IntersectOnlySameRowAndX3Test(int row, int col1, int col2)
        {
            var intersect = (row, col1).IntersectFields((row, col2)).ToList();
            var rowColX3  = (row, col1).ConvertTo(Orientation.X3);
            intersect.Should().HaveCount(15);
            intersect.Distinct().Should().HaveCount(15);
            intersect.Should().OnlyContain(f => f.Row == row || f.ConvertTo(Orientation.X3).Row == rowColX3.Row);
        }

        [Fact]
        public void IntersectAllTest()
        {
            for (int pos1 = 0; pos1 < 9 * 9; pos1++)
            {
                for (int pos2 = pos1; pos2 < 9 * 9; pos2++)
                {
                    var rowCol1   = (pos1 / 9, pos1 % 9);
                    var rowCol2   = (pos2 / 9, pos2 % 9);

                    var intersect = rowCol1.IntersectFields(rowCol2).ToList();

                    var dependent1 = rowCol1.DependentFields();
                    var dependent2 = rowCol2.DependentFields();

                    var inersectViaDependent = dependent1.Intersect(dependent2).ToList();

                    intersect.Should().HaveCount(inersectViaDependent.Count,$"pos1: {rowCol1} - pos2: {rowCol2}");
                    intersect.Should().OnlyContain(pos => inersectViaDependent.Contains(pos));
                }
            }
        }
    }
}