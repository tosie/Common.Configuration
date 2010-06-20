using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Common.Configuration {
    public class ConfigurationEntry : INotifyPropertyChanged {

        #region Properties

        public String Name { get; set; } // short name
        public String Text { get; set; } // text to show on a form
        public String SubText { get; set; }
        public Int32 SortKey { get; set; }
        public Object GroupKey { get; set; }
        public Object Tag { get; set; }
        public Boolean ReadOnly { get; set; }
        public Object Minimum { get; set; }
        public Object Maximum { get; set; }
        public enum ControlTypes { None, TextBox, ComboBox, CheckBox, Label, Button, GenericConfiguration, Slider };
        public ControlTypes ControlType { get; set; }
        
        protected Object value;
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

        public String ValueAsString {
            get { return GetValueAsString(value); }
        }

        public String GetValueAsString(Object Value) {
            String result = "";

            if (!RaiseFormatValue(Value, out result))
                result = (value == null ? "" : Value.ToString());

            return result;
        }

        #endregion

        #region Events

        public delegate void QueryPossibleValuesEvent(ConfigurationEntry Sender, out Object[] PossibleValues);
        public event QueryPossibleValuesEvent QueryPossibleValues;
        public Object[] GetPossibleValues() {
            if (QueryPossibleValues == null)
                return new Object[] { };

            Object[] values;
            QueryPossibleValues(this, out values);
            return values;
        }

        public delegate void ValidateEvent(ConfigurationEntry Sender, ref Object Value, out Boolean Valid);
        public event ValidateEvent ValidateValue;
        public Boolean IsValueValid(ref Object Value) {
            if (ValidateValue == null)
                return true;

            Boolean result;
            ValidateValue(this, ref Value, out result);
            return result;
        }

        public delegate void ButtonEditorHandler(ConfigurationEntry Sender);
        public event ButtonEditorHandler ButtonEditor;
        protected void RaiseButtonEditor() {
            if (ButtonEditor == null)
                return;

            ButtonEditor(this);
        }

        public delegate String FormatValueHandler(ConfigurationEntry Sender, Object Value);
        public event FormatValueHandler FormatValue;
        protected Boolean RaiseFormatValue(Object Value, out String ValueAsString) {
            ValueAsString = "";
            if (FormatValue == null)
                return false;

            ValueAsString = FormatValue(this, Value);
            return true;
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(String PropertyName) {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        #endregion

        #region Value Validators and Transformers

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

        public static void ValidateBooleanValue(ConfigurationEntry Sender, ref object Value, out bool Valid) {
            Valid = false;

            if (Value == null) {
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

        #endregion

    }
}
