using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace Common.Configuration {

    /// <summary>Class that represents a single element which can be configured.</summary>
    [DebuggerDisplay("ConfigurationEntry: {Name}, Value = {Value}")]
    public class ConfigurationEntry : INotifyPropertyChanged {

        #region Properties

        /// <summary>Short name of the configuration entry.</summary>
        public String Name { get; set; } // TODO: Must not contain a quote. Otherwise there will be problems with the serialization. Enforce the no-quote-allowed rule here.

        /// <summary>Short text to describe the configuration entry.</summary>
        public String Text { get; set; }

        /// <summary>Text that describes the configuration entry in a more detailed fashion.</summary>
        public String SubText { get; set; }

        /// <summary>Using this the order of all configuration entries is determined.</summary>
        public Int32 SortKey { get; set; }

        /// <summary>Configuration entries are grouped using this.</summary>
        public Object GroupKey { get; set; }

        /// <summary>Set of GUI controls that can be used to edit a configuration entry.</summary>
        public enum ControlTypes {
            /// <summary>Entry cannot be edited via GUI.</summary>
            None,

            /// <summary>Show a single line textbox.</summary>
            TextBox,

            /// <summary>Show a combobox. Remember to define the *PossibleValues property when using the <see cref="ConfigurationAttribute"/>.</summary>
            ComboBox,

            /// <summary>Show a checkbox.</summary>
            CheckBox,

            /// <summary>Show a label. Editing will not be possible.</summary>
            Label,

            /// <summary>Show a button. The *Editor method will be called upon a button click when using the <see cref="ConfigurationAttribute"/>.</summary>
            Button,

            /// <summary>If the entry's value type is of <see cref="GenericConfiguration"/> use this
            /// to show a button to edit that configuration.</summary>
            GenericConfiguration,

            /// <summary>Show a slider.</summary>
            Slider,

            /// <summary>Show a file selector textbox.</summary>
            File,

            /// <summary>Show a directory selector textbox.</summary>
            Directory,

            /// <summary>Show a multiline textbox.</summary>
            MultiLineTextBox,

            /// <summary>Show a link label that opens a menu containing combobox items whenever it is clicked.</summary>
            ComboBoxAsLinkLabel,

            /// <summary>Show a link label that raises the *Editor method (when using the <see cref="ConfigurationAttribute"/>) or the <see cref="Editor"/> event whenever it is clicked.</summary>
            ButtonAsLinkLabel
        };

        /// <summary>Defines the GUI control to be used when editing a configuration entry.</summary>
        public ControlTypes ControlType { get; set; }

        /// <summary>Custom object assigned to the configuration entry. Is not used internally.</summary>
        public Object Tag { get; set; }

        /// <summary>If true the GUI control representing the property will be disabled and any user input is prohibited.</summary>
        public Boolean ReadOnly { get; set; }

        /// <summary>Minimum value that can be accepted. Used by validators.</summary>
        public Object Minimum { get; set; }

        /// <summary>Maximum value that can be accepted. Used by validators.</summary>
        public Object Maximum { get; set; }

        /// <summary>Holds the current value of the configuration entry. Should be accessed using the <see cref="Value"/> property.</summary>
        protected Object value;

        /// <summary>Current value of the configuration entry. Raises the <see cref="PropertyChanged"/> event whenever the value changes.</summary>
        public Object Value {
            get {
                return value;
            }

            set {
                Object converted_value = value;
                if (value == this.value)
                    return;

                if (!IsValueValid(ref converted_value))
                    throw new ArgumentException(String.Format("Invalid value for property {0}: {1}", Name, value));

                this.value = converted_value;
                RaisePropertyChanged("Value");
            }
        }

        /// <summary>Returns the current value as a formatted string.</summary>
        public String ValueAsString {
            get { return GetValueAsString(value); }
        }

        /// <summary>
        /// Returns the given value as a formatted string.
        /// </summary>
        /// <param name="Value">The value to format.</param>
        public String GetValueAsString(Object Value) {
            return GetValueAsString(Value, "");
        }

        /// <summary>
        /// Returns the given value as a formatted string using the <paramref name="NullString"/>
        /// when the <paramref name="Value"/> is null.
        /// </summary>
        /// <param name="Value">The value to format.</param>
        /// <param name="NullString">The string to show when <paramref name="Value"/> is null.</param>
        /// <returns></returns>
        public String GetValueAsString(Object Value, String NullString) {
            String result = "";

            if (!RaiseFormatValue(Value, out result))
                result = (value == null ? NullString : Value.ToString());

            return result;
        }

        #endregion

        #region Events

        #region PossibleValues

        /// <summary>
        /// Defines the signature of the <see cref="QueryPossibleValues"/> event.
        /// </summary>
        /// <param name="Sender">Configuration entry for which to return an array of possible values.</param>
        /// <param name="PossibleValues">The parameter that receives the array of possible values.</param>
        public delegate void QueryPossibleValuesEvent(ConfigurationEntry Sender, out Object[] PossibleValues);

        /// <summary>
        /// Event that is raised whenever a list of valid and accepted values for the configuration entry is needed
        /// (e.g. when showing a combobox to edit the configuration entry).
        /// </summary>
        public event QueryPossibleValuesEvent QueryPossibleValues;

        /// <summary>
        /// Helper method that queries handlers of the <see cref="QueryPossibleValues"/> event and returns the array
        /// of possible values.
        /// </summary>
        /// <returns></returns>
        public Object[] GetPossibleValues() {
            if (QueryPossibleValues == null)
                return new Object[] { };

            Object[] values;
            QueryPossibleValues(this, out values);
            return values;
        }

        #endregion

        #region Validate

        /// <summary>
        /// Defines the signature for the <see cref="ValidateValue"/> event.
        /// </summary>
        /// <param name="Sender">Configuration entry for which a value should be validated.</param>
        /// <param name="Value">The value to validate. Passed by reference and may be changed by event handlers.</param>
        /// <param name="Valid">Set to true if the value is valid, otherwise false.</param>
        public delegate void ValidateEvent(ConfigurationEntry Sender, ref Object Value, out Boolean Valid);

        /// <summary>
        /// Event used to validate values for the configuration entry.
        /// </summary>
        public event ValidateEvent ValidateValue;

        /// <summary>
        /// Determines if a given value is valid or not by using handlers of the <see cref="ValidateValue"/> event.
        /// </summary>
        /// <param name="Value">The value to validate. Passed by reference and may be changed by a call to this method.</param>
        /// <returns>True if the value is valid, false otherwise.</returns>
        public Boolean IsValueValid(ref Object Value) {
            if (ValidateValue == null)
                return true;

            Boolean result;
            ValidateValue(this, ref Value, out result);
            return result;
        }

        #endregion

        #region Editor

        /// <summary>
        /// Defines the signature of the <see cref="Editor"/> event.
        /// </summary>
        /// <param name="Sender">Configuration entry for which to show an editor.</param>
        /// <param name="Owner">Window that acts as owner of any newly created windows by the event handlers.</param>
        /// <returns>True if the editor changed the value, false otherwise.</returns>
        public delegate Boolean EditorHandler(ConfigurationEntry Sender, IWin32Window Owner);

        /// <summary>
        /// Event that is called when an advanced window to edit the configuration entry should be shown.
        /// </summary>
        public event EditorHandler Editor;

        /// <summary>
        /// Uses the <see cref="Editor"/> event to show an advanced editor window to modify the configuration entry.
        /// </summary>
        /// <param name="Owner">Window that acts as owner of newly created windows.</param>
        /// <returns>True if the value changed, false otherwise.</returns>
        public Boolean RaiseEditor(IWin32Window Owner) {
            if (Editor == null)
                return false;

            return Editor(this, Owner);
        }

        #endregion

        #region FormatValue

        /// <summary>
        /// Defines the signature of the <see cref="FormatValue"/> event.
        /// </summary>
        /// <param name="Sender">The configuration entry for which a value should be formatted as a string.</param>
        /// <param name="Value">The value to be formatted.</param>
        /// <returns>A formatted string representing the <paramref name="Value"/>.</returns>
        public delegate String FormatValueHandler(ConfigurationEntry Sender, Object Value);

        /// <summary>
        /// Raised whenever the value of a configuration entry needs to be represented as a string.
        /// </summary>
        public event FormatValueHandler FormatValue;

        /// <summary>
        /// Formats a value to a string representation using the <see cref="FormatValue"/> event.
        /// </summary>
        /// <param name="Value">The value to be formatted.</param>
        /// <param name="ValueAsString">The string representation of the given value.</param>
        /// <returns>True if the value was formatted successfully, otherwise false.</returns>
        protected Boolean RaiseFormatValue(Object Value, out String ValueAsString) {
            ValueAsString = "";
            if (FormatValue == null)
                return false;

            ValueAsString = FormatValue(this, Value);
            return true;
        }

        #endregion

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised whenever the value of a property has changed. Currently this is used
        /// exclusively for the <see cref="Value"/> property.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(String PropertyName) {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event for the <see cref="Value"/> property.
        /// </summary>
        public void ValueHasChanged() {
            RaisePropertyChanged("Value");
        }

        #endregion

        #region Serialization / Deserialization

        /// <summary>
        /// Converts the <see cref="Value"/> to a string that can be re-converted by <see cref="Deserialize"/>.
        /// </summary>
        /// <returns></returns>
        public string Serialize() {
            if (Value == null)
                return "";
            else
                return Value.ToString();
        }

        /// <summary>
        /// Deserializes a string serialized by a call to (<seealso cref="Serialize"/>) and assigns it to the <see cref="Value"/> property..
        /// </summary>
        /// <param name="data"></param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Deserialize(string data) {
            try {
                Value = data;
                return true;
            } catch (ArgumentException) {
                return false;
            }
        }

        #endregion

        #region Value Validators and Transformers

        /// <summary>
        /// Tries to convert everything into an Int32.
        /// </summary>
        /// <param name="Value">The value to be converted.</param>
        /// <param name="Default">The value to use when the given <paramref name="Value"/> cannot be converted.</param>
        /// <returns>The converted value.</returns>
        public static Int32 ToInt32(Object Value, Int32 Default) {
            if (Value == null)
                return Default;

            Int32 temp_value = Default;
            if (Value is Int32)
                temp_value = (Int32)Value;
            else
                Int32.TryParse(Value.ToString(), out temp_value);

            return temp_value;
        }

        /// <summary>
        /// Tries to convert everything into an Double.
        /// </summary>
        /// <param name="Value">The value to be converted.</param>
        /// <param name="Default">The value to use when the given <paramref name="Value"/> cannot be converted.</param>
        /// <returns>The converted value.</returns>
        public static Double ToDouble(Object Value, Double Default) {
            if (Value == null)
                return Default;

            Double temp_value = Default;
            if (Value is Double)
                temp_value = (Double)Value;
            else
                Double.TryParse(Value.ToString(), out temp_value);

            return temp_value;
        }

        /// <summary>
        /// Generic validator for Int32 values. Includes constraint checking by respecting <see cref="Minimum"/> and <see cref="Maximum"/> values.
        /// </summary>
        /// <param name="Sender">The configuration entry for which the given <paramref name="Value"/> should be validated.</param>
        /// <param name="Value">The value to validate.</param>
        /// <param name="Valid">True if the value is valid or has been converted into a valid value, otherwise false.</param>
        public static void ValidateInt32Value(ConfigurationEntry Sender, ref object Value, out bool Valid) {
            Valid = false;

            // Value conversion
            Int32 temp_value = 0;
            if (Value is Int32) {
                Valid = true;
                temp_value = (Int32)Value;
            } else if (Value != null && Int32.TryParse(Value.ToString(), out temp_value)) {
                Valid = true;
            }

            // Constraint checking
            Int32 min = ToInt32(Sender.Minimum, Int32.MinValue);
            Int32 max = ToInt32(Sender.Maximum, Int32.MaxValue);
            if (Valid)
                Valid = temp_value >= min && temp_value <= max;

            // Transformation
            if (Valid)
                Value = temp_value;
        }


        /// <summary>
        /// Generic validator for Double values. Includes constraint checking by respecting <see cref="Minimum"/> and <see cref="Maximum"/> values.
        /// </summary>
        /// <param name="Sender">The configuration entry for which the given <paramref name="Value"/> should be validated.</param>
        /// <param name="Value">The value to validate.</param>
        /// <param name="Valid">True if the value is valid or has been converted into a valid value, otherwise false.</param>
        public static void ValidateDoubleValue(ConfigurationEntry Sender, ref object Value, out bool Valid) {
            Valid = false;

            // Value conversion
            Double temp_value = 0.0;
            if (Value is Double) {
                Valid = true;
                temp_value = (Double)Value;
            } else if (Value != null && Double.TryParse(Value.ToString(), out temp_value)) {
                Valid = true;
            }

            // Constraint checking
            Double min = ToDouble(Sender.Minimum, Double.MinValue);
            Double max = ToDouble(Sender.Maximum, Double.MaxValue);
            if (Valid)
                Valid = temp_value >= min && temp_value <= max;

            // Transformation
            if (Valid)
                Value = temp_value;
        }

        /// <summary>
        /// Generic validator for Boolean values.
        /// </summary>
        /// <param name="Sender">The configuration entry for which the given <paramref name="Value"/> should be validated.</param>
        /// <param name="Value">The value to validate.</param>
        /// <param name="Valid">True if the value is valid or has been converted into a valid value, otherwise false.</param>
        public static void ValidateBooleanValue(ConfigurationEntry Sender, ref object Value, out bool Valid) {
            Valid = false;

            if (Value == null || (Value is String && (String)Value == "")) {
                Value = false;
                Valid = true;
                return;
            }

            Boolean bool_value;
            if (Boolean.TryParse(Value.ToString(), out bool_value)) {
                Value = bool_value;
                Valid = true;
                return;
            }
        }

        /// <summary>
        /// Generic validator for files. Checks a file exists.
        /// </summary>
        /// <param name="Sender">The configuration entry for which the given <paramref name="Value"/> should be validated.</param>
        /// <param name="Value">The value to validate.</param>
        /// <param name="Valid">True if the value is valid or has been converted into a valid value, otherwise false.</param>
        public static void ValidateFileExists(ConfigurationEntry Sender, ref object Value, out bool Valid) {
            Valid = false;
            if (Value == null)
                return;

            String val = Value.ToString();

            // Accept empty strings
            if (String.IsNullOrEmpty(val)) {
                Valid = true;
                return;
            }

            Valid = File.Exists(val);
        }

        /// <summary>
        /// Generic validator for directories. Checks if a directory exists.
        /// </summary>
        /// <param name="Sender">The configuration entry for which the given <paramref name="Value"/> should be validated.</param>
        /// <param name="Value">The value to validate.</param>
        /// <param name="Valid">True if the value is valid or has been converted into a valid value, otherwise false.</param>
        public static void ValidateDirectoryExists(ConfigurationEntry Sender, ref object Value, out bool Valid) {
            Valid = false;
            if (Value == null)
                return;

            String val = Value.ToString();

            // Accept empty strings
            if (String.IsNullOrEmpty(val)) {
                Valid = true;
                return;
            }

            Valid = Directory.Exists(val);
        }

        #endregion

    }

}
