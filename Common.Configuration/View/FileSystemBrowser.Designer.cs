namespace Common.Configuration {
    partial class FileSystemBrowser {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(0, 2);
            this.txtName.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(239, 20);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnBrowse.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnBrowse.Location = new System.Drawing.Point(245, 0);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(87, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Durchsuchen";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Verzeichnis auswählen";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Alle Dateien|*.*";
            this.openFileDialog.Title = "Datei auswählen";
            // 
            // FileSystemBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtName);
            this.Name = "FileSystemBrowser";
            this.Size = new System.Drawing.Size(332, 23);
            this.Enter += new System.EventHandler(this.FileSystemBrowser_Enter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;

    }
}
