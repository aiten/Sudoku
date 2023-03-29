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

using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using Sudoku.Solve;
using Sudoku.Solve.NotPossible;

public class SudokuBaseUnitTest
{
    protected struct ExpectResult
    {
        public ExpectResult(int x, int y, string possibleString, string toButtonToolTip)
        {
            X               = x;
            Y               = y;
            PossibleString  = possibleString;
            ToButtonToolTip = toButtonToolTip;
        }

        public int    X               { get; set; }
        public int    Y               { get; set; }
        public string PossibleString  { get; set; }
        public string ToButtonToolTip { get; set; }

        public ExpectResult Rotate()
        {
            string ToButtonToolTip(string buttonToolTip)
            {
                if (buttonToolTip.Contains(":C:"))
                {
                    return buttonToolTip.Replace(":C:", ":R:");
                }

                return buttonToolTip.Replace(":R:", ":C:");
            }

            return new ExpectResult(Y, 8 - X, PossibleString, ToButtonToolTip(this.ToButtonToolTip));
        }
    }

    protected IList<ExpectResult> Rotate(IList<ExpectResult> expected)
    {
        return expected.Select(expect => expect.Rotate()).ToList();
    }

    protected IList<ExpectResult> Mirror(IList<ExpectResult> expected)
    {
        return expected.Select(expect => new ExpectResult(expect.X, 8 - expect.Y, expect.PossibleString, expect.ToButtonToolTip)).ToList();
    }

    protected void CheckSudoku(string[] lines, IEnumerable<ExpectResult> expected, bool rotate = true)
    {
        var ex = (IList<ExpectResult>)expected.ToList();
        var s  = lines.CreateSudoku();

        if (rotate)
        {
            CheckOrigAndMirror(s, ex);
            for (int i = 0; i < 3; i++)
            {
                s  = s.Rotate();
                ex = Rotate(ex);
                CheckOrigAndMirror(s, ex);
            }
        }
        else
        {
            CheckSudokuInternal(s, ex);
        }
    }

    protected void CheckOrigAndMirror(Solve.Sudoku s, IList<ExpectResult> expected)
    {
        CheckSudokuInternal(s,          expected);
        CheckSudokuInternal(s.Mirror(), Mirror(expected));
    }

    protected void CheckSudokuInternal(Solve.Sudoku s, IList<ExpectResult> expected)
    {
        s.UpdatePossible();

        var lines = s.SmartPrint(" ");

        foreach (var expect in expected)
        {
            s.GetDef(expect.X, expect.Y).PossibleString().Should().Be(expect.PossibleString);
            var toolTips     = s.GetDef(expect.X, expect.Y).ToButtonToolTip().Split('\n');
            var toolTipRegEx = expect.ToButtonToolTip.Split('\n');
            for (int i = 0; i < toolTips.Length && i < toolTipRegEx.Length; i++)
            {
                var regex = toolTipRegEx[i];
                if (regex.StartsWith("B"))
                {
                    var notPossible = NotPossibleBase.Create(regex);

                    if (notPossible != null)
                    {
                        regex = notPossible.ToString();
                        regex = regex
                            .Replace("(", "\\(")
                            .Replace(")", "\\)")
                            .Replace("+", "\\+")
                            .Replace("-", "\\-")
                            .Replace("*", "\\*");
                    }
                    else
                    {
                        throw new AggregateException();
                    }
                }

                if (string.IsNullOrEmpty(regex))
                {
                    toolTips[i].Should().BeEmpty();
                }
                else
                {
                    toolTips[i].Should().MatchRegex(regex);
                }
            }
        }
    }
}