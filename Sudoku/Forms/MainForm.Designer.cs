namespace Sudoku.Forms
{
    partial class MainForm
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
            StopThread();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._11 = new System.Windows.Forms.Button();
            this._12 = new System.Windows.Forms.Button();
            this._13 = new System.Windows.Forms.Button();
            this._14 = new System.Windows.Forms.Button();
            this._15 = new System.Windows.Forms.Button();
            this._16 = new System.Windows.Forms.Button();
            this._17 = new System.Windows.Forms.Button();
            this._18 = new System.Windows.Forms.Button();
            this._19 = new System.Windows.Forms.Button();
            this._29 = new System.Windows.Forms.Button();
            this._28 = new System.Windows.Forms.Button();
            this._27 = new System.Windows.Forms.Button();
            this._26 = new System.Windows.Forms.Button();
            this._25 = new System.Windows.Forms.Button();
            this._24 = new System.Windows.Forms.Button();
            this._23 = new System.Windows.Forms.Button();
            this._22 = new System.Windows.Forms.Button();
            this._21 = new System.Windows.Forms.Button();
            this._49 = new System.Windows.Forms.Button();
            this._48 = new System.Windows.Forms.Button();
            this._47 = new System.Windows.Forms.Button();
            this._46 = new System.Windows.Forms.Button();
            this._45 = new System.Windows.Forms.Button();
            this._44 = new System.Windows.Forms.Button();
            this._43 = new System.Windows.Forms.Button();
            this._42 = new System.Windows.Forms.Button();
            this._41 = new System.Windows.Forms.Button();
            this._39 = new System.Windows.Forms.Button();
            this._38 = new System.Windows.Forms.Button();
            this._37 = new System.Windows.Forms.Button();
            this._36 = new System.Windows.Forms.Button();
            this._35 = new System.Windows.Forms.Button();
            this._34 = new System.Windows.Forms.Button();
            this._33 = new System.Windows.Forms.Button();
            this._32 = new System.Windows.Forms.Button();
            this._31 = new System.Windows.Forms.Button();
            this._89 = new System.Windows.Forms.Button();
            this._88 = new System.Windows.Forms.Button();
            this._87 = new System.Windows.Forms.Button();
            this._86 = new System.Windows.Forms.Button();
            this._85 = new System.Windows.Forms.Button();
            this._84 = new System.Windows.Forms.Button();
            this._83 = new System.Windows.Forms.Button();
            this._82 = new System.Windows.Forms.Button();
            this._81 = new System.Windows.Forms.Button();
            this._79 = new System.Windows.Forms.Button();
            this._78 = new System.Windows.Forms.Button();
            this._77 = new System.Windows.Forms.Button();
            this._76 = new System.Windows.Forms.Button();
            this._75 = new System.Windows.Forms.Button();
            this._74 = new System.Windows.Forms.Button();
            this._73 = new System.Windows.Forms.Button();
            this._72 = new System.Windows.Forms.Button();
            this._71 = new System.Windows.Forms.Button();
            this._69 = new System.Windows.Forms.Button();
            this._68 = new System.Windows.Forms.Button();
            this._67 = new System.Windows.Forms.Button();
            this._66 = new System.Windows.Forms.Button();
            this._65 = new System.Windows.Forms.Button();
            this._64 = new System.Windows.Forms.Button();
            this._63 = new System.Windows.Forms.Button();
            this._62 = new System.Windows.Forms.Button();
            this._61 = new System.Windows.Forms.Button();
            this._59 = new System.Windows.Forms.Button();
            this._58 = new System.Windows.Forms.Button();
            this._57 = new System.Windows.Forms.Button();
            this._56 = new System.Windows.Forms.Button();
            this._55 = new System.Windows.Forms.Button();
            this._54 = new System.Windows.Forms.Button();
            this._53 = new System.Windows.Forms.Button();
            this._52 = new System.Windows.Forms.Button();
            this._51 = new System.Windows.Forms.Button();
            this._99 = new System.Windows.Forms.Button();
            this._98 = new System.Windows.Forms.Button();
            this._97 = new System.Windows.Forms.Button();
            this._96 = new System.Windows.Forms.Button();
            this._95 = new System.Windows.Forms.Button();
            this._94 = new System.Windows.Forms.Button();
            this._93 = new System.Windows.Forms.Button();
            this._92 = new System.Windows.Forms.Button();
            this._91 = new System.Windows.Forms.Button();
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.printpreviewtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sudokuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mirrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._IC1 = new System.Windows.Forms.TextBox();
            this._IC2 = new System.Windows.Forms.TextBox();
            this._IC3 = new System.Windows.Forms.TextBox();
            this._IC4 = new System.Windows.Forms.TextBox();
            this._IC5 = new System.Windows.Forms.TextBox();
            this._IC6 = new System.Windows.Forms.TextBox();
            this._IC7 = new System.Windows.Forms.TextBox();
            this._IC8 = new System.Windows.Forms.TextBox();
            this._IC9 = new System.Windows.Forms.TextBox();
            this._IR1 = new System.Windows.Forms.TextBox();
            this._IR2 = new System.Windows.Forms.TextBox();
            this._IR3 = new System.Windows.Forms.TextBox();
            this._IR4 = new System.Windows.Forms.TextBox();
            this._IR5 = new System.Windows.Forms.TextBox();
            this._IR6 = new System.Windows.Forms.TextBox();
            this._IR7 = new System.Windows.Forms.TextBox();
            this._IR8 = new System.Windows.Forms.TextBox();
            this._IR9 = new System.Windows.Forms.TextBox();
            this._statusStrip = new System.Windows.Forms.StatusStrip();
            this._toolStripStatusMoveLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._toolStripStatusMove = new System.Windows.Forms.ToolStripStatusLabel();
            this._toolStripStatusPossibleSolutionsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._toolStripStatusPossibleSolutions = new System.Windows.Forms.ToolStripStatusLabel();
            this._toolStripStatusPossibleSolutions1 = new System.Windows.Forms.ToolStripStatusLabel();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.optionstoolStripButton = new System.Windows.Forms.ToolStripButton();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._lc1 = new System.Windows.Forms.Label();
            this._lc2 = new System.Windows.Forms.Label();
            this._lc3 = new System.Windows.Forms.Label();
            this._lc4 = new System.Windows.Forms.Label();
            this._lc5 = new System.Windows.Forms.Label();
            this._lc6 = new System.Windows.Forms.Label();
            this._lc7 = new System.Windows.Forms.Label();
            this._lc8 = new System.Windows.Forms.Label();
            this._lc9 = new System.Windows.Forms.Label();
            this._lr1 = new System.Windows.Forms.Label();
            this._lr2 = new System.Windows.Forms.Label();
            this._lr3 = new System.Windows.Forms.Label();
            this._lr4 = new System.Windows.Forms.Label();
            this._lr5 = new System.Windows.Forms.Label();
            this._lr6 = new System.Windows.Forms.Label();
            this._lr7 = new System.Windows.Forms.Label();
            this._lr8 = new System.Windows.Forms.Label();
            this._lr9 = new System.Windows.Forms.Label();
            this._menuStrip.SuspendLayout();
            this._statusStrip.SuspendLayout();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _11
            // 
            this._11.Location = new System.Drawing.Point(15, 63);
            this._11.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._11.Name = "_11";
            this._11.Size = new System.Drawing.Size(70, 50);
            this._11.TabIndex = 0;
            this._11.UseVisualStyleBackColor = true;
            // 
            // _12
            // 
            this._12.Location = new System.Drawing.Point(84, 63);
            this._12.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._12.Name = "_12";
            this._12.Size = new System.Drawing.Size(70, 50);
            this._12.TabIndex = 2;
            this._12.UseVisualStyleBackColor = true;
            // 
            // _13
            // 
            this._13.Location = new System.Drawing.Point(152, 63);
            this._13.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._13.Name = "_13";
            this._13.Size = new System.Drawing.Size(70, 50);
            this._13.TabIndex = 3;
            this._13.UseVisualStyleBackColor = true;
            // 
            // _14
            // 
            this._14.Location = new System.Drawing.Point(233, 63);
            this._14.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._14.Name = "_14";
            this._14.Size = new System.Drawing.Size(70, 50);
            this._14.TabIndex = 4;
            this._14.UseVisualStyleBackColor = true;
            // 
            // _15
            // 
            this._15.Location = new System.Drawing.Point(302, 63);
            this._15.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._15.Name = "_15";
            this._15.Size = new System.Drawing.Size(70, 50);
            this._15.TabIndex = 5;
            this._15.UseVisualStyleBackColor = true;
            // 
            // _16
            // 
            this._16.Location = new System.Drawing.Point(370, 63);
            this._16.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._16.Name = "_16";
            this._16.Size = new System.Drawing.Size(70, 50);
            this._16.TabIndex = 6;
            this._16.UseVisualStyleBackColor = true;
            // 
            // _17
            // 
            this._17.Location = new System.Drawing.Point(448, 63);
            this._17.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._17.Name = "_17";
            this._17.Size = new System.Drawing.Size(70, 50);
            this._17.TabIndex = 7;
            this._17.UseVisualStyleBackColor = true;
            // 
            // _18
            // 
            this._18.Location = new System.Drawing.Point(516, 63);
            this._18.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._18.Name = "_18";
            this._18.Size = new System.Drawing.Size(70, 50);
            this._18.TabIndex = 8;
            this._18.UseVisualStyleBackColor = true;
            // 
            // _19
            // 
            this._19.Location = new System.Drawing.Point(585, 63);
            this._19.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._19.Name = "_19";
            this._19.Size = new System.Drawing.Size(70, 50);
            this._19.TabIndex = 9;
            this._19.UseVisualStyleBackColor = true;
            // 
            // _29
            // 
            this._29.Location = new System.Drawing.Point(585, 112);
            this._29.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._29.Name = "_29";
            this._29.Size = new System.Drawing.Size(70, 50);
            this._29.TabIndex = 18;
            this._29.UseVisualStyleBackColor = true;
            // 
            // _28
            // 
            this._28.Location = new System.Drawing.Point(516, 112);
            this._28.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._28.Name = "_28";
            this._28.Size = new System.Drawing.Size(70, 50);
            this._28.TabIndex = 17;
            this._28.UseVisualStyleBackColor = true;
            // 
            // _27
            // 
            this._27.Location = new System.Drawing.Point(448, 112);
            this._27.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._27.Name = "_27";
            this._27.Size = new System.Drawing.Size(70, 50);
            this._27.TabIndex = 16;
            this._27.UseVisualStyleBackColor = true;
            // 
            // _26
            // 
            this._26.Location = new System.Drawing.Point(370, 112);
            this._26.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._26.Name = "_26";
            this._26.Size = new System.Drawing.Size(70, 50);
            this._26.TabIndex = 15;
            this._26.UseVisualStyleBackColor = true;
            // 
            // _25
            // 
            this._25.Location = new System.Drawing.Point(302, 112);
            this._25.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._25.Name = "_25";
            this._25.Size = new System.Drawing.Size(70, 50);
            this._25.TabIndex = 14;
            this._25.UseVisualStyleBackColor = true;
            // 
            // _24
            // 
            this._24.Location = new System.Drawing.Point(233, 112);
            this._24.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._24.Name = "_24";
            this._24.Size = new System.Drawing.Size(70, 50);
            this._24.TabIndex = 13;
            this._24.UseVisualStyleBackColor = true;
            // 
            // _23
            // 
            this._23.Location = new System.Drawing.Point(152, 112);
            this._23.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._23.Name = "_23";
            this._23.Size = new System.Drawing.Size(70, 50);
            this._23.TabIndex = 12;
            this._23.UseVisualStyleBackColor = true;
            // 
            // _22
            // 
            this._22.Location = new System.Drawing.Point(84, 112);
            this._22.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._22.Name = "_22";
            this._22.Size = new System.Drawing.Size(70, 50);
            this._22.TabIndex = 11;
            this._22.UseVisualStyleBackColor = true;
            // 
            // _21
            // 
            this._21.Location = new System.Drawing.Point(15, 112);
            this._21.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._21.Name = "_21";
            this._21.Size = new System.Drawing.Size(70, 50);
            this._21.TabIndex = 10;
            this._21.UseVisualStyleBackColor = true;
            // 
            // _49
            // 
            this._49.Location = new System.Drawing.Point(585, 217);
            this._49.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._49.Name = "_49";
            this._49.Size = new System.Drawing.Size(70, 50);
            this._49.TabIndex = 36;
            this._49.UseVisualStyleBackColor = true;
            // 
            // _48
            // 
            this._48.Location = new System.Drawing.Point(516, 217);
            this._48.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._48.Name = "_48";
            this._48.Size = new System.Drawing.Size(70, 50);
            this._48.TabIndex = 35;
            this._48.UseVisualStyleBackColor = true;
            // 
            // _47
            // 
            this._47.Location = new System.Drawing.Point(448, 217);
            this._47.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._47.Name = "_47";
            this._47.Size = new System.Drawing.Size(70, 50);
            this._47.TabIndex = 34;
            this._47.UseVisualStyleBackColor = true;
            // 
            // _46
            // 
            this._46.Location = new System.Drawing.Point(370, 217);
            this._46.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._46.Name = "_46";
            this._46.Size = new System.Drawing.Size(70, 50);
            this._46.TabIndex = 33;
            this._46.UseVisualStyleBackColor = true;
            // 
            // _45
            // 
            this._45.Location = new System.Drawing.Point(302, 217);
            this._45.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._45.Name = "_45";
            this._45.Size = new System.Drawing.Size(70, 50);
            this._45.TabIndex = 32;
            this._45.UseVisualStyleBackColor = true;
            // 
            // _44
            // 
            this._44.Location = new System.Drawing.Point(233, 217);
            this._44.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._44.Name = "_44";
            this._44.Size = new System.Drawing.Size(70, 50);
            this._44.TabIndex = 31;
            this._44.UseVisualStyleBackColor = true;
            // 
            // _43
            // 
            this._43.Location = new System.Drawing.Point(152, 217);
            this._43.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._43.Name = "_43";
            this._43.Size = new System.Drawing.Size(70, 50);
            this._43.TabIndex = 30;
            this._43.UseVisualStyleBackColor = true;
            // 
            // _42
            // 
            this._42.Location = new System.Drawing.Point(84, 217);
            this._42.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._42.Name = "_42";
            this._42.Size = new System.Drawing.Size(70, 50);
            this._42.TabIndex = 29;
            this._42.UseVisualStyleBackColor = true;
            // 
            // _41
            // 
            this._41.Location = new System.Drawing.Point(15, 217);
            this._41.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._41.Name = "_41";
            this._41.Size = new System.Drawing.Size(70, 50);
            this._41.TabIndex = 28;
            this._41.UseVisualStyleBackColor = true;
            // 
            // _39
            // 
            this._39.Location = new System.Drawing.Point(585, 160);
            this._39.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._39.Name = "_39";
            this._39.Size = new System.Drawing.Size(70, 50);
            this._39.TabIndex = 27;
            this._39.UseVisualStyleBackColor = true;
            // 
            // _38
            // 
            this._38.Location = new System.Drawing.Point(516, 160);
            this._38.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._38.Name = "_38";
            this._38.Size = new System.Drawing.Size(70, 50);
            this._38.TabIndex = 26;
            this._38.UseVisualStyleBackColor = true;
            // 
            // _37
            // 
            this._37.Location = new System.Drawing.Point(448, 160);
            this._37.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._37.Name = "_37";
            this._37.Size = new System.Drawing.Size(70, 50);
            this._37.TabIndex = 25;
            this._37.UseVisualStyleBackColor = true;
            // 
            // _36
            // 
            this._36.Location = new System.Drawing.Point(370, 160);
            this._36.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._36.Name = "_36";
            this._36.Size = new System.Drawing.Size(70, 50);
            this._36.TabIndex = 24;
            this._36.UseVisualStyleBackColor = true;
            // 
            // _35
            // 
            this._35.Location = new System.Drawing.Point(302, 160);
            this._35.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._35.Name = "_35";
            this._35.Size = new System.Drawing.Size(70, 50);
            this._35.TabIndex = 23;
            this._35.UseVisualStyleBackColor = true;
            // 
            // _34
            // 
            this._34.Location = new System.Drawing.Point(233, 160);
            this._34.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._34.Name = "_34";
            this._34.Size = new System.Drawing.Size(70, 50);
            this._34.TabIndex = 22;
            this._34.UseVisualStyleBackColor = true;
            // 
            // _33
            // 
            this._33.Location = new System.Drawing.Point(152, 160);
            this._33.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._33.Name = "_33";
            this._33.Size = new System.Drawing.Size(70, 50);
            this._33.TabIndex = 21;
            this._33.UseVisualStyleBackColor = true;
            // 
            // _32
            // 
            this._32.Location = new System.Drawing.Point(84, 160);
            this._32.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._32.Name = "_32";
            this._32.Size = new System.Drawing.Size(70, 50);
            this._32.TabIndex = 20;
            this._32.UseVisualStyleBackColor = true;
            // 
            // _31
            // 
            this._31.Location = new System.Drawing.Point(15, 160);
            this._31.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._31.Name = "_31";
            this._31.Size = new System.Drawing.Size(70, 50);
            this._31.TabIndex = 19;
            this._31.UseVisualStyleBackColor = true;
            // 
            // _89
            // 
            this._89.Location = new System.Drawing.Point(585, 419);
            this._89.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._89.Name = "_89";
            this._89.Size = new System.Drawing.Size(70, 50);
            this._89.TabIndex = 72;
            this._89.UseVisualStyleBackColor = true;
            // 
            // _88
            // 
            this._88.Location = new System.Drawing.Point(516, 419);
            this._88.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._88.Name = "_88";
            this._88.Size = new System.Drawing.Size(70, 50);
            this._88.TabIndex = 71;
            this._88.UseVisualStyleBackColor = true;
            // 
            // _87
            // 
            this._87.Location = new System.Drawing.Point(448, 419);
            this._87.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._87.Name = "_87";
            this._87.Size = new System.Drawing.Size(70, 50);
            this._87.TabIndex = 70;
            this._87.UseVisualStyleBackColor = true;
            // 
            // _86
            // 
            this._86.Location = new System.Drawing.Point(370, 419);
            this._86.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._86.Name = "_86";
            this._86.Size = new System.Drawing.Size(70, 50);
            this._86.TabIndex = 69;
            this._86.UseVisualStyleBackColor = true;
            // 
            // _85
            // 
            this._85.Location = new System.Drawing.Point(302, 419);
            this._85.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._85.Name = "_85";
            this._85.Size = new System.Drawing.Size(70, 50);
            this._85.TabIndex = 68;
            this._85.UseVisualStyleBackColor = true;
            // 
            // _84
            // 
            this._84.Location = new System.Drawing.Point(233, 419);
            this._84.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._84.Name = "_84";
            this._84.Size = new System.Drawing.Size(70, 50);
            this._84.TabIndex = 67;
            this._84.UseVisualStyleBackColor = true;
            // 
            // _83
            // 
            this._83.Location = new System.Drawing.Point(152, 419);
            this._83.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._83.Name = "_83";
            this._83.Size = new System.Drawing.Size(70, 50);
            this._83.TabIndex = 66;
            this._83.UseVisualStyleBackColor = true;
            // 
            // _82
            // 
            this._82.Location = new System.Drawing.Point(84, 419);
            this._82.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._82.Name = "_82";
            this._82.Size = new System.Drawing.Size(70, 50);
            this._82.TabIndex = 65;
            this._82.UseVisualStyleBackColor = true;
            // 
            // _81
            // 
            this._81.Location = new System.Drawing.Point(15, 419);
            this._81.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._81.Name = "_81";
            this._81.Size = new System.Drawing.Size(70, 50);
            this._81.TabIndex = 64;
            this._81.UseVisualStyleBackColor = true;
            // 
            // _79
            // 
            this._79.Location = new System.Drawing.Point(585, 370);
            this._79.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._79.Name = "_79";
            this._79.Size = new System.Drawing.Size(70, 50);
            this._79.TabIndex = 63;
            this._79.UseVisualStyleBackColor = true;
            // 
            // _78
            // 
            this._78.Location = new System.Drawing.Point(516, 370);
            this._78.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._78.Name = "_78";
            this._78.Size = new System.Drawing.Size(70, 50);
            this._78.TabIndex = 62;
            this._78.UseVisualStyleBackColor = true;
            // 
            // _77
            // 
            this._77.Location = new System.Drawing.Point(448, 370);
            this._77.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._77.Name = "_77";
            this._77.Size = new System.Drawing.Size(70, 50);
            this._77.TabIndex = 61;
            this._77.UseVisualStyleBackColor = true;
            // 
            // _76
            // 
            this._76.Location = new System.Drawing.Point(370, 370);
            this._76.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._76.Name = "_76";
            this._76.Size = new System.Drawing.Size(70, 50);
            this._76.TabIndex = 60;
            this._76.UseVisualStyleBackColor = true;
            // 
            // _75
            // 
            this._75.Location = new System.Drawing.Point(302, 370);
            this._75.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._75.Name = "_75";
            this._75.Size = new System.Drawing.Size(70, 50);
            this._75.TabIndex = 59;
            this._75.UseVisualStyleBackColor = true;
            // 
            // _74
            // 
            this._74.Location = new System.Drawing.Point(233, 370);
            this._74.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._74.Name = "_74";
            this._74.Size = new System.Drawing.Size(70, 50);
            this._74.TabIndex = 58;
            this._74.UseVisualStyleBackColor = true;
            // 
            // _73
            // 
            this._73.Location = new System.Drawing.Point(152, 370);
            this._73.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._73.Name = "_73";
            this._73.Size = new System.Drawing.Size(70, 50);
            this._73.TabIndex = 57;
            this._73.UseVisualStyleBackColor = true;
            // 
            // _72
            // 
            this._72.Location = new System.Drawing.Point(84, 370);
            this._72.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._72.Name = "_72";
            this._72.Size = new System.Drawing.Size(70, 50);
            this._72.TabIndex = 56;
            this._72.UseVisualStyleBackColor = true;
            // 
            // _71
            // 
            this._71.Location = new System.Drawing.Point(15, 370);
            this._71.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._71.Name = "_71";
            this._71.Size = new System.Drawing.Size(70, 50);
            this._71.TabIndex = 55;
            this._71.UseVisualStyleBackColor = true;
            // 
            // _69
            // 
            this._69.Location = new System.Drawing.Point(585, 314);
            this._69.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._69.Name = "_69";
            this._69.Size = new System.Drawing.Size(70, 50);
            this._69.TabIndex = 54;
            this._69.UseVisualStyleBackColor = true;
            // 
            // _68
            // 
            this._68.Location = new System.Drawing.Point(516, 314);
            this._68.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._68.Name = "_68";
            this._68.Size = new System.Drawing.Size(70, 50);
            this._68.TabIndex = 53;
            this._68.UseVisualStyleBackColor = true;
            // 
            // _67
            // 
            this._67.Location = new System.Drawing.Point(448, 314);
            this._67.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._67.Name = "_67";
            this._67.Size = new System.Drawing.Size(70, 50);
            this._67.TabIndex = 52;
            this._67.UseVisualStyleBackColor = true;
            // 
            // _66
            // 
            this._66.Location = new System.Drawing.Point(370, 314);
            this._66.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._66.Name = "_66";
            this._66.Size = new System.Drawing.Size(70, 50);
            this._66.TabIndex = 51;
            this._66.UseVisualStyleBackColor = true;
            // 
            // _65
            // 
            this._65.Location = new System.Drawing.Point(302, 314);
            this._65.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._65.Name = "_65";
            this._65.Size = new System.Drawing.Size(70, 50);
            this._65.TabIndex = 50;
            this._65.UseVisualStyleBackColor = true;
            // 
            // _64
            // 
            this._64.Location = new System.Drawing.Point(233, 314);
            this._64.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._64.Name = "_64";
            this._64.Size = new System.Drawing.Size(70, 50);
            this._64.TabIndex = 49;
            this._64.UseVisualStyleBackColor = true;
            // 
            // _63
            // 
            this._63.Location = new System.Drawing.Point(152, 314);
            this._63.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._63.Name = "_63";
            this._63.Size = new System.Drawing.Size(70, 50);
            this._63.TabIndex = 48;
            this._63.UseVisualStyleBackColor = true;
            // 
            // _62
            // 
            this._62.Location = new System.Drawing.Point(84, 314);
            this._62.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._62.Name = "_62";
            this._62.Size = new System.Drawing.Size(70, 50);
            this._62.TabIndex = 47;
            this._62.UseVisualStyleBackColor = true;
            // 
            // _61
            // 
            this._61.Location = new System.Drawing.Point(15, 314);
            this._61.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._61.Name = "_61";
            this._61.Size = new System.Drawing.Size(70, 50);
            this._61.TabIndex = 46;
            this._61.UseVisualStyleBackColor = true;
            // 
            // _59
            // 
            this._59.Location = new System.Drawing.Point(585, 265);
            this._59.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._59.Name = "_59";
            this._59.Size = new System.Drawing.Size(70, 50);
            this._59.TabIndex = 45;
            this._59.UseVisualStyleBackColor = true;
            // 
            // _58
            // 
            this._58.Location = new System.Drawing.Point(516, 265);
            this._58.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._58.Name = "_58";
            this._58.Size = new System.Drawing.Size(70, 50);
            this._58.TabIndex = 44;
            this._58.UseVisualStyleBackColor = true;
            // 
            // _57
            // 
            this._57.Location = new System.Drawing.Point(448, 265);
            this._57.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._57.Name = "_57";
            this._57.Size = new System.Drawing.Size(70, 50);
            this._57.TabIndex = 43;
            this._57.UseVisualStyleBackColor = true;
            // 
            // _56
            // 
            this._56.Location = new System.Drawing.Point(370, 265);
            this._56.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._56.Name = "_56";
            this._56.Size = new System.Drawing.Size(70, 50);
            this._56.TabIndex = 42;
            this._56.UseVisualStyleBackColor = true;
            // 
            // _55
            // 
            this._55.Location = new System.Drawing.Point(302, 265);
            this._55.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._55.Name = "_55";
            this._55.Size = new System.Drawing.Size(70, 50);
            this._55.TabIndex = 41;
            this._55.UseVisualStyleBackColor = true;
            // 
            // _54
            // 
            this._54.Location = new System.Drawing.Point(233, 265);
            this._54.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._54.Name = "_54";
            this._54.Size = new System.Drawing.Size(70, 50);
            this._54.TabIndex = 40;
            this._54.UseVisualStyleBackColor = true;
            // 
            // _53
            // 
            this._53.Location = new System.Drawing.Point(152, 265);
            this._53.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._53.Name = "_53";
            this._53.Size = new System.Drawing.Size(70, 50);
            this._53.TabIndex = 39;
            this._53.UseVisualStyleBackColor = true;
            // 
            // _52
            // 
            this._52.Location = new System.Drawing.Point(84, 265);
            this._52.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._52.Name = "_52";
            this._52.Size = new System.Drawing.Size(70, 50);
            this._52.TabIndex = 38;
            this._52.UseVisualStyleBackColor = true;
            // 
            // _51
            // 
            this._51.Location = new System.Drawing.Point(15, 265);
            this._51.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._51.Name = "_51";
            this._51.Size = new System.Drawing.Size(70, 50);
            this._51.TabIndex = 37;
            this._51.UseVisualStyleBackColor = true;
            // 
            // _99
            // 
            this._99.Location = new System.Drawing.Point(585, 467);
            this._99.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._99.Name = "_99";
            this._99.Size = new System.Drawing.Size(70, 50);
            this._99.TabIndex = 81;
            this._99.UseVisualStyleBackColor = true;
            // 
            // _98
            // 
            this._98.Location = new System.Drawing.Point(516, 467);
            this._98.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._98.Name = "_98";
            this._98.Size = new System.Drawing.Size(70, 50);
            this._98.TabIndex = 80;
            this._98.UseVisualStyleBackColor = true;
            // 
            // _97
            // 
            this._97.Location = new System.Drawing.Point(448, 467);
            this._97.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._97.Name = "_97";
            this._97.Size = new System.Drawing.Size(70, 50);
            this._97.TabIndex = 79;
            this._97.UseVisualStyleBackColor = true;
            // 
            // _96
            // 
            this._96.Location = new System.Drawing.Point(370, 467);
            this._96.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._96.Name = "_96";
            this._96.Size = new System.Drawing.Size(70, 50);
            this._96.TabIndex = 78;
            this._96.UseVisualStyleBackColor = true;
            // 
            // _95
            // 
            this._95.Location = new System.Drawing.Point(302, 467);
            this._95.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._95.Name = "_95";
            this._95.Size = new System.Drawing.Size(70, 50);
            this._95.TabIndex = 77;
            this._95.UseVisualStyleBackColor = true;
            // 
            // _94
            // 
            this._94.Location = new System.Drawing.Point(233, 467);
            this._94.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._94.Name = "_94";
            this._94.Size = new System.Drawing.Size(70, 50);
            this._94.TabIndex = 76;
            this._94.UseVisualStyleBackColor = true;
            // 
            // _93
            // 
            this._93.Location = new System.Drawing.Point(152, 467);
            this._93.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._93.Name = "_93";
            this._93.Size = new System.Drawing.Size(70, 50);
            this._93.TabIndex = 75;
            this._93.UseVisualStyleBackColor = true;
            // 
            // _92
            // 
            this._92.Location = new System.Drawing.Point(84, 467);
            this._92.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._92.Name = "_92";
            this._92.Size = new System.Drawing.Size(70, 50);
            this._92.TabIndex = 74;
            this._92.UseVisualStyleBackColor = true;
            // 
            // _91
            // 
            this._91.Location = new System.Drawing.Point(15, 467);
            this._91.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._91.Name = "_91";
            this._91.Size = new System.Drawing.Size(70, 50);
            this._91.TabIndex = 73;
            this._91.UseVisualStyleBackColor = true;
            // 
            // _menuStrip
            // 
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.sudokuToolStripMenuItem});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this._menuStrip.Size = new System.Drawing.Size(751, 24);
            this._menuStrip.TabIndex = 82;
            this._menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem1,
            this.saveAsToolStripMenuItem1,
            this.toolStripSeparator1,
            this.printpreviewtoolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItemClick);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(145, 6);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem1.Image")));
            this.saveToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.saveToolStripMenuItem1.Text = "&Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.SaveToolStripMenuItem1Click);
            // 
            // saveAsToolStripMenuItem1
            // 
            this.saveAsToolStripMenuItem1.Name = "saveAsToolStripMenuItem1";
            this.saveAsToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.saveAsToolStripMenuItem1.Text = "Save &As";
            this.saveAsToolStripMenuItem1.Click += new System.EventHandler(this.SaveAsToolStripMenuItem1Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // printpreviewtoolStripMenuItem
            // 
            this.printpreviewtoolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printpreviewtoolStripMenuItem.Image")));
            this.printpreviewtoolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printpreviewtoolStripMenuItem.Name = "printpreviewtoolStripMenuItem";
            this.printpreviewtoolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.printpreviewtoolStripMenuItem.Text = "Print Pre&view";
            this.printpreviewtoolStripMenuItem.Click += new System.EventHandler(this.PrintPreviewToolStripMenuItemClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // sudokuToolStripMenuItem
            // 
            this.sudokuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.finishToolStripMenuItem,
            this.rotateToolStripMenuItem,
            this.mirrorToolStripMenuItem,
            this.toolStripSeparator3,
            this.aboutToolStripMenuItem});
            this.sudokuToolStripMenuItem.Name = "sudokuToolStripMenuItem";
            this.sudokuToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.sudokuToolStripMenuItem.Text = "Sudoku";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("optionsToolStripMenuItem.Image")));
            this.optionsToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItemClick);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripMenuItem.Image")));
            this.undoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.UndoToolStripMenuItemClick);
            // 
            // finishToolStripMenuItem
            // 
            this.finishToolStripMenuItem.Name = "finishToolStripMenuItem";
            this.finishToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.finishToolStripMenuItem.Text = "Finish";
            this.finishToolStripMenuItem.Click += new System.EventHandler(this.FinishToolStripMenuItemClick);
            // 
            // rotateToolStripMenuItem
            // 
            this.rotateToolStripMenuItem.Name = "rotateToolStripMenuItem";
            this.rotateToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.rotateToolStripMenuItem.Text = "Rotate";
            this.rotateToolStripMenuItem.Click += new System.EventHandler(this.RotateToolStripMenuItemClick);
            // 
            // mirrorToolStripMenuItem
            // 
            this.mirrorToolStripMenuItem.Name = "mirrorToolStripMenuItem";
            this.mirrorToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.mirrorToolStripMenuItem.Text = "Mirror";
            this.mirrorToolStripMenuItem.Click += new System.EventHandler(this.MirrorToolStripMenuItemClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(147, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.aboutToolStripMenuItem.Text = "About Sudoku";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
            // 
            // _IC1
            // 
            this._IC1.Location = new System.Drawing.Point(15, 524);
            this._IC1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IC1.Name = "_IC1";
            this._IC1.Size = new System.Drawing.Size(69, 23);
            this._IC1.TabIndex = 89;
            this._IC1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._IC1.Visible = false;
            // 
            // _IC2
            // 
            this._IC2.Location = new System.Drawing.Point(84, 524);
            this._IC2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IC2.Name = "_IC2";
            this._IC2.Size = new System.Drawing.Size(69, 23);
            this._IC2.TabIndex = 90;
            this._IC2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._IC2.Visible = false;
            // 
            // _IC3
            // 
            this._IC3.Location = new System.Drawing.Point(152, 524);
            this._IC3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IC3.Name = "_IC3";
            this._IC3.Size = new System.Drawing.Size(69, 23);
            this._IC3.TabIndex = 91;
            this._IC3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._IC3.Visible = false;
            // 
            // _IC4
            // 
            this._IC4.Location = new System.Drawing.Point(233, 524);
            this._IC4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IC4.Name = "_IC4";
            this._IC4.Size = new System.Drawing.Size(69, 23);
            this._IC4.TabIndex = 92;
            this._IC4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._IC4.Visible = false;
            // 
            // _IC5
            // 
            this._IC5.Location = new System.Drawing.Point(302, 524);
            this._IC5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IC5.Name = "_IC5";
            this._IC5.Size = new System.Drawing.Size(69, 23);
            this._IC5.TabIndex = 93;
            this._IC5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._IC5.Visible = false;
            // 
            // _IC6
            // 
            this._IC6.Location = new System.Drawing.Point(370, 524);
            this._IC6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IC6.Name = "_IC6";
            this._IC6.Size = new System.Drawing.Size(69, 23);
            this._IC6.TabIndex = 94;
            this._IC6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._IC6.Visible = false;
            // 
            // _IC7
            // 
            this._IC7.Location = new System.Drawing.Point(448, 524);
            this._IC7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IC7.Name = "_IC7";
            this._IC7.Size = new System.Drawing.Size(69, 23);
            this._IC7.TabIndex = 95;
            this._IC7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._IC7.Visible = false;
            // 
            // _IC8
            // 
            this._IC8.Location = new System.Drawing.Point(516, 524);
            this._IC8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IC8.Name = "_IC8";
            this._IC8.Size = new System.Drawing.Size(69, 23);
            this._IC8.TabIndex = 96;
            this._IC8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._IC8.Visible = false;
            // 
            // _IC9
            // 
            this._IC9.Location = new System.Drawing.Point(585, 524);
            this._IC9.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IC9.Name = "_IC9";
            this._IC9.Size = new System.Drawing.Size(69, 23);
            this._IC9.TabIndex = 97;
            this._IC9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._IC9.Visible = false;
            // 
            // _IR1
            // 
            this._IR1.Location = new System.Drawing.Point(662, 77);
            this._IR1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IR1.Name = "_IR1";
            this._IR1.Size = new System.Drawing.Size(69, 23);
            this._IR1.TabIndex = 98;
            this._IR1.Visible = false;
            // 
            // _IR2
            // 
            this._IR2.Location = new System.Drawing.Point(662, 125);
            this._IR2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IR2.Name = "_IR2";
            this._IR2.Size = new System.Drawing.Size(69, 23);
            this._IR2.TabIndex = 99;
            this._IR2.Visible = false;
            // 
            // _IR3
            // 
            this._IR3.Location = new System.Drawing.Point(662, 174);
            this._IR3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IR3.Name = "_IR3";
            this._IR3.Size = new System.Drawing.Size(69, 23);
            this._IR3.TabIndex = 100;
            this._IR3.Visible = false;
            // 
            // _IR4
            // 
            this._IR4.Location = new System.Drawing.Point(662, 230);
            this._IR4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IR4.Name = "_IR4";
            this._IR4.Size = new System.Drawing.Size(69, 23);
            this._IR4.TabIndex = 101;
            this._IR4.Visible = false;
            // 
            // _IR5
            // 
            this._IR5.Location = new System.Drawing.Point(662, 279);
            this._IR5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IR5.Name = "_IR5";
            this._IR5.Size = new System.Drawing.Size(69, 23);
            this._IR5.TabIndex = 102;
            this._IR5.Visible = false;
            // 
            // _IR6
            // 
            this._IR6.Location = new System.Drawing.Point(662, 327);
            this._IR6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IR6.Name = "_IR6";
            this._IR6.Size = new System.Drawing.Size(69, 23);
            this._IR6.TabIndex = 103;
            this._IR6.Visible = false;
            // 
            // _IR7
            // 
            this._IR7.Location = new System.Drawing.Point(662, 384);
            this._IR7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IR7.Name = "_IR7";
            this._IR7.Size = new System.Drawing.Size(69, 23);
            this._IR7.TabIndex = 104;
            this._IR7.Visible = false;
            // 
            // _IR8
            // 
            this._IR8.Location = new System.Drawing.Point(662, 432);
            this._IR8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IR8.Name = "_IR8";
            this._IR8.Size = new System.Drawing.Size(69, 23);
            this._IR8.TabIndex = 105;
            this._IR8.Visible = false;
            // 
            // _IR9
            // 
            this._IR9.Location = new System.Drawing.Point(662, 481);
            this._IR9.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._IR9.Name = "_IR9";
            this._IR9.Size = new System.Drawing.Size(69, 23);
            this._IR9.TabIndex = 106;
            this._IR9.Visible = false;
            // 
            // _statusStrip
            // 
            this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripStatusMoveLabel,
            this._toolStripStatusMove,
            this._toolStripStatusPossibleSolutionsLabel,
            this._toolStripStatusPossibleSolutions,
            this._toolStripStatusPossibleSolutions1});
            this._statusStrip.Location = new System.Drawing.Point(0, 573);
            this._statusStrip.Name = "_statusStrip";
            this._statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this._statusStrip.Size = new System.Drawing.Size(751, 22);
            this._statusStrip.TabIndex = 107;
            this._statusStrip.Text = "_statusStrip";
            // 
            // _toolStripStatusMoveLabel
            // 
            this._toolStripStatusMoveLabel.Name = "_toolStripStatusMoveLabel";
            this._toolStripStatusMoveLabel.Size = new System.Drawing.Size(40, 17);
            this._toolStripStatusMoveLabel.Text = "Move:";
            // 
            // _toolStripStatusMove
            // 
            this._toolStripStatusMove.Name = "_toolStripStatusMove";
            this._toolStripStatusMove.Size = new System.Drawing.Size(13, 17);
            this._toolStripStatusMove.Text = "0";
            // 
            // _toolStripStatusPossibleSolutionsLabel
            // 
            this._toolStripStatusPossibleSolutionsLabel.Name = "_toolStripStatusPossibleSolutionsLabel";
            this._toolStripStatusPossibleSolutionsLabel.Size = new System.Drawing.Size(107, 17);
            this._toolStripStatusPossibleSolutionsLabel.Text = "Possible solutions: ";
            // 
            // _toolStripStatusPossibleSolutions
            // 
            this._toolStripStatusPossibleSolutions.Name = "_toolStripStatusPossibleSolutions";
            this._toolStripStatusPossibleSolutions.Size = new System.Drawing.Size(0, 17);
            // 
            // _toolStripStatusPossibleSolutions1
            // 
            this._toolStripStatusPossibleSolutions1.Name = "_toolStripStatusPossibleSolutions1";
            this._toolStripStatusPossibleSolutions1.Size = new System.Drawing.Size(21, 17);
            this._toolStripStatusPossibleSolutions1.Text = "(0)";
            // 
            // _toolStrip
            // 
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.printToolStripButton,
            this.toolStripSeparator4,
            this.undoToolStripButton,
            this.optionstoolStripButton});
            this._toolStrip.Location = new System.Drawing.Point(0, 24);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(751, 25);
            this._toolStrip.TabIndex = 108;
            this._toolStrip.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Click += new System.EventHandler(this.NewToolStripMenuItemClick);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.SaveToolStripMenuItem1Click);
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.printToolStripButton.Text = "&Print Preview";
            this.printToolStripButton.Click += new System.EventHandler(this.PrintPreviewToolStripMenuItemClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // undoToolStripButton
            // 
            this.undoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripButton.Image")));
            this.undoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoToolStripButton.Name = "undoToolStripButton";
            this.undoToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.undoToolStripButton.Text = "Undo";
            this.undoToolStripButton.ToolTipText = "Undo last move";
            this.undoToolStripButton.Click += new System.EventHandler(this.UndoToolStripMenuItemClick);
            // 
            // optionstoolStripButton
            // 
            this.optionstoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.optionstoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("optionstoolStripButton.Image")));
            this.optionstoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.optionstoolStripButton.Name = "optionstoolStripButton";
            this.optionstoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.optionstoolStripButton.Text = "Options ...";
            this.optionstoolStripButton.Click += new System.EventHandler(this.OptionsToolStripMenuItemClick);
            // 
            // _lc1
            // 
            this._lc1.AutoSize = true;
            this._lc1.Location = new System.Drawing.Point(3, 82);
            this._lc1.Margin = new System.Windows.Forms.Padding(0);
            this._lc1.Name = "_lc1";
            this._lc1.Size = new System.Drawing.Size(13, 15);
            this._lc1.TabIndex = 109;
            this._lc1.Text = "1";
            // 
            // _lc2
            // 
            this._lc2.AutoSize = true;
            this._lc2.Location = new System.Drawing.Point(3, 130);
            this._lc2.Margin = new System.Windows.Forms.Padding(0);
            this._lc2.Name = "_lc2";
            this._lc2.Size = new System.Drawing.Size(13, 15);
            this._lc2.TabIndex = 110;
            this._lc2.Text = "2";
            // 
            // _lc3
            // 
            this._lc3.AutoSize = true;
            this._lc3.Location = new System.Drawing.Point(3, 177);
            this._lc3.Margin = new System.Windows.Forms.Padding(0);
            this._lc3.Name = "_lc3";
            this._lc3.Size = new System.Drawing.Size(13, 15);
            this._lc3.TabIndex = 111;
            this._lc3.Text = "3";
            // 
            // _lc4
            // 
            this._lc4.AutoSize = true;
            this._lc4.Location = new System.Drawing.Point(3, 235);
            this._lc4.Margin = new System.Windows.Forms.Padding(0);
            this._lc4.Name = "_lc4";
            this._lc4.Size = new System.Drawing.Size(13, 15);
            this._lc4.TabIndex = 112;
            this._lc4.Text = "4";
            // 
            // _lc5
            // 
            this._lc5.AutoSize = true;
            this._lc5.Location = new System.Drawing.Point(3, 283);
            this._lc5.Margin = new System.Windows.Forms.Padding(0);
            this._lc5.Name = "_lc5";
            this._lc5.Size = new System.Drawing.Size(13, 15);
            this._lc5.TabIndex = 113;
            this._lc5.Text = "5";
            // 
            // _lc6
            // 
            this._lc6.AutoSize = true;
            this._lc6.Location = new System.Drawing.Point(3, 330);
            this._lc6.Margin = new System.Windows.Forms.Padding(0);
            this._lc6.Name = "_lc6";
            this._lc6.Size = new System.Drawing.Size(13, 15);
            this._lc6.TabIndex = 114;
            this._lc6.Text = "6";
            // 
            // _lc7
            // 
            this._lc7.AutoSize = true;
            this._lc7.Location = new System.Drawing.Point(3, 388);
            this._lc7.Margin = new System.Windows.Forms.Padding(0);
            this._lc7.Name = "_lc7";
            this._lc7.Size = new System.Drawing.Size(13, 15);
            this._lc7.TabIndex = 115;
            this._lc7.Text = "7";
            // 
            // _lc8
            // 
            this._lc8.AutoSize = true;
            this._lc8.Location = new System.Drawing.Point(3, 435);
            this._lc8.Margin = new System.Windows.Forms.Padding(0);
            this._lc8.Name = "_lc8";
            this._lc8.Size = new System.Drawing.Size(13, 15);
            this._lc8.TabIndex = 116;
            this._lc8.Text = "8";
            // 
            // _lc9
            // 
            this._lc9.AutoSize = true;
            this._lc9.Location = new System.Drawing.Point(3, 483);
            this._lc9.Margin = new System.Windows.Forms.Padding(0);
            this._lc9.Name = "_lc9";
            this._lc9.Size = new System.Drawing.Size(13, 15);
            this._lc9.TabIndex = 117;
            this._lc9.Text = "9";
            // 
            // _lr1
            // 
            this._lr1.AutoSize = true;
            this._lr1.Location = new System.Drawing.Point(44, 49);
            this._lr1.Margin = new System.Windows.Forms.Padding(0);
            this._lr1.Name = "_lr1";
            this._lr1.Size = new System.Drawing.Size(13, 15);
            this._lr1.TabIndex = 118;
            this._lr1.Text = "1";
            // 
            // _lr2
            // 
            this._lr2.AutoSize = true;
            this._lr2.Location = new System.Drawing.Point(112, 49);
            this._lr2.Margin = new System.Windows.Forms.Padding(0);
            this._lr2.Name = "_lr2";
            this._lr2.Size = new System.Drawing.Size(13, 15);
            this._lr2.TabIndex = 119;
            this._lr2.Text = "2";
            // 
            // _lr3
            // 
            this._lr3.AutoSize = true;
            this._lr3.Location = new System.Drawing.Point(179, 49);
            this._lr3.Margin = new System.Windows.Forms.Padding(0);
            this._lr3.Name = "_lr3";
            this._lr3.Size = new System.Drawing.Size(13, 15);
            this._lr3.TabIndex = 120;
            this._lr3.Text = "3";
            // 
            // _lr4
            // 
            this._lr4.AutoSize = true;
            this._lr4.Location = new System.Drawing.Point(262, 49);
            this._lr4.Margin = new System.Windows.Forms.Padding(0);
            this._lr4.Name = "_lr4";
            this._lr4.Size = new System.Drawing.Size(13, 15);
            this._lr4.TabIndex = 121;
            this._lr4.Text = "4";
            // 
            // _lr5
            // 
            this._lr5.AutoSize = true;
            this._lr5.Location = new System.Drawing.Point(329, 49);
            this._lr5.Margin = new System.Windows.Forms.Padding(0);
            this._lr5.Name = "_lr5";
            this._lr5.Size = new System.Drawing.Size(13, 15);
            this._lr5.TabIndex = 122;
            this._lr5.Text = "5";
            // 
            // _lr6
            // 
            this._lr6.AutoSize = true;
            this._lr6.Location = new System.Drawing.Point(396, 49);
            this._lr6.Margin = new System.Windows.Forms.Padding(0);
            this._lr6.Name = "_lr6";
            this._lr6.Size = new System.Drawing.Size(13, 15);
            this._lr6.TabIndex = 123;
            this._lr6.Text = "6";
            // 
            // _lr7
            // 
            this._lr7.AutoSize = true;
            this._lr7.Location = new System.Drawing.Point(478, 49);
            this._lr7.Margin = new System.Windows.Forms.Padding(0);
            this._lr7.Name = "_lr7";
            this._lr7.Size = new System.Drawing.Size(13, 15);
            this._lr7.TabIndex = 124;
            this._lr7.Text = "7";
            // 
            // _lr8
            // 
            this._lr8.AutoSize = true;
            this._lr8.Location = new System.Drawing.Point(547, 49);
            this._lr8.Margin = new System.Windows.Forms.Padding(0);
            this._lr8.Name = "_lr8";
            this._lr8.Size = new System.Drawing.Size(13, 15);
            this._lr8.TabIndex = 125;
            this._lr8.Text = "8";
            // 
            // _lr9
            // 
            this._lr9.AutoSize = true;
            this._lr9.Location = new System.Drawing.Point(612, 49);
            this._lr9.Margin = new System.Windows.Forms.Padding(0);
            this._lr9.Name = "_lr9";
            this._lr9.Size = new System.Drawing.Size(13, 15);
            this._lr9.TabIndex = 126;
            this._lr9.Text = "9";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 595);
            this.Controls.Add(this._lr9);
            this.Controls.Add(this._lr8);
            this.Controls.Add(this._lr7);
            this.Controls.Add(this._lr6);
            this.Controls.Add(this._lr5);
            this.Controls.Add(this._lr4);
            this.Controls.Add(this._lr3);
            this.Controls.Add(this._lr2);
            this.Controls.Add(this._lr1);
            this.Controls.Add(this._lc9);
            this.Controls.Add(this._lc8);
            this.Controls.Add(this._lc7);
            this.Controls.Add(this._lc6);
            this.Controls.Add(this._lc5);
            this.Controls.Add(this._lc4);
            this.Controls.Add(this._lc3);
            this.Controls.Add(this._lc2);
            this.Controls.Add(this._lc1);
            this.Controls.Add(this._toolStrip);
            this.Controls.Add(this._statusStrip);
            this.Controls.Add(this._IR9);
            this.Controls.Add(this._IR8);
            this.Controls.Add(this._IR7);
            this.Controls.Add(this._IR6);
            this.Controls.Add(this._IR5);
            this.Controls.Add(this._IR4);
            this.Controls.Add(this._IR3);
            this.Controls.Add(this._IR2);
            this.Controls.Add(this._IR1);
            this.Controls.Add(this._IC9);
            this.Controls.Add(this._IC8);
            this.Controls.Add(this._IC7);
            this.Controls.Add(this._IC6);
            this.Controls.Add(this._IC5);
            this.Controls.Add(this._IC4);
            this.Controls.Add(this._IC3);
            this.Controls.Add(this._IC2);
            this.Controls.Add(this._IC1);
            this.Controls.Add(this._99);
            this.Controls.Add(this._98);
            this.Controls.Add(this._97);
            this.Controls.Add(this._96);
            this.Controls.Add(this._95);
            this.Controls.Add(this._94);
            this.Controls.Add(this._93);
            this.Controls.Add(this._92);
            this.Controls.Add(this._91);
            this.Controls.Add(this._89);
            this.Controls.Add(this._88);
            this.Controls.Add(this._87);
            this.Controls.Add(this._86);
            this.Controls.Add(this._85);
            this.Controls.Add(this._84);
            this.Controls.Add(this._83);
            this.Controls.Add(this._82);
            this.Controls.Add(this._81);
            this.Controls.Add(this._79);
            this.Controls.Add(this._78);
            this.Controls.Add(this._77);
            this.Controls.Add(this._76);
            this.Controls.Add(this._75);
            this.Controls.Add(this._74);
            this.Controls.Add(this._73);
            this.Controls.Add(this._72);
            this.Controls.Add(this._71);
            this.Controls.Add(this._69);
            this.Controls.Add(this._68);
            this.Controls.Add(this._67);
            this.Controls.Add(this._66);
            this.Controls.Add(this._65);
            this.Controls.Add(this._64);
            this.Controls.Add(this._63);
            this.Controls.Add(this._62);
            this.Controls.Add(this._61);
            this.Controls.Add(this._59);
            this.Controls.Add(this._58);
            this.Controls.Add(this._57);
            this.Controls.Add(this._56);
            this.Controls.Add(this._55);
            this.Controls.Add(this._54);
            this.Controls.Add(this._53);
            this.Controls.Add(this._52);
            this.Controls.Add(this._51);
            this.Controls.Add(this._49);
            this.Controls.Add(this._48);
            this.Controls.Add(this._47);
            this.Controls.Add(this._46);
            this.Controls.Add(this._45);
            this.Controls.Add(this._44);
            this.Controls.Add(this._43);
            this.Controls.Add(this._42);
            this.Controls.Add(this._41);
            this.Controls.Add(this._39);
            this.Controls.Add(this._38);
            this.Controls.Add(this._37);
            this.Controls.Add(this._36);
            this.Controls.Add(this._35);
            this.Controls.Add(this._34);
            this.Controls.Add(this._33);
            this.Controls.Add(this._32);
            this.Controls.Add(this._31);
            this.Controls.Add(this._29);
            this.Controls.Add(this._28);
            this.Controls.Add(this._27);
            this.Controls.Add(this._26);
            this.Controls.Add(this._25);
            this.Controls.Add(this._24);
            this.Controls.Add(this._23);
            this.Controls.Add(this._22);
            this.Controls.Add(this._21);
            this.Controls.Add(this._19);
            this.Controls.Add(this._18);
            this.Controls.Add(this._17);
            this.Controls.Add(this._16);
            this.Controls.Add(this._15);
            this.Controls.Add(this._14);
            this.Controls.Add(this._13);
            this.Controls.Add(this._12);
            this.Controls.Add(this._11);
            this.Controls.Add(this._menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "Sudoku - new";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this._statusStrip.ResumeLayout(false);
            this._statusStrip.PerformLayout();
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _11;
        private System.Windows.Forms.Button _12;
        private System.Windows.Forms.Button _13;
        private System.Windows.Forms.Button _14;
        private System.Windows.Forms.Button _15;
        private System.Windows.Forms.Button _16;
        private System.Windows.Forms.Button _17;
        private System.Windows.Forms.Button _18;
        private System.Windows.Forms.Button _19;
        private System.Windows.Forms.Button _29;
        private System.Windows.Forms.Button _28;
        private System.Windows.Forms.Button _27;
        private System.Windows.Forms.Button _26;
        private System.Windows.Forms.Button _25;
        private System.Windows.Forms.Button _24;
        private System.Windows.Forms.Button _23;
        private System.Windows.Forms.Button _22;
        private System.Windows.Forms.Button _21;
        private System.Windows.Forms.Button _49;
        private System.Windows.Forms.Button _48;
        private System.Windows.Forms.Button _47;
        private System.Windows.Forms.Button _46;
        private System.Windows.Forms.Button _45;
        private System.Windows.Forms.Button _44;
        private System.Windows.Forms.Button _43;
        private System.Windows.Forms.Button _42;
        private System.Windows.Forms.Button _41;
        private System.Windows.Forms.Button _39;
        private System.Windows.Forms.Button _38;
        private System.Windows.Forms.Button _37;
        private System.Windows.Forms.Button _36;
        private System.Windows.Forms.Button _35;
        private System.Windows.Forms.Button _34;
        private System.Windows.Forms.Button _33;
        private System.Windows.Forms.Button _32;
        private System.Windows.Forms.Button _31;
        private System.Windows.Forms.Button _89;
        private System.Windows.Forms.Button _88;
        private System.Windows.Forms.Button _87;
        private System.Windows.Forms.Button _86;
        private System.Windows.Forms.Button _85;
        private System.Windows.Forms.Button _84;
        private System.Windows.Forms.Button _83;
        private System.Windows.Forms.Button _82;
        private System.Windows.Forms.Button _81;
        private System.Windows.Forms.Button _79;
        private System.Windows.Forms.Button _78;
        private System.Windows.Forms.Button _77;
        private System.Windows.Forms.Button _76;
        private System.Windows.Forms.Button _75;
        private System.Windows.Forms.Button _74;
        private System.Windows.Forms.Button _73;
        private System.Windows.Forms.Button _72;
        private System.Windows.Forms.Button _71;
        private System.Windows.Forms.Button _69;
        private System.Windows.Forms.Button _68;
        private System.Windows.Forms.Button _67;
        private System.Windows.Forms.Button _66;
        private System.Windows.Forms.Button _65;
        private System.Windows.Forms.Button _64;
        private System.Windows.Forms.Button _63;
        private System.Windows.Forms.Button _62;
        private System.Windows.Forms.Button _61;
        private System.Windows.Forms.Button _59;
        private System.Windows.Forms.Button _58;
        private System.Windows.Forms.Button _57;
        private System.Windows.Forms.Button _56;
        private System.Windows.Forms.Button _55;
        private System.Windows.Forms.Button _54;
        private System.Windows.Forms.Button _53;
        private System.Windows.Forms.Button _52;
        private System.Windows.Forms.Button _51;
        private System.Windows.Forms.Button _99;
        private System.Windows.Forms.Button _98;
        private System.Windows.Forms.Button _97;
        private System.Windows.Forms.Button _96;
        private System.Windows.Forms.Button _95;
        private System.Windows.Forms.Button _94;
        private System.Windows.Forms.Button _93;
        private System.Windows.Forms.Button _92;
        private System.Windows.Forms.Button _91;
        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.ToolStripMenuItem sudokuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.TextBox _IC1;
        private System.Windows.Forms.TextBox _IC2;
        private System.Windows.Forms.TextBox _IC3;
        private System.Windows.Forms.TextBox _IC4;
        private System.Windows.Forms.TextBox _IC5;
        private System.Windows.Forms.TextBox _IC6;
        private System.Windows.Forms.TextBox _IC7;
        private System.Windows.Forms.TextBox _IC8;
        private System.Windows.Forms.TextBox _IC9;
        private System.Windows.Forms.TextBox _IR1;
        private System.Windows.Forms.TextBox _IR2;
        private System.Windows.Forms.TextBox _IR3;
        private System.Windows.Forms.TextBox _IR4;
        private System.Windows.Forms.TextBox _IR5;
        private System.Windows.Forms.TextBox _IR6;
        private System.Windows.Forms.TextBox _IR7;
        private System.Windows.Forms.TextBox _IR8;
        private System.Windows.Forms.TextBox _IR9;
        private System.Windows.Forms.ToolStripMenuItem finishToolStripMenuItem;
        private System.Windows.Forms.StatusStrip _statusStrip;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton undoToolStripButton;
        private System.Windows.Forms.ToolStripStatusLabel _toolStripStatusMoveLabel;
        private System.Windows.Forms.ToolStripStatusLabel _toolStripStatusMove;
        private System.Windows.Forms.ToolStripStatusLabel _toolStripStatusPossibleSolutionsLabel;
        private System.Windows.Forms.ToolStripStatusLabel _toolStripStatusPossibleSolutions;
        private System.Windows.Forms.ToolStripStatusLabel _toolStripStatusPossibleSolutions1;
        private System.Windows.Forms.ToolTip _toolTip;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton optionstoolStripButton;
        private System.Windows.Forms.ToolStripMenuItem printpreviewtoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mirrorToolStripMenuItem;
        private System.Windows.Forms.Label _lc1;
        private System.Windows.Forms.Label _lc2;
        private System.Windows.Forms.Label _lc3;
        private System.Windows.Forms.Label _lc4;
        private System.Windows.Forms.Label _lc5;
        private System.Windows.Forms.Label _lc6;
        private System.Windows.Forms.Label _lc7;
        private System.Windows.Forms.Label _lc8;
        private System.Windows.Forms.Label _lc9;
        private System.Windows.Forms.Label _lr1;
        private System.Windows.Forms.Label _lr2;
        private System.Windows.Forms.Label _lr3;
        private System.Windows.Forms.Label _lr4;
        private System.Windows.Forms.Label _lr5;
        private System.Windows.Forms.Label _lr6;
        private System.Windows.Forms.Label _lr7;
        private System.Windows.Forms.Label _lr8;
        private System.Windows.Forms.Label _lr9;
    }
}

