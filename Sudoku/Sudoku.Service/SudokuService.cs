using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sodoku.Service.Contracts;
using System.ServiceModel;
using Framework.Server.Logic;
using System.Runtime.InteropServices;
using Sudoku.Logic;
using Sudoku.Service.BO.Tools;

namespace Sudoku.Service
{
    [Service]
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, UseSynchronizationContext = false, IncludeExceptionDetailInFaults = true)]
    public class SudokuService : ISudokuService
    {
        public void DoSomething()
        {
            new Sudoku.Logic.SudokuManager().DoSomething();
        }


        public Sudoku.Service.Contracts.DTO.Sudoku GetRandomSudoku()
        {
            return new Sudoku.Logic.SudokuManager().GetRandomSudoku().ToDTO();
        }
    }
}
