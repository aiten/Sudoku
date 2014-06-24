using System.Windows.Forms;

namespace Sudoku.Forms
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