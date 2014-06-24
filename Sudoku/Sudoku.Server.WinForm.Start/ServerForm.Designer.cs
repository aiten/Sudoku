namespace Sudoku.Server.WinForm.Start
{
    partial class ServerForm
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
            this._stop = new System.Windows.Forms.Button();
            this._start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _stop
            // 
            this._stop.Location = new System.Drawing.Point(35, 34);
            this._stop.Name = "_stop";
            this._stop.Size = new System.Drawing.Size(75, 23);
            this._stop.TabIndex = 0;
            this._stop.Text = "Stop";
            this._stop.UseVisualStyleBackColor = true;
            this._stop.Click += new System.EventHandler(this._stop_Click);
            // 
            // _start
            // 
            this._start.Location = new System.Drawing.Point(140, 34);
            this._start.Name = "_start";
            this._start.Size = new System.Drawing.Size(75, 23);
            this._start.TabIndex = 1;
            this._start.Text = "Start";
            this._start.UseVisualStyleBackColor = true;
            this._start.Click += new System.EventHandler(this._start_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 88);
            this.Controls.Add(this._start);
            this.Controls.Add(this._stop);
            this.Name = "ServerForm";
            this.Text = "Server";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _stop;
        private System.Windows.Forms.Button _start;
    }
}

