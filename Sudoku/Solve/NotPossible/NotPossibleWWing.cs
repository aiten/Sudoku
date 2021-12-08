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

    public class NotPossibleWWing : NotPossibleBase
    {
        public NotPossibleWWing()
        {
            RoleName = "B9";
        }

        public override string SerializeTo()
        {
            return $"{RoleName}:{ForNo}:{PairField1.ToCellString()}:{PairField2.ToCellString()}:{StrongLinkNo}:{StrongLinkField1.ToCellString()}:{StrongLinkField2.ToCellString()}";
        }

        protected override void SerializeFrom(string[] serialized)
        {
            ForNo            = int.Parse(serialized[1]);
            PairField1       = serialized[2].FromCellString();
            PairField2       = serialized[3].FromCellString();
            StrongLinkNo     = int.Parse(serialized[4]);
            StrongLinkField1 = serialized[5].FromCellString();
            StrongLinkField2 = serialized[6].FromCellString();
        }

        public override IEnumerable<(int Row, int Col, int Level)> Explain(Sudoku sudoku, int myRow, int myCol)
        {
            var expl = new List<(int Row, int Col, int Level)>();

            expl.Add((PairField1.Row, PairField1.Col, 4));
            expl.Add((PairField2.Row, PairField2.Col, 4));

            expl.Add((StrongLinkField1.Row, StrongLinkField1.Col, 3));
            expl.Add((StrongLinkField2.Row, StrongLinkField2.Col, 3));
/*
                        var intersect = IntersectExplain(sudoku, myRow, myCol);

                        foreach (var field in intersect)
                        {
                            expl.Add((field.Row, field.Col, 5));
                        }
            */
            return expl;
        }

        public override string ToString()
        {
            return $"{ForNo}: W-Wing with {PairField1.ToCellStringUser()}:{PairField2.ToCellStringUser()} ({RoleName})";
        }

        public (int Row, int Col) PairField1       { get; set; }
        public (int Row, int Col) PairField2       { get; set; }
        public (int Row, int Col) StrongLinkField1 { get; set; }
        public (int Row, int Col) StrongLinkField2 { get; set; }
        public int StrongLinkNo { get; set; }
    }
}