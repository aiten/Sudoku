
using System;
using System.IO;
using System.Collections;
using System.Xml.Serialization;

namespace Sudoku.Solve
{
    public class Sudoku
    {
        #region public struct

        public struct SudokuOptions
        {
            public bool Help;
            public bool ShowToolTip;
        };

        #endregion

        #region Private Data

        private readonly Sudoku3[,] _sudoku3 = new Sudoku3[3,3];
        private string[] _userNoteCol = new string[9];
        private string[] _userNoteRow = new string[9];

        #endregion

        #region For XML Serialization
        
        public string[] XmlUserNoteCol
        {
            get { return _userNoteCol; }
            set { _userNoteCol = value; }
        }
        public string[] XmlUserNoteRow
        {
            get { return _userNoteRow; }
            set { _userNoteRow = value; }
        }
        public Sudoku3 XmlSudoku00
        {
            get { return _sudoku3[0,0]; }
            set { _sudoku3[0,0] = value; }
        }
        public Sudoku3 XmlSudoku10
        {
            get { return _sudoku3[1, 0]; }
            set { _sudoku3[1, 0] = value; }
        }
        public Sudoku3 XmlSudoku20
        {
            get { return _sudoku3[2, 0]; }
            set { _sudoku3[2, 0] = value; }
        }
        public Sudoku3 XmlSudoku01
        {
            get { return _sudoku3[0, 1]; }
            set { _sudoku3[0, 1] = value; }
        }
        public Sudoku3 XmlSudoku11
        {
            get { return _sudoku3[1, 1]; }
            set { _sudoku3[1, 1] = value; }
        }
        public Sudoku3 XmlSudoku21
        {
            get { return _sudoku3[2, 1]; }
            set { _sudoku3[2, 1] = value; }
        }
        public Sudoku3 XmlSudoku02
        {
            get { return _sudoku3[0, 2]; }
            set { _sudoku3[0, 2] = value; }
        }
        public Sudoku3 XmlSudoku12
        {
            get { return _sudoku3[1, 2]; }
            set { _sudoku3[1, 2] = value; }
        }
        public Sudoku3 XmlSudoku22
        {
            get { return _sudoku3[2, 2]; }
            set { _sudoku3[2, 2] = value; }
        }

        #endregion

        #region Constructor / Initalisation

        public Sudoku()
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    _sudoku3[x, y] = new Sudoku3();
        }

        public Sudoku Clone()
        {
            Sudoku newsoduku = new Sudoku();
            int x, y;
            for (x = 0; x < 9; x++)
            {
                for (y = 0; y < 9; y++)
                {
                    int no = Get(x, y);
                    newsoduku.Set(x, y, no, true);
                }
            }
            return newsoduku;
        }

        #endregion

        #region UserNote

        public void SetUserNote(int x, int y, string usernote)
        {
            SudokuField def = GetDef(x, y);
            if (def.UserNote.CompareTo(usernote) != 0)
                _modified = true;
            def.SetUserNote(usernote);
        }

        public string GetUserNoteRow(int idx)
        {
            if (_userNoteRow==null) _userNoteRow= new string [9];
            string ret = _userNoteRow[idx];
            return ret == null ? "" : ret;
        }
        public string GetUserNoteCol(int idx)
        {
            if (_userNoteCol== null) _userNoteCol = new string[9];
            string ret = _userNoteCol[idx];
            return ret==null ? "" : ret;
        }
        public void SetUserNoteRow(int idx, string usernote)
        {
            string old = GetUserNoteRow(idx);
            if (old.CompareTo(usernote) != 0)
                _modified = true;
            _userNoteRow[idx] = usernote;
        }
        public void SetUserNoteCol(int idx, string usernote)
        {
            string old = GetUserNoteCol(idx);
            if (old.CompareTo(usernote) != 0)
                _modified = true;
            _userNoteCol[idx] = usernote;
        }

        #endregion

        #region Get Field Information

        public int Get(int x, int y)
        {
            return _sudoku3[x / 3, y / 3].Get(x % 3, y % 3);
        }
        public SudokuField GetDef(int x, int y)
        {
            return _sudoku3[x / 3, y / 3].GetDef(x % 3, y % 3);
        }

        #endregion

        #region Undo / Redo

        private class UndoInfo
        {
            public int X;
            public int Y;
            public int No;
        };

        [NonSerialized]
        private ArrayList _undoList;

        public bool Undo()
        {
            if (CanUndo())
            {
                UndoInfo undo = (UndoInfo)_undoList[_undoList.Count - 1];
                Set(undo.X, undo.Y, undo.No, true);
                _undoList.RemoveAt(_undoList.Count-1);
                return true;
            }
            return false;
        }
        public bool CanUndo()
        {
            return _undoList != null && _undoList.Count > 0;
        }

        public int GetStepCount()
        {
            int x, y;
            int stepCount = 0;
            for (x = 0; x < 9; x++)
            {
                for (y = 0; y < 9; y++)
                {
                    int no = Get(x, y);

                    if (no > 0)
                    {
                        stepCount++;
                    }
                }
            }
            return stepCount;
        }

        #endregion

        #region Calc Possibilities

        private delegate SudokuField GetSudokuField(int x, int y);
        [NonSerialized]
        private bool[] _foundIdx1 = new bool[9];
        [NonSerialized]
        private bool[] _foundIdx2 = new bool[9];

        private SudokuField GetSudokuFieldRow(int x, int y)
        {
            return GetDef(x, y);
        }
        private SudokuField GetSudokuFieldCol(int x, int y)
        {
            return GetDef(y, x);
        }
        private SudokuField GetSudokuFieldS3(int x, int y)
        {
            return _sudoku3[x / 3, x % 3].GetDef(y / 3, y % 3);
        }
        private void CommitChanges()
        {
            int x,y;
            for (x = 0; x < 9; x++)
            {
                for (y = 0; y < 9; y++)
                {
                    GetSudokuFieldCol(x, y).CommitChanges();
                }
            }
        }
        private int UpdatePossibleBlockade1(GetSudokuField getdef, string rowcol3)
        {
            // set all onlyPossible 
            int changcount=0;
            int x, y, z, t;
            for (z = 0; z < 9; z++)
            {
                for (x = 0; x < 9; x++)
                {
                    for (y = 0; y < 9; y++)
                    {
                        SudokuField def = getdef(x, y);
                        if (def.No == 0 && def.IsPossible(z + 1))
                        {
                            bool foundOther=false;
                            for (t = 0; t < 9; t++)
                            {
                                if (t != y)
                                {
                                    SudokuField def2 = getdef(x, t);
                                    if (def2.No == 0 && def2.IsPossible(z + 1))
                                    {
                                        foundOther=true;
                                        break;
                                    }
                                }
                            }
                            if (!foundOther)
                            {
                                for (t = 0; t < 9; t++)
                                {
                                    if (t != z && !def.IsNotPossible(t+1))
                                    {
                                        changcount++;
                                        string reason = (z + 1) + " only in " + rowcol3;
                                        def.SetNotPossible(t + 1, reason + " (B1)");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return changcount;
        }

        private int UpdatePossibleBlockade2(GetSudokuField getdef, string rowcol3)
        {
            // set all onlyPossible 
            int changcount = 0;
            int x, y, z, t;

            for (x = 0; x < 9; x++)
            {
                for (y = 0; y < 9; y++)
                {
                    SudokuField def = getdef(x, y);
                    if (def.No == 0)
                    {
                        for (t = 0; t < 9; t++)
                            _foundIdx1[t] = false;

                        _foundIdx1[y] = true;
                        int foundCount = 1;

                        for (t = 0; t < 9; t++)
                        {
                            if (t != y)
                            {
                                SudokuField def2 = getdef(x, t);
                                if (def2.No == 0)
                                {
                                    if (def.IsSubSetPossible(def2))
                                    {
                                        _foundIdx1[t] = true;
                                        foundCount++;
                                    }
                                }
                            }
                        }

                        if (foundCount == def.PossibleCount())
                        {
                            string allToolTip=null;
                            for (t = 0; t < 9; t++)
                                if (_foundIdx1[t])
                                {
                                    SudokuField def2 = getdef(x, t);
                                    if (def2.No == 0)
                                    {
                                        if (allToolTip == null)
                                        {
                                            allToolTip = def2.PossibleString();
                                            allToolTip += ": in " + rowcol3 + "-index: ";

                                        }
                                        else
                                        {
                                            allToolTip += ",";
                                        }
                                        allToolTip += (t + 1).ToString();
                                    }
                            }
                        
                            for (t = 0; t < 9; t++)
                            {
                                if (!_foundIdx1[t])
                                {
                                    SudokuField def2 = getdef(x, t);
                                    if (def2.No == 0)
                                    {
                                        for (z = 1; z <= 9; z++)
                                        {
                                            if (def.IsPossible(z) && def2.IsPossible(z))
                                            {
                                                changcount++;
                                                string reason = allToolTip;
                                                def2.SetNotPossible(z, reason + " (B2)");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return changcount;
        }

        private int UpdatePossibleBlockade2SubSet(GetSudokuField getdef, string rowcol3)
        {
            // set all onlyPossible 
            int changcount = 0;
            int x, y, t, z;

            for (x = 0; x < 9; x++)
            {
                for (y = 0; y < 9; y++)
                {
                    SudokuField def = getdef(x, y);
                    if (def.No == 0)
                    {
                        for (t = 0; t < 9; t++)
                            _foundIdx1[t] = false;

                        _foundIdx1[y] = true;

                        if (FindSubSet(getdef, x, y + 1, _foundIdx1, _foundIdx2))
                        {
                            string allToolTip=null;
                            for (t = 0; t < 9; t++)
                                if (_foundIdx1[t])
                                {
                                    SudokuField def2 = getdef(x, t);
                                    if (def2.No == 0)
                                    {
                                        if (allToolTip == null)
                                        {
                                            allToolTip = string.Empty;
                                            for (z = 1; z <= 9; z++)
                                            {
                                                if (_foundIdx2[z-1])
                                                {
                                                    if (!string.IsNullOrEmpty(allToolTip))
                                                        allToolTip += ",";
                                                    allToolTip += z.ToString();
                                                }
                                            }
                                            allToolTip += ": in " + rowcol3 + "-index: ";
                                        }
                                        else
                                        {
                                            allToolTip += ",";
                                        }
                                        allToolTip += (t + 1).ToString();
                                    }
                            }
                        
                            for (t = 0; t < 9; t++)
                            {
                                if (!_foundIdx1[t])
                                {
                                    SudokuField def2 = getdef(x, t);
                                    if (def2.No == 0)
                                    {
                                        for (z = 1; z <= 9; z++)
                                        {
                                            if (_foundIdx2[z-1] && def2.IsPossible(z))
                                            {
                                                changcount++;
                                                string reason = allToolTip;
                                                def2.SetNotPossible(z, reason + " (B2+)");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return changcount;
        }

        private bool FindSubSet(GetSudokuField getdef, int x, int y, bool[] use, bool[] noSet)
        {
            // find union with backtracking

            if (y>=9)
                return false;

            if (IsSubSet(getdef, x, use, noSet))
                return true;

            int z;
            for (z = y; z < 9; z++)
            {
                use[z] = true;
                if (FindSubSet(getdef, x, z+1, use, noSet))
                {
                    return true;
                }
                use[z] = false;
            }
            return false;
        }

        private bool IsSubSet(GetSudokuField getdef, int x, bool[] use, bool[] noSet)
        {
            int y,t;
            int countUsed = 0;
            int countPossibel = 0;
            int countSet = 0;
            for (t = 0; t < 9; t++)
                noSet[t] = false;
            for (y = 0; y < 9; y++)
            {
                SudokuField def = getdef(x, y);
                if (def.No == 0)
                {
                    countPossibel++;
                    if (use[y])
                    {
                        countUsed++;
                        for (t = 1; t <= 9; t++)
                        {
                            if (def.IsPossible(t))
                            {
                                if (!noSet[t - 1])
                                {
                                    countSet++;
                                    noSet[t - 1] = true;
                                }
                            }
                        }
                    }
                }
            }
            return countSet == countUsed && countUsed != countPossibel && countSet > 1;
        }

        private int UpdatePossibleBlockade3(GetSudokuField getdef, string rowcol3)
        {
            // set all onlyPossible 
            int changcount = 0;
            int x, y, z, t;

            for (x = 0; x < 9; x++)
            {
                for (y = 0; y < 9; y++)
                {
                    SudokuField def = getdef(x, y);
                    if (def.No == 0)
                    {
                        for (z = 1; z <= 9; z++)
                        {
                            string info = null;

                            if (def.IsPossible(z))
                            {
                                // test all in same sudoku but not in same row/col

                                int s3x = x / 3;
                                int s3y = y / 3;

                                for (t=0;t<9;t++)
                                {
                                    int sx = s3x*3 + t/3;
                                    int sy = s3y*3 + t%3;
                                    SudokuField def2 = getdef(sx, sy);
                                    if (def2.No == 0)
                                    {
                                        if (x != sx)
                                        {
                                            if (def2.IsPossible(z))
                                            {
                                                // abort with z
                                                break;
                                            }
                                        }
                                        else if (def2.IsPossible(z))
                                        {
                                            if (info != null)
                                                info += ",";
                                            info += (sy+1).ToString();
                                        }
                                    }
                                }
                                if (t >= 9)
                                {
                                    for (t = 0; t < 9; t++)
                                    {
                                        if (t / 3 != y / 3)
                                        {
                                            SudokuField def2 = getdef(x, t);
                                            if (def2.No == 0 && def2.IsPossible(z))
                                            {
                                                changcount++;
                                                def2.SetNotPossible(z, "only in " + rowcol3 + "-index:" + info + " (B3)");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return changcount;
        }

        private bool HavePossibility()
        {
           int x, y;
 
            // Update with sudoku (mainRule) rules

            for (x = 0; x < 9; x++)
            {
                for (y = 0; y < 9; y++)
                {
                    SudokuField def = GetDef(x, y);
                    
                    if (def.No == 0 && def.OnlyPossible() > 0)
                        return true;
                }
            }
   
            return false;
        }

        public void UpdatePossible()
        {
            int x, y, z;

            // Update with sudoku (mainRule) rules

            for (x = 0; x < 9; x++)
            {
                for (y = 0; y < 9; y++)
                {
                    SudokuField def = GetDef(x, y);
                    def.InitHelpVar();

                    if (def.No == 0)
                    {
                        for (z = 1; z <= 9; z++)
                        {
                            if (Set(x, y, z, true))
                            {
                                def.MainRulePossible[z - 1] = true;
                            }
                        }
                        Set(x, y, 0, true);
                    }
                }
            }

            CommitChanges();

            const string row = "row";
            const string col = "col";
            const string s3 = "3*3";

            UpdatePossibleBlockade1(GetSudokuFieldS3, s3);

            CommitChanges();

            while (!HavePossibility())
            {
                UpdatePossibleBlockade1(GetSudokuFieldRow, row);
                UpdatePossibleBlockade1(GetSudokuFieldCol, col);
                UpdatePossibleBlockade1(GetSudokuFieldS3, s3);

                CommitChanges();

                if (HavePossibility())
                    break;

                int changeCount = 0;

                if (0 < UpdatePossibleBlockade3(GetSudokuFieldRow, row)) changeCount++;
                if (0 < UpdatePossibleBlockade3(GetSudokuFieldCol, col)) changeCount++;

                if (0 < UpdatePossibleBlockade2(GetSudokuFieldRow, row)) changeCount++;
                if (0 < UpdatePossibleBlockade2(GetSudokuFieldCol, col)) changeCount++;
                if (0 < UpdatePossibleBlockade2(GetSudokuFieldS3, s3)) changeCount++;

                if (0 < UpdatePossibleBlockade2SubSet(GetSudokuFieldRow, row)) changeCount++;
                if (0 < UpdatePossibleBlockade2SubSet(GetSudokuFieldCol, col)) changeCount++;
                if (0 < UpdatePossibleBlockade2SubSet(GetSudokuFieldS3, s3)) changeCount++;

                CommitChanges();

                if (changeCount == 0)
                    break;

            }
        }

        #endregion

        #region Other public User Functions

        public bool Modified { get { return _modified; } }

        public bool Finish()
        {
            if (!IsValid(true))
                return false;

            int dummy = 0;
            return _CalcPossibleSolutions(-1, 9, true, ref dummy) == 1;
        }

        public Sudoku RotateMirror(bool mirror)
        {
            Sudoku newSudoku = new Sudoku();
            int x, y;

            for (x = 0; x < 9; x++)
            {
                newSudoku.SetUserNoteCol(x, GetUserNoteRow(x));
                newSudoku.SetUserNoteRow(x, GetUserNoteCol(x));
                for (y = 0; y < 9; y++)
                {
                    int myx;
                    int myy;
                    if (mirror)
                    {
                        myx = x;
                        myy = 8 - y;
                    }
                    else
                    {
                        myx = 8 - y;
                        myy = x;
                    }
                    SudokuField def = GetDef(myx,myy);
                    newSudoku.SetUserNote(x, y, def.UserNote);
                    if (def.No > 0)
                        newSudoku.Set(x, y, def.No);
                }
            }
            return newSudoku;
        }

        #endregion

        #region Private Members

        [NonSerialized]
        private bool _modified = false;

        #endregion

        #region Set and Validation

        public bool SetNextPossible(int x, int y)
        {
            SudokuField def = GetDef(x, y);

            int no = def.No;

            if (no == 0)
            {
                int only = def.OnlyPossible();
                if (only > 0 && Set(x, y, only))
                {
                    return true;
                }
            }

            for (int i = 1; i <= 10; i++)
            {
                int newNo = no + i;
                if (newNo > 9) newNo -= 10;
                if (Set(x, y, newNo))
                {
                    return true;
                }
            }

            Clear(x, y);
            return false;
        }
        public bool Set(int x, int y, int no)
        {
            if (no < 0 || no > 9)
                return false;

            return Set(x, y, no, false);
        }
        public bool Clear(int x, int y)
        {
            return Set(x, y, 0, false);
        }

        private bool Set(int x, int y, int no, bool testOnly)
        {
            int sx = x / 3;
            int sy = y / 3;
            int dx = x % 3;
            int dy = y % 3;

            int old = _sudoku3[sx, sy].Get(dx, dy);

            if (!_sudoku3[sx, sy].Set(dx, dy, no))
                return false;

            if (no == 0 || IsValid(false))
            {
                if (!testOnly)
                {
                    if (_undoList==null) _undoList=new ArrayList();
                    UndoInfo info = new UndoInfo();
                    info.X = x;
                    info.Y = y;
                    info.No = old;
                    _undoList.Add(info);
                    _modified = true;
                }

                return true;
            }

            _sudoku3[sx, sy].Set(dx, dy, old);
            return false;
        }

        [NonSerialized]
        bool[] _foundX;
        [NonSerialized]
        bool[] _foundY;
 
        private bool IsValid(bool test3)
        {
            if (_foundX == null) _foundX = new bool[9];
            if (_foundY == null) _foundY = new bool[9];

            int x, y;

            if (test3)
            {
                for (x = 0; x < 3; x++)
                    for (y = 0; y < 3; y++)
                        if (!_sudoku3[x, y].IsValid())
                            return false;
            }

            for (x = 0; x < 9; x++)
            {
                for (y = 0; y < 9; y++)
                {
                    _foundX[y] = false;
                    _foundY[y] = false;
                }

                for (y = 0; y < 9; y++)
                {
                    int noX = Get(x, y);
                    if (noX > 0)
                    {
                        if (_foundX[noX-1])
                            return false;
                        
                        _foundX[noX-1] = true;
                    }

                    int noY = Get(y, x);
                    if (noY > 0)
                    {
                        if (_foundY[noY - 1])
                            return false;
                        
                        _foundY[noY - 1] = true;
                    }
                }
            }
            return true;
        }

        #endregion

        #region Load/Save

        public bool SaveXml(string fileName)
        {
            using (TextWriter fs = new StreamWriter(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Sudoku));

                try
                {
                    serializer.Serialize(fs, this);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
            _modified = false;
            return true;
        }

        private static Sudoku LoadXml(string fileName)
        {
            Sudoku sudoku;
            // Open the file containing the data that you want to deserialize.
            using (TextReader fs = new StreamReader(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Sudoku));

                try
                {
                    sudoku = (Sudoku)serializer.Deserialize(fs);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
            sudoku._modified = false;
            return sudoku;
        }

        public static Sudoku Load(string fileName)
        {
            return LoadXml(fileName);
        }

        #endregion

        #region Debug Help Functions

        private void ConsolOut()
        {
            //for (int xx = 0; xx < 9; xx++)
            //{
            //    for (int yy = 0; yy < 9; yy++)
            //    {
            //        Console.Write(Get(xx, yy).ToString());
            //        Console.Write(" ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();
        }

        #endregion

        #region Calc Possibile Solutions (BackTracking)

        public delegate void FoundSolutionHandler(object sudoku,SudokuEventArgs solutioninfo);
        public event FoundSolutionHandler FoundSolution;

        protected virtual void OnFoundSolution(object sudoku, SudokuEventArgs solutioninfo)
        {
            if (FoundSolution != null)
            {
                FoundSolution(sudoku, solutioninfo);
            }
        }

        volatile private bool _endCalc = false;
        private Sudoku _calcSudoku = null;

        public int CalcPossibleSolutions()
        {
            if (!IsValid(true))
                return 0;

            OnFoundSolution(this, new SudokuEventArgs(0));

            _endCalc = false;
            _calcSudoku = Clone();
            _calcSudoku.FoundSolution = FoundSolution;
            int possibleSolutions = 0;
            int ret = _calcSudoku._CalcPossibleSolutions(-1, 9, false, ref possibleSolutions);
            _calcSudoku = null;
            OnFoundSolution(this, new SudokuEventArgs(ret,true));
            return ret;
        }

        public void StopPossibleSolutions()
        {
            _endCalc = true;
            if (_calcSudoku != null)
                _calcSudoku.StopPossibleSolutions();
        }

        private int _CalcPossibleSolutions(int x, int y, bool calcEnd, ref int possibleSolutions)
        {
            if (!calcEnd && _endCalc)
                return -1;

            if (GetStepCount() >= 9 * 9)
            {
                possibleSolutions = possibleSolutions > 1 ? possibleSolutions : 1;
#if DEBUG
                ConsolOut();
#endif
                OnFoundSolution(this, new SudokuEventArgs(possibleSolutions));
                return 1;
            }


            int results = 0;
            int myx;
            int myy;
            y++;
            if (y >= 9)
            {
                x++; y = 0;
            }

            if (x > 8)
            {
                possibleSolutions = possibleSolutions > 1 ? possibleSolutions : 1;
#if DEBUG
                ConsolOut();
#endif
                return 1;
            }

            for (myx = x; myx < 9; myx++)
            {
                for (myy = y; myy < 9; myy++)
                {
                    int no = Get(myx, myy);

                    if (no == 0)
                    {
                        //int old = no;
                        for (int newNo = 1; newNo <= 9; newNo++)
                        {
                            if (Set(myx, myy, newNo, true))
                            {
                                int res = _CalcPossibleSolutions(myx, myy, calcEnd, ref possibleSolutions);
                                if (res < 0)
                                {
                                    if (!calcEnd)
                                        Set(myx, myy, 0, true);
                                    return res;     // abort
                                }

                                if (res > 0)
                                {
                                    if (calcEnd)
                                    {
                                        return 1;
                                    }

                                    results += res;
                                    possibleSolutions = possibleSolutions > results ? possibleSolutions : results;
                                }
                            }
                        }
                        Set(myx, myy, 0, true);
                        return results;
                    }
                }
                y = 0;
            }
            return 0;
        }
        #endregion
    }

    public class SudokuEventArgs : EventArgs
    {
        public SudokuEventArgs(int possibleSolutions, bool finished=false)
        {
            PossibleSolutions = possibleSolutions;
            FindSolutionsFinished = finished;
        }
        public readonly int PossibleSolutions;
        public readonly bool FindSolutionsFinished;
    };
}
