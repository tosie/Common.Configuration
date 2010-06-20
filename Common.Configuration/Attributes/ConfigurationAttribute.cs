using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Configuration {
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ConfigurationAttribute : Attribute {

        public String Text { get; set; }
        public String SubText { get; set; }
        public Int32 SortKey { get; set; }
        public Object GroupKey { get; set; }
        public Boolean ReadOnly { get; set; }
        public Object Minimum { get; set; }
        public Object Maximum { get; set; }
        public ConfigurationEntry.ControlTypes ControlType { get; set; }

        public String Validator { get; set; }

        public void ApplyToConfigurationEntry(ConfigurationEntry Entry) {
            Entry.Text = Text;
            Entry.SubText = SubText;
            Entry.SortKey = SortKey;
            Entry.GroupKey = GroupKey;
            Entry.ReadOnly = ReadOnly;
            Entry.Minimum = Minimum;
            Entry.Maximum = Maximum;
            Entry.ControlType = ControlType;

            Dictionary<String, ConfigurationEntry.ValidateEvent> available_validators = new Dictionary<String, ConfigurationEntry.ValidateEvent>() {
                { "Int32", ConfigurationEntry.ValidateInt32Value },
                { "Double", ConfigurationEntry.ValidateDoubleValue },
                { "Boolean", ConfigurationEntry.ValidateBooleanValue },
                { "FileExists", ConfigurationEntry.ValidateFileExists },
                { "DirectoryExists", ConfigurationEntry.ValidateDirectoryExists }
            };

            if (!String.IsNullOrEmpty(Validator)) {
                String[] validators = Validator.Split(',');
                foreach (String v in validators) {
                    String validator = v.Trim();
                    if (String.IsNullOrEmpty(validator))
                        continue;

                    if (available_validators.ContainsKey(validator))
                        Entry.ValidateValue += new ConfigurationEntry.ValidateEvent(available_validators[validator]);
                    else
                            throw new ArgumentException(String.Format("Unknown validator: {0}", validator), "Validator");
                }
            }
        }

        public ConfigurationAttribute(String Text, Int32 SortKey) {
            this.Text = Text;
            this.SortKey = SortKey;

            this.GroupKey = null;
            this.ControlType = ConfigurationEntry.ControlTypes.TextBox;
        }

        public ConfigurationAttribute(String Text, Int32 SortKey, Object GroupKey) {
            this.Text = Text;
            this.SortKey = SortKey;
            this.GroupKey = GroupKey;

            this.ControlType = ConfigurationEntry.ControlTypes.TextBox;
        }

        public ConfigurationAttribute(String Text, Int32 SortKey, Object GroupKey, ConfigurationEntry.ControlTypes ControlType) {
            this.Text = Text;
            this.SortKey = SortKey;
            this.GroupKey = GroupKey;
            this.ControlType = ControlType;
        }

        public ConfigurationAttribute(String Text, Int32 SortKey, Object GroupKey, ConfigurationEntry.ControlTypes ControlType, Object Mininum, Object Maximum) {
            this.Text = Text;
            this.SortKey = SortKey;
            this.GroupKey = GroupKey;
            this.ControlType = ControlType;
            this.Minimum = Minimum;
            this.Maximum = Maximum;
        }
    }
}
