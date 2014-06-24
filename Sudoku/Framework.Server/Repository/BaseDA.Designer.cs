using System.ComponentModel;

namespace Framework.Server.Repository
{
    public partial class BaseDA : Component
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this._connection = new System.Data.SqlServerCe.SqlCeConnection();
            this._getIdentity = new System.Data.SqlServerCe.SqlCeCommand();
            // 
            // _connection
            // 
            this._connection.ConnectionString = @"Data Source=C:\User\Herbert\Schule\CSharp\Beispiele\Enterprise\Version6\Claculator.sdf";
            // 
            // _getIdentity
            // 
            this._getIdentity.CommandText = "SELECT @@IDENTITY";
            this._getIdentity.Connection = this._connection;

        }

        #endregion

        protected System.Data.SqlServerCe.SqlCeConnection _connection;
        protected System.Data.SqlServerCe.SqlCeCommand _getIdentity;

    }
}
