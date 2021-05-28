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

    using Xunit;

    public class SudokuUnitTestGenerated : SudokuBaseUnitTest
    {
        [Fact]
        public void Test1NotPossible()
        {
            CheckSudoku(new[]
                {
                    "1, , , , , , , , ",
                    "2, , , , , , , , ",
                    "3, , , , , , , , ",
                    "4, , , , , , , , ",
                    "5, , , , , , , , ",
                    "6, , , , , , , , ",
                    "7, , , , , , , , ",
                    "8, , , , , , , , ",
                    " , , , , , , , ,9",
                },
                new ExpectResult[]
                {
                    new (0, 8, "", ""),
                }
            );
        }

        [Fact]
        public void Test1Singleton()
        {
            CheckSudoku(new[]
              {
                "1,4,5,2,3,6,7,8, ",
                "2, , , , , , , , ",
                "3, , , , , , , , ",
                "4, , ,1,2,7, , , ",
                "5, , ,3,6,8, , , ",
                "6, , ,4,5, , , , ",
                "7, , , , , , , , ",
                "8, , , , , , , , ",
                " , , , , , , , , ",
              },
              new ExpectResult[]
              {
                new (8, 0, "9", "9"),
                new (5, 5, "9", "9"),
                new (0, 8, "9", "9"),
              }
            );
        }
        [Fact]
        public void Test2KreuzendeLinien1b1()
        {
            CheckSudoku(new[]
              {
                "1, , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " ,1, , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , , , ,1, , , , ",
                " , ,2, , , , , , ",
                " , , , , , , , , ",
              },
              new ExpectResult[]
              {
                new (2, 8, "1", "1 - 3,4,5,6,7,8,9\nB13:3:1\nB13:4:1\nB13:5:1\nB13:6:1\nB13:7:1\nB13:8:1\nB13:9:1"),
              }
            );
        }
        [Fact]
        public void Test2KreuzendeLinien2b1()
        {
            CheckSudoku(new[]
              {
                "1, , , , , , , , ",
                " , , ,1, , , , , ",
                " , , , , , ,2, , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , ,1, ",
                " , , , , , , , , ",
                " , , , , , , , , ",
              },
              new ExpectResult[]
              {
                new (8, 2, "1", "1 - 3,4,5,6,7,8,9\nB13:3:1\nB13:4:1\nB13:5:1\nB13:6:1\nB13:7:1\nB13:8:1\nB13:9:1"),
              }
            );
        }
        [Fact]
        public void Test2KreuzendeLinien3b1()
        {
            CheckSudoku(new[]
              {
                "1,2, , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , , ,2, ,1, , , ",
                " , ,1, , , ,2, , ",
                "2, , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
              },
              new ExpectResult[]
              {
                new (2, 3, "2", "2 - 3,4,5,6,7,8,9\nB13:3:2\nB13:4:2\nB13:5:2\nB13:6:2\nB13:7:2\nB13:8:2\nB13:9:2"),
              }
            );
        }
        [Fact]
        public void Test3ParalleleLinienb1()
        {
            CheckSudoku(new[]
              {
                "1, , , , , , , , ",
                " , , , ,1, , , , ",
                " , , , , , ,2,3, ",
                " ,1, , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , ,2, , , , , , ",
                " , ,3, , , , , , ",
                " , , , , , , , , ",
              },
              new ExpectResult[]
              {
                new (8, 2, "1", "1 - 4,5,6,7,8,9\nB13:4:1\nB13:5:1\nB13:6:1\nB13:7:1\nB13:8:1\nB13:9:1"),
                new (2, 8, "1", "1 - 4,5,6,7,8,9\nB13:4:1\nB13:5:1\nB13:6:1\nB13:7:1\nB13:8:1\nB13:9:1"),
              }
            );
        }
        [Fact]
        public void Test4Blockade1col()
        {
            CheckSudoku(new[]
              {
                " ,2,3, , , , , ,1",
                " , , , , , , , , ",
                " ,4,5,6,7, , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                "1, , , , , , , , ",
              },
              new ExpectResult[]
              {
                new (5, 2, "1", "1 - 2,3,8,9\nB1C:2:1\nB1C:3:1\nB1C:8:1\nB1C:9:1"),
              }
            );
        }
        [Fact]
        public void Test4Blockade1row()
        {
            CheckSudoku(new[]
              {
                " , , , , , , , ,1",
                "2, ,4, , , , , , ",
                "3, ,5, , , , , , ",
                " , ,6, , , , , , ",
                " , ,7, , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                "1, , , , , , , , ",
              },
              new ExpectResult[]
              {
                new (2, 5, "1", "1 - 2,3,8,9\nB1R:2:1\nB1R:3:1\nB1R:8:1\nB1R:9:1"),
              }
            );
        }
        [Fact]
        public void Test4Blockade1s3()
        {
            CheckSudoku(new[]
              {
                " , , , , , , , , ",
                " , , ,1, , , , , ",
                " , , , , , , , ,1",
                " ,1, , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                " , ,1, , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
              },
              new ExpectResult[]
              {
                new (0, 0, "1", "1 - 2,3,4,5,6,7,8,9\nB13:2:1\nB13:3:1\nB13:4:1\nB13:5:1\nB13:6:1\nB13:7:1\nB13:8:1\nB13:9:1"),
              }
            );
        }
        [Fact]
        public void Test4Blockade2()
        {
            CheckSudoku(new[]
              {
                " , , , , , , , , ",
                "8, , , , , , , ,9",
                " , , , , , , , , ",
                " , , , ,2, , , , ",
                " , , , ,3, , , , ",
                " , , , ,4, , , , ",
                " , , , ,5, , , , ",
                " , , , ,6, , , , ",
                " , , , ,7, , , , ",
              },
              new ExpectResult[]
              {
                new (4, 1, "1", "1"),
              }
            );
        }
        [Fact]
        public void Test4Blockade2neu()
        {
            CheckSudoku(new[]
              {
                " , , , , , , , , ",
                " , ,7, , , , , , ",
                "1,2,3,7, , , , , ",
                "2, , ,6, , , , , ",
                "3, ,5,4, , , , , ",
                "4, , ,5, , , , , ",
                "5, ,4,3, , , , , ",
                "6, , ,2, , , , , ",
                "7, ,2,1, , , , , ",
              },
              new ExpectResult[]
              {
                new (1, 0, "4,5", "4,5 - 6,8,9\nB2PC:6:6,8,9:1,3,4\nB2P3:8:8,9:1,2\nB2P3:9:8,9:1,2"),
                new (2, 0, "6", "6 - 8,9\nB2P3:8:8,9:1,2\nB2P3:9:8,9:1,2"),
                new (4, 0, "1,2,3,4,5", "1,2,3,4,5 - 6,8,9\nB2PC:6:6,8,9:1,3,4\nB2P3:8:8,9:1,2\nB2P3:9:8,9:1,2"),
                new (5, 0, "1,2,3,4,5", "1,2,3,4,5 - 6,8,9\nB2PC:6:6,8,9:1,3,4\nB2P3:8:8,9:1,2\nB2P3:9:8,9:1,2"),
                new (6, 0, "1,2,3,4,5,7", "1,2,3,4,5,7 - 6,8,9\nB2PC:6:6,8,9:1,3,4\nB2PC:8:6,8,9:1,3,4\nB2PC:9:6,8,9:1,3,4"),
                new (7, 0, "1,2,3,4,5,7", "1,2,3,4,5,7 - 6,8,9\nB2PC:6:6,8,9:1,3,4\nB2PC:8:6,8,9:1,3,4\nB2PC:9:6,8,9:1,3,4"),
                new (8, 0, "1,2,3,4,5,7", "1,2,3,4,5,7 - 6,8,9\nB2PC:6:6,8,9:1,3,4\nB2PC:8:6,8,9:1,3,4\nB2PC:9:6,8,9:1,3,4"),
                new (1, 1, "4,5", "4,5 - 6,8,9\nB23:6:8,9:1,2,7\nB2P3:8:8,9:1,2\nB2P3:9:8,9:1,2"),
                new (4, 1, "1,2,3,4,5,6", "1,2,3,4,5,6 - 8,9\nB2P3:8:8,9:1,2\nB2P3:9:8,9:1,2"),
                new (5, 1, "1,2,3,4,5,6", "1,2,3,4,5,6 - 8,9\nB2P3:8:8,9:1,2\nB2P3:9:8,9:1,2"),
                new (6, 1, "1,2,3,4,5,6", "1,2,3,4,5,6 - 8,9\nB2PC:8:8,9:1,4\nB2PC:9:8,9:1,4"),
                new (7, 1, "1,2,3,4,5,6", "1,2,3,4,5,6 - 8,9\nB2PC:8:8,9:1,4\nB2PC:9:8,9:1,4"),
                new (8, 1, "1,2,3,4,5,6", "1,2,3,4,5,6 - 8,9\nB2PC:8:8,9:1,4\nB2PC:9:8,9:1,4"),
                new (4, 2, "4,5,6", "4,5,6 - 8,9\nB2P3:8:8,9:1,2\nB2P3:9:8,9:1,2"),
                new (5, 2, "4,5,6", "4,5,6 - 8,9\nB2P3:8:8,9:1,2\nB2P3:9:8,9:1,2"),
              },
              false
            );
        }
        [Fact]
        public void Test4Blockade2neu2()
        {
            CheckSudoku(new[]
              {
                " , , , , , , , ,7",
                " , , , , , , , ,8",
                " , , , , , , , ,9",
                "1, , , , , , , , ",
                "2, , , , , , , , ",
                "3, , , , , , , , ",
                "4, , , , , , , , ",
                "5, , , , , , , , ",
                "6, , , , , , , , ",
              },
              new ExpectResult[]
              {
                new (1, 0, "1,2,3,4,5,6", "1,2,3,4,5,6 - 8,9\nB2P3:8:7,8,9:1,2,3\nB2P3:9:7,8,9:1,2,3"),
                new (2, 0, "1,2,3,4,5,6", "1,2,3,4,5,6 - 8,9\nB2P3:8:7,8,9:1,2,3\nB2P3:9:7,8,9:1,2,3"),
                new (1, 1, "1,2,3,4,5,6", "1,2,3,4,5,6 - 7,9\nB2P3:7:7,8,9:1,2,3\nB2P3:9:7,8,9:1,2,3"),
                new (2, 1, "1,2,3,4,5,6", "1,2,3,4,5,6 - 7,9\nB2P3:7:7,8,9:1,2,3\nB2P3:9:7,8,9:1,2,3"),
                new (1, 2, "1,2,3,4,5,6", "1,2,3,4,5,6 - 7,8\nB2P3:7:7,8,9:1,2,3\nB2P3:8:7,8,9:1,2,3"),
                new (2, 2, "1,2,3,4,5,6", "1,2,3,4,5,6 - 7,8\nB2P3:7:7,8,9:1,2,3\nB2P3:8:7,8,9:1,2,3"),
              },
              false
            );
        }
        [Fact]
        public void Test4Blockade2neu3()
        {
            CheckSudoku(new[]
              {
                " , ,9, ,8, , , ,6",
                " ,3, , , ,5, , ,7",
                " , ,4, ,7, , , ,8",
                " ,4, , , ,6,3,5,9",
                "1, , , , , , , , ",
                " , , , , , , , , ",
                "2, , , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
              },
              new ExpectResult[]
              {
                new (0, 5, "3,9", "3,9 - 5,6,7,8\nB2PR:5:5,6,7,8:1,2,3,4\nB2PR:6:5,6,7,8:1,2,3,4\nB2PR:7:5,6,7,8:1,2,3,4\nB2PR:8:5,6,7,8:1,2,3,4"),
                new (3, 5, "2,3,4,5,7,8,9", "2,3,4,5,7,8,9 - 1\nB3C:1:7,8,9"),
                new (4, 5, "2,3,4,5,9", "2,3,4,5,9 - 1\nB3C:1:7,8,9"),
                new (5, 5, "2,3,4,7,8,9", "2,3,4,7,8,9 - 1\nB3C:1:7,8,9"),
                new (6, 6, "1,4,6,7,8,9", "1,4,6,7,8,9 - 5\nB3R:5:1,3"),
                new (7, 6, "1,4,6,7,8,9", "1,4,6,7,8,9 - 3\nB3R:3:1,3"),
                new (0, 7, "3,4,9", "3,4,9 - 5,6,7,8\nB2PR:5:5,6,7,8:1,2,3,4\nB2PR:6:5,6,7,8:1,2,3,4\nB2PR:7:5,6,7,8:1,2,3,4\nB2PR:8:5,6,7,8:1,2,3,4"),
                new (6, 7, "1,2,4,6,7,8,9", "1,2,4,6,7,8,9 - 5\nB3R:5:1,3"),
                new (7, 7, "1,2,4,6,7,8,9", "1,2,4,6,7,8,9 - 3\nB3R:3:1,3"),
                new (0, 8, "3,4,9", "3,4,9 - 5,6,7,8\nB2PR:5:5,6,7,8:1,2,3,4\nB2PR:6:5,6,7,8:1,2,3,4\nB2PR:7:5,6,7,8:1,2,3,4\nB2PR:8:5,6,7,8:1,2,3,4"),
                new (6, 8, "1,2,4,6,7,8,9", "1,2,4,6,7,8,9 - 5\nB3R:5:1,3"),
                new (7, 8, "1,2,4,6,7,8,9", "1,2,4,6,7,8,9 - 3\nB3R:3:1,3"),
              },
              false
            );
        }
        [Fact]
        public void Test4Blockade3neu()
        {
            CheckSudoku(new[]
              {
                "1,2,3, , , , , , ",
                " , , , , , , , , ",
                " ,5,6,2, ,1, , , ",
                " , , , , ,7, , , ",
                " , ,8, , ,9, , , ",
                " , , , , ,3, , , ",
                "4, , , , ,5, , , ",
                " ,7, , , ,2, , , ",
                " , , , , ,6, , , ",
              },
              new ExpectResult[]
              {
                new (3, 0, "5,6,7,9", "5,6,7,9 - 4,8\nB2P3:4:4,8:7,8\nB2P3:8:4,8:7,8"),
                new (4, 0, "5,6,7,9", "5,6,7,9 - 4,8\nB2P3:4:4,8:7,8\nB2P3:8:4,8:7,8"),
                new (3, 1, "3,5,6", "3,5,6 - 4,7,8,9\nB2P3:4:4,8:7,8\nB2PC:7:4,7,8,9:1,2,3,6\nB2P3:8:4,8:7,8\nB2PC:9:4,7,8,9:1,2,3,6"),
                new (4, 1, "3,5,6", "3,5,6 - 4,7,8,9\nB2P3:4:4,8:7,8\nB2PC:7:4,7,8,9:1,2,3,6\nB2P3:8:4,8:7,8\nB2PC:9:4,7,8,9:1,2,3,6"),
                new (5, 1, "8", "8 - 4\nB3C:4:2,3"),
                new (6, 1, "1,2,3,5,6", "1,2,3,5,6 - 4,7,8,9\nB2PC:4:4,7,8,9:1,2,3,6\nB2PC:7:4,7,8,9:1,2,3,6\nB2PC:8:4,7,8,9:1,2,3,6\nB2PC:9:4,7,8,9:1,2,3,6"),
                new (7, 1, "1,2,3,5,6", "1,2,3,5,6 - 4,7,8,9\nB2PC:4:4,7,8,9:1,2,3,6\nB2PC:7:4,7,8,9:1,2,3,6\nB2PC:8:4,7,8,9:1,2,3,6\nB2PC:9:4,7,8,9:1,2,3,6"),
                new (8, 1, "1,2,3,5,6", "1,2,3,5,6 - 4,7,8,9\nB2PC:4:4,7,8,9:1,2,3,6\nB2PC:7:4,7,8,9:1,2,3,6\nB2PC:8:4,7,8,9:1,2,3,6\nB2PC:9:4,7,8,9:1,2,3,6"),
                new (4, 2, "3,7,9", "3,7,9 - 4,8\nB2P3:4:4,8:7,8\nB2P3:8:4,8:7,8"),
              },
              false
            );
        }
        [Fact]
        public void Test4Blockade31()
        {
            CheckSudoku(new[]
              {
                " , , , ,2, , ,3, ",
                " ,4, , , , , , , ",
                " ,5, , , , , , , ",
                " , , , , , , , , ",
                "2, , , , , , , , ",
                "3,1, , , , , , , ",
                " , , , , , , , , ",
                " , , , , , , , , ",
                "1, , , , , , , , ",
              },
              new ExpectResult[]
              {
                new (2, 0, "1", "1 - 6,7,8,9\nB2P3:6:6,7,8,9:1,2,3,4\nB2P3:7:6,7,8,9:1,2,3,4\nB2P3:8:6,7,8,9:1,2,3,4\nB2P3:9:6,7,8,9:1,2,3,4"),
                new (2, 1, "2,3", "2,3 - 1,6,7,8,9\nB23:1:6,7,8,9:1,2,3,4,7\nB2P3:6:6,7,8,9:1,2,3,4\nB2P3:7:6,7,8,9:1,2,3,4\nB2P3:8:6,7,8,9:1,2,3,4\nB2P3:9:6,7,8,9:1,2,3,4"),
                new (2, 2, "2,3", "2,3 - 1,6,7,8,9\nB23:1:6,7,8,9:1,2,3,4,7\nB2P3:6:6,7,8,9:1,2,3,4\nB2P3:7:6,7,8,9:1,2,3,4\nB2P3:8:6,7,8,9:1,2,3,4\nB2P3:9:6,7,8,9:1,2,3,4"),
                new (2, 6, "4,5,6,7,8,9", "4,5,6,7,8,9 - 2,3\nB3R:2:2,3\nB3R:3:2,3"),
                new (2, 7, "4,5,6,7,8,9", "4,5,6,7,8,9 - 2,3\nB3R:2:2,3\nB3R:3:2,3"),
                new (2, 8, "4,5,6,7,8,9", "4,5,6,7,8,9 - 2,3\nB3R:2:2,3\nB3R:3:2,3"),
              },
              false
            );
        }
        [Fact]
        public void Test4Blockade32()
        {
            CheckSudoku(new[]
              {
                " , ,6,5, ,3,7, , ",
                " , , ,7, ,1, , , ",
                "7, , , ,9, , , ,2",
                "8,7,1,2,5, , ,4,3",
                " , ,9,3, , ,2, , ",
                "4,2,3,1, , , ,7,5",
                "3, , , ,1, , , ,4",
                " , , ,4, ,5, , , ",
                " , ,4,9, ,2,5, , ",
              },
              new ExpectResult[]
              {
                new (6, 1, "3,4", "3,4 - 6,8,9\nB2PR:6:6,8,9:4,6,7\nB2PR:8:6,8,9:4,6,7\nB2PR:9:6,8,9:4,6,7"),
                new (6, 2, "1,3,4", "1,3,4 - 6,8\nB2PR:6:6,8,9:4,6,7\nB2PR:8:6,8,9:4,6,7"),
                new (4, 4, "4,7", "4,7 - 6,8\nB2PC:6:5,6:1,2\nB23:8:6,8:6,7,9"),
                new (5, 4, "4,7", "4,7 - 6,8\nB2PC:6:5,6:1,2\nB23:8:6,8:6,7,9"),
                new (7, 4, "1,8", "1,8 - 6\nB2PC:6:5,6:1,2"),
                new (8, 4, "1,8", "1,8 - 6\nB2PC:6:5,6:1,2"),
                new (6, 5, "6,9", "6,9 - 8\nB2P3:8:1,8:5,8"),
                new (1, 6, "5,9", "5,9 - 6,8\nB2PC:6:6,7,8:4,6,7\nB2PC:8:6,7,8:4,6,7"),
                new (2, 6, "2,5", "2,5 - 7,8\nB2PC:7:6,7,8:4,6,7\nB2PC:8:6,7,8:4,6,7"),
                new (5, 6, "7", "7 - 6,8\nB2C:6:6,8:4,7\nB2C:8:6,8:4,7"),
                new (6, 6, "8", "8 - 6,9\nB3R:6:4,6\nB3R:9:4,6"),
                new (7, 6, "2,9", "2,9 - 6,8\nB2PC:6:6,7,8:4,6,7\nB2PC:8:6,7,8:4,6,7"),
                new (6, 7, "1,3", "1,3 - 6,8,9\nB2PR:6:6,8,9:4,6,7\nB2PR:8:6,8,9:4,6,7\nB2PR:9:6,8,9:4,6,7"),
              },
              false
            );
        }

        [Fact]
        public void SimpleTest()
        {
            var s = new Sudoku();
            s.UpdatePossible();

            for (int x = 0; x < 9; x++)
            for (int y = 0; y < 9; y++)
                s.GetDef(x, y).MainRulePossibleCount().Should().Be(9);
        }

        [Fact]
        public void SimplePossibleTest()
        {
            var s   = new Sudoku();
            var opt = new SudokuOptions();
            opt.Help        = true;
            opt.ShowToolTip = true;

            s.UpdatePossible();

            for (int x = 0; x < 9; x++)
            for (int y = 0; y < 9; y++)
            {
                s.GetDef(x, y).PossibleCount().Should().Be(9);
                s.GetDef(x, y).PossibleString().Should().BeEquivalentTo("1,2,3,4,5,6,7,8,9");

                s.GetDef(x, y).MainRulePossibleCount().Should().Be(9);
                s.GetDef(x, y).ToButtonString(opt).Should().BeEquivalentTo("1,2,3,4,5,6,7,8,9");
            }
        }
    }
}