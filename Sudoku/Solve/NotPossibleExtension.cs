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
    public static class NotPossibleExtension
    {
        public static void SetNotPossibleBlockade1(this SudokuField def, int forNo, char rowcol3, int becauseNo)
        {
            def.SetNotPossible(forNo, $"B1:{forNo}:{rowcol3}:{becauseNo}");
        }

        public static void SetNotPossibleBlockade2(this SudokuField def, int forNo, char rowcol3, string reasonPossible, string reasonIndex)
        {
            def.SetNotPossible(forNo, $"B2:{forNo}:{rowcol3}:{reasonPossible}:{reasonIndex}");
        }

        public static void SetNotPossibleBlockade2P(this SudokuField def, int forNo, char rowcol3, string reasonPossible, string reasonIndex)
        {
            def.SetNotPossible(forNo, $"B2P:{forNo}:{rowcol3}:{reasonPossible}:{reasonIndex}");
        }

        public static void SetNotPossibleBlockade3(this SudokuField def, int forNo, char rowcol3, string becauseNo)
        {
            def.SetNotPossible(forNo, $"B3:{forNo}:{rowcol3}:{becauseNo}");
        }

        private static string ToOrientation(string rowCol3)
        {
            switch (rowCol3)
            {
                case "R": return "row";
                case "C": return "col";
                case "X": return "3*3";
                default:  return rowCol3;
            }
        }

        public static string GetNotPossibleReason(this string notPossibleReason)
        {
            var val = notPossibleReason.Split(':');

            switch (val[0])
            {
                case "B1":  return $"{val[1]}: {val[3]} only in {ToOrientation(val[2])} (B1)";
                case "B2P": return $"{val[1]}: {val[3]}: in {ToOrientation(val[2])}-index: {val[4]} (B2+)";
                case "B2":  return $"{val[1]}: {val[3]}: in {ToOrientation(val[2])}-index: {val[4]} (B2)";
                case "B3":  return $"{val[1]}: only in {ToOrientation(val[2])}-index: {val[3]} (B3)";
            }

            return notPossibleReason;
        }
    }
}