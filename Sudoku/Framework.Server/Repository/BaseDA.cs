using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.OleDb;
using System.Data.Common;
using System.Data;

namespace Framework.Server.Repository
{
    public partial class BaseDA : Component
    {
        public BaseDA()
        {
            InitializeComponent();
        }

        public BaseDA(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public Transaction Transaction 
        { 
            get; 
            set; 
        }

        public void OpenConnection()
        {
            OpenConnection(_connection);
        }

        public static void OpenConnection(IDbConnection con)
        {
            if (con.State != System.Data.ConnectionState.Open)
            {
                string mdbfile = ConfigurationManager.AppSettings["MDBFile"];
                con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + mdbfile + ";OLE DB Services=-1";
                con.Open();
            }
        }

        public void InitCommand(DbCommand cmd)
        {
            if (cmd != null)
            {
                if (Transaction==null)
                {
                    OpenConnection();
                    cmd.Connection = _connection;
                }
                else
                {
                    Transaction.InitCommand(cmd);
                }
            }
        }

        public void InitApapter(DbDataAdapter da)
        {
            InitCommand(da.SelectCommand);
            InitCommand(da.InsertCommand);
            InitCommand(da.DeleteCommand);
            InitCommand(da.UpdateCommand);
        }

        public TRetType ExecuteScalar<TRetType>(DbCommand cmd, TRetType retIfNull)
        {
            InitCommand(cmd);
            object ret = cmd.ExecuteScalar();

            if (ret == null || ret is DBNull)
                return retIfNull;

            return (TRetType)ret;
        }


        public void SetPrimaryID_RowUpdated(object sender, System.Data.OleDb.OleDbRowUpdatedEventArgs e)
        {
            if (e.Status == UpdateStatus.Continue && e.StatementType == StatementType.Insert)
            {
                // Get the Identity column value
                e.Row["ID"] = Int32.Parse(_getIdentity.ExecuteScalar().ToString());
                e.Row.AcceptChanges();
            }
        }

    }
}
