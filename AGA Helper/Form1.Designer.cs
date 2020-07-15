namespace AGA_Helper
{
    partial class Form1
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
            this.button_patch = new System.Windows.Forms.Button();
            this.button_restore = new System.Windows.Forms.Button();
            this.label_gamepath = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_launch = new System.Windows.Forms.Button();
            this.checkBox_lock_a = new System.Windows.Forms.CheckBox();
            this.checkBox_lock_u = new System.Windows.Forms.CheckBox();
            this.button_updateTranslation = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button_patchTranslation = new System.Windows.Forms.Button();
            this.button_permaLock = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox_assembly = new System.Windows.Forms.GroupBox();
            this.button_deobfuscate = new System.Windows.Forms.Button();
            this.button_obfuscate = new System.Windows.Forms.Button();
            this.groupBox_mcodeSearch = new System.Windows.Forms.GroupBox();
            this.button_mcodeSearch = new System.Windows.Forms.Button();
            this.button_mcodeSearchStripped = new System.Windows.Forms.Button();
            this.textBox_mcodeSearch = new System.Windows.Forms.TextBox();
            this.label_boneOrderFix = new System.Windows.Forms.Label();
            this.groupBox_crc_pc = new System.Windows.Forms.GroupBox();
            this.button_stripCRC = new System.Windows.Forms.Button();
            this.label_restoreCRC = new System.Windows.Forms.Label();
            this.label_stripCRC = new System.Windows.Forms.Label();
            this.button_strippeddataPath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label_strippedDataPath = new System.Windows.Forms.Label();
            this.button_gamedataPC = new System.Windows.Forms.Button();
            this.textBox_suffix = new System.Windows.Forms.TextBox();
            this.label_dataPath = new System.Windows.Forms.Label();
            this.groupBox_swap = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button_swap = new System.Windows.Forms.Button();
            this.groupBox_target = new System.Windows.Forms.GroupBox();
            this.textBox_crc = new System.Windows.Forms.TextBox();
            this.textBox_cab = new System.Windows.Forms.TextBox();
            this.textBox_code = new System.Windows.Forms.TextBox();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.groupBox_source = new System.Windows.Forms.GroupBox();
            this.label_hint1 = new System.Windows.Forms.Label();
            this.label_crc = new System.Windows.Forms.Label();
            this.label_cab = new System.Windows.Forms.Label();
            this.label_code = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.button_help = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox_assembly.SuspendLayout();
            this.groupBox_mcodeSearch.SuspendLayout();
            this.groupBox_crc_pc.SuspendLayout();
            this.groupBox_swap.SuspendLayout();
            this.groupBox_target.SuspendLayout();
            this.groupBox_source.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_patch
            // 
            this.button_patch.Location = new System.Drawing.Point(167, 29);
            this.button_patch.Name = "button_patch";
            this.button_patch.Size = new System.Drawing.Size(120, 23);
            this.button_patch.TabIndex = 0;
            this.button_patch.Text = "Patch";
            this.button_patch.UseVisualStyleBackColor = true;
            this.button_patch.Click += new System.EventHandler(this.button_patch_Click);
            // 
            // button_restore
            // 
            this.button_restore.Enabled = false;
            this.button_restore.Location = new System.Drawing.Point(305, 29);
            this.button_restore.Name = "button_restore";
            this.button_restore.Size = new System.Drawing.Size(120, 23);
            this.button_restore.TabIndex = 1;
            this.button_restore.Text = "Restore";
            this.button_restore.UseVisualStyleBackColor = true;
            this.button_restore.Visible = false;
            this.button_restore.Click += new System.EventHandler(this.button_restore_Click);
            // 
            // label_gamepath
            // 
            this.label_gamepath.Location = new System.Drawing.Point(19, 9);
            this.label_gamepath.Name = "label_gamepath";
            this.label_gamepath.Size = new System.Drawing.Size(552, 16);
            this.label_gamepath.TabIndex = 9;
            this.label_gamepath.Text = "path";
            this.label_gamepath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Uncensor Patch";
            // 
            // button_launch
            // 
            this.button_launch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_launch.Location = new System.Drawing.Point(106, 28);
            this.button_launch.Name = "button_launch";
            this.button_launch.Size = new System.Drawing.Size(201, 41);
            this.button_launch.TabIndex = 15;
            this.button_launch.Text = "Launch";
            this.button_launch.UseVisualStyleBackColor = true;
            this.button_launch.Click += new System.EventHandler(this.button_launch_Click);
            // 
            // checkBox_lock_a
            // 
            this.checkBox_lock_a.AutoSize = true;
            this.checkBox_lock_a.Checked = true;
            this.checkBox_lock_a.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_lock_a.Location = new System.Drawing.Point(325, 29);
            this.checkBox_lock_a.Name = "checkBox_lock_a";
            this.checkBox_lock_a.Size = new System.Drawing.Size(201, 17);
            this.checkBox_lock_a.TabIndex = 16;
            this.checkBox_lock_a.Text = "Lock Assembly-CSharp.dll (uncensor)";
            this.checkBox_lock_a.UseVisualStyleBackColor = true;
            // 
            // checkBox_lock_u
            // 
            this.checkBox_lock_u.AutoSize = true;
            this.checkBox_lock_u.Checked = true;
            this.checkBox_lock_u.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_lock_u.Location = new System.Drawing.Point(325, 52);
            this.checkBox_lock_u.Name = "checkBox_lock_u";
            this.checkBox_lock_u.Size = new System.Drawing.Size(240, 17);
            this.checkBox_lock_u.TabIndex = 17;
            this.checkBox_lock_u.Text = "Lock UnityEngine.CoreModule.dll (translation)";
            this.checkBox_lock_u.UseVisualStyleBackColor = true;
            // 
            // button_updateTranslation
            // 
            this.button_updateTranslation.Location = new System.Drawing.Point(305, 89);
            this.button_updateTranslation.Name = "button_updateTranslation";
            this.button_updateTranslation.Size = new System.Drawing.Size(120, 23);
            this.button_updateTranslation.TabIndex = 18;
            this.button_updateTranslation.Text = "Get Latest Translation";
            this.button_updateTranslation.UseVisualStyleBackColor = true;
            this.button_updateTranslation.Click += new System.EventHandler(this.button_updateTranslation_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Translation Patch";
            // 
            // button_patchTranslation
            // 
            this.button_patchTranslation.Location = new System.Drawing.Point(167, 89);
            this.button_patchTranslation.Name = "button_patchTranslation";
            this.button_patchTranslation.Size = new System.Drawing.Size(120, 23);
            this.button_patchTranslation.TabIndex = 20;
            this.button_patchTranslation.Text = "Patch";
            this.button_patchTranslation.UseVisualStyleBackColor = true;
            this.button_patchTranslation.Click += new System.EventHandler(this.button_patchTranslation_Click);
            // 
            // button_permaLock
            // 
            this.button_permaLock.Location = new System.Drawing.Point(167, 144);
            this.button_permaLock.Name = "button_permaLock";
            this.button_permaLock.Size = new System.Drawing.Size(258, 23);
            this.button_permaLock.TabIndex = 21;
            this.button_permaLock.Text = "Permanent Lock / Unlock";
            this.button_permaLock.UseVisualStyleBackColor = true;
            this.button_permaLock.Click += new System.EventHandler(this.button_permaLock_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 96);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(601, 434);
            this.tabControl1.TabIndex = 25;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.button_patch);
            this.tabPage1.Controls.Add(this.button_restore);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.button_permaLock);
            this.tabPage1.Controls.Add(this.button_patchTranslation);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.button_updateTranslation);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(593, 408);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "User Tools";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.groupBox_assembly);
            this.tabPage2.Controls.Add(this.groupBox_mcodeSearch);
            this.tabPage2.Controls.Add(this.label_boneOrderFix);
            this.tabPage2.Controls.Add(this.groupBox_crc_pc);
            this.tabPage2.Controls.Add(this.groupBox_swap);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(593, 408);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Modding tools";
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(480, 168);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(85, 23);
            this.button4.TabIndex = 28;
            this.button4.Text = "download";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(512, 136);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(53, 23);
            this.button2.TabIndex = 27;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox_assembly
            // 
            this.groupBox_assembly.Controls.Add(this.button_deobfuscate);
            this.groupBox_assembly.Controls.Add(this.button_obfuscate);
            this.groupBox_assembly.Location = new System.Drawing.Point(6, 352);
            this.groupBox_assembly.Name = "groupBox_assembly";
            this.groupBox_assembly.Size = new System.Drawing.Size(207, 46);
            this.groupBox_assembly.TabIndex = 26;
            this.groupBox_assembly.TabStop = false;
            this.groupBox_assembly.Text = "Assembly editing";
            // 
            // button_deobfuscate
            // 
            this.button_deobfuscate.Location = new System.Drawing.Point(6, 17);
            this.button_deobfuscate.Name = "button_deobfuscate";
            this.button_deobfuscate.Size = new System.Drawing.Size(94, 23);
            this.button_deobfuscate.TabIndex = 16;
            this.button_deobfuscate.Text = "Deobfuscate";
            this.button_deobfuscate.UseVisualStyleBackColor = true;
            this.button_deobfuscate.Click += new System.EventHandler(this.button_deobfuscate_Click);
            // 
            // button_obfuscate
            // 
            this.button_obfuscate.Location = new System.Drawing.Point(106, 17);
            this.button_obfuscate.Name = "button_obfuscate";
            this.button_obfuscate.Size = new System.Drawing.Size(94, 23);
            this.button_obfuscate.TabIndex = 20;
            this.button_obfuscate.Text = "Obfuscate";
            this.button_obfuscate.UseVisualStyleBackColor = true;
            this.button_obfuscate.Click += new System.EventHandler(this.button_obfuscate_Click);
            // 
            // groupBox_mcodeSearch
            // 
            this.groupBox_mcodeSearch.Controls.Add(this.button_mcodeSearch);
            this.groupBox_mcodeSearch.Controls.Add(this.button_mcodeSearchStripped);
            this.groupBox_mcodeSearch.Controls.Add(this.textBox_mcodeSearch);
            this.groupBox_mcodeSearch.Location = new System.Drawing.Point(6, 136);
            this.groupBox_mcodeSearch.Name = "groupBox_mcodeSearch";
            this.groupBox_mcodeSearch.Size = new System.Drawing.Size(448, 46);
            this.groupBox_mcodeSearch.TabIndex = 21;
            this.groupBox_mcodeSearch.TabStop = false;
            this.groupBox_mcodeSearch.Text = "Search files by model code";
            // 
            // button_mcodeSearch
            // 
            this.button_mcodeSearch.Location = new System.Drawing.Point(147, 17);
            this.button_mcodeSearch.Name = "button_mcodeSearch";
            this.button_mcodeSearch.Size = new System.Drawing.Size(138, 23);
            this.button_mcodeSearch.TabIndex = 16;
            this.button_mcodeSearch.Text = "Search by code (original)";
            this.button_mcodeSearch.UseVisualStyleBackColor = true;
            this.button_mcodeSearch.Click += new System.EventHandler(this.button_mcodeSearch_Click);
            // 
            // button_mcodeSearchStripped
            // 
            this.button_mcodeSearchStripped.Location = new System.Drawing.Point(291, 17);
            this.button_mcodeSearchStripped.Name = "button_mcodeSearchStripped";
            this.button_mcodeSearchStripped.Size = new System.Drawing.Size(138, 23);
            this.button_mcodeSearchStripped.TabIndex = 20;
            this.button_mcodeSearchStripped.Text = "Search by code (stripped)";
            this.button_mcodeSearchStripped.UseVisualStyleBackColor = true;
            this.button_mcodeSearchStripped.Click += new System.EventHandler(this.button_mcodeSearchStripped_Click);
            // 
            // textBox_mcodeSearch
            // 
            this.textBox_mcodeSearch.Location = new System.Drawing.Point(6, 19);
            this.textBox_mcodeSearch.Name = "textBox_mcodeSearch";
            this.textBox_mcodeSearch.Size = new System.Drawing.Size(135, 20);
            this.textBox_mcodeSearch.TabIndex = 17;
            // 
            // label_boneOrderFix
            // 
            this.label_boneOrderFix.AllowDrop = true;
            this.label_boneOrderFix.BackColor = System.Drawing.Color.Transparent;
            this.label_boneOrderFix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_boneOrderFix.Location = new System.Drawing.Point(464, 352);
            this.label_boneOrderFix.Name = "label_boneOrderFix";
            this.label_boneOrderFix.Size = new System.Drawing.Size(102, 46);
            this.label_boneOrderFix.TabIndex = 25;
            this.label_boneOrderFix.Text = "Bone order fix\r\n\r\nDrop Area";
            this.label_boneOrderFix.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_boneOrderFix.DragDrop += new System.Windows.Forms.DragEventHandler(this.label_boneOrderFix_DragDrop);
            this.label_boneOrderFix.DragEnter += new System.Windows.Forms.DragEventHandler(this.label_boneOrderFix_DragEnter);
            // 
            // groupBox_crc_pc
            // 
            this.groupBox_crc_pc.Controls.Add(this.button_stripCRC);
            this.groupBox_crc_pc.Controls.Add(this.label_restoreCRC);
            this.groupBox_crc_pc.Controls.Add(this.label_stripCRC);
            this.groupBox_crc_pc.Controls.Add(this.button_strippeddataPath);
            this.groupBox_crc_pc.Controls.Add(this.label2);
            this.groupBox_crc_pc.Controls.Add(this.label_strippedDataPath);
            this.groupBox_crc_pc.Controls.Add(this.button_gamedataPC);
            this.groupBox_crc_pc.Controls.Add(this.textBox_suffix);
            this.groupBox_crc_pc.Controls.Add(this.label_dataPath);
            this.groupBox_crc_pc.Location = new System.Drawing.Point(6, 6);
            this.groupBox_crc_pc.Name = "groupBox_crc_pc";
            this.groupBox_crc_pc.Size = new System.Drawing.Size(581, 115);
            this.groupBox_crc_pc.TabIndex = 15;
            this.groupBox_crc_pc.TabStop = false;
            this.groupBox_crc_pc.Text = "Data processing";
            // 
            // button_stripCRC
            // 
            this.button_stripCRC.AllowDrop = true;
            this.button_stripCRC.Location = new System.Drawing.Point(6, 12);
            this.button_stripCRC.Name = "button_stripCRC";
            this.button_stripCRC.Size = new System.Drawing.Size(128, 46);
            this.button_stripCRC.TabIndex = 1;
            this.button_stripCRC.Text = "Strip CRC \r\nfor entire Data folder";
            this.button_stripCRC.UseVisualStyleBackColor = true;
            this.button_stripCRC.Click += new System.EventHandler(this.button_stripCRC_Click);
            // 
            // label_restoreCRC
            // 
            this.label_restoreCRC.AllowDrop = true;
            this.label_restoreCRC.BackColor = System.Drawing.Color.Transparent;
            this.label_restoreCRC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_restoreCRC.Location = new System.Drawing.Point(429, 12);
            this.label_restoreCRC.Name = "label_restoreCRC";
            this.label_restoreCRC.Size = new System.Drawing.Size(146, 46);
            this.label_restoreCRC.TabIndex = 2;
            this.label_restoreCRC.Text = "Restore CRC\r\n\r\nDrop Area";
            this.label_restoreCRC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_restoreCRC.DragDrop += new System.Windows.Forms.DragEventHandler(this.label_restoreCRC_DragDrop);
            this.label_restoreCRC.DragEnter += new System.Windows.Forms.DragEventHandler(this.label_restoreCRC_DragEnter);
            // 
            // label_stripCRC
            // 
            this.label_stripCRC.AllowDrop = true;
            this.label_stripCRC.BackColor = System.Drawing.Color.Transparent;
            this.label_stripCRC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_stripCRC.Location = new System.Drawing.Point(260, 12);
            this.label_stripCRC.Name = "label_stripCRC";
            this.label_stripCRC.Size = new System.Drawing.Size(142, 46);
            this.label_stripCRC.TabIndex = 8;
            this.label_stripCRC.Text = "Strip CRC and Decompress\r\n\r\nDrop Area";
            this.label_stripCRC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_stripCRC.DragDrop += new System.Windows.Forms.DragEventHandler(this.label_stripCRC_DragDrop);
            this.label_stripCRC.DragEnter += new System.Windows.Forms.DragEventHandler(this.label_stripCRC_DragEnter);
            // 
            // button_strippeddataPath
            // 
            this.button_strippeddataPath.Location = new System.Drawing.Point(6, 87);
            this.button_strippeddataPath.Name = "button_strippeddataPath";
            this.button_strippeddataPath.Size = new System.Drawing.Size(97, 23);
            this.button_strippeddataPath.TabIndex = 12;
            this.button_strippeddataPath.Text = "Stripped Data";
            this.button_strippeddataPath.UseVisualStyleBackColor = true;
            this.button_strippeddataPath.Click += new System.EventHandler(this.button_strippeddataPath_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Add Suffix";
            // 
            // label_strippedDataPath
            // 
            this.label_strippedDataPath.Location = new System.Drawing.Point(109, 88);
            this.label_strippedDataPath.Name = "label_strippedDataPath";
            this.label_strippedDataPath.Size = new System.Drawing.Size(417, 23);
            this.label_strippedDataPath.TabIndex = 11;
            this.label_strippedDataPath.Text = "Stripped data path";
            this.label_strippedDataPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_gamedataPC
            // 
            this.button_gamedataPC.Location = new System.Drawing.Point(6, 60);
            this.button_gamedataPC.Name = "button_gamedataPC";
            this.button_gamedataPC.Size = new System.Drawing.Size(97, 23);
            this.button_gamedataPC.TabIndex = 10;
            this.button_gamedataPC.Text = "Game Data";
            this.button_gamedataPC.UseVisualStyleBackColor = true;
            this.button_gamedataPC.Click += new System.EventHandler(this.button_gamedataPC_Click);
            // 
            // textBox_suffix
            // 
            this.textBox_suffix.Location = new System.Drawing.Point(143, 32);
            this.textBox_suffix.Name = "textBox_suffix";
            this.textBox_suffix.Size = new System.Drawing.Size(78, 20);
            this.textBox_suffix.TabIndex = 6;
            // 
            // label_dataPath
            // 
            this.label_dataPath.Location = new System.Drawing.Point(109, 61);
            this.label_dataPath.Name = "label_dataPath";
            this.label_dataPath.Size = new System.Drawing.Size(417, 23);
            this.label_dataPath.TabIndex = 0;
            this.label_dataPath.Text = "Game data path";
            this.label_dataPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox_swap
            // 
            this.groupBox_swap.Controls.Add(this.button3);
            this.groupBox_swap.Controls.Add(this.label5);
            this.groupBox_swap.Controls.Add(this.button1);
            this.groupBox_swap.Controls.Add(this.button_swap);
            this.groupBox_swap.Controls.Add(this.groupBox_target);
            this.groupBox_swap.Controls.Add(this.groupBox_source);
            this.groupBox_swap.Location = new System.Drawing.Point(6, 197);
            this.groupBox_swap.Name = "groupBox_swap";
            this.groupBox_swap.Size = new System.Drawing.Size(581, 149);
            this.groupBox_swap.TabIndex = 7;
            this.groupBox_swap.TabStop = false;
            this.groupBox_swap.Text = "Model Swap";
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(506, 35);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(54, 23);
            this.button3.TabIndex = 24;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(509, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "(optional)";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(506, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_swap
            // 
            this.button_swap.Location = new System.Drawing.Point(500, 73);
            this.button_swap.Name = "button_swap";
            this.button_swap.Size = new System.Drawing.Size(75, 23);
            this.button_swap.TabIndex = 2;
            this.button_swap.Text = "Swap";
            this.button_swap.UseVisualStyleBackColor = true;
            this.button_swap.Click += new System.EventHandler(this.button_swap_Click);
            // 
            // groupBox_target
            // 
            this.groupBox_target.Controls.Add(this.textBox_crc);
            this.groupBox_target.Controls.Add(this.textBox_cab);
            this.groupBox_target.Controls.Add(this.textBox_code);
            this.groupBox_target.Controls.Add(this.textBox_name);
            this.groupBox_target.Location = new System.Drawing.Point(250, 19);
            this.groupBox_target.Name = "groupBox_target";
            this.groupBox_target.Size = new System.Drawing.Size(244, 123);
            this.groupBox_target.TabIndex = 1;
            this.groupBox_target.TabStop = false;
            this.groupBox_target.Text = "Replaced model";
            this.groupBox_target.DragDrop += new System.Windows.Forms.DragEventHandler(this.groupBox_target_DragDrop);
            this.groupBox_target.DragEnter += new System.Windows.Forms.DragEventHandler(this.groupBox_target_DragEnter);
            // 
            // textBox_crc
            // 
            this.textBox_crc.Location = new System.Drawing.Point(10, 94);
            this.textBox_crc.Name = "textBox_crc";
            this.textBox_crc.Size = new System.Drawing.Size(228, 20);
            this.textBox_crc.TabIndex = 3;
            // 
            // textBox_cab
            // 
            this.textBox_cab.Location = new System.Drawing.Point(10, 69);
            this.textBox_cab.Name = "textBox_cab";
            this.textBox_cab.Size = new System.Drawing.Size(228, 20);
            this.textBox_cab.TabIndex = 2;
            // 
            // textBox_code
            // 
            this.textBox_code.Location = new System.Drawing.Point(10, 44);
            this.textBox_code.Name = "textBox_code";
            this.textBox_code.Size = new System.Drawing.Size(228, 20);
            this.textBox_code.TabIndex = 1;
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(10, 19);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(228, 20);
            this.textBox_name.TabIndex = 0;
            // 
            // groupBox_source
            // 
            this.groupBox_source.Controls.Add(this.label_hint1);
            this.groupBox_source.Controls.Add(this.label_crc);
            this.groupBox_source.Controls.Add(this.label_cab);
            this.groupBox_source.Controls.Add(this.label_code);
            this.groupBox_source.Controls.Add(this.label_name);
            this.groupBox_source.Location = new System.Drawing.Point(3, 18);
            this.groupBox_source.Name = "groupBox_source";
            this.groupBox_source.Size = new System.Drawing.Size(244, 124);
            this.groupBox_source.TabIndex = 0;
            this.groupBox_source.TabStop = false;
            this.groupBox_source.Text = "Replacing model";
            this.groupBox_source.DragDrop += new System.Windows.Forms.DragEventHandler(this.groupBox_source_DragDrop);
            this.groupBox_source.DragEnter += new System.Windows.Forms.DragEventHandler(this.groupBox_source_DragEnter);
            // 
            // label_hint1
            // 
            this.label_hint1.AutoSize = true;
            this.label_hint1.Location = new System.Drawing.Point(121, 25);
            this.label_hint1.Name = "label_hint1";
            this.label_hint1.Size = new System.Drawing.Size(66, 65);
            this.label_hint1.TabIndex = 24;
            this.label_hint1.Text = "Drop\r\n\r\nreplacing file\r\n\r\nhere";
            this.label_hint1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_crc
            // 
            this.label_crc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_crc.Location = new System.Drawing.Point(6, 97);
            this.label_crc.Name = "label_crc";
            this.label_crc.Size = new System.Drawing.Size(232, 17);
            this.label_crc.TabIndex = 3;
            this.label_crc.Text = "CRC32";
            // 
            // label_cab
            // 
            this.label_cab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_cab.Location = new System.Drawing.Point(6, 72);
            this.label_cab.Name = "label_cab";
            this.label_cab.Size = new System.Drawing.Size(232, 17);
            this.label_cab.TabIndex = 2;
            this.label_cab.Text = "CAB";
            // 
            // label_code
            // 
            this.label_code.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_code.Location = new System.Drawing.Point(6, 47);
            this.label_code.Name = "label_code";
            this.label_code.Size = new System.Drawing.Size(232, 13);
            this.label_code.TabIndex = 1;
            this.label_code.Text = "Code";
            // 
            // label_name
            // 
            this.label_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_name.Location = new System.Drawing.Point(6, 22);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(232, 13);
            this.label_name.TabIndex = 0;
            this.label_name.Text = "Name";
            // 
            // button_help
            // 
            this.button_help.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_help.Location = new System.Drawing.Point(590, 5);
            this.button_help.Name = "button_help";
            this.button_help.Size = new System.Drawing.Size(23, 23);
            this.button_help.TabIndex = 24;
            this.button_help.Text = "?";
            this.button_help.UseVisualStyleBackColor = true;
            this.button_help.Click += new System.EventHandler(this.button_help_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 542);
            this.Controls.Add(this.button_launch);
            this.Controls.Add(this.button_help);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.checkBox_lock_u);
            this.Controls.Add(this.label_gamepath);
            this.Controls.Add(this.checkBox_lock_a);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AGA Helper v0.40";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox_assembly.ResumeLayout(false);
            this.groupBox_mcodeSearch.ResumeLayout(false);
            this.groupBox_mcodeSearch.PerformLayout();
            this.groupBox_crc_pc.ResumeLayout(false);
            this.groupBox_crc_pc.PerformLayout();
            this.groupBox_swap.ResumeLayout(false);
            this.groupBox_swap.PerformLayout();
            this.groupBox_target.ResumeLayout(false);
            this.groupBox_target.PerformLayout();
            this.groupBox_source.ResumeLayout(false);
            this.groupBox_source.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_patch;
        private System.Windows.Forms.Button button_restore;
        private System.Windows.Forms.Label label_gamepath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_launch;
        private System.Windows.Forms.CheckBox checkBox_lock_a;
        private System.Windows.Forms.CheckBox checkBox_lock_u;
        private System.Windows.Forms.Button button_updateTranslation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_patchTranslation;
        private System.Windows.Forms.Button button_permaLock;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label_dataPath;
        private System.Windows.Forms.Button button_stripCRC;
        private System.Windows.Forms.Label label_restoreCRC;
        private System.Windows.Forms.TextBox textBox_suffix;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox_swap;
        private System.Windows.Forms.GroupBox groupBox_target;
        private System.Windows.Forms.GroupBox groupBox_source;
        private System.Windows.Forms.TextBox textBox_code;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Button button_swap;
        private System.Windows.Forms.Label label_code;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_stripCRC;
        private System.Windows.Forms.Button button_strippeddataPath;
        private System.Windows.Forms.Label label_strippedDataPath;
        private System.Windows.Forms.Button button_gamedataPC;
        private System.Windows.Forms.GroupBox groupBox_crc_pc;
        private System.Windows.Forms.TextBox textBox_cab;
        private System.Windows.Forms.Label label_cab;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_crc;
        private System.Windows.Forms.Label label_crc;
        private System.Windows.Forms.Label label_hint1;
        private System.Windows.Forms.Button button_help;
        private System.Windows.Forms.TextBox textBox_mcodeSearch;
        private System.Windows.Forms.Button button_mcodeSearch;
        private System.Windows.Forms.Button button_mcodeSearchStripped;
        private System.Windows.Forms.GroupBox groupBox_mcodeSearch;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label_boneOrderFix;
        private System.Windows.Forms.GroupBox groupBox_assembly;
        private System.Windows.Forms.Button button_deobfuscate;
        private System.Windows.Forms.Button button_obfuscate;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
    }
}

