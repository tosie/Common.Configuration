using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Configuration.Test.GUI {
    public partial class ValueMatrixForm : Form {

        static Dictionary<string, Dictionary<string, string>> dict = new Dictionary<string, Dictionary<string, string>>() {
            { "from1", new Dictionary<string, string>() {
                { "to1.1", "test1.1" },
                { "to1.2", "test1.2" }
            } },

            { "from2", new Dictionary<string, string>() {
                { "to2.1", "test2.1" },
                { "to2.2", "test2.2" }
            } }
        };

        public ValueMatrixForm() {
            InitializeComponent();
        }

        private void ValueMatrixForm_Load(object sender, EventArgs e) {
            var config = new ConfigurationEntry() {
                Name = "test",
                Value = dict
            };

            config.ValidateValue += new ConfigurationEntry.ValidateEvent(config_ValidateValue);
            config.FormatValue += new ConfigurationEntry.FormatValueHandler(config_FormatValue);

            valueMatrix1.ConfigEntry = config;
        }

        string config_FormatValue(ConfigurationEntry Sender, object Value) {
            return (Value == null ? "" : Value.ToString().ToUpper());
        }

        void config_ValidateValue(ConfigurationEntry Sender, ref object Value, out bool Valid) {
            var s = (string)Value;
            if (s == null) {
                Value = "";
                Valid = true;
            }

            Valid = s.StartsWith("test");
        }
    }
}
