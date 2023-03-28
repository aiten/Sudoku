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

using System.Collections.Generic;
using System.IO;
using System.Linq;

using FluentAssertions;

using Sudoku.Solve;
using Sudoku.Solve.NotPossible;
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

        var asCsvList = new List<string> { "Id;Comment;Content;LastStored" };

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
                var lines = newSudoku.SmartPrint(" ");

                asCsvList.Add($"{asCsvList.Count};{testName};{string.Join('|', newSudoku.SmartPrint(string.Empty))};2022/10/20 00:00:00");

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
                            var notPossible = NotPossibleBase.Create(p);

                            if (notPossible != null)
                            {
                                notPossible.ToString().Should().NotBeEmpty();
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

                    return def.PossibleString().Length <= 1 || def.PossibleString() != def.GetFullFiledInfo();
                }

                for (int y = 0; y < 9; y++)
                {
                    for (int x = 0; x < 9; x++)
                    {
                        if (ShouldCheck(x, y))
                        {
                            sw.WriteLine($"                new ({x}, {y}, \"{newSudoku.GetDef(x, y).PossibleString()}\", \"{ToButtonToolTip(newSudoku.GetDef(x, y).GetFullFiledInfo())}\"),");
                        }
                    }
                }

                sw.WriteLine("              }");
                sw.WriteLine("            );");
                sw.WriteLine("        }");
            }
        }

        File.WriteAllLines(@"c:\tmp\test.csv", asCsvList);
    }
}