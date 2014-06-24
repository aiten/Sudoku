using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku.Solve;

namespace Sudoku.Test
{
    [TestClass]
    public class SudokuUnitTest
    {
        [TestMethod]
        public void SimpleTest()
        {
            Sudoku.Solve.Sudoku s = new Sudoku.Solve.Sudoku();
            s.UpdatePossible();

            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                    Assert.AreEqual<int>(9, s.GetDef(x, y).MainRulePossibleCount());
        }

        [TestMethod]
        public void SimplePossibleTest()
        {
            Sudoku.Solve.Sudoku s = new Sudoku.Solve.Sudoku();
            Sudoku.Solve.Sudoku.SudokuOptions opt = new Sudoku.Solve.Sudoku.SudokuOptions();
            opt.Help = true;
            opt.ShowToolTip = true;

            s.UpdatePossible();

            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                {
                    Assert.AreEqual<int>(9, s.GetDef(x, y).PossibleCount());
                    Assert.AreEqual<string>("1,2,3,4,5,6,7,8,9", s.GetDef(x, y).PossibleString());

                    Assert.AreEqual<int>(9, s.GetDef(x, y).MainRulePossibleCount());

                    Assert.AreEqual<string>("1,2,3,4,5,6,7,8,9", s.GetDef(x, y).ToButtonString(opt));
                }
        }
    }
}
