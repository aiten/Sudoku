using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SudokuSolve;

namespace SudokuGui
{
    public partial class OptionForm : Form
    {
        public OptionForm()
        {
            InitializeComponent();
        }

        private SudokuOptions _Options;
        public SudokuOptions Options
        {
            get 
            {
                _Options.help = _Help.Checked;
                _Options.showooltip = _ShowToolTip.Checked;

                return _Options; 
            }
            set 
            { 
                _Options = value;
                _Help.Checked = _Options.help;
                _ShowToolTip.Checked = _Options.showooltip;
            }
        }
    }
}