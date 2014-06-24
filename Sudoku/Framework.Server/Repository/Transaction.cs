using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Configuration;
using System.Data.Common;

namespace Framework.Server.Repository
{
    public class Transaction : IDisposable
    {
        SqlCeConnection _con = new SqlCeConnection();
        SqlCeTransaction _trans;

        public SqlCeConnection Connection { get { return _con; } }

        public Transaction()
        {
            BaseDA.OpenConnection(_con);
            _trans = _con.BeginTransaction();
        }

        public Transaction(string connectionstring)
        {
            _con.ConnectionString = connectionstring;
            _con.Open();
            _trans = _con.BeginTransaction();
        }

        public void InitCommand(DbCommand cmd)
        {
            if (cmd != null)
            {
                cmd.Connection = _con;
                cmd.Transaction = _trans;
            }
        }

        public void InitApapter(DbDataAdapter da)
        {
            InitCommand(da.SelectCommand);
            InitCommand(da.InsertCommand);
            InitCommand(da.DeleteCommand);
            InitCommand(da.UpdateCommand);
        }

        public void Commit()
        {
            _trans.Commit();
            _trans = null;
        }

        public void Dispose()
        {
            if (_con.State == System.Data.ConnectionState.Open)
                _con.Close();
            _con = null;
        }
    }
}
