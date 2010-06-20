using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Common.Configuration.Test.GUI {
    public partial class MainForm : Form {

        // None, TextBox, ComboBox, CheckBox, Label, Button, GenericConfiguration, Slider

        [Configuration("Automatically apply settings", 1, ControlType = ConfigurationEntry.ControlTypes.CheckBox)]
        public Boolean AutoSave {
            get { return ConfigControl.AutoSave; }
            set { ConfigControl.AutoSave = value; }
        }

        [Configuration("Invisible", 10, ControlType = ConfigurationEntry.ControlTypes.None)]
        public String InvisibleOption { get; set; }

        [Configuration("Name", 11)]
        public String NewName { get; set; }

        [Configuration("Description (read only)", 20,
            ReadOnly = true)]
        public String Description { get; set; }

        [Configuration("Option", 30, ControlType = ConfigurationEntry.ControlTypes.ComboBox)]
        public String Option { get; set; }
        public object[] OptionPossibleValues {
            get {
                return new object[] { "Option 1", "Option 2", "Option 3" };
            }
        }

        [Configuration("Checker", 40, ControlType = ConfigurationEntry.ControlTypes.CheckBox,
            Validator = "Boolean")]
        public Boolean DoSomething { get; set; }

        [Configuration("A Label", 50, ControlType = ConfigurationEntry.ControlTypes.Label)]
        public String ReadOnlyText { get; set; }

        [Configuration("Button", 60, ControlType = ConfigurationEntry.ControlTypes.Button)]
        public String WithAButton { get; set; }

        [Configuration("Option", 70, ControlType = ConfigurationEntry.ControlTypes.Slider,
            Minimum = 18, Maximum = 35, Validator = "Int32")]
        public Int32 Age { get; set; }

        [Configuration("OutDir", 80, ControlType = ConfigurationEntry.ControlTypes.Directory,
            Validator = "DirectoryExists")]
        public String OutDir { get; set; }

        [Configuration("OutFile", 81, ControlType = ConfigurationEntry.ControlTypes.File,
            Validator = "FileExists")]
        public String FileDir { get; set; }

        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            // Set up default values
            NewName = "Mika";
            Description = "Somebody";
            Option = "Option 2";
            DoSomething = true;
            ReadOnlyText = "Not to be edited.";
            WithAButton = "something";
            Age = 23;
            OutDir = Environment.CurrentDirectory;
            FileDir = Application.ExecutablePath;

            // Show the configuration
            ConfigControl.Configuration = GenericConfiguration.CreateFor(this);
        }
    }
}
