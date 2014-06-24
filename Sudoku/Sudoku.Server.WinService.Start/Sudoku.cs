using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Server.WinService.Start
{
    public partial class SudokuService : ServiceBase
    {
        SudokuServer _server = new SudokuServer();

        public SudokuService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _server.Start();
        }

        protected override void OnStop()
        {
            _server.Stop();
        }
    }
}
