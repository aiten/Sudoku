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

using System.IO;

using Sudoku.Solve;

using FluentAssertions;

using Sudoku.Solve.Serialization;

using Xunit;

public class SudokuLoadSaveTest : SudokuBaseUnitTest
{
    [Fact]
    public void LoadSaveTest()
    {
        var lines = new[]
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
        };

        var save = lines.CreateSudoku();

        for (var x = 0; x < 9; x++)
        {
            save.SetUserNoteCol(x, $"col{x}");
            save.SetUserNoteRow(x, $"row{x}");
            for (var y = 0; y < 9; y++)
            {
                save.SetUserNote(x, y, $"rowcol{x}{y}");
            }
        }

        var filename = Path.GetTempFileName();

        save.SaveXml(filename).Should().BeTrue();

        var loaded = SudokuLoadSaveExtensions.Load(filename);

        File.Delete(filename);

        for (var x = 0; x < 9; x++)
        {
            loaded.GetUserNoteCol(x).Should().Be(save.GetUserNoteCol(x));
            loaded.GetUserNoteRow(x).Should().Be(save.GetUserNoteRow(x));
            for (var y = 0; y < 9; y++)
            {
                loaded.Get(x, y).Should().Be(save.Get(x, y));
                loaded.GetDef(x, y).UserNote.Should().Be(save.GetDef(x, y).UserNote);
            }
        }
    }
}