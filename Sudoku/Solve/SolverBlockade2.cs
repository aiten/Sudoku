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
    using global::Sudoku.Solve.Tools;

    public class SolverBlockade2 : SolverBase
    {
        public SolverBlockade2(Sudoku sudoku) : base(sudoku)
        {
        }

        private readonly bool[] _foundIdx1 = new bool[9];

        public override bool Solve()
        {
            var isCol = Solve(Orientation.Column);
            var isRow = Solve(Orientation.Row);
            var isX3  = Solve(Orientation.X3);

            return isCol || isRow || isX3;
        }

        public override bool Solve(Orientation orientation)
        {
            return UpdatePossibleBlockade2(ToGetDef(orientation), ToChar(orientation)) > 0;
        }

        public int UpdatePossibleBlockade2(Sudoku.GetSudokuField getDef, char rowcol3)
        {
            var changeCount = 0;

            ForEachEmpty(getDef, (def, row, col) =>
            {
                _foundIdx1.Init(false);

                _foundIdx1[col] = true;
                var foundCount = 1;

                for (var t = 0; t < 9; t++)
                {
                    if (t != col)
                    {
                        var def2 = getDef(row, t);
                        if (def2.IsEmpty)
                        {
                            if (def.IsSubSetPossible(def2))
                            {
                                _foundIdx1[t] = true;
                                foundCount++;
                            }
                        }
                    }
                }

                if (foundCount == def.PossibleCount())
                {
                    string reasonPossible = null;
                    string reasonIndex    = null;
                    for (var t = 0; t < 9; t++)
                    {
                        if (_foundIdx1[t])
                        {
                            var def2 = getDef(row, t);
                            if (def2.IsEmpty)
                            {
                                if (string.IsNullOrEmpty(reasonPossible))
                                {
                                    reasonPossible = def2.PossibleString();
                                }

                                reasonIndex = reasonIndex.Add(',', t + 1);
                            }
                        }
                    }

                    for (var t = 0; t < 9; t++)
                    {
                        if (!_foundIdx1[t])
                        {
                            var def2 = getDef(row, t);
                            if (def2.IsEmpty)
                            {
                                for (var no = 1; no <= 9; no++)
                                {
                                    if (def.IsPossible(no) && def2.IsPossible(no))
                                    {
                                        changeCount++;
                                        def2.SetNotPossibleBlockade2(no, rowcol3, reasonPossible, reasonIndex);
                                    }
                                }
                            }
                        }
                    }
                }
            });

            return changeCount;
        }
    }
}