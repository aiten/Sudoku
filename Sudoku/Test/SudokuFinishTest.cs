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
    using System.Threading;

    using FluentAssertions;

    using Sudoku.Solve;

    using Xunit;

    public class SudokuFinishTest : SudokuBaseUnitTest
    {
        [Fact]
        public void FinishTest()
        {
            var lines = new[]
            {
                " , ,7,8, ,4,3, , ",
                " , , ,7, ,2, , , ",
                "6, , ,1,9,3, , ,4",
                "5,7, ,2,3,1, ,4,9",
                " , ,9,5, , ,1, , ",
                "3,1, , , , , ,5,2",
                "7, , , ,2, , , ,5",
                " , , ,4, ,7, , , ",
                " , ,2,3, ,5,4, , ",
            };

            var s = lines.CreateSudoku();

            s.StepCount.Should().Be(34);
            s.CalcPossibleSolutions(new CancellationToken(false)).Should().Be(1);
            s.UndoAvailable = true;
            s.CanUndo().Should().BeFalse();

            s.Clear(2, 0);

            s.StepCount.Should().Be(33);
            s.CalcPossibleSolutions(new CancellationToken(false)).Should().Be(3);

            s.CanUndo().Should().BeTrue();
            s.Undo();
            s.CanUndo().Should().BeFalse();

            s.StepCount.Should().Be(34);
            s.CalcPossibleSolutions(new CancellationToken(false)).Should().Be(1);

            s.UpdatePossible();
            s.SetNextPossible(6, 6);
            s.GetDef(6, 6).No.Should().Be(8);

            s.SetNextPossible(3, 6);
            s.GetDef(3, 6).No.Should().Be(6);

            s.SetNextPossible(3, 6);
            s.GetDef(3, 6).No.Should().Be(0);

            s.SetNextPossible(1, 5);
            s.GetDef(1, 5).No.Should().Be(6);
            s.CalcPossibleSolutions(new CancellationToken(false)).Should().Be(0);

            s.Finish().Should().BeFalse();
            s.Set(1, 5, 2);
            s.Finish().Should().BeTrue();
        }

        [Fact]
        public void FoundSolutionTest()
        {
            var lines = new[]
            {
                " , ,7,8, ,4,3, , ",
                " , , ,7, ,2, , , ",
                "6, , ,1,9,3, , ,4",
                "5,7, ,2,3,1, ,4,9",
                " , ,9,5, , ,1, , ",
                "3,1, , , , , ,5,2",
                "7, , , ,2, , , ,5",
                " , , ,4, ,7, , , ",
                " , ,2,3, ,5,4, , ",
            };

            var s = lines.CreateSudoku();

            s.Clear(2, 0);
            s.StepCount.Should().Be(33);

            var count = 1;

            s.FoundSolution += (object sudoku, SudokuEventArgs sarg) =>
            {
                if (sarg.FindSolutionsFinished)
                {
                    sarg.PossibleSolutions.Should().Be(3);
                    count = sarg.PossibleSolutions;
                }
            };

            s.CalcPossibleSolutions(new CancellationToken(false)).Should().Be(3);
            count.Should().Be(3);
        }
    }
}