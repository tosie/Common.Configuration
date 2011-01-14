using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Configuration {
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ConfigurationAttribute : Attribute {

        /// <summary>
        /// Label text naming a property in a very short manner.
        /// </summary>
        public String Text { get; set; }

        /// <summary>
        /// Text that describes a property in a more detailed fashion.
        /// </summary>
        public String SubText { get; set; }

        /// <summary>
        /// Using this the order of all configurable properties is determined.
        /// </summary>
        public Int32 SortKey { get; set; }

        /// <summary>
        /// Configurable properties are grouped using this.
        /// </summary>
        public Object GroupKey { get; set; }

        /// <summary>
        /// Defines the GUI control that should be used when editing a property.
        /// </summary>
        public ConfigurationEntry.ControlTypes ControlType { get; set; }

        /// <summary>
        /// If true a property cannot be edited by a user.
        /// </summary>
        public Boolean ReadOnly { get; set; }

        /// <summary>
        /// Minimum value that should be accepted by a validator.
        /// </summary>
        public Object Minimum { get; set; }

        /// <summary>
        /// Maximum value that should be accepted by a validator.
        /// </summary>
        public Object Maximum { get; set; }

        /// <summary>
        /// Validators to use when validating user input. Currently available validators are "Int32",
        /// "Double", "Boolean", "FileExists" and "DirectoryExists". They can be combined by separating
        /// them with a comma.
        /// </summary>
        public String Validator { get; set; }

        /// <summary>
        /// Contains a mapping of predefined validator methods with their string representation (= name).
        /// Used in <see cref="ApplyToConfigurationEntry"/>.
        /// </summary>
        protected static readonly Dictionary<String, ConfigurationEntry.ValidateEvent> AvailableValidators = new Dictionary<String, ConfigurationEntry.ValidateEvent>() {
            { "Int32", ConfigurationEntry.ValidateInt32Value },
            { "Double", ConfigurationEntry.ValidateDoubleValue },
            { "Boolean", ConfigurationEntry.ValidateBooleanValue },
            { "FileExists", ConfigurationEntry.ValidateFileExists },
            { "DirectoryExists", ConfigurationEntry.ValidateDirectoryExists }
        };

        /// <summary>
        /// Applies the settings of this attribute to the given configuration entry.
        /// </summary>
        /// <param name="Entry"></param>
        public void ApplyToConfigurationEntry(ConfigurationEntry Entry) {
            Entry.Text = Text;
            Entry.SubText = SubText;
            Entry.SortKey = SortKey;
            Entry.GroupKey = GroupKey;
            Entry.ReadOnly = ReadOnly;
            Entry.Minimum = Minimum;
            Entry.Maximum = Maximum;
            Entry.ControlType = ControlType;

            if (!String.IsNullOrEmpty(Validator)) {
                String[] validators = Validator.Split(',');
                foreach (String v in validators) {
                    String validator = v.Trim();
                    if (String.IsNullOrEmpty(validator))
                        continue;

                    if (AvailableValidators.ContainsKey(validator))
                        Entry.ValidateValue += new ConfigurationEntry.ValidateEvent(AvailableValidators[validator]);
                    else
                            throw new ArgumentException(String.Format("Unknown validator: {0}", validator), "Validator");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text">Label text naming a property in a very short manner.</param>
        /// <param name="SortKey">Using this the order of all configurable properties is determined.</param>
        public ConfigurationAttribute(String Text, Int32 SortKey) {
            this.Text = Text;
            this.SortKey = SortKey;

            this.GroupKey = null;
            this.ControlType = ConfigurationEntry.ControlTypes.TextBox;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text">Label text naming a property in a very short manner.</param>
        /// <param name="SortKey">Using this the order of all configurable properties is determined.</param>
        /// <param name="GroupKey">Configurable properties are grouped using this.</param>
        public ConfigurationAttribute(String Text, Int32 SortKey, Object GroupKey) {
            this.Text = Text;
            this.SortKey = SortKey;
            this.GroupKey = GroupKey;

            this.ControlType = ConfigurationEntry.ControlTypes.TextBox;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text">Label text naming a property in a very short manner.</param>
        /// <param name="SortKey">Using this the order of all configurable properties is determined.</param>
        /// <param name="GroupKey">Configurable properties are grouped using this.</param>
        /// <param name="ControlType">Defines the GUI control that should be used when editing a property.</param>
        public ConfigurationAttribute(String Text, Int32 SortKey, Object GroupKey, ConfigurationEntry.ControlTypes ControlType) {
            this.Text = Text;
            this.SortKey = SortKey;
            this.GroupKey = GroupKey;
            this.ControlType = ControlType;
        }

    }
}
