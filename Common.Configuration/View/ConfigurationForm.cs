using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Configuration {
    public partial class ConfigurationForm : Form {

        #region Properties / Class Variables

        public GenericConfiguration Configuration {
            get {
                return ConfigControl.Configuration;
            }

            set {
                ConfigControl.Configuration = value;
            }
        }

        #endregion

        #region Constructors / Initialization

        protected ConfigurationForm() {
            InitializeComponent();
        }

        public static Boolean EditConfiguration(IWin32Window Owner, GenericConfiguration Config) {
            using (ConfigurationForm form = new ConfigurationForm()) {
                form.ShowConfiguration(Config);
                if (form.ShowDialog(Owner) == DialogResult.OK) {
                    form.SaveConfiguration();
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Configuration Handling

        void ShowConfiguration(GenericConfiguration Config) {
            Configuration = Config;
        }

        void SaveConfiguration() {
            ConfigControl.Save();
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

    }
}
