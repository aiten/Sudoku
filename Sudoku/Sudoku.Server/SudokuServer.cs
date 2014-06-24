using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Sudoku.Logic;
using Framework.Server.Logic;
using Framework.Server.WcfServer;

namespace Sudoku.Server
{
    public class SudokuServer : WcfServer
    {
        public SudokuServer()
        {
            Port = 32224;
            Logic.EntityController.GlobalInitialize();
        }

        public override void RegisterAssemblies()
        {
            RegisterAssembly(typeof(Sudoku.Service.SudokuService).Assembly);
        }
    }
}
