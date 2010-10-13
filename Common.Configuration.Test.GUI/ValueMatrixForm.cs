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
                { "from1", "" },
                { "from2", "" }
            } },

            { "from2", new Dictionary<string, string>() {
                { "from1", "" },
                { "from2", "" }
            } }
        };

        public ValueMatrixForm() {
            InitializeComponent();

            valueMatrix1.SynchronizeTwoWayAssignments = true;
            valueMatrix1.AllowSelfToSelfAssignments = false;
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
            return (Value == null ? "" : Value.ToString());
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
