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

namespace Sudoku.Solve
{
    public static class SudokuMirrorRotateExtensions
    {
        public static Sudoku Clone(this Sudoku sudoku)
        {
            var newSoduku = new Solve.Sudoku();
            for (var row = 0; row < 9; row++)
            {
                for (var col = 0; col < 9; col++)
                {
                    var no = sudoku.Get(row, col);
                    newSoduku.Set(row, col, no);
                }
            }

            newSoduku.ClearUndo();
            return newSoduku;
        }

        public static Sudoku Rotate(this Sudoku sudoku)
        {
            var newSudoku = new Sudoku();

            for (var row = 0; row < 9; row++)
            {
                newSudoku.SetUserNoteCol(row, sudoku.GetUserNoteRow(row));
                newSudoku.SetUserNoteRow(row, sudoku.GetUserNoteCol(row));
                for (var col = 0; col < 9; col++)
                {
                    int myRow = 8 - col;
                    int myCol = row;

                    var def = sudoku.GetDef(myRow, myCol);
                    newSudoku.SetUserNote(row, col, def.UserNote);
                    if (def.HasNo)
                        newSudoku.Set(row, col, def.No);
                }
            }

            newSudoku.ClearUndo();
            return newSudoku;
        }

        public static Sudoku Mirror(this Sudoku sudoku)
        {
            var newSudoku = new Sudoku();

            for (var row = 0; row < 9; row++)
            {
                newSudoku.SetUserNoteCol(row, sudoku.GetUserNoteRow(row));
                newSudoku.SetUserNoteRow(row, sudoku.GetUserNoteCol(row));
                for (var col = 0; col < 9; col++)
                {
                    var myx = row;
                    var myy = 8 - col;

                    var def = sudoku.GetDef(myx, myy);
                    newSudoku.SetUserNote(row, col, def.UserNote);
                    if (def.No > 0)
                        newSudoku.Set(row, col, def.No);
                }
            }

            newSudoku.ClearUndo();
            return newSudoku;
        }
    }
}