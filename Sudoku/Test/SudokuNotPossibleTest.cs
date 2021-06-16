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
    using FluentAssertions;

    using Sudoku.Solve;
    using Sudoku.Solve.NotPossible;

    using Xunit;

    public class SudokuNotPossibleTest : SudokuBaseUnitTest
    {
        private void CloneAndCompare(NotPossibleBase notPossible)
        {
            var serialized       = notPossible.SerializeTo();
            var notPossibleClone = NotPossibleBase.Create(serialized);

            notPossibleClone.SerializeTo().Should().Be(serialized);
            notPossibleClone.Should().BeEquivalentTo(notPossible);
        }

        [Fact]
        public void SerializeNotPossibleB1()
        {
            var notPossible = new NotPossibleBlockade1()
            {
                BecauseNo   = 1,
                ForNo       = 2,
                Orientation = Orientation.Column
            };

            CloneAndCompare(notPossible);
        }

        [Fact]
        public void SerializeNotPossibleB2()
        {
            var notPossible = new NotPossibleBlockade2()
            {
                ForNo       = 2,
                Orientation = Orientation.Column,
                BecauseIdx  = new[] { 1, 2, 3 },
                BecauseNos  = new[] { 9, 8, 7 }
            };

            CloneAndCompare(notPossible);
        }

        [Fact]
        public void SerializeNotPossibleB2SubSet()
        {
            var notPossible = new NotPossibleBlockade2SubSet()
            {
                ForNo       = 2,
                Orientation = Orientation.Column,
                BecauseIdx  = new[] { 1, 2, 3 },
                BecauseNos  = new[] { 9, 8, 7 }
            };

            CloneAndCompare(notPossible);
        }

        [Fact]
        public void SerializeNotPossibleB3()
        {
            var notPossible = new NotPossibleBlockade3()
            {
                ForNo       = 3,
                Orientation = Orientation.Column,
                BecauseIdx  = new[] { 1, 2, 3 },
            };

            CloneAndCompare(notPossible);
        }

        [Fact]
        public void SerializeNotPossibleXWing()
        {
            var notPossible = new NotPossibleXWing()
            {
                ForNo       = 4,
                Orientation = Orientation.Column,
                BecauseCol  = new[] { 1, 2 },
                BecauseRow  = new[] { 8, 7 },
            };

            CloneAndCompare(notPossible);
        }

        [Fact]
        public void SerializeNotPossibleSwordfish()
        {
            var notPossible = new NotPossibleSwordfish()
            {
                ForNo       = 4,
                Orientation = Orientation.Column,
                BecauseCol  = new[] { 1, 2, 3 },
                BecauseRow  = new[] { 8, 7, 6 },
            };

            CloneAndCompare(notPossible);
        }

        [Fact]
        public void SerializeNotPossibleJellyfish()
        {
            var notPossible = new NotPossibleJellyfish()
            {
                ForNo       = 4,
                Orientation = Orientation.Column,
                BecauseCol  = new[] { 1, 2, 3, 4 },
                BecauseRow  = new[] { 8, 7, 6, 5 },
            };

            CloneAndCompare(notPossible);
        }

        [Fact]
        public void ToStringNotPossibleB1()
        {
            var notPossible = new NotPossibleBlockade1()
            {
                BecauseNo   = 1,
                ForNo       = 2,
                Orientation = Orientation.Column
            };

            notPossible.ToString().Should().Be("2: 1 only in col (B1)");
        }

        [Fact]
        public void ToStringNotPossibleB2()
        {
            var notPossible = new NotPossibleBlockade2()
            {
                ForNo       = 2,
                Orientation = Orientation.Column,
                BecauseIdx  = new[] { 1, 2, 3 },
                BecauseNos  = new[] { 9, 8, 7 }
            };

            notPossible.ToString().Should().Be("2: 9,8,7: in col-index: 2,3,4 (B2)");
        }

        [Fact]
        public void ToStringNotPossibleB2SubSet()
        {
            var notPossible = new NotPossibleBlockade2SubSet()
            {
                ForNo       = 2,
                Orientation = Orientation.Column,
                BecauseIdx  = new[] { 1, 2, 3 },
                BecauseNos  = new[] { 9, 8, 7 }
            };

            notPossible.ToString().Should().Be("2: 9,8,7: in col-index: 2,3,4 (B2+)");
        }

        [Fact]
        public void ToStringNotPossibleB3()
        {
            var notPossible = new NotPossibleBlockade3()
            {
                ForNo       = 3,
                Orientation = Orientation.Column,
                BecauseIdx  = new[] { 1, 2, 3 },
            };

            notPossible.ToString().Should().Be("3: only in col-index: 2,3,4 (B3)");
        }

        [Fact]
        public void ToStringNotPossibleXWing()
        {
            var notPossible = new NotPossibleXWing()
            {
                ForNo       = 4,
                Orientation = Orientation.Column,
                BecauseCol  = new[] { 1, 2 },
                BecauseRow  = new[] { 8, 7 },
            };

            notPossible.ToString().Should().Be("4: X-Wing col 9,8 with row 2,3");
        }

        [Fact]
        public void ToStringNotPossibleSwordfish()
        {
            var notPossible = new NotPossibleSwordfish()
            {
                ForNo       = 4,
                Orientation = Orientation.Column,
                BecauseCol  = new[] { 1, 2, 3 },
                BecauseRow  = new[] { 8, 7, 6 },
            };

            notPossible.ToString().Should().Be("4: swordfish col 9,8,7 with row 2,3,4");
        }

        [Fact]
        public void ToStringNotPossibleJellyfish()
        {
            var notPossible = new NotPossibleJellyfish()
            {
                ForNo       = 4,
                Orientation = Orientation.Column,
                BecauseCol  = new[] { 1, 2, 3, 4 },
                BecauseRow  = new[] { 8, 7, 6, 5 },
            };

            notPossible.ToString().Should().Be("4: jellyfish col 9,8,7,6 with row 2,3,4,5");
        }
    }
}