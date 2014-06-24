using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sudoku.Server.WinForm.Start
{
    public partial class ServerForm : Form
    {
        SudokuServer _server = new SudokuServer();

        public ServerForm()
        {
            InitializeComponent();
            _stop.Enabled = false;
        }

        private void _stop_Click(object sender, EventArgs e)
        {
            _server.Stop();
            _stop.Enabled = false;
            _start.Enabled = true;
        }

        private void _start_Click(object sender, EventArgs e)
        {
            _server.Start();
            _stop.Enabled = true;
            _start.Enabled = false;
        }
    }
}
