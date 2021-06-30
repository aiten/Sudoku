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
    using System.Linq;

    using global::Sudoku.Solve.Tools;

    public class NotPossibleBlockade2 : NotPossibleBase
    {
        public NotPossibleBlockade2()
        {
            RoleName = "B2";
        }

        public override string SerializeTo()
        {
            return $"{RoleName}:{ForNo}:{Orientation.ToChar()}:{BecauseNos.ToNoList()}:{BecauseIdx.ToRowList()}";
        }

        protected override void SerializeFrom(string[] serialized)
        {
            ForNo       = int.Parse(serialized[1]);
            Orientation = serialized[2].ToOrientation();
            BecauseNos  = serialized[3].FromNoList();
            BecauseIdx  = serialized[4].FromRowList();
        }

        public override IEnumerable<(int row, int col, int level)> Explain(Sudoku sudoku, int myRow, int myCol)
        {
            var expl = new List<(int row, int col, int level)>();
            var pos  = Orientation.ConvertFrom(myRow, myCol);

            foreach (var col in BecauseIdx)
            {
                var rowCol = ConvertTo(pos.row, col);
                expl.Add((rowCol.row, rowCol.col, 3));
            }

            return expl;
        }

        public override string ToString()
        {
            return $"{ForNo}: {BecauseNos.ToUserNoList()}: in {Orientation.ToOrientationDesc()}-index: {BecauseIdx.ToUserRowList()} (B2)";
        }

        public IEnumerable<int> BecauseNos { get; set; }
        public IEnumerable<int> BecauseIdx { get; set; }
    }
}