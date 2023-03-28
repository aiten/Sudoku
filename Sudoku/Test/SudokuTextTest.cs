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

namespace Sudoku.Test;

using FluentAssertions;

using Sudoku.Solve;

using Xunit;

public class SudokuTextTest : SudokuBaseUnitTest
{
    [Fact]
    public void PossibilityTextTest()
    {
        var lines = new[]
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
        };

        var s = lines.CreateSudoku();
        s.UpdatePossible();

        for (var x = 0; x < 9; x++)
        {
            for (var y = 0; y < 9; y++)
            {
                var def = s.GetDef(x, y);
                if (def.IsEmpty)
                {
                    var toolTipsText = s.GetDef(x, y).GetFullFiledInfo();
                    var toolTips     = toolTipsText.Split('\n');

                    if (def.PossibleString() != toolTipsText)
                    {
                        for (int i = 1; i < toolTips.Length; i++)
                        {
                            var toolTip = toolTips[i];
                            toolTip.Should().StartWith("B");
                            var localized = toolTip;
                            localized.Should().NotBeEmpty();
                        }
                    }
                }
            }
        }
    }
}