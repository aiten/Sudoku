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
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Sudoku.Solve;
    using Sudoku.Solve.Serialization;

    using Xunit;

    public class SudokuTestCreator : SudokuBaseUnitTest
    {
        private enum RoleType
        {
            R3x3 = 0,
            Row  = 1,
            Col  = 2,
            NC   = 3
        }

        RoleType GetRoleType(string info)
        {
            if (info.Contains("3*3"))
            {
                return RoleType.R3x3;
            }

            if (info.Contains("row"))
            {
                return RoleType.Row;
            }

            if (info.Contains("col"))
            {
                return RoleType.Col;
            }

            return RoleType.NC;
        }

        [Fact]
        public void FillTest()
        {
            var dirInfo = Directory.EnumerateFiles(@"C:\dev\Sudoku\Sudoku\Test\TestSamples", "_*.sud");

            var opt = new SudokuOptions
            {
                Help        = true,
                ShowToolTip = true
            };

            using (var sw = new StreamWriter(@"c:\tmp\test.txt"))
            {
                foreach (var file in dirInfo)
                {
                    var testName = Path.GetFileNameWithoutExtension(file);
                    testName = testName.Replace('(', '_');
                    testName = testName.Replace(')', '_');
                    testName = testName.Replace("_", "");
                    testName = testName.Replace(" ", "");
                    sw.WriteLine("        [Fact]");
                    sw.WriteLine($"        public void Test{testName}()");
                    sw.WriteLine("        {");
                    sw.WriteLine($"            CheckSudoku(new[]");
                    sw.WriteLine("              {");
                    var newSudoku = SudokuLoadSaveExtensions.Load(file);
                    newSudoku.UpdatePossible();
                    var lines = newSudoku.SmartPrint();
                    foreach (var line in lines)
                    {
                        sw.WriteLine($"                \"{line}\",");
                    }

                    sw.WriteLine("              },");
                    sw.WriteLine("              new ExpectResult[]");
                    sw.WriteLine("              {");

                    string ToButtonToolTip(string buttonToolTip)
                    {
                        var part = buttonToolTip.Split('\n').Select(
                            p =>
                            {
                                string role;

                                // 4: 1 only in 3*3 (B1)
                                var regExB1      = new Regex(@"(\d): (\d) only in (col|row|3\*3).*\(B1\)");
                                var regExB1Match = regExB1.Match(p);

                                // 6: 8,9: in 3*3-index: 1,2,7 (B2)
                                var regExB2      = new Regex(@"(\d): (.*): in (col|row|3\*3).*: (.*) \(B2\)");
                                var regExB2Match = regExB2.Match(p);

                                // 6: 6,8,9: in col-index: 1,3,4(B2+)
                                var regExB2P      = new Regex(@"(\d): (.*): in (col|row|3\*3).*: (.*) \(B2\+\)");
                                var regExB2PMatch = regExB2P.Match(p);

                                // 1: only in col-index: 7,8,9 (B3)
                                var regExB3      = new Regex(@"(\d): only in (col|row|3\*3)-index: (.*) \(B3\)");
                                var regExB3Match = regExB3.Match(p);

                                if (regExB1Match.Success)
                                {
                                    role = new[] { "B13", "B1R", "B1C", "NC" }[(int)GetRoleType(regExB1Match.Groups[3].Value)];
                                    p    = $"{role}:{regExB1Match.Groups[1]}:{regExB1Match.Groups[2]}";
                                }
                                else if (regExB2Match.Success)
                                {
                                    role = new[] { "B23", "B2R", "B2C", "NC" }[(int)GetRoleType(regExB2Match.Groups[3].Value)];
                                    p    = $"{role}:{regExB2Match.Groups[1]}:{regExB2Match.Groups[2]}:{regExB2Match.Groups[4]}";
                                }
                                else if (regExB2PMatch.Success)
                                {
                                    role = new[] { "B2P3", "B2PR", "B2PC", "NC" }[(int)GetRoleType(regExB2PMatch.Groups[3].Value)];
                                    p    = $"{role}:{regExB2PMatch.Groups[1]}:{regExB2PMatch.Groups[2]}:{regExB2PMatch.Groups[4]}";
                                }
                                else if (regExB3Match.Success)
                                {
                                    role = new[] { "B33", "B3R", "B3C", "NC" }[(int)GetRoleType(regExB3Match.Groups[2].Value)];
                                    p    = $"{role}:{regExB3Match.Groups[1]}:{regExB3Match.Groups[3]}";
                                }

                                return p;
                            });

                        return string.Join(@"\n", part);
                    }

                    bool ShouldCheck(int x, int y)
                    {
                        if (newSudoku.Get(x, y) != 0)
                        {
                            return false;
                        }

                        var def = newSudoku.GetDef(x, y);

                        return def.PossibleString().Length <= 1 || def.PossibleString() != def.ToButtonToolTip(opt);
                    }

                    for (int y = 0; y < 9; y++)
                    {
                        for (int x = 0; x < 9; x++)
                        {
                            if (ShouldCheck(x, y))
                            {
                                sw.WriteLine($"                new ({x}, {y}, \"{newSudoku.GetDef(x, y).PossibleString()}\", \"{ToButtonToolTip(newSudoku.GetDef(x, y).ToButtonToolTip(opt))}\"),");
                            }
                        }
                    }

                    sw.WriteLine("              }");
                    sw.WriteLine("            );");
                    sw.WriteLine("        }");
                }
            }
        }
    }
}