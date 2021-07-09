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

namespace Sudoku.Solve.NotPossible
{
    using System.Collections.Generic;

    public class NotPossibleXYWing : NotPossibleBase
    {
        public NotPossibleXYWing()
        {
            RoleName = "B7";
        }

        public override string SerializeTo()
        {
            return $"{RoleName}:{ForNo}:{Pivot.ToCellString()}:{Pincer1.ToCellString()}:{Pincer2.ToCellString()}";
        }

        protected override void SerializeFrom(string[] serialized)
        {
            ForNo   = int.Parse(serialized[1]);
            Pivot   = serialized[2].FromCellString();
            Pincer1 = serialized[3].FromCellString();
            Pincer2 = serialized[4].FromCellString();
        }

        public override IEnumerable<(int Row, int Col, int Level)> Explain(Sudoku sudoku, int myRow, int myCol)
        {
            var expl = new List<(int Row, int Col, int Level)>();

            expl.Add((Pivot.Row, Pivot.Col, 3));
            expl.Add((Pincer1.Row, Pincer1.Col, 4));
            expl.Add((Pincer2.Row, Pincer2.Col, 4));

            return expl;
        }

        public override string ToString()
        {
            return $"{ForNo}: XY-Wing in {Pivot.ToCellStringUser()} with {Pincer1.ToCellStringUser()}:{Pincer2.ToCellStringUser()} (B7)";
        }

        public (int Row, int Col) Pivot   { get; set; }
        public (int Row, int Col) Pincer1 { get; set; }
        public (int Row, int Col) Pincer2 { get; set; }
    }
}