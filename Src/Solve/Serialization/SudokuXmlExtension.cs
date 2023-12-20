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

namespace Sudoku.Solve.Serialization;

public static class SudokuXmlExtension
{
    public static SudokuXml ToSudokuXml(this Solve.Sudoku sudoku)
    {
        var sudokuXml = new SudokuXml();
        sudokuXml.Version = 1;

        var colUserNote = new string[9];
        var rowUserNote = new string[9];

        for (int idx = 0; idx < 9; idx++)
        {
            rowUserNote[idx] = sudoku.GetUserNoteRow(idx);
            colUserNote[idx] = sudoku.GetUserNoteCol(idx);
        }

        sudokuXml.XmlUserNoteRow = rowUserNote;
        sudokuXml.XmlUserNoteCol = colUserNote;

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                var pi3X3  = typeof(SudokuXml).GetProperty($"XmlSudoku{row}{col}")!;
                var xml3x3 = (Sudoku3X3Xml)pi3X3.GetValue(sudokuXml)!;

                for (int iRow = 0; iRow < 3; iRow++)
                {
                    for (int iCol = 0; iCol < 3; iCol++)
                    {
                        var piElem  = typeof(Sudoku3X3Xml).GetProperty($"XmlSudoku{iRow}{iCol}")!;
                        var xmlElem = (SudokuElementXml)piElem.GetValue(xml3x3)!;
                        var def     = sudoku.GetDef(row * 3 + iRow, col * 3 + iCol);
                        xmlElem.XmlNo       = def.No;
                        xmlElem.XmlUserNote = def.UserNote;
                    }
                }
            }
        }

        return sudokuXml;
    }

    public static Sudoku ToSudoku(this SudokuXml sudokuXml)
    {
        var sudoku = new Sudoku();

        for (int idx = 0; idx < 9 && idx < sudokuXml.XmlUserNoteRow.Length; idx++)
        {
            sudoku.SetUserNoteRow(idx, sudokuXml.XmlUserNoteRow[idx]);
        }

        for (int idx = 0; idx < 9 && idx < sudokuXml.XmlUserNoteCol.Length; idx++)
        {
            sudoku.SetUserNoteCol(idx, sudokuXml.XmlUserNoteCol[idx]);
        }

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                var pi3X3  = typeof(SudokuXml).GetProperty($"XmlSudoku{row}{col}")!;
                var xml3x3 = (Sudoku3X3Xml)pi3X3.GetValue(sudokuXml)!;

                for (int iRow = 0; iRow < 3; iRow++)
                {
                    for (int iCol = 0; iCol < 3; iCol++)
                    {
                        var piElem  = typeof(Sudoku3X3Xml).GetProperty($"XmlSudoku{iRow}{iCol}")!;
                        var xmlElem = (SudokuElementXml)piElem.GetValue(xml3x3)!;

                        var myRow = row * 3 + iRow;
                        var myCol = col * 3 + iCol;

                        if (!string.IsNullOrEmpty(xmlElem.XmlUserNote))
                        {
                            sudoku.SetUserNote(myRow, myCol, xmlElem.XmlUserNote);
                        }

                        if (xmlElem.XmlNo > 0)
                        {
                            sudoku.Set(myRow, myCol, xmlElem.XmlNo);
                        }
                    }
                }
            }
        }

        if (sudokuXml.Version < 1)
        {
        }

        sudoku.ClearUndo();
        sudoku.Modified = false;
        return sudoku;
    }
}