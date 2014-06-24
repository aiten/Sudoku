using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Configuration;

namespace Framework.Server.Repository
{
    public class FilterBuilder : IDisposable
    {
        OleDbCommand _cmd;
        string _oldCommandText;
        List<OleDbParameter> _paramlist = new List<OleDbParameter>(); 

        public FilterBuilder(OleDbCommand cmd)
        {
            _cmd = cmd;
            _oldCommandText = _cmd.CommandText;
        }

        public FilterBuilder(OleDbDataAdapter da)
        {
            _cmd = da.SelectCommand;
            _oldCommandText = _cmd.CommandText;
        }

        public void AddWithValue(string filtername, string filtervalue)
        {
            OleDbParameter param = _cmd.Parameters.AddWithValue(filtername, filtervalue);
            _paramlist.Add(param);
        }

        public void AddWithValue(string filtername, int filtervalue)
        {
            OleDbParameter param = _cmd.Parameters.AddWithValue(filtername, filtervalue);
            _paramlist.Add(param);
        }

        public void AddWithValue(string filtername, DateTime filtervalue)
        {
            OleDbParameter param = _cmd.Parameters.AddWithValue(filtername, filtervalue);
            _paramlist.Add(param);
        }

        public void Dispose()
        {
            if (_cmd != null)
            {
                _cmd.CommandText = _oldCommandText;
                foreach (OleDbParameter param in _paramlist)
                {
                    _cmd.Parameters.Remove(param);
                }
                _cmd = null;
            }
        }
    }
}
