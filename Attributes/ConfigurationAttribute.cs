using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Configuration {
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ConfigurationAttribute : Attribute {

        public String Text { get; set; }
        public Int32 SortKey { get; set; }
        public Object GroupKey { get; set; }
        public Boolean ReadOnly { get; set; }
        public Object Minimum { get; set; }
        public Object Maximum { get; set; }
        public ConfigurationEntry.InputTypeEnum InputType { get; set; }

        public String Validator { get; set; }

        public void ApplyToConfigurationEntry(ConfigurationEntry Entry) {
            Entry.Text = Text;
            Entry.SortKey = SortKey;
            Entry.GroupKey = GroupKey;
            Entry.ReadOnly = ReadOnly;
            Entry.Minimum = Minimum;
            Entry.Maximum = Maximum;
            Entry.InputType = InputType;

            if (!String.IsNullOrEmpty(Validator)) {
                switch (Validator) {
                    case "Int32":
                        Entry.ValidateValue += new ConfigurationEntry.ValidateEvent(ConfigurationEntry.ValidateInt32Value);
                        break;
                    case "Double":
                        Entry.ValidateValue += new ConfigurationEntry.ValidateEvent(ConfigurationEntry.ValidateDoubleValue);
                        break;
                    case "Boolean":
                        Entry.ValidateValue += new ConfigurationEntry.ValidateEvent(ConfigurationEntry.ValidateBooleanValue);
                        break;
                    default:
                        throw new ArgumentException(String.Format("Unknown validator: {0}", Validator), "Validator");
                }
            }
        }

        public ConfigurationAttribute(String Text, Int32 SortKey, Object GroupKey) {
            this.Text = Text;
            this.SortKey = SortKey;
            this.GroupKey = GroupKey;

            this.InputType = ConfigurationEntry.InputTypeEnum.TextBox;
        }

        public ConfigurationAttribute(String Text, Int32 SortKey, Object GroupKey, ConfigurationEntry.InputTypeEnum InputType) {
            this.Text = Text;
            this.SortKey = SortKey;
            this.GroupKey = GroupKey;
            this.InputType = InputType;
        }

        public ConfigurationAttribute(String Text, Int32 SortKey, Object GroupKey, ConfigurationEntry.InputTypeEnum InputType, Object Mininum, Object Maximum) {
            this.Text = Text;
            this.SortKey = SortKey;
            this.GroupKey = GroupKey;
            this.InputType = InputType;
            this.Minimum = Minimum;
            this.Maximum = Maximum;
        }
    }
}
