using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SudokuGui
{
    public partial class EnterFieldForm : Form
    {
        public EnterFieldForm()
        {
            InitializeComponent();
        }

        public string UserNote
        {
            get { return _UserNote.Text;  }
            set { _UserNote.Text = value; }

        }
        public int No
        {
            get { return _No.SelectedIndex; }
            set { _No.SelectedIndex = value; }
        }
     }
}