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
    using System.Linq;

    public class SolverSwordfish : SolverFishBase
    {
        public SolverSwordfish(Sudoku sudoku) : base(sudoku)
        {
        }

        protected override void SetNotPossible(SudokuField def, int forNo, char rowcol3, string becauseRow, string becauseCol)
        {
            def.SetNotPossibleSwordfish(forNo, rowcol3, becauseRow, becauseCol);
        }

        public override bool Solve()
        {
            var isCol = Solve(Orientation.Column);
            var isRow = Solve(Orientation.Row);

            return isCol || isRow;
        }

        public override bool Solve(Orientation orientation)
        {
            return UpdateFish(ToGetDef(orientation), 3, ToChar(orientation)) > 0;
        }
    }
}