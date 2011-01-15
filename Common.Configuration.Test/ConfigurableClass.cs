using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Configuration.Test {
    class ConfigurableClass {

        [Configuration("Invisible", 10,
            ControlType = ConfigurationEntry.ControlTypes.None)]
        public String InvisibleOption { get; set; }

        [Configuration("Name", 11, "First")]
        public String NewName { get; set; }

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

        [Configuration("A Label", 40, "Second",
            ControlType = ConfigurationEntry.ControlTypes.Label)]
        public String ReadOnlyText { get; set; }

        [Configuration("Checker", 60, "Second",
            ControlType = ConfigurationEntry.ControlTypes.CheckBox,
            Validator = "Boolean")]
        public Boolean DoSomething { get; set; }

        [Configuration("Option", 70, "Third",
            ControlType = ConfigurationEntry.ControlTypes.Slider,
            Minimum = 10, Maximum = 35, Validator = "Int32")]
        public Int32 Age { get; set; }

        [Configuration("Checker2", 75, "Third",
            ControlType = ConfigurationEntry.ControlTypes.CheckBox,
            Validator = "Boolean")]
        public Boolean DoSomething2 { get; set; }

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

    }
}
