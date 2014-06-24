using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sudoku.Solve;

namespace Sudoku.Forms
{
    public partial class OptionForm : Form
    {
        public OptionForm()
        {
            InitializeComponent();
        }

        private Sudoku.Solve.Sudoku.SudokuOptions _options;
        public Sudoku.Solve.Sudoku.SudokuOptions Options
        {
            get 
            {
                _options.Help = _Help.Checked;
                _options.ShowToolTip = _ShowToolTip.Checked;

                return _options; 
            }
            set 
            { 
                _options = value;
                _Help.Checked = _options.Help;
                _ShowToolTip.Checked = _options.ShowToolTip;
            }
        }
    }
}