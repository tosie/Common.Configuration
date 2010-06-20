using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Common.Configuration {
    public partial class FileSystemBrowser : UserControl {

        #region Properties / Class Variables

        public String Value {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        public Boolean IsDirectoryBrowser { get; set; }

        #endregion

        #region Events

        public event EventHandler ValueChanged;
        protected void RaiseValueChanged() {
            if (ValueChanged != null)
                ValueChanged(this, new EventArgs());
        }

        #endregion

        #region Constructors / Initialization

        public FileSystemBrowser() {
            InitializeComponent();
        }

        private void FileSystemBrowser_Enter(object sender, EventArgs e) {
            txtName.Focus();
        }

        #endregion

        #region Dialogs

        void SelectDirectory() {
            if (Directory.Exists(Value))
                folderBrowserDialog.SelectedPath = Value;

            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
                Value = folderBrowserDialog.SelectedPath;
        }

        void SelectFileName() {
            if (File.Exists(Value))
                openFileDialog.FileName = Value;

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                Value = openFileDialog.FileName;
        }

        #endregion

        #region GUI

        private void txtName_TextChanged(object sender, EventArgs e) {
            RaiseValueChanged();
        }

        private void btnBrowse_Click(object sender, EventArgs e) {
            if (IsDirectoryBrowser)
                SelectDirectory();
            else
                SelectFileName();
        }

        #endregion

    }
}
