using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Threading;
using System.Media;
using SudokuSolve;

namespace SudokuGui
{
    public partial class MainForm : Form
    {
        #region Constructor / Initialisation

        public MainForm(string Filename)
        {
            _Options.help = true;
            _Options.showooltip = true;

            _FileName = Filename;

            InitializeComponent();
            _Buttons[0, 0] = _11;
            _Buttons[0, 1] = _12;
            _Buttons[0, 2] = _13;
            _Buttons[0, 3] = _14;
            _Buttons[0, 4] = _15;
            _Buttons[0, 5] = _16;
            _Buttons[0, 6] = _17;
            _Buttons[0, 7] = _18;
            _Buttons[0, 8] = _19;
            _Buttons[1, 0] = _21;
            _Buttons[1, 1] = _22;
            _Buttons[1, 2] = _23;
            _Buttons[1, 3] = _24;
            _Buttons[1, 4] = _25;
            _Buttons[1, 5] = _26;
            _Buttons[1, 6] = _27;
            _Buttons[1, 7] = _28;
            _Buttons[1, 8] = _29;
            _Buttons[2, 0] = _31;
            _Buttons[2, 1] = _32;
            _Buttons[2, 2] = _33;
            _Buttons[2, 3] = _34;
            _Buttons[2, 4] = _35;
            _Buttons[2, 5] = _36;
            _Buttons[2, 6] = _37;
            _Buttons[2, 7] = _38;
            _Buttons[2, 8] = _39;
            _Buttons[3, 0] = _41;
            _Buttons[3, 1] = _42;
            _Buttons[3, 2] = _43;
            _Buttons[3, 3] = _44;
            _Buttons[3, 4] = _45;
            _Buttons[3, 5] = _46;
            _Buttons[3, 6] = _47;
            _Buttons[3, 7] = _48;
            _Buttons[3, 8] = _49;
            _Buttons[4, 0] = _51;
            _Buttons[4, 1] = _52;
            _Buttons[4, 2] = _53;
            _Buttons[4, 3] = _54;
            _Buttons[4, 4] = _55;
            _Buttons[4, 5] = _56;
            _Buttons[4, 6] = _57;
            _Buttons[4, 7] = _58;
            _Buttons[4, 8] = _59;
            _Buttons[5, 0] = _61;
            _Buttons[5, 1] = _62;
            _Buttons[5, 2] = _63;
            _Buttons[5, 3] = _64;
            _Buttons[5, 4] = _65;
            _Buttons[5, 5] = _66;
            _Buttons[5, 6] = _67;
            _Buttons[5, 7] = _68;
            _Buttons[5, 8] = _69;
            _Buttons[6, 0] = _71;
            _Buttons[6, 1] = _72;
            _Buttons[6, 2] = _73;
            _Buttons[6, 3] = _74;
            _Buttons[6, 4] = _75;
            _Buttons[6, 5] = _76;
            _Buttons[6, 6] = _77;
            _Buttons[6, 7] = _78;
            _Buttons[6, 8] = _79;
            _Buttons[7, 0] = _81;
            _Buttons[7, 1] = _82;
            _Buttons[7, 2] = _83;
            _Buttons[7, 3] = _84;
            _Buttons[7, 4] = _85;
            _Buttons[7, 5] = _86;
            _Buttons[7, 6] = _87;
            _Buttons[7, 7] = _88;
            _Buttons[7, 8] = _89;
            _Buttons[8, 0] = _91;
            _Buttons[8, 1] = _92;
            _Buttons[8, 2] = _93;
            _Buttons[8, 3] = _94;
            _Buttons[8, 4] = _95;
            _Buttons[8, 5] = _96;
            _Buttons[8, 6] = _97;
            _Buttons[8, 7] = _98;
            _Buttons[8, 8] = _99;

            _UserNoteRow[0] = _IR1;
            _UserNoteRow[1] = _IR2;
            _UserNoteRow[2] = _IR3;
            _UserNoteRow[3] = _IR4;
            _UserNoteRow[4] = _IR5;
            _UserNoteRow[5] = _IR6;
            _UserNoteRow[6] = _IR7;
            _UserNoteRow[7] = _IR8;
            _UserNoteRow[8] = _IR9;

            _UserNoteCol[0] = _IC1;
            _UserNoteCol[1] = _IC2;
            _UserNoteCol[2] = _IC3;
            _UserNoteCol[3] = _IC4;
            _UserNoteCol[4] = _IC5;
            _UserNoteCol[5] = _IC6;
            _UserNoteCol[6] = _IC7;
            _UserNoteCol[7] = _IC8;
            _UserNoteCol[8] = _IC9;

            int x, y;
            for (x = 0; x < 9; x++)
            {
                _UserNoteCol[x].TextChanged += new System.EventHandler(this._UserNote_ChangedCol);
                _UserNoteRow[x].TextChanged += new System.EventHandler(this._UserNote_ChangedRow);

                for (y = 0; y < 9; y++)
                {
                    _Buttons[x, y].Click   += new System.EventHandler(this._Button_Click);
                    _Buttons[x, y].KeyDown += new System.Windows.Forms.KeyEventHandler(this._Button_KeyDown);
                }
            }
        }
        private System.Windows.Forms.Button[,] _Buttons = new Button[9, 9];
        private Sudoku _Sudoku = new Sudoku();
        private System.Windows.Forms.TextBox[] _UserNoteRow = new TextBox[9];
        private System.Windows.Forms.TextBox[] _UserNoteCol = new TextBox[9];

        private SudokuOptions _Options;

        #endregion

        #region Calculation Thread / Timer

        private Thread _t = null;

        public void ThreadProc()
        {
#if _DEBUG
            Console.WriteLine("IN Thread");
#endif
            _Sudoku.CalcPossibleResults();
#if _DEBUG
            Console.WriteLine("OUT Thread");
#endif
            _t = null;
        }

        private void StopThread()
        {
            Thread myThread = _t;

            if (_t != null)
            {
                _Sudoku.StopCalcPossibleResults();
                Thread.Sleep(0);

                if (myThread != null)
                    myThread.Join();

                _t = null;
            }
        }

        // This is the method to run when the timer is raised.
        private void TimerEventProcessor(Object myObject,
                                                EventArgs myEventArgs)
        {
            _timer.Stop();
            _timer.Interval = 500;
            _timer.Start();

            int possibilities = Sudoku.PossibleSolutions;
            string outstr;

            if (_t == null)
                outstr = possibilities.ToString();
            else
                outstr = "(" + possibilities.ToString() + ")";

            if (_toolStripStatusPossibleSolutions1.Text.CompareTo(outstr) != 0)
                _toolStripStatusPossibleSolutions1.Text = outstr;

        }

        private System.Windows.Forms.Timer _timer;

        #endregion

        #region Button Draw

        private Font _ButtonFontSet;
        private Font _ButtonFontClear;

        private void SetButtons()
        {
            undoToolStripButton.Enabled = _Sudoku.CanUndo();

            _Sudoku.UpdatePossible();
            _toolStripStatusMove.Text = _Sudoku.GetStepCount().ToString();
            _toolStripStatusPossibleSolutions1.Text = "";

            _timer = null;

            StopThread();
            Sudoku.InitCalcPossibleResults();

            _t = new Thread(new ThreadStart(ThreadProc));
            _t.Priority = ThreadPriority.BelowNormal;
            _t.Start();

            _timer = new System.Windows.Forms.Timer();
            _timer.Tick += new EventHandler(TimerEventProcessor);

            // Sets the timer interval to 5 seconds.
            _timer.Interval = 500;
            _timer.Start();

            if (_ButtonFontSet == null)
            {
                _ButtonFontClear = new Font(_Buttons[0,0].Font, FontStyle.Regular);
                _ButtonFontSet   = new Font(_Buttons[0, 0].Font.FontFamily, 24);
            }


            int x, y;
            for (x = 0; x < 9; x++)
            {
                for (y = 0; y < 9; y++)
                {
                    SudokuField def = _Sudoku.GetDef(x, y);

                    if (def.No > 0)
                    {
                        _Buttons[x, y].Font = _ButtonFontSet;
                    }
                    else
                    {
                        _Buttons[x, y].Font = _ButtonFontClear;
                    }

                    _Buttons[x, y].BackColor = def.ToButtonColor(_Options);
                    _Buttons[x, y].Text = def.ToButtonString(_Options);
                    _toolTip.SetToolTip(_Buttons[x, y], def.ToButtonToolTip(_Options));

                }
            }
            for (x = 0; x < 9; x++)
            {
                _UserNoteCol[x].Text = _Sudoku.GetUserNoteCol(x);
                _UserNoteRow[x].Text = _Sudoku.GetUserNoteRow(x);
            }
        }

        private void _Button_KeyDown(object sender, KeyEventArgs e)
        {
            int No = -1;
            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
                No = e.KeyCode - Keys.D0;
            else if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
                No = e.KeyCode - Keys.NumPad0;

            if (No >= 0)
            {
                int x, y;
                Button btn = GetButton(sender, out x, out y);
                if (btn != null)
                {
                    if (_Sudoku.Set(x, y, No))
                    {
                        SetButtons();
                    }
                    else
                    {
                        SystemSounds.Asterisk.Play();
                    }

                }
            }
        }

        private void _Button_Click(object sender, EventArgs e)
        {
            int x, y;
            Button btn = GetButton(sender, out x, out y);
            if (btn != null)
            {
                if (_Options.help)
                {
                    if (_Sudoku.SetNextPossible(x, y))
                    {
                        SetButtons();
                    }
                    else
                    {
                        SystemSounds.Asterisk.Play();
                    }
                }
                else
                {
                    SudokuField def = _Sudoku.GetDef(x, y);
                    EnterFieldForm form = new EnterFieldForm();
                    form.UserNote = def.UserNote;
                    form.No = def.No;

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        _Sudoku.SetUserNote(x,y,form.UserNote);
                        if (!_Sudoku.Set(x, y, form.No))
                        {
                            SystemSounds.Asterisk.Play();
                        }
                        SetButtons();
                    }
                }
            }
        }

        private void OptionsChanged()
        {
            int x;
            bool showUserNote = !_Options.help;
            for (x = 0; x < 9; x++)
            {
                _UserNoteRow[x].Visible = showUserNote;
                _UserNoteCol[x].Visible = showUserNote;
            }
            SetButtons();
        }

        #endregion

        #region UserNote Events

        private void _UserNote_ChangedCol(object sender, EventArgs e)
        {
            int x;
            for (x = 0; x < 9; x++)
            {
                if (_UserNoteCol[x] == sender)
                {
                    _Sudoku.SetUserNoteCol(x, _UserNoteCol[x].Text);
                    return;
                }
            }
        }

        private void _UserNote_ChangedRow(object sender, EventArgs e)
        {
            int x;
            for (x = 0; x < 9; x++)
            {
                if (_UserNoteRow[x] == sender)
                {
                    _Sudoku.SetUserNoteRow(x, _UserNoteRow[x].Text);
                    return;
                }
            }
        }

        #endregion

        #region Help Functions

        private Button GetButton(object sender, out int x, out int y)
        {
            x = y = 0;
            for (x = 0; x < 9; x++)
            {
                for (y = 0; y < 9; y++)
                {
                    if (_Buttons[x, y] == sender)
                        return _Buttons[x, y];
                }
            }
            return null;
        }

        #endregion

        #region Load /Save

        private string _FileName;

        private bool Save(bool bSaveAs)
        {
            if (bSaveAs || _FileName == null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                //                saveFileDialog1.InitialDirectory = "c:\\";
                saveFileDialog1.Filter = "Sudoku files (*.sud)|*.sud";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _FileName = saveFileDialog1.FileName;
                    SetCaption();
                }
                else
                {
                    return false;
                }
            }
            if (_FileName != null)
            {
                return _Sudoku.SaveXml(_FileName);
            }
            return false;
        }

        private bool CheckSave()
        {
            if (_Sudoku.Modified)
            {
                switch (MessageBox.Show("Save changes?", "Sudoku", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Cancel: return false;
                    case DialogResult.No: break;
                    case DialogResult.Yes: if (!Save(false)) return false;
                        break;
                }
            }
            return true;
        }

        private void LoadFile()
        {
            if (!CheckSave())
                return;

            if (_FileName == null)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                //              openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "Sudoku files (*.sud)|*.sud";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _FileName = openFileDialog1.FileName;
                }
            }
            if (_FileName != null)
            {
                Sudoku newSudoku = Sudoku.Load(_FileName);

                SetCaption();

                if (newSudoku != null)
                {
                    _Sudoku = newSudoku;
                    SetButtons();
                }
            }
        }

        private void SetCaption()
        {
            Text = "Sudoku - ";
            if (_FileName != null)
                Text += _FileName;
            else
                Text += "new";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(true);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _FileName = null;
            LoadFile();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        private void saveAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Save(true);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckSave())
                return;

            _Sudoku = new Sudoku();
            _FileName = null;
            SetCaption();
            SetButtons();
        }

        #endregion

        #region Other Events

        private void calcPossibleResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
//            MessageBox.Show("Possible results = " + _Sudoku.CalcPossibleResults(5).ToString());
        }

         private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopThread();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (_FileName != null)
                LoadFile();
            else
                SetButtons();
        }

        private void finishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Sudoku.Finish();
            SetButtons();
        }

        private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Sudoku = _Sudoku.RotateMirror(false);
            SetButtons();
        }

        private void mirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Sudoku = _Sudoku.RotateMirror(true);
            SetButtons();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_Sudoku.Undo())
                SetButtons();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionForm form = new OptionForm();
            form.Options = _Options;

            if (form.ShowDialog() == DialogResult.OK)
            {
                _Options = form.Options;
                OptionsChanged();
            }
        }

        #endregion

        #region Print / PrintPreview

        // Declare the dialog.
        internal PrintPreviewDialog PrintPreviewDialog1;

        // Declare a PrintDocument object named document.
        private System.Drawing.Printing.PrintDocument document =
            new System.Drawing.Printing.PrintDocument();

        // Initalize the dialog.
        private void InitializePrintPreviewDialog()
        {
            // Create a new PrintPreviewDialog using constructor.
            this.PrintPreviewDialog1 = new PrintPreviewDialog();

            //Set the size, location, and name.
            this.PrintPreviewDialog1.ClientSize =
                new System.Drawing.Size(400, 300);
            this.PrintPreviewDialog1.Location =
                new System.Drawing.Point(29, 29);
            this.PrintPreviewDialog1.Name = "Sudoku PrintPreview";
            this.PrintPreviewDialog1.WindowState = FormWindowState.Maximized;

            // Associate the event-handling method with the 
            // document's PrintPage event.
            this.document.PrintPage +=
                new System.Drawing.Printing.PrintPageEventHandler
                (document_PrintPage);

            // Set the minimum size the dialog can be resized to.
            this.PrintPreviewDialog1.MinimumSize =
                new System.Drawing.Size(375, 250);

            // Set the UseAntiAlias property to true, which will allow the 
            // operating system to smooth fonts.
            this.PrintPreviewDialog1.UseAntiAlias = true;
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.PrintPreviewDialog1 == null)
                InitializePrintPreviewDialog();

            // Set the PrintPreviewDialog.Document property to
            // the PrintDocument object selected by the user.
            PrintPreviewDialog1.Document = document;

            // Call the ShowDialog method. This will trigger the document's
            //  PrintPage event.
            PrintPreviewDialog1.ShowDialog();
        }
        private void document_PrintPage(object sender,   System.Drawing.Printing.PrintPageEventArgs e)
        {

            // Insert code to render the page here.
            // This code will be called when the PrintPreviewDialog.Show 
            // method is called.

            // The following code will render a simple
            // message on the document in the dialog.

            int x, y;
            int RectSize=50;
            int Offset = 150;
           
            System.Drawing.Font printFont =
                new System.Drawing.Font("Arial", 35,
                System.Drawing.FontStyle.Regular);

 
            System.Drawing.Pen pen = new Pen(System.Drawing.Brushes.Black);
            pen.Width = 2;

            for (x = 0; x < 9; x++)
                if ((x % 3) != 0)
                {
                    e.Graphics.DrawLine(pen, Offset, Offset + RectSize * x, Offset + RectSize * 9, Offset + RectSize * x);
                    e.Graphics.DrawLine(pen, Offset + RectSize * x, Offset, Offset + RectSize * x, Offset + RectSize * 9);
                }

            pen.Width = 5;

            for (x=0;x<3;x++)
                for (y = 0; y < 3; y++)
                {
                    e.Graphics.DrawRectangle(pen, Offset + x * RectSize*3, Offset+y * RectSize*3, RectSize*3, RectSize*3);
                }

            for (x = 0; x < 9; x++)
                for (y = 0; y < 9; y++)
                {
                    int No = _Sudoku.Get(y,x);
                    if (No > 0)
                    {

                        e.Graphics.DrawString(No.ToString(),printFont,System.Drawing.Brushes.Black,
                            Offset+x*RectSize+5,Offset+y*RectSize);
                    }
                }

            string text = _FileName + "\n(c) by Herbert Aitenbichler";

            printFont =
                new System.Drawing.Font("Arial", 14,
                System.Drawing.FontStyle.Regular);

            e.Graphics.DrawString(text, printFont,
                System.Drawing.Brushes.Black, 40, 40);

        }

        #endregion
    }
}