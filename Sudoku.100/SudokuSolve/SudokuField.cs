using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SudokuSolve
{
    public class SudokuField
    {
        #region Data

        public int No
        {
            get { return _No; }
        }
        public void SetNo(int No)
        {
            _No = No;
        }

        public string UserNote
        {
            get
            {
                string ret = _UserNote;
                return ret == null ? "" : ret;
            }
        }
        public void SetUserNote(string userNote)
        {
            _UserNote = userNote;
        }

        private int _No;
        private string _UserNote = null;

        #endregion
        
        #region For XML Serialization

        public string XmlUserNote
        {
            get { return _UserNote; }
            set { _UserNote = value; }
        }
        public int XmlNo
        {
            get { return _No; }
            set { _No = value; }
        }

        #endregion

        #region Calculation Help Variabled

        [NonSerialized()]
        private bool[] _mainRulePossible = new bool[9];
        [NonSerialized()]
        private bool[] _notPossible = new bool[9];
        [NonSerialized()]
        private bool[] _notPossibleChanged = new bool[9];
        [NonSerialized()]
        private string[] _notPossibleReason = new string[9];

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool[] mainRulePossible
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
        private int GetNextPossible(bool[] ar, int StartNo)
        {
            for (int x = StartNo; x < 9; x++)
                if (ar[x])
                    return x + 1;
            return 0;
        }
        #endregion

        #region External Access functions
 
        public void InitHelpVar()
        {
            _No = No;
            _UserNote = UserNote;

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

        public bool IsPossibleMainRule(int No)
        {
            int Idx = No - 1;
            return _mainRulePossible[Idx];
        }

        public bool IsPossible(int No)
        {
            int Idx = No-1;
            return _mainRulePossible[Idx] && !_notPossible[Idx];
        }

        public bool IsNotPossible(int No)
        {
            int Idx = No - 1;
            return _notPossible[Idx];
        }

        public bool IsSubSetPossible(SudokuField field)
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

        public void SetNotPossible(int No, string reason)
        {
            if (_notPossibleChanged == null)
            {
                _notPossibleChanged = new bool[9];
                for (int z = 0; z < 9; z++)
                {
                    _notPossibleChanged[z] = _notPossible[z];
                }
            }
            int Idx = No - 1;
            _notPossibleChanged[Idx] = true;
            _notPossibleReason[Idx]  = reason;
        }

        public void CommitChanges()
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

        public Color ToButtonColor(SudokuOptions opt)
        {
            if (No != 0)
                return Color.Green;

            if (opt.help)
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
                if (mainRulePossible[z])
                {
                    if (str2.Length > 0)
                        str2.Append(",");
                    str2.Append((z + 1).ToString());
                }
            }
            return str2.ToString();
        }

        public string ToButtonToolTip(SudokuOptions opt)
        {
            if (!opt.showooltip)
                return "";

            if (opt.help)
            {
                string ret = ToButtonString(opt);
                string reason=null;
                if (No == 0)
                {
                    int z;
                    for (z = 0; z < 9; z++)
                    {
                        if (mainRulePossible[z])
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
            StringBuilder str2 = new StringBuilder();

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

        public string ToButtonString(SudokuOptions opt)
        {
            if (No > 0)
            {
                return No.ToString();
            }
            else if (opt.help)
            {
                int z;
                StringBuilder str1 = new StringBuilder();
                StringBuilder str2 = new StringBuilder();

                for (z = 0; z < 9; z++)
                {
                    if (mainRulePossible[z])
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
                        return str2.ToString() + " - " + str1.ToString();
                    else
                        return str1.ToString();
                }
                return str2.ToString(); 
            }
            else
            {
                return UserNote;
            }
        }
        #endregion
    }
}
