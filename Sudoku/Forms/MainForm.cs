/*
  This file is part of Sudoku - A library to solve a sudoku.

  Copyright (c) Herbert Aitenbichler

  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
  to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
  and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
  WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
*/

namespace Sudoku.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Threading;
    using System.Media;

    using Sudoku.Solve;
    using Sudoku.Solve.Serialization;

    public partial class MainForm : Form
    {
        #region Constructor / Initialisation

        public MainForm(string filename)
        {
            AttacheSudoku(new Sudoku());

            _options.Help        = true;
            _options.ShowToolTip = true;

            _fileName = filename;

            InitializeComponent();
            _buttons[0, 0] = _11;
            _buttons[0, 1] = _12;
            _buttons[0, 2] = _13;
            _buttons[0, 3] = _14;
            _buttons[0, 4] = _15;
            _buttons[0, 5] = _16;
            _buttons[0, 6] = _17;
            _buttons[0, 7] = _18;
            _buttons[0, 8] = _19;
            _buttons[1, 0] = _21;
            _buttons[1, 1] = _22;
            _buttons[1, 2] = _23;
            _buttons[1, 3] = _24;
            _buttons[1, 4] = _25;
            _buttons[1, 5] = _26;
            _buttons[1, 6] = _27;
            _buttons[1, 7] = _28;
            _buttons[1, 8] = _29;
            _buttons[2, 0] = _31;
            _buttons[2, 1] = _32;
            _buttons[2, 2] = _33;
            _buttons[2, 3] = _34;
            _buttons[2, 4] = _35;
            _buttons[2, 5] = _36;
            _buttons[2, 6] = _37;
            _buttons[2, 7] = _38;
            _buttons[2, 8] = _39;
            _buttons[3, 0] = _41;
            _buttons[3, 1] = _42;
            _buttons[3, 2] = _43;
            _buttons[3, 3] = _44;
            _buttons[3, 4] = _45;
            _buttons[3, 5] = _46;
            _buttons[3, 6] = _47;
            _buttons[3, 7] = _48;
            _buttons[3, 8] = _49;
            _buttons[4, 0] = _51;
            _buttons[4, 1] = _52;
            _buttons[4, 2] = _53;
            _buttons[4, 3] = _54;
            _buttons[4, 4] = _55;
            _buttons[4, 5] = _56;
            _buttons[4, 6] = _57;
            _buttons[4, 7] = _58;
            _buttons[4, 8] = _59;
            _buttons[5, 0] = _61;
            _buttons[5, 1] = _62;
            _buttons[5, 2] = _63;
            _buttons[5, 3] = _64;
            _buttons[5, 4] = _65;
            _buttons[5, 5] = _66;
            _buttons[5, 6] = _67;
            _buttons[5, 7] = _68;
            _buttons[5, 8] = _69;
            _buttons[6, 0] = _71;
            _buttons[6, 1] = _72;
            _buttons[6, 2] = _73;
            _buttons[6, 3] = _74;
            _buttons[6, 4] = _75;
            _buttons[6, 5] = _76;
            _buttons[6, 6] = _77;
            _buttons[6, 7] = _78;
            _buttons[6, 8] = _79;
            _buttons[7, 0] = _81;
            _buttons[7, 1] = _82;
            _buttons[7, 2] = _83;
            _buttons[7, 3] = _84;
            _buttons[7, 4] = _85;
            _buttons[7, 5] = _86;
            _buttons[7, 6] = _87;
            _buttons[7, 7] = _88;
            _buttons[7, 8] = _89;
            _buttons[8, 0] = _91;
            _buttons[8, 1] = _92;
            _buttons[8, 2] = _93;
            _buttons[8, 3] = _94;
            _buttons[8, 4] = _95;
            _buttons[8, 5] = _96;
            _buttons[8, 6] = _97;
            _buttons[8, 7] = _98;
            _buttons[8, 8] = _99;

            _userNoteRow[0] = _IR1;
            _userNoteRow[1] = _IR2;
            _userNoteRow[2] = _IR3;
            _userNoteRow[3] = _IR4;
            _userNoteRow[4] = _IR5;
            _userNoteRow[5] = _IR6;
            _userNoteRow[6] = _IR7;
            _userNoteRow[7] = _IR8;
            _userNoteRow[8] = _IR9;

            _userNoteCol[0] = _IC1;
            _userNoteCol[1] = _IC2;
            _userNoteCol[2] = _IC3;
            _userNoteCol[3] = _IC4;
            _userNoteCol[4] = _IC5;
            _userNoteCol[5] = _IC6;
            _userNoteCol[6] = _IC7;
            _userNoteCol[7] = _IC8;
            _userNoteCol[8] = _IC9;

            int x, y;
            for (x = 0; x < 9; x++)
            {
                _userNoteCol[x].TextChanged += UserNoteChangedCol;
                _userNoteRow[x].TextChanged += UserNoteChangedRow;

                for (y = 0; y < 9; y++)
                {
                    _buttons[x, y].Click   += ButtonClick;
                    _buttons[x, y].KeyDown += ButtonKeyDown;
                }
            }
        }

        private Button[,]    _buttons     = new Button[9, 9];
        private Solve.Sudoku _sudoku;
        private TextBox[]    _userNoteRow = new TextBox[9];
        private TextBox[]    _userNoteCol = new TextBox[9];

        private SudokuOptions _options;

        #endregion

        #region Calculation Thread

        private volatile Thread                  _t;
        private          CancellationTokenSource _cts = new CancellationTokenSource();
        private volatile Solve.Sudoku            _tsudoku;

        public void ThreadProc()
        {
#if _DEBUG
            Console.WriteLine("IN Thread");
#endif
            _tsudoku              =  _sudoku;
            _sudoku.FoundSolution += OnFoundSolution;
            _tsudoku.CalcPossibleSolutions(_cts.Token);
#if _DEBUG
            Console.WriteLine("OUT Thread");
#endif
            _sudoku.FoundSolution -= new Solve.Sudoku.FoundSolutionHandler(OnFoundSolution);
        }

        private void StartThread()
        {
            StopThread();

            _cts        = new CancellationTokenSource();
            _t          = new Thread(new ThreadStart(ThreadProc));
            _t.Priority = ThreadPriority.BelowNormal;
            _t.Start();
        }

        private void StopThread()
        {
            if (_t != null && _t.IsAlive)
            {
                _cts.Cancel();
                _cts.Dispose();
                _t   = null;
                _cts = null;
            }

            _tsudoku              =  null;
            _sudoku.FoundSolution -= OnFoundSolution;

            SetControlText(_toolStripStatusPossibleSolutions1, "");
        }

        private delegate void SetControlTextDelegate(ToolStripLabel control, string text);

        private void SetControlText(ToolStripLabel control, string text)
        {
            if (control.Text.CompareTo(text) != 0)
                control.Text = text;
        }

        int _test = 0;

        protected void OnFoundSolution(object sudoku, SudokuEventArgs solutioninfo)
        {
            _test++;
            int    possibilities = solutioninfo.PossibleSolutions;
            string outstr;

            if (solutioninfo.FindSolutionsFinished)
                outstr = possibilities.ToString();
            else
                outstr = "(" + possibilities.ToString() + ")";

            try
            {
                Invoke(new SetControlTextDelegate(SetControlText), _toolStripStatusPossibleSolutions1, outstr);
            }
            catch (ObjectDisposedException)
            {

            }
//            BeginInvoke(new MethodInvoker( () => _toolStripStatusPossibleSolutions1.Text = outstr  ));
        }

        #endregion

        #region Button Draw

        private Font _buttonFontSet;
        private Font _buttonFontClear;

        private void SetButtons()
        {
            undoToolStripButton.Enabled = _sudoku.CanUndo();

            _sudoku.UpdatePossible();
            _toolStripStatusMove.Text               = _sudoku.StepCount.ToString();
            _toolStripStatusPossibleSolutions1.Text = "";

            StartThread();

            if (_buttonFontSet == null)
            {
                _buttonFontClear = new Font(_buttons[0, 0].Font,            FontStyle.Regular);
                _buttonFontSet   = new Font(_buttons[0, 0].Font.FontFamily, 24);
            }


            int x, y;
            for (x = 0; x < 9; x++)
            {
                for (y = 0; y < 9; y++)
                {
                    SudokuField def = _sudoku.GetDef(x, y);

                    if (def.No > 0)
                    {
                        _buttons[x, y].Font = _buttonFontSet;
                    }
                    else
                    {
                        _buttons[x, y].Font = _buttonFontClear;
                    }

                    _buttons[x, y].BackColor = def.ToButtonColor(_options);
                    _buttons[x, y].Text      = def.ToButtonString(_options);
                    _toolTip.SetToolTip(_buttons[x, y], def.ToButtonToolTip(_options));
                }
            }

            for (x = 0; x < 9; x++)
            {
                _userNoteCol[x].Text = _sudoku.GetUserNoteCol(x);
                _userNoteRow[x].Text = _sudoku.GetUserNoteRow(x);
            }
        }

        private void ButtonKeyDown(object sender, KeyEventArgs e)
        {
            int no = -1;
            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
                no = e.KeyCode - Keys.D0;
            else if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
                no = e.KeyCode - Keys.NumPad0;

            if (no >= 0)
            {
                int    x, y;
                Button btn = GetButton(sender, out x, out y);
                if (btn != null)
                {
                    if (_sudoku.Set(x, y, no))
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

        private void ButtonClick(object sender, EventArgs e)
        {
            int    x, y;
            Button btn = GetButton(sender, out x, out y);
            if (btn != null)
            {
                if (_options.Help)
                {
                    if (_sudoku.SetNextPossible(x, y))
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
                    SudokuField    def  = _sudoku.GetDef(x, y);
                    EnterFieldForm form = new EnterFieldForm();
                    form.UserNote = def.UserNote;
                    form.No       = def.No;

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        _sudoku.SetUserNote(x, y, form.UserNote);
                        if (!_sudoku.Set(x, y, form.No))
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
            int  x;
            bool showUserNote = !_options.Help;
            for (x = 0; x < 9; x++)
            {
                _userNoteRow[x].Visible = showUserNote;
                _userNoteCol[x].Visible = showUserNote;
            }

            SetButtons();
        }

        #endregion

        #region UserNote Events

        private void UserNoteChangedCol(object sender, EventArgs e)
        {
            int x;
            for (x = 0; x < 9; x++)
            {
                if (_userNoteCol[x] == sender)
                {
                    _sudoku.SetUserNoteCol(x, _userNoteCol[x].Text);
                    return;
                }
            }
        }

        private void UserNoteChangedRow(object sender, EventArgs e)
        {
            int x;
            for (x = 0; x < 9; x++)
            {
                if (_userNoteRow[x] == sender)
                {
                    _sudoku.SetUserNoteRow(x, _userNoteRow[x].Text);
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
                    if (_buttons[x, y] == sender)
                        return _buttons[x, y];
                }
            }

            return null;
        }

        #endregion

        #region Load /Save

        private string _fileName;

        private bool Save(bool bSaveAs)
        {
            if (bSaveAs || _fileName == null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                //                saveFileDialog1.InitialDirectory = "c:\\";
                saveFileDialog1.Filter           = "Sudoku files (*.sud)|*.sud";
                saveFileDialog1.FilterIndex      = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _fileName = saveFileDialog1.FileName;
                    SetCaption();
                }
                else
                {
                    return false;
                }
            }

            if (_fileName != null)
            {
                return _sudoku.SaveXml(_fileName);
            }

            return false;
        }

        private bool CheckSave()
        {
            if (_sudoku.Modified)
            {
                switch (MessageBox.Show("Save changes?", "Sudoku", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Cancel: return false;
                    case DialogResult.No:     break;
                    case DialogResult.Yes:
                        if (!Save(false)) return false;
                        break;
                }
            }

            return true;
        }

        private void LoadFile()
        {
            if (!CheckSave())
                return;

            if (_fileName == null)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                //              openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter           = "Sudoku files (*.sud)|*.sud";
                openFileDialog1.FilterIndex      = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _fileName = openFileDialog1.FileName;
                }
            }

            if (_fileName != null)
            {
                var newSudoku = SudokuLoadSaveExtensions.Load(_fileName);
                newSudoku.UndoAvailable = true;

                SetCaption();

                if (newSudoku != null)
                {
                    AttacheSudoku(newSudoku);
                    SetButtons();
                }
            }
        }

        private void SetCaption()
        {
            Text = "Sudoku - ";
            if (_fileName != null)
                Text += _fileName;
            else
                Text += "new";
        }

        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            Save(false);
        }

        private void SaveAsToolStripMenuItemClick(object sender, EventArgs e)
        {
            Save(true);
        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            _fileName = null;
            LoadFile();
        }

        private void SaveToolStripMenuItem1Click(object sender, EventArgs e)
        {
            Save(false);
        }

        private void SaveAsToolStripMenuItem1Click(object sender, EventArgs e)
        {
            Save(true);
        }

        private void AttacheSudoku(Solve.Sudoku s)
        {
            _sudoku               = s;
            _sudoku.UndoAvailable = true;
            _sudoku.ClearUndo();
        }

        private void NewToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (!CheckSave())
                return;

            AttacheSudoku(new Solve.Sudoku());

            _fileName = null;
            SetCaption();
            SetButtons();
        }

        #endregion

        #region Other Events

        private void CalcPossibleResultsToolStripMenuItemClick(object sender, EventArgs e)
        {
//            MessageBox.Show("Possible results = " + _Sudoku.CalcPossibleResults(5).ToString());
        }

        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            StopThread();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            if (_fileName != null)
                LoadFile();
            else
                SetButtons();
        }

        private void FinishToolStripMenuItemClick(object sender, EventArgs e)
        {
            _sudoku.Finish();
            SetButtons();
        }

        private void RotateToolStripMenuItemClick(object sender, EventArgs e)
        {
            AttacheSudoku(_sudoku.Rotate());
            SetButtons();
        }

        private void MirrorToolStripMenuItemClick(object sender, EventArgs e)
        {
            AttacheSudoku(_sudoku.Mirror());
            SetButtons();
        }

        private void UndoToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_sudoku.Undo())
                SetButtons();
        }

        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        private void OptionsToolStripMenuItemClick(object sender, EventArgs e)
        {
            OptionForm form = new OptionForm();
            form.Options = _options;

            if (form.ShowDialog() == DialogResult.OK)
            {
                _options = form.Options;
                OptionsChanged();
            }
        }

        #endregion

        #region Print / PrintPreview

        // Declare the dialog.
        private PrintPreviewDialog _printPreviewDialog;

        // Declare a PrintDocument object named document.
        private System.Drawing.Printing.PrintDocument _document = new System.Drawing.Printing.PrintDocument();

        // Initalize the dialog.
        private void InitializePrintPreviewDialog()
        {
            // Create a new PrintPreviewDialog using constructor.
            _printPreviewDialog = new PrintPreviewDialog();

            //Set the size, location, and name.
            _printPreviewDialog.ClientSize  = new Size(400, 300);
            _printPreviewDialog.Location    = new Point(29, 29);
            _printPreviewDialog.Name        = "Sudoku PrintPreview";
            _printPreviewDialog.WindowState = FormWindowState.Maximized;

            // Associate the event-handling method with the 
            // document's PrintPage event.
            _document.PrintPage += DocumentPrintPage;

            // Set the minimum size the dialog can be resized to.
            _printPreviewDialog.MinimumSize = new Size(375, 250);

            // Set the UseAntiAlias property to true, which will allow the 
            // operating system to smooth fonts.
            _printPreviewDialog.UseAntiAlias = true;
        }

        private void PrintPreviewToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_printPreviewDialog == null)
                InitializePrintPreviewDialog();

            // Set the PrintPreviewDialog.Document property to
            // the PrintDocument object selected by the user.
            _printPreviewDialog.Document = _document;

            // Call the ShowDialog method. This will trigger the document's
            //  PrintPage event.
            _printPreviewDialog.ShowDialog();
        }

        private void DocumentPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Insert code to render the page here.
            // This code will be called when the PrintPreviewDialog.Show 
            // method is called.

            // The following code will render a simple
            // message on the document in the dialog.

            int       x, y;
            const int RectSize = 50;
            const int Offset   = 150;

            System.Drawing.Font printFont = new System.Drawing.Font("Arial", 35, System.Drawing.FontStyle.Regular);

            Pen pen = new Pen(System.Drawing.Brushes.Black);
            pen.Width = 2;

            for (x = 0; x < 9; x++)
            {
                if ((x % 3) != 0)
                {
                    e.Graphics.DrawLine(pen, Offset,                Offset + RectSize * x, Offset + RectSize * 9, Offset + RectSize * x);
                    e.Graphics.DrawLine(pen, Offset + RectSize * x, Offset,                Offset + RectSize * x, Offset + RectSize * 9);
                }
            }

            pen.Width = 5;

            for (x = 0; x < 3; x++)
            {
                for (y = 0; y < 3; y++)
                {
                    e.Graphics.DrawRectangle(pen, Offset + x * RectSize * 3, Offset + y * RectSize * 3, RectSize * 3, RectSize * 3);
                }
            }

            for (x = 0; x < 9; x++)
            {
                for (y = 0; y < 9; y++)
                {
                    int no = _sudoku.Get(y, x);
                    if (no > 0)
                    {
                        e.Graphics.DrawString(no.ToString(), printFont, Brushes.Black, Offset + x * RectSize + 5, Offset + y * RectSize);
                    }
                }
            }

            string text = _fileName + "\n(c) by Herbert Aitenbichler";

            printFont = new Font("Arial", 14, FontStyle.Regular);
            e.Graphics.DrawString(text, printFont, Brushes.Black, 40, 40);
        }

        #endregion
    }
}