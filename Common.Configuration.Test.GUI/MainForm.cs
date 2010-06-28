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

        [Configuration("Automatically apply settings", 1,
            ControlType = ConfigurationEntry.ControlTypes.CheckBox)]
        public Boolean AutoSave {
            get { return ConfigControl.AutoSave; }
            set { ConfigControl.AutoSave = value; }
        }

        [Configuration("Invisible", 10,
            ControlType = ConfigurationEntry.ControlTypes.None)]
        public String InvisibleOption { get; set; }

        [Configuration("Name", 11, "First")]
        public String NewName {
            get { return Text; }
            set { Text = value; }
        }

        [Configuration("Description (read only)", 20, "First",
            ReadOnly = true)]
        public String Description { get; set; }

        [Configuration("Option", 30, "First",
            ControlType = ConfigurationEntry.ControlTypes.ComboBox,
            SubText = "Really long long long long long long long long long long long long long long long text.")]
        public String Option { get; set; }
        public object[] OptionPossibleValues {
            get {
                return new object[] { "Option 1", "Option 2", "Option 3" };
            }
        }

        [Configuration("Option", 30, "First",
            ControlType = ConfigurationEntry.ControlTypes.ComboBoxAsLinkLabel)]
        public String OptionLink { get; set; }
        public object[] OptionLinkPossibleValues {
            get {
                return new object[] { "Option 1", "Option 2", "Option 3" };
            }
        }

        [Configuration("Checker", 40, "Second",
            ControlType = ConfigurationEntry.ControlTypes.CheckBox,
            Validator = "Boolean")]
        public Boolean DoSomething { get; set; }

        [Configuration("A Label", 50, "Second",
            ControlType = ConfigurationEntry.ControlTypes.Label)]
        public String ReadOnlyText { get; set; }

        [Configuration("Button", 60, "Second",
            ControlType = ConfigurationEntry.ControlTypes.Button)]
        public String WithAButton { get; set; }

        protected void WithAButtonButtonEditor(ConfigurationEntry Sender) {
            MessageBox.Show(Sender.ControlType.ToString());
        }

        [Configuration("Option", 70, "Second",
            ControlType = ConfigurationEntry.ControlTypes.Slider,
            Minimum = 10, Maximum = 35, Validator = "Int32")]
        public Int32 Age { get; set; }

        [Configuration("OutDir", 80, "Third",
            ControlType = ConfigurationEntry.ControlTypes.Directory,
            Validator = "DirectoryExists",
            SubText = "Must Exist!")]
        public String OutDir { get; set; }

        [Configuration("OutFile", 81, "Third",
            ControlType = ConfigurationEntry.ControlTypes.File,
            Validator = "FileExists",
            SubText = "Slightly longer longer longer longer longer text here.")]
        public String FileDir { get; set; }

        [Configuration("Multiline Text", 90, "Fourth",
            ControlType = ConfigurationEntry.ControlTypes.MultiLineTextBox)]
        public String MultilineText { get; set; }

        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            // Set up default values
            NewName = "Mika";
            Description = "Somebody";
            Option = "Option 2";
            OptionLink = "Option 3";
            DoSomething = true;
            ReadOnlyText = "Not to be edited.";
            WithAButton = "something";
            Age = 10;
            OutDir = Environment.CurrentDirectory;
            FileDir = Application.ExecutablePath;
            MultilineText = @"Line 1
Line 2
Line 3";

            // Create a second instance of the GenericConfiguration class
            GenericConfiguration second = new GenericConfiguration();
            second.Add(new ConfigurationEntry() {
                Name = "Slider",
                Text = "Slider",
                GroupKey = "RR",
                SortKey = 1,
                ControlType = ConfigurationEntry.ControlTypes.Slider,
                Minimum = 1,
                Maximum = 30
            }, 5);
            second.Add(new ConfigurationEntry() {
                Name = "CheckBox",
                Text = "CheckBox",
                GroupKey = "RR",
                SortKey = 2,
                ControlType = ConfigurationEntry.ControlTypes.CheckBox
            });

            // Show the configuration
            //ConfigControl.Configuration = GenericConfiguration.CreateFor(this);
            ConfigControl.MultipleConfigs = (new GenericConfiguration[] {
                GenericConfiguration.CreateFor(this),
                second
            }).ToList();
        }
    }
}
