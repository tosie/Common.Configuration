namespace Common.Configuration.Test.GUI {
    partial class ValueMatrixForm {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.valueMatrix1 = new Common.Configuration.ValueMatrix();
            this.SuspendLayout();
            // 
            // valueMatrix1
            // 
            this.valueMatrix1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.valueMatrix1.ConfigEntry = null;
            this.valueMatrix1.Location = new System.Drawing.Point(12, 12);
            this.valueMatrix1.Name = "valueMatrix1";
            this.valueMatrix1.Size = new System.Drawing.Size(478, 280);
            this.valueMatrix1.TabIndex = 0;
            // 
            // ValueMatrixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 304);
            this.Controls.Add(this.valueMatrix1);
            this.Name = "ValueMatrixForm";
            this.Text = "ValueMatrixForm";
            this.Load += new System.EventHandler(this.ValueMatrixForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ValueMatrix valueMatrix1;
    }
}