using System;
using System.Drawing;
using System.Text;

namespace Sudoku.Solve
{
    public class SudokuField
    {
        #region Data

        public int No
        {
            get { return _no; }
        }
        internal void SetNo(int no)
        {
            _no = no;
        }

        public string UserNote
        {
            get
            {
                string ret = _userNote;
                return ret == null ? "" : ret;
            }
        }
        internal void SetUserNote(string userNote)
        {
            _userNote = userNote;
        }

        private int _no;
        private string _userNote;

        #endregion
        
        #region For XML Serialization

        public string XmlUserNote
        {
            get { return _userNote; }
            set { _userNote = value; }
        }
        public int XmlNo
        {
            get { return _no; }
            set { _no = value; }
        }

        #endregion

        #region Calculation Help Variabled

        [NonSerialized]
        private bool[] _mainRulePossible = new bool[9];
        [NonSerialized]
        private bool[] _notPossible = new bool[9];
        [NonSerialized]
        private bool[] _notPossibleChanged = new bool[9];
        [NonSerialized]
        private string[] _notPossibleReason = new string[9];

        [System.Xml.Serialization.XmlIgnoreAttribute]
        internal bool[] MainRulePossible
        {
            get { return _mainRulePossible; }
        }

        #endregion 

        #region Private Help Functions

        private int PossibleCount(bool[] ar)
        {
            int count = 0;
            if (No == 0)
            {
                for (int x = 0; x < 9; x++)
                    if (ar[x])
                        count++;
            }
            return count;
        }
/*
        private int GetNextPossible(bool[] ar, int startNo)
        {
            for (int x = startNo; x < 9; x++)
                if (ar[x])
                    return x + 1;
            return 0;
        }
*/
        #endregion

        #region External Access functions
 
        internal void InitHelpVar()
        {
            _no = No;
            _userNote = UserNote;

            if (_notPossibleReason == null)
                _notPossibleReason = new string[9];

            if (_mainRulePossible == null)
                _mainRulePossible = new bool[9];
            if (_notPossible == null)
                _notPossible = new bool[9];

            for (int z = 0; z < 9; z++)
            {
                _mainRulePossible[z]= false;
                _notPossible[z]     = false;
            }

            _notPossibleChanged = null;
        }

/*
        private bool IsPossibleMainRule(int no)
        {
            int idx = no - 1;
            return _mainRulePossible[idx];
        }
*/

        internal bool IsPossible(int no)
        {
            int idx = no-1;
            return _mainRulePossible[idx] && !_notPossible[idx];
        }

        internal bool IsNotPossible(int no)
        {
            int idx = no - 1;
            return _notPossible[idx];
        }

        internal bool IsSubSetPossible(SudokuField field)
        {
            for (int z = 1; z <= 9; z++)
            {
                if (IsPossible(z) != field.IsPossible(z))
                {
                    if (!IsPossible(z))
                        return false;
                }
            }
            return true;
        }

        internal void SetNotPossible(int no, string reason)
        {
            if (_notPossibleChanged == null)
            {
                _notPossibleChanged = new bool[9];
                for (int z = 0; z < 9; z++)
                {
                    _notPossibleChanged[z] = _notPossible[z];
                }
            }
            int idx = no - 1;
            _notPossibleChanged[idx] = true;
            _notPossibleReason[idx]  = reason;
        }

        internal void CommitChanges()
        {
            if (_notPossibleChanged != null)
                _notPossible = _notPossibleChanged;
            _notPossibleChanged = null;
        }

        public int PossibleCount()
        {
            int x;
            int ret = 0;
            for (x = 1; x <= 9; x++)
            {
                if (IsPossible(x))
                {
                    ret++;
                }
            }
            return ret;
        }

        public int OnlyPossible()
        {
            int x;
            int ret = 0;
            for (x = 1; x <= 9; x++)
            {
                if (IsPossible(x))
                {
                    if (ret != 0)
                        return 0;
                    ret = x;
                }
            }
            return ret;
        }

        public int MainRulePossibleCount()
        {
            return PossibleCount(_mainRulePossible);
        }

        #endregion

        #region Frame Output

        public Color ToButtonColor(Sudoku.SudokuOptions opt)
        {
            if (No != 0)
                return Color.Green;

            if (opt.Help)
            {
                switch (PossibleCount())
                {
                    default:    return Color.Gray;
                    case 0:     return Color.Red;
                    case 1:     return Color.LightGray;
                }
            }
            return Color.Gray;
        }

        private string ToButtonStringMainRuleOnly()
        {
            if (No > 0)
            {
                return No.ToString();
            }

            StringBuilder str2 = new StringBuilder();
            int z;

            for (z = 0; z < 9; z++)
            {
                if (MainRulePossible[z])
                {
                    if (str2.Length > 0)
                        str2.Append(",");
                    str2.Append((z + 1).ToString());
                }
            }
            return str2.ToString();
        }

        public string ToButtonToolTip(Sudoku.SudokuOptions opt)
        {
            if (!opt.ShowToolTip)
                return "";

            if (opt.Help)
            {
                string ret = ToButtonString(opt);
                string reason=null;
                if (No == 0)
                {
                    int z;
                    for (z = 0; z < 9; z++)
                    {
                        if (MainRulePossible[z])
                        {
                            if (_notPossible[z])
                            {
                                if (reason != null)
                                    reason = reason + "\n";
                                else 
                                    reason = "\n";
                                reason = reason + (z+1).ToString();
                                reason = reason + ": ";
                                reason = reason + _notPossibleReason[z];
                            }
                        }
                    }
                }
                if (reason==null)
                    return ret;
                return ret + reason;
            }

            return ToButtonStringMainRuleOnly(); 
        }

        public string PossibleString()
        {
            int z;
            StringBuilder str1 = new StringBuilder();

            for (z = 0; z < 9; z++)
            {
                if (IsPossible(z+1))
                {
                    if (str1.Length > 0)
                        str1.Append(",");
                    str1.Append((z + 1).ToString());
                }
            }
            return str1.ToString();
        }

        public string ToButtonString(Sudoku.SudokuOptions opt)
        {
            if (No > 0)
            {
                return No.ToString();
            }
            
            if (opt.Help)
            {
                int z;
                StringBuilder str1 = new StringBuilder();
                StringBuilder str2 = new StringBuilder();

                for (z = 0; z < 9; z++)
                {
                    if (MainRulePossible[z])
                    {
                        if (_notPossible[z])
                        {
                            if (str1.Length > 0)
                                str1.Append(",");
                            str1.Append((z + 1).ToString());
                        }
                        else
                        {
                            if (str2.Length > 0)
                                str2.Append(",");
                            str2.Append((z + 1).ToString());
                        }
                    }
                }

                if (str1.Length > 0)
                {
                    if (str2.Length > 0)
                        return str2 + " - " + str1;
                    
                    return str1.ToString();
                }
                return str2.ToString(); 
            }
            
            return UserNote;
        }

        #endregion
    }
}
