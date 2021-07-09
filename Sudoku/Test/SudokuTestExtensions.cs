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
    using System.Linq;

    using Sudoku.Solve;

    public static class SudokuExtensions
    {
        private static string ToButtonStringMainRuleOnly(this SudokuField field)
        {
            if (field.HasNo)
            {
                return field.No.ToString();
            }

            return string.Join(',', field.GetPossibleMainRuleNos());
        }

        public static string ToButtonString(this SudokuField field)
        {
            if (field.HasNo)
            {
                return field.No.ToString();
            }

            var possible    = string.Join(',', field.GetPossibleNos());
            var notPossible = string.Join(',', field.GetNotPossibleNos());

            if (string.IsNullOrEmpty(notPossible))
            {
                return possible;
            }

            return possible + " - " + notPossible;
        }

        public static string GetFullFiledInfo(this SudokuField field)
        {
            var reason = field.ToButtonString();
            if (field.IsEmpty)
            {
                var notPossibleExplanation = string.Join('\n', field.GetNotPossible().Select(notPossible => notPossible.SerializeTo()));

                if (!string.IsNullOrEmpty(notPossibleExplanation))
                {
                    reason += "\n";
                    reason += notPossibleExplanation;
                }
            }

            return reason;
        }

        public static string ToButtonToolTip(this SudokuField field)
        {
            var reason = field.ToButtonString();
            if (field.IsEmpty)
            {
                var notPossibleExplanation = string.Join('\n', field.GetNotPossible().Select(notPossible => notPossible.ToString()));

                if (!string.IsNullOrEmpty(notPossibleExplanation))
                {
                    reason += "\n";
                    reason += notPossibleExplanation;
                }
            }

            return reason;
        }
    };
}