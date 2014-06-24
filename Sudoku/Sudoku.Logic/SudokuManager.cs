using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Sudoku.Repository;
using Framework.Server.Logic;
using System.Runtime.InteropServices;

namespace Sudoku.Logic
{
    public class SudokuManager
    {
        public void DoSomething()
        {
        }

        public BO.Sudoku GetRandomSudoku()
        {
            return new BO.Sudoku();
        }
    }
}
