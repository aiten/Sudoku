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

    public class SudokuUnitTest : SudokuBaseUnitTest
    {
        [Fact]
        public void FillTest()
        {
            var lines = new[]
            {
                "1,8,7,6,5,4,3,2,9",
                "2, , , , , , , , ",
                "3, , , , , , , , ",
                "4, , , , , , , , ",
                "5, , , , , , , , ",
                "6, , , , , , , , ",
                "7, , , , , , , , ",
                "8, , , , , , , , ",
                "9, , , , , , , , "
            };

            var s = lines.CreateSudoku();

            s.Set(1, 1, 10).Should().BeFalse();
            s.Set(1, 1, -1).Should().BeFalse();

            var nowLines = s.SmartPrint();

            for (int y = 0; y < 9; y++)
            {
                nowLines[y].Should().Be(lines[y]);
            }
        }

        [Fact]
        public void CanSetRow1Test()
        {
            var lines = new[]
            {
                "1,4,5,2,3,6,7, ,8",
            };

            var s = lines.CreateSudoku();

            var cannotSet = new[]
            {
                (7, 0),
            };

            TestCanSet(s, cannotSet);
        }

        [Fact]
        public void CanSetRow2Test()
        {
            var lines = new[]
            {
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                "2,3,6,7, ,8,1,4,5",
            };

            var s = lines.CreateSudoku();

            var cannotSet = new[]
            {
                (5, 4),
            };

            TestCanSet(s, cannotSet);
        }

        [Fact]
        public void CanSetRow3Test()
        {
            var lines = new[]
            {
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                "7, ,8,1,4,5,2,3,6",
            };

            var s = lines.CreateSudoku();

            var cannotSet = new[]
            {
                (1, 8)
            };

            TestCanSet(s, cannotSet);
        }

        [Fact]
        public void CanSetCol1Test()
        {
            var lines = new[]
            {
                "1, , , , , , , , ",
                "2, , , , , , , , ",
                "3, , , , , , , , ",
                "4, , , , , , , , ",
                "5, , , , , , , , ",
                "6, , , , , , , , ",
                "7, , , , , , , , ",
                " , , , , , , , , ",
                "8, , , , , , , , ",
            };

            var s = lines.CreateSudoku();

            var cannotSet = new[]
            {
                (0, 7),
            };

            TestCanSet(s, cannotSet);
        }

        [Fact]
        public void CanSetCol2Test()
        {
            var lines = new[]
            {
                " , , , ,1, , , , ",
                " , , , ,2, , , , ",
                " , , , ,3, , , , ",
                " , , , ,4, , , , ",
                " , , , ,5, , , , ",
                " , , , ,6, , , , ",
                " , , , ,7, , , , ",
                " , , , , , , , , ",
                " , , , ,8, , , , ",
            };

            var s = lines.CreateSudoku();

            var cannotSet = new[]
            {
                (4, 7),
            };

            TestCanSet(s, cannotSet);
        }

        [Fact]
        public void CanSetCol3Test()
        {
            var lines = new[]
            {
                " , , , , , , , ,1",
                " , , , , , , , ,2",
                " , , , , , , , ,3",
                " , , , , , , , ,4",
                " , , , , , , , ,5",
                " , , , , , , , ,6",
                " , , , , , , , ,7",
                " , , , , , , , , ",
                " , , , , , , , ,8",
            };

            var s = lines.CreateSudoku();

            var cannotSet = new[]
            {
                (8, 7),
            };

            TestCanSet(s, cannotSet);
        }

        [Fact]
        public void CanSet3X31Test()
        {
            var lines = new[]
            {
                "1,2,7, , , , , , ",
                "3,6, , , , , , , ",
                "4,5,8, , , , , , ",
                " , , ,1,2,7, , , ",
                " , , ,3, ,6, , , ",
                " , , ,4,5,8, , , ",
                " , , , , , ,1,2,7",
                " , , , , , , ,3,6",
                " , , , , , ,4,5,8",
            };

            var s = lines.CreateSudoku();

            var cannotSet = new[]
            {
                (2, 1),
                (4, 4),
                (6, 7),
            };

            TestCanSet(s, cannotSet);
        }

        [Fact]
        public void CanSet3X32Test()
        {
            var lines = new[]
            {
                " , , , , , ,1,2,7",
                " , , , , , , ,3,6",
                " , , , , , ,4,5,8",
                "1,2,7, , , , , , ",
                "3,6, , , , , , , ",
                "4,5,8, , , , , , ",
                " , , ,1,2,7, , , ",
                " , , ,3, ,6, , , ",
                " , , ,4,5,8, , , ",
            };

            var s = lines.CreateSudoku();

            var cannotSet = new[]
            {
                (6, 1),
                (2, 4),
                (4, 7),
            };

            TestCanSet(s, cannotSet);
        }

        [Fact]
        public void CanSet3X33Test()
        {
            var lines = new[]
            {
                " , , ,1,2,7, , , ",
                " , , ,3, ,6, , , ",
                " , , ,4,5,8, , , ",
                " , , , , , ,1,2,7",
                " , , , , , , ,3,6",
                " , , , , , ,4,5,8",
                "1,2,7, , , , , , ",
                "3,6, , , , , , , ",
                "4,5,8, , , , , , ",
            };

            var s = lines.CreateSudoku();

            var cannotSet = new[]
            {
                (4, 1),
                (6, 4),
                (2, 7),
            };

            TestCanSet(s, cannotSet);
        }

        [Fact]
        public void CanSetTest()
        {
            var lines = new[]
            {
                "1,4,5,2,3,6,7, ,8",
                "2, , , , , , , , ",
                "3, , , , , , , , ",
                "4, , ,1,2,7, , , ",
                "5, , ,3,6, , , , ",
                "6, , ,4,5,8, , , ",
                "7, , , , , , , , ",
                " , , , , , , , , ",
                "8, , , , , , , , ",
            };

            var s = lines.CreateSudoku();

            var cannotSet = new[]
            {
                (0, 7),
                (8, 0),
                (5, 4)
            };

            TestCanSet(s, cannotSet);
        }

        private static void TestCanSet(Sudoku s, (int, int)[] cannotSet)
        {
            for (var x = 0; x < 9; x++)
            {
                for (var y = 0; y < 9; y++)
                {
                    s.CanSet(x, y, 0).Should().BeTrue();
                    s.CanSet(x, y, 9).Should().BeTrue();

                    if (cannotSet.Contains((x, y)))
                    {
                        // cannot set 0..8, only 9
                        for (int no = 1; no < 9; no++)
                        {
                            s.CanSet(x, y, no).Should().BeFalse($"{x}:{y} with {no}");
                        }
                    }
                }
            }
        }
    }
}