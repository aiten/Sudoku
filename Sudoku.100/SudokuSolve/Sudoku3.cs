using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using System.Xml.Serialization;

namespace SudokuSolve
{
    public class Sudoku3
    {
        #region Constructor / Initalisation
        public Sudoku3()
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                {
                    _Fields[x, y] = new SudokuField();
                    _Fields[x, y].SetNo(0);

                }
        }
        #endregion

        private SudokuField[,] _Fields = new SudokuField[3, 3];

        #region For XML Serialization

        public SudokuField XmlSudoku00
        {
            get { return _Fields[0, 0]; }
            set { _Fields[0, 0] = value; }
        }
        public SudokuField XmlSudoku10
        {
            get { return _Fields[1, 0]; }
            set { _Fields[1, 0] = value; }
        }
        public SudokuField XmlSudoku20
        {
            get { return _Fields[2, 0]; }
            set { _Fields[2, 0] = value; }
        }
        public SudokuField XmlSudoku01
        {
            get { return _Fields[0, 1]; }
            set { _Fields[0, 1] = value; }
        }
        public SudokuField XmlSudoku11
        {
            get { return _Fields[1, 1]; }
            set { _Fields[1, 1] = value; }
        }
        public SudokuField XmlSudoku21
        {
            get { return _Fields[2, 1]; }
            set { _Fields[2, 1] = value; }
        }
        public SudokuField XmlSudoku02
        {
            get { return _Fields[0, 2]; }
            set { _Fields[0, 2] = value; }
        }
        public SudokuField XmlSudoku12
        {
            get { return _Fields[1, 2]; }
            set { _Fields[1, 2] = value; }
        }
        public SudokuField XmlSudoku22
        {
            get { return _Fields[2, 2]; }
            set { _Fields[2, 2] = value; }
        }

        #endregion

        #region Get Fieldinformation

        public int Get(int x ,int y)
        {
            return _Fields[x, y].No;
        }
        public SudokuField GetDef(int x ,int y)
        {
            return _Fields[x, y];
        }

        #endregion

        #region Set and Validation

        [NonSerialized()]
        private bool[] _Found = new bool[9];

        public bool IsValid()
        {
            if (_Found == null)
                _Found = new bool[9];

            int x, y;
            for (y = 0; y < 9; y++)
                _Found[y] = false;

            for (x = 0; x < 3; x++)
                for (y = 0; y < 3; y++)
                {
                    int No = _Fields[x, y].No;
                    if (No > 0)
                    {
                        if (_Found[No-1])
                            return false;
                        _Found[No-1] = true;
                    }
                }
            return true;
        }

        public bool Set(int x, int y, int No)
        {
            int old = _Fields[x, y].No;
            _Fields[x, y].SetNo(No);

            if (IsValid())
                return true;

            _Fields[x, y].SetNo(old);
            return false;
        }

        #endregion
    }
}
