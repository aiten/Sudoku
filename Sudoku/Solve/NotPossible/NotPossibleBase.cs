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

    public abstract class NotPossibleBase
    {
        public NotPossibleBase()
        {
        }

        public string RoleName { get; protected set; }
        public int    ForNo    { get; set; }

        public abstract    string SerializeTo();
        protected abstract void   SerializeFrom(string[] serialized);

        public static NotPossibleBase Create(string serialized)
        {
            var val = serialized.Split(':');

            if (serialized.Length == 0)
            {
                return null;
            }

            NotPossibleBase Serialize(NotPossibleBase notPossible, string[] val)
            {
                notPossible.SerializeFrom(val);
                return notPossible;
            }

            switch (val[0])
            {
                case "B1":  return Serialize(new NotPossibleBlockade1(),       val);
                case "B2P": return Serialize(new NotPossibleBlockade2SubSet(), val);
                case "B2":  return Serialize(new NotPossibleBlockade2(),       val);
                case "B3":  return Serialize(new NotPossibleBlockade3(),       val);
                case "B4":  return Serialize(new NotPossibleXWing(),           val);
                case "B5":  return Serialize(new NotPossibleSwordfish(),       val);
                case "B6":  return Serialize(new NotPossibleJellyfish(),       val);
                default:    return null;
            }
        }

        public Orientation Orientation { get; set; }
    }
}