namespace AGA_Helper
{
    partial class Form2
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
            this.button_replace = new System.Windows.Forms.Button();
            this.button_merge = new System.Windows.Forms.Button();
            this.button_rename = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_replace
            // 
            this.button_replace.Location = new System.Drawing.Point(12, 46);
            this.button_replace.Name = "button_replace";
            this.button_replace.Size = new System.Drawing.Size(75, 23);
            this.button_replace.TabIndex = 0;
            this.button_replace.Text = "Replace";
            this.button_replace.UseVisualStyleBackColor = true;
            this.button_replace.Click += new System.EventHandler(this.button_replace_Click);
            // 
            // button_merge
            // 
            this.button_merge.Location = new System.Drawing.Point(116, 46);
            this.button_merge.Name = "button_merge";
            this.button_merge.Size = new System.Drawing.Size(75, 23);
            this.button_merge.TabIndex = 1;
            this.button_merge.Text = "Merge";
            this.button_merge.UseVisualStyleBackColor = true;
            this.button_merge.Click += new System.EventHandler(this.button_merge_Click);
            // 
            // button_rename
            // 
            this.button_rename.Location = new System.Drawing.Point(216, 46);
            this.button_rename.Name = "button_rename";
            this.button_rename.Size = new System.Drawing.Size(75, 23);
            this.button_rename.TabIndex = 2;
            this.button_rename.Text = "Rename";
            this.button_rename.UseVisualStyleBackColor = true;
            this.button_rename.Click += new System.EventHandler(this.button_rename_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "What do you want to do with existing translation files?";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 82);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_rename);
            this.Controls.Add(this.button_merge);
            this.Controls.Add(this.button_replace);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_replace;
        private System.Windows.Forms.Button button_merge;
        private System.Windows.Forms.Button button_rename;
        private System.Windows.Forms.Label label1;
    }
}