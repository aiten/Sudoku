namespace SudokuGui
{
    partial class EnterFieldForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._OK = new System.Windows.Forms.Button();
            this._Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._UserNote = new System.Windows.Forms.TextBox();
            this._No = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // _OK
            // 
            this._OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._OK.Location = new System.Drawing.Point(27, 204);
            this._OK.Name = "_OK";
            this._OK.Size = new System.Drawing.Size(75, 23);
            this._OK.TabIndex = 2;
            this._OK.Text = "OK";
            // 
            // _Cancel
            // 
            this._Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._Cancel.Location = new System.Drawing.Point(130, 204);
            this._Cancel.Name = "_Cancel";
            this._Cancel.Size = new System.Drawing.Size(75, 23);
            this._Cancel.TabIndex = 3;
            this._Cancel.Text = "Cancel";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(21, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.Text = "Value";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(21, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 18);
            this.label2.Text = "User-Note";
            // 
            // _UserNote
            // 
            this._UserNote.Location = new System.Drawing.Point(109, 168);
            this._UserNote.Name = "_UserNote";
            this._UserNote.Size = new System.Drawing.Size(106, 21);
            this._UserNote.TabIndex = 1;
            // 
            // _No
            // 
            this._No.Items.Add("<n/a>");
            this._No.Items.Add("1");
            this._No.Items.Add("2");
            this._No.Items.Add("3");
            this._No.Items.Add("4");
            this._No.Items.Add("5");
            this._No.Items.Add("6");
            this._No.Items.Add("7");
            this._No.Items.Add("8");
            this._No.Items.Add("9");
            this._No.Location = new System.Drawing.Point(109, 15);
            this._No.Name = "_No";
            this._No.Size = new System.Drawing.Size(53, 142);
            this._No.TabIndex = 0;
            // 
            // EnterFieldForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(240, 240);
            this.Controls.Add(this._No);
            this.Controls.Add(this._UserNote);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._Cancel);
            this.Controls.Add(this._OK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnterFieldForm";
            this.Text = "Enter";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _OK;
        private System.Windows.Forms.Button _Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _UserNote;
        private System.Windows.Forms.ListBox _No;
    }
}