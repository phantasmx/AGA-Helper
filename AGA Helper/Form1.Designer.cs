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
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_patch
            // 
            this.button_patch.Location = new System.Drawing.Point(112, 161);
            this.button_patch.Name = "button_patch";
            this.button_patch.Size = new System.Drawing.Size(120, 23);
            this.button_patch.TabIndex = 0;
            this.button_patch.Text = "Grant";
            this.button_patch.UseVisualStyleBackColor = true;
            this.button_patch.Click += new System.EventHandler(this.button_patch_Click);
            // 
            // button_restore
            // 
            this.button_restore.Location = new System.Drawing.Point(250, 161);
            this.button_restore.Name = "button_restore";
            this.button_restore.Size = new System.Drawing.Size(120, 23);
            this.button_restore.TabIndex = 1;
            this.button_restore.Text = "Deny";
            this.button_restore.UseVisualStyleBackColor = true;
            this.button_restore.Click += new System.EventHandler(this.button_restore_Click);
            // 
            // label_gamepath
            // 
            this.label_gamepath.Location = new System.Drawing.Point(9, 19);
            this.label_gamepath.Name = "label_gamepath";
            this.label_gamepath.Size = new System.Drawing.Size(363, 16);
            this.label_gamepath.TabIndex = 9;
            this.label_gamepath.Text = "path";
            this.label_gamepath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Uncensor Patch";
            // 
            // button_launch
            // 
            this.button_launch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_launch.Location = new System.Drawing.Point(86, 38);
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
            this.checkBox_lock_a.Location = new System.Drawing.Point(86, 85);
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
            this.checkBox_lock_u.Location = new System.Drawing.Point(86, 108);
            this.checkBox_lock_u.Name = "checkBox_lock_u";
            this.checkBox_lock_u.Size = new System.Drawing.Size(180, 17);
            this.checkBox_lock_u.TabIndex = 17;
            this.checkBox_lock_u.Text = "Lock UnityEngine.dll (translation)";
            this.checkBox_lock_u.UseVisualStyleBackColor = true;
            // 
            // button_updateTranslation
            // 
            this.button_updateTranslation.Location = new System.Drawing.Point(250, 221);
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
            this.label1.Location = new System.Drawing.Point(9, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Translation Patch";
            // 
            // button_patchTranslation
            // 
            this.button_patchTranslation.Location = new System.Drawing.Point(112, 221);
            this.button_patchTranslation.Name = "button_patchTranslation";
            this.button_patchTranslation.Size = new System.Drawing.Size(120, 23);
            this.button_patchTranslation.TabIndex = 20;
            this.button_patchTranslation.Text = "Patch";
            this.button_patchTranslation.UseVisualStyleBackColor = true;
            this.button_patchTranslation.Click += new System.EventHandler(this.button_patchTranslation_Click);
            // 
            // button_permaLock
            // 
            this.button_permaLock.Location = new System.Drawing.Point(112, 276);
            this.button_permaLock.Name = "button_permaLock";
            this.button_permaLock.Size = new System.Drawing.Size(258, 23);
            this.button_permaLock.TabIndex = 21;
            this.button_permaLock.Text = "Permanent Lock / Unlock";
            this.button_permaLock.UseVisualStyleBackColor = true;
            this.button_permaLock.Click += new System.EventHandler(this.button_permaLock_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(347, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "?";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_permaLock);
            this.Controls.Add(this.button_patchTranslation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_updateTranslation);
            this.Controls.Add(this.checkBox_lock_u);
            this.Controls.Add(this.checkBox_lock_a);
            this.Controls.Add(this.button_launch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_gamepath);
            this.Controls.Add(this.button_restore);
            this.Controls.Add(this.button_patch);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AGA Helper";
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
        private System.Windows.Forms.Button button1;
    }
}

