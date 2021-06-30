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

    public class NotPossibleFish : NotPossibleBase
    {
        protected NotPossibleFish()
        {
        }

        public string FishName { get; protected set; }

        public override string SerializeTo()
        {
            return $"{RoleName}:{ForNo}:{Orientation.ToChar()}:{BecauseRow.ToRowList()}:{BecauseCol.ToRowList()}";
        }

        protected override void SerializeFrom(string[] serialized)
        {
            ForNo       = int.Parse(serialized[1]);
            Orientation = serialized[2].ToOrientation();
            BecauseRow  = serialized[3].FromRowList();
            BecauseCol  = serialized[4].FromRowList();
        }

        public override IEnumerable<(int row, int col, int level)> Explain(Sudoku sudoku, int myRow, int myCol)
        {
            var expl = new List<(int row, int col, int level)>();

            foreach (var row in BecauseRow)
            {
/*
                foreach (var col in LoopExtensions.Cols)
                {
                    if (!BecauseCol.Contains(col))
                    {
                        var rowCol = ConvertTo(row, col);
                        expl.Add((rowCol.row, rowCol.col, 1));
                    }
                }
*/
                foreach (var col in BecauseCol)
                {
                    var rowCol = ConvertTo(row, col);
                    if (sudoku.GetDef(rowCol.row, rowCol.col).IsEmpty)
                    {
                        expl.Add((rowCol.row, rowCol.col, 3));
                    }
                }
            }

            foreach (var col in BecauseCol)
            {
                foreach (var row in LoopExtensions.Rows)
                {
                    if (!BecauseRow.Contains(row))
                    {
                        var rowCol = ConvertTo(row, col);
                        var def    = sudoku.GetDef(rowCol.row, rowCol.col);
                        var isRole = def.IsPossibleMainRule(ForNo) && def.IsNotPossible(ForNo) ? 5 : 2;
                        expl.Add((rowCol.row, rowCol.col, isRole));
                    }
                }
            }

            return expl;
        }

        public override string ToString()
        {
            return $"{ForNo}: {FishName} {Orientation.ToOrientationDesc()} {BecauseRow.ToUserRowList()} with {Orientation.ToOppositeOrientation().ToOrientationDesc()} {BecauseCol.ToUserRowList()}";
        }

        public IEnumerable<int> BecauseRow { get; set; }
        public IEnumerable<int> BecauseCol { get; set; }
    }
}