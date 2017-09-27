
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using System.Xml.Serialization;

namespace SudokuSolve
{
    public class Sudoku
    {
        #region Private Data

        private Sudoku3[,] _Sudoku3 = new Sudoku3[3,3];
        private string[] _UserNoteCol = new string[9];
        private string[] _UserNoteRow = new string[9];

        #endregion

        #region For XML Serialization
        
        public string[] XmlUserNortCol
        {
            get { return _UserNoteCol; }
            set { _UserNoteCol = value; }
        }
        public string[] XmlUserNortRow
        {
            get { return _UserNoteRow; }
            set { _UserNoteRow = value; }
        }
        public Sudoku3 XmlSudoku00
        {
            get { return _Sudoku3[0,0]; }
            set { _Sudoku3[0,0] = value; }
        }
        public Sudoku3 XmlSudoku10
        {
            get { return _Sudoku3[1, 0]; }
            set { _Sudoku3[1, 0] = value; }
        }
        public Sudoku3 XmlSudoku20
        {
            get { return _Sudoku3[2, 0]; }
            set { _Sudoku3[2, 0] = value; }
        }
        public Sudoku3 XmlSudoku01
        {
            get { return _Sudoku3[0, 1]; }
            set { _Sudoku3[0, 1] = value; }
        }
        public Sudoku3 XmlSudoku11
        {
            get { return _Sudoku3[1, 1]; }
            set { _Sudoku3[1, 1] = value; }
        }
        public Sudoku3 XmlSudoku21
        {
            get { return _Sudoku3[2, 1]; }
            set { _Sudoku3[2, 1] = value; }
        }
        public Sudoku3 XmlSudoku02
        {
            get { return _Sudoku3[0, 2]; }
            set { _Sudoku3[0, 2] = value; }
        }
        public Sudoku3 XmlSudoku12
        {
            get { return _Sudoku3[1, 2]; }
            set { _Sudoku3[1, 2] = value; }
        }
        public Sudoku3 XmlSudoku22
        {
            get { return _Sudoku3[2, 2]; }
            set { _Sudoku3[2, 2] = value; }
        }

        #endregion

        #region Constructor / Initalisation

        public Sudoku()
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    _Sudoku3[x, y] = new Sudoku3();
        }

        public Sudoku Clone()
        {
            Sudoku newsoduku = new Sudoku();
            int x, y;
            for (x = 0; x < 9; x++)
            {
                for (y = 0; y < 9; y++)
                {
                    int No = Get(x, y);
                    newsoduku.Set(x, y, No, true);
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

        public string GetUserNoteRow(int Idx)
        {
            if (_UserNoteRow==null) _UserNoteRow= new string [9];
            string ret = _UserNoteRow[Idx];
            return ret == null ? "" : ret;
        }
        public string GetUserNoteCol(int Idx)
        {
            if (_UserNoteCol== null) _UserNoteCol = new string[9];
            string ret = _UserNoteCol[Idx];
            return ret==null ? "" : ret;
        }
        public void SetUserNoteRow(int Idx, string usernote)
        {
            string old = GetUserNoteRow(Idx);
            if (old.CompareTo(usernote) != 0)
                _modified = true;
            _UserNoteRow[Idx] = usernote;
        }
        public void SetUserNoteCol(int Idx, string usernote)
        {
            string old = GetUserNoteCol(Idx);
            if (old.CompareTo(usernote) != 0)
                _modified = true;
            _UserNoteCol[Idx] = usernote;
        }

        #endregion

        #region Get Field Information

        public int Get(int x, int y)
        {
            return _Sudoku3[x / 3, y / 3].Get(x % 3, y % 3);
        }
        public SudokuField GetDef(int x, int y)
        {
            return _Sudoku3[x / 3, y / 3].GetDef(x % 3, y % 3);
        }

        #endregion

        #region Undo / Redo

        private class UndoInfo
        {
            public int x;
            public int y;
            public int No;
        };

        [NonSerialized()]
        private ArrayList _UndoList;

        public bool Undo()
        {
            if (CanUndo())
            {
                UndoInfo undo = (UndoInfo)_UndoList[_UndoList.Count - 1];
                Set(undo.x, undo.y, undo.No, true);
                _UndoList.RemoveAt(_UndoList.Count-1);
                return true;
            }
            return false;
        }
        public bool CanUndo()
        {
            return _UndoList != null && _UndoList.Count > 0;
        }

        public int GetStepCount()
        {
            int x, y;
            int StepCount = 0;
            for (x = 0; x < 9; x++)
            {
                for (y = 0; y < 9; y++)
                {
                    int No = Get(x, y);

                    if (No > 0)
                    {
                        StepCount++;
                    }
                }
            }
            return StepCount;
        }

        #endregion

        #region Calc Possibilities

        private delegate SudokuField GetSudokuField(int x, int y);
        [NonSerialized()]
        private bool[] _FoundIdx1 = new bool[9];
        [NonSerialized()]
        private bool[] _FoundIdx2 = new bool[9];

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
            return _Sudoku3[x / 3, x % 3].GetDef(y / 3, y % 3);
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
                            bool FoundOther=false;
                            for (t = 0; t < 9; t++)
                            {
                                if (t != y)
                                {
                                    SudokuField def2 = getdef(x, t);
                                    if (def2.No == 0 && def2.IsPossible(z + 1))
                                    {
                                        FoundOther=true;
                                        break;
                                    }
                                }
                            }
                            if (!FoundOther)
                            {
                                for (t = 0; t < 9; t++)
                                {
                                    if (t != z && !def.IsNotPossible(t+1))
                                    {
                                        changcount++;
                                        string reason = (z + 1).ToString() + " only in " + rowcol3;
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
                            _FoundIdx1[t] = false;

                        _FoundIdx1[y] = true;
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
                                        _FoundIdx1[t] = true;
                                        foundCount++;
                                    }
                                }
                            }
                        }

                        if (foundCount == def.PossibleCount())
                        {
                            string allToolTip=null;
                            for (t = 0; t < 9; t++)
                                if (_FoundIdx1[t])
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
                                if (!_FoundIdx1[t])
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
                            _FoundIdx1[t] = false;

                        _FoundIdx1[y] = true;

                        if (FindSubSet(getdef, x, y + 1, _FoundIdx1, _FoundIdx2))
                        {
                            string allToolTip=null;
                            for (t = 0; t < 9; t++)
                                if (_FoundIdx1[t])
                                {
                                    SudokuField def2 = getdef(x, t);
                                    if (def2.No == 0)
                                    {
                                        if (allToolTip == null)
                                        {
                                            allToolTip = string.Empty;
                                            for (z = 1; z <= 9; z++)
                                            {
                                                if (_FoundIdx2[z-1])
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
                                if (!_FoundIdx1[t])
                                {
                                    SudokuField def2 = getdef(x, t);
                                    if (def2.No == 0)
                                    {
                                        for (z = 1; z <= 9; z++)
                                        {
                                            if (_FoundIdx2[z-1] && def2.IsPossible(z))
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
                                def.mainRulePossible[z - 1] = true;
                            }
                        }
                        Set(x, y, 0, true);
                    }
                }
            }

            CommitChanges();

            string row = "row";
            string col = "col";
            string s3  = "3*3";

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

            };
        }

        #endregion

        #region Other public User Functions

        public bool Modified { get { return _modified; } }

        public bool Finish()
        {
            if (!IsValid(true))
                return false;

            return _CalcPossibleResults(-1, 9, true) == 1;
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

        [NonSerialized()]
        private bool _modified = false;

        #endregion

        #region Set and Validation

        public bool SetNextPossible(int x, int y)
        {
            SudokuField def = GetDef(x, y);

            int No = def.No;

            if (No == 0)
            {
                int Only = def.OnlyPossible();
                if (Only > 0 && Set(x, y, Only))
                {
                    return true;
                }
            }

            for (int i = 1; i <= 10; i++)
            {
                int NewNo = No + i;
                if (NewNo > 9) NewNo -= 10;
                if (Set(x, y, NewNo))
                {
                    return true;
                }
            }

            Clear(x, y);
            return false;
        }
        public bool Set(int x, int y, int No)
        {
            if (No < 0 || No > 9)
                return false;

            return Set(x, y, No, false);
        }
        public bool Clear(int x, int y)
        {
            return Set(x, y, 0, false);
        }

        private bool Set(int x, int y, int No, bool TestOnly)
        {
            int sx = x / 3;
            int sy = y / 3;
            int dx = x % 3;
            int dy = y % 3;

            int Old = _Sudoku3[sx, sy].Get(dx, dy);

            if (!_Sudoku3[sx, sy].Set(dx, dy, No))
                return false;

            if (No == 0 || IsValid(false))
            {
                if (!TestOnly)
                {
                    if (_UndoList==null) _UndoList=new ArrayList();
                    UndoInfo info = new UndoInfo();
                    info.x = x;
                    info.y = y;
                    info.No = Old;
                    _UndoList.Add(info);
                    _modified = true;
                }

                return true;
            }

            _Sudoku3[sx, sy].Set(dx, dy, Old);
            return false;
        }

        [NonSerialized()]
        bool[] _FoundX;
        [NonSerialized()]
        bool[] _FoundY;
 
        public bool IsValid(bool Test3)
        {
            if (_FoundX == null) _FoundX = new bool[9];
            if (_FoundY == null) _FoundY = new bool[9];

            int x, y;

            if (Test3)
            {
                for (x = 0; x < 3; x++)
                    for (y = 0; y < 3; y++)
                        if (!_Sudoku3[x, y].IsValid())
                            return false;
            }

            for (x = 0; x < 9; x++)
            {
                for (y = 0; y < 9; y++)
                {
                    _FoundX[y] = false;
                    _FoundY[y] = false;
                }

                for (y = 0; y < 9; y++)
                {
                    int NoX = Get(x, y);
                    if (NoX > 0)
                    {
                        if (_FoundX[NoX-1])
                            return false;
                        else
                            _FoundX[NoX-1] = true;
                    }

                    int NoY = Get(y, x);
                    if (NoY > 0)
                    {
                        if (_FoundY[NoY - 1])
                            return false;
                        else
                            _FoundY[NoY - 1] = true;
                    }
                }
            }
            return true;
        }

        #endregion

        #region Load/Save

        public bool SaveXml(string FileName)
        {
            using (TextWriter fs = new StreamWriter(FileName))
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

        private static Sudoku LoadXml(string FileName)
        {
            Sudoku sudoku = null;
            // Open the file containing the data that you want to deserialize.
            using (TextReader fs = new StreamReader(FileName))
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

        public static Sudoku Load(string FileName)
        {
            return LoadXml(FileName);
        }

        #endregion

        #region Debug Help Functions

        private void ConsolOut()
        {
            return;
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

        #region Calc Possibile Results (BackTracking)

        private static bool _EndCalc;
        private static int _PossibleSolutions;
        public static int PossibleSolutions { get { return _PossibleSolutions; } }
        public void StopCalcPossibleResults()
        {
            _EndCalc = true;
        }

        public static void InitCalcPossibleResults()
        {
            _EndCalc = false;
        }

        public int CalcPossibleResults()
        {
            Sudoku._PossibleSolutions = 0;

            if (!IsValid(true))
                return 0;

            Sudoku tmp = Clone();
            Sudoku._PossibleSolutions = 0;
            return tmp._CalcPossibleResults(-1, 9, false);
        }
        private int _CalcPossibleResults(int x, int y, bool calcEnd)
        {
            if (!calcEnd && _EndCalc)
                return -1;

            int Results = 0;
            int myx;
            int myy;
            y++;
            if (y >= 9)
            {
                x++; y = 0;
            }

            if (x > 8)
            {
                _PossibleSolutions = _PossibleSolutions > 1 ? _PossibleSolutions : 1;
#if DEBUG
                ConsolOut();
#endif
                return 1;
            }

            for (myx = x; myx < 9; myx++)
            {
                for (myy = y; myy < 9; myy++)
                {
                    int No = Get(myx, myy);

                    if (No == 0)
                    {
                        int Old = No;
                        for (int NewNo = 1; NewNo <= 9; NewNo++)
                        {
                            if (Set(myx, myy, NewNo, true))
                            {
                                int Res = _CalcPossibleResults(myx, myy,calcEnd);
                                if (Res < 0)
                                {
                                    if (!calcEnd)
                                        Set(myx, myy, 0, true);
                                    return Res;     // abort
                                }

                                if (Res > 0)
                                {
                                    if (calcEnd)
                                    {
                                        return 1;
                                    }
                                    //if (!Findall)
                                    //{
                                    //    if (!calcEnd)
                                    //        Set(myx, myy, 0, true);
                                    //    return 1;
                                    //}
                                    Results += Res;
                                    _PossibleSolutions = _PossibleSolutions > Results ? _PossibleSolutions : Results;
                                }
                            }
                        }
                        Set(myx, myy, 0, true);
                        return Results;
                    }
                }
                y = 0;
            }
            if (GetStepCount() >= 9*9)
            {
                _PossibleSolutions = _PossibleSolutions > 1 ? _PossibleSolutions : 1;
#if DEBUG
                ConsolOut();
#endif
                return 1;
            }
            return 0;
        }
        #endregion
    }

    public struct SudokuOptions
    {
        public bool help;
        public bool showooltip;
    };
}
