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

namespace Sudoku.Forms;

using System.Drawing;

using Sudoku.Solve;

public static class SudokuFormExtensions
{
    public static Color ToButtonColor(this SudokuField field, SudokuOptions opt)
    {
        if (field.HasNo)
            return Color.Green;

        if (opt.Help)
        {
            switch (field.PossibleCount())
            {
                default: return Color.Gray;
                case 0:  return Color.Red;
                case 1:  return Color.LightGray;
            }
        }

        return Color.Gray;
    }

    private static string ToButtonStringMainRuleOnly(this SudokuField field)
    {
        if (field.HasNo)
        {
            return field.No.ToString();
        }

        return string.Join(',', field.GetPossibleMainRuleNos());
    }

    public static string ToButtonString(this SudokuField field, SudokuOptions opt)
    {
        if (field.HasNo)
        {
            return field.No.ToString();
        }

        if (opt.Help)
        {
            var possible    = string.Join(',', field.GetPossibleNos());
            var notPossible = string.Join(',', field.GetNotPossibleNos());

            if (string.IsNullOrEmpty(notPossible))
            {
                return possible;
            }

            return possible + " - " + notPossible;
        }

        return field.UserNote;
    }

    public static string ToButtonToolTip(this SudokuField field, SudokuOptions opt)
    {
        if (!opt.ShowToolTip)
            return "";

        if (opt.Help)
        {
            var reason = field.ToButtonString(opt);
            if (field.IsEmpty)
            {
                var notPossibleExplanation = field.NotPossibleExplanation();

                if (!string.IsNullOrEmpty(notPossibleExplanation))
                {
                    reason += "\n";
                    reason += notPossibleExplanation;
                }
            }

            return reason;
        }

        return field.ToButtonStringMainRuleOnly();
    }
};