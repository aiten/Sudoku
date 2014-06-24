namespace Sudoku.Forms
{
    partial class OptionForm
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
            this._Help = new System.Windows.Forms.CheckBox();
            this._ShowToolTip = new System.Windows.Forms.CheckBox();
            this._OK = new System.Windows.Forms.Button();
            this._Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _Help
            // 
            this._Help.AutoSize = true;
            this._Help.Location = new System.Drawing.Point(12, 13);
            this._Help.Name = "_Help";
            this._Help.Size = new System.Drawing.Size(48, 17);
            this._Help.TabIndex = 0;
            this._Help.Text = "Help";
            this._Help.UseVisualStyleBackColor = true;
            // 
            // _ShowToolTip
            // 
            this._ShowToolTip.AutoSize = true;
            this._ShowToolTip.Location = new System.Drawing.Point(12, 36);
            this._ShowToolTip.Name = "_ShowToolTip";
            this._ShowToolTip.Size = new System.Drawing.Size(120, 17);
            this._ShowToolTip.TabIndex = 1;
            this._ShowToolTip.Text = "Show button tool tip";
            this._ShowToolTip.UseVisualStyleBackColor = true;
            // 
            // _OK
            // 
            this._OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._OK.Location = new System.Drawing.Point(205, 12);
            this._OK.Name = "_OK";
            this._OK.Size = new System.Drawing.Size(75, 23);
            this._OK.TabIndex = 2;
            this._OK.Text = "OK";
            this._OK.UseVisualStyleBackColor = true;
            // 
            // _Cancel
            // 
            this._Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._Cancel.Location = new System.Drawing.Point(205, 43);
            this._Cancel.Name = "_Cancel";
            this._Cancel.Size = new System.Drawing.Size(75, 23);
            this._Cancel.TabIndex = 3;
            this._Cancel.Text = "Cancel";
            this._Cancel.UseVisualStyleBackColor = true;
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 73);
            this.Controls.Add(this._Cancel);
            this.Controls.Add(this._OK);
            this.Controls.Add(this._ShowToolTip);
            this.Controls.Add(this._Help);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options ...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox _Help;
        private System.Windows.Forms.CheckBox _ShowToolTip;
        private System.Windows.Forms.Button _OK;
        private System.Windows.Forms.Button _Cancel;
    }
}