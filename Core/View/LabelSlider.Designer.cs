namespace Common.Configuration {
    partial class LabelSlider {
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
            this.valueLabel = new System.Windows.Forms.Label();
            this.trackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // valueLabel
            // 
            this.valueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.valueLabel.Location = new System.Drawing.Point(0, 0);
            this.valueLabel.Margin = new System.Windows.Forms.Padding(0);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(58, 22);
            this.valueLabel.TabIndex = 0;
            this.valueLabel.Text = "0";
            this.valueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trackBar
            // 
            this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar.AutoSize = false;
            this.trackBar.LargeChange = 60;
            this.trackBar.Location = new System.Drawing.Point(61, 0);
            this.trackBar.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.trackBar.Maximum = 3600;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(271, 27);
            this.trackBar.TabIndex = 19;
            this.trackBar.TickFrequency = 60;
            this.trackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar.Value = 10;
            this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // LabelSlider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.valueLabel);
            this.Name = "LabelSlider";
            this.Size = new System.Drawing.Size(332, 27);
            this.Enter += new System.EventHandler(this.LabelSlider_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.TrackBar trackBar;
    }
}
