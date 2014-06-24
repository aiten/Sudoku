using System;

namespace Sudoku.Solve
{
    public class Sudoku3
    {
        #region Constructor / Initalisation
        public Sudoku3()
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                {
                    _fields[x, y] = new SudokuField();
                    _fields[x, y].SetNo(0);

                }
        }
        #endregion

        private readonly SudokuField[,] _fields = new SudokuField[3, 3];

        #region For XML Serialization

        public SudokuField XmlSudoku00
        {
            get { return _fields[0, 0]; }
            set { _fields[0, 0] = value; }
        }
        public SudokuField XmlSudoku10
        {
            get { return _fields[1, 0]; }
            set { _fields[1, 0] = value; }
        }
        public SudokuField XmlSudoku20
        {
            get { return _fields[2, 0]; }
            set { _fields[2, 0] = value; }
        }
        public SudokuField XmlSudoku01
        {
            get { return _fields[0, 1]; }
            set { _fields[0, 1] = value; }
        }
        public SudokuField XmlSudoku11
        {
            get { return _fields[1, 1]; }
            set { _fields[1, 1] = value; }
        }
        public SudokuField XmlSudoku21
        {
            get { return _fields[2, 1]; }
            set { _fields[2, 1] = value; }
        }
        public SudokuField XmlSudoku02
        {
            get { return _fields[0, 2]; }
            set { _fields[0, 2] = value; }
        }
        public SudokuField XmlSudoku12
        {
            get { return _fields[1, 2]; }
            set { _fields[1, 2] = value; }
        }
        public SudokuField XmlSudoku22
        {
            get { return _fields[2, 2]; }
            set { _fields[2, 2] = value; }
        }

        #endregion

        #region Get Fieldinformation

        internal int Get(int x ,int y)
        {
            return _fields[x, y].No;
        }
        internal SudokuField GetDef(int x, int y)
        {
            return _fields[x, y];
        }

        #endregion

        #region Set and Validation

        [NonSerialized]
        private bool[] _found = new bool[9];

        public bool IsValid()
        {
            if (_found == null)
                _found = new bool[9];

            int x, y;
            for (y = 0; y < 9; y++)
                _found[y] = false;

            for (x = 0; x < 3; x++)
                for (y = 0; y < 3; y++)
                {
                    int no = _fields[x, y].No;
                    if (no > 0)
                    {
                        if (_found[no-1])
                            return false;
                        _found[no-1] = true;
                    }
                }
            return true;
        }

        public bool Set(int x, int y, int no)
        {
            int old = _fields[x, y].No;
            _fields[x, y].SetNo(no);

            if (IsValid())
                return true;

            _fields[x, y].SetNo(old);
            return false;
        }

        #endregion
    }
}
