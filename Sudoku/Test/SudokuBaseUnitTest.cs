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
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Sudoku.Solve;

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
        }

        protected IList<ExpectResult> Rotate(IList<ExpectResult> expected)
        {
            string ToButtonToolTip(string buttonToolTip)
            {
                if (buttonToolTip.Contains("B1C"))
                {
                    return buttonToolTip.Replace("B1C", "B1R");
                }

                return buttonToolTip.Replace("B1R", "B1C");
            }

            return expected.Select(expect => new ExpectResult(expect.Y, 8 - expect.X, expect.PossibleString, ToButtonToolTip(expect.ToButtonToolTip))).ToList();
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

            var lines = s.SmartPrint();

            var opt = new SudokuOptions
            {
                Help        = true,
                ShowToolTip = true
            };

            foreach (var expect in expected)
            {
                s.GetDef(expect.X, expect.Y).PossibleString().Should().Be(expect.PossibleString);
                var toolTips     = s.GetDef(expect.X, expect.Y).ToButtonToolTip(opt).Split('\n');
                var toolTipRegEx = expect.ToButtonToolTip.Split('\n');
                for (int i = 0; i < toolTips.Length && i < toolTipRegEx.Length; i++)
                {
                    var regex = toolTipRegEx[i];
                    if (regex.StartsWith("B"))
                    {
                        var parts = regex.Split(':');

                        // 4: 1 only in 3*3 (B1)
                        // 6: 8,9: in 3*3-index: 1,2,7 (B2)
                        // 6: 6,8,9: in col-index: 1,3,4 (B2+)
                        // 1: only in col-index:7,8,9 (B3)

                        switch (parts[0])
                        {
                            case "B13":
                                regex = $"{parts[1]}: {parts[2]} only in 3\\*3 \\(B1\\)";
                                break;
                            case "B1C":
                                regex = $"{parts[1]}: {parts[2]} only in col \\(B1\\)";
                                break;
                            case "B1R":
                                regex = $"{parts[1]}: {parts[2]} only in row \\(B1\\)";
                                break;

                            case "B23":
                                regex = $"{parts[1]}: {parts[2]}: in 3\\*3\\-index: {parts[3]} \\(B2\\)";
                                break;
                            case "B2C":
                                regex = $"{parts[1]}: {parts[2]}: in col\\-index: {parts[3]} \\(B2\\)";
                                break;
                            case "B2R":
                                regex = $"{parts[1]}: {parts[2]}: in row\\-index: {parts[3]} \\(B2\\)";
                                break;

                            case "B2P3":
                                regex = $"{parts[1]}: {parts[2]}: in 3\\*3\\-index: {parts[3]} \\(B2\\+\\)";
                                break;
                            case "B2PC":
                                regex = $"{parts[1]}: {parts[2]}: in col\\-index: {parts[3]} \\(B2\\+\\)";
                                break;
                            case "B2PR":
                                regex = $"{parts[1]}: {parts[2]}: in row\\-index: {parts[3]} \\(B2\\+\\)";
                                break;

                            case "B33":
                                regex = $"{parts[1]}: only in 3\\*3\\-index: {parts[2]} \\(B3\\)";
                                break;
                            case "B3C":
                                regex = $"{parts[1]}: only in col\\-index: {parts[2]} \\(B3\\)";
                                break;
                            case "B3R":
                                regex = $"{parts[1]}: only in row\\-index: {parts[2]} \\(B3\\)";
                                break;
                        }
                    }

                    toolTips[i].Should().MatchRegex(regex);
                }
            }
        }
    }
}