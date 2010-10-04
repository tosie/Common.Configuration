using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

namespace Common.Configuration {
    public partial class ValueMatrix : UserControl {

        #region Properties / Class Variables

        protected ConfigurationEntry configEntry = null;

        public ConfigurationEntry ConfigEntry {
            get {
                return configEntry;
            }

            set {
                if (configEntry == value)
                    return;

                configEntry = value;
                RefreshView();
            }
        }

        protected Dictionary<object, Dictionary<object, string>> Data = null;

        protected MethodInfo FromEnumerator = null;

        protected PropertyInfo FromData = null;

        protected MethodInfo ToEnumerator = null;

        protected PropertyInfo ToData = null;

        #endregion

        #region Constructor / Initialization

        public ValueMatrix() {
            InitializeComponent();

            Reset();
        }

        #endregion

        #region Data Handling

        protected virtual string GetDefaultPropertyName(Type type) {
            if (type == null)
                return "Item";
            
            var attrs = type.GetCustomAttributes(typeof(DefaultMemberAttribute), true);
            if (attrs == null || attrs.Length <= 0)
                return "Item";

            var attr = (DefaultMemberAttribute)attrs[0];
            return attr.MemberName;
        }

        protected virtual MethodInfo GetDictionaryEnumerator(Type type) {
            if (type == null)
                return null;

            // There might be more than one GetEnumerator method, so just take the first one ...
            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            if (methods == null || methods.Length <= 0)
                return null;

            for (int i = 0; i < methods.Length; i++) {
                var method = methods[i];
                if (method.Name == "GetEnumerator") {
                    return method;
                    //if (method.ReturnType == typeof(IDictionaryEnumerator))
                    //    return method;
                }
            }

            return null;
        }

        protected virtual void Reset() {
            Grid.Columns.Clear();
            Grid.Rows.Clear();

            Data = null;
            FromEnumerator = null;
            FromData = null;
            ToEnumerator = null;
            ToData = null;
        }

        protected virtual bool PreparePropertyReflection() {
            if (configEntry == null)
                throw new ArgumentNullException("configEntry");

            if (configEntry.Value == null)
                return false;

            try {
                Type from = configEntry.Value.GetType();
                FromEnumerator = GetDictionaryEnumerator(from);
                if (FromEnumerator == null)
                    return false;
                var default_indexer_name = GetDefaultPropertyName(from);
                FromData = from.GetProperty(default_indexer_name);
                if (FromData == null)
                    return false;

                Type to = FromData.PropertyType;
                ToEnumerator = GetDictionaryEnumerator(to);
                if (ToEnumerator == null)
                    return false;
                default_indexer_name = GetDefaultPropertyName(to);
                ToData = to.GetProperty(default_indexer_name);
                if (ToData == null)
                    return false;

                return true;
            } catch {
                Reset();
                return false;
            }
        }

        protected virtual void LoadData() {
            if (Data != null)
                return;
            
            Reset();
            Data = new Dictionary<object, Dictionary<object, string>>();

            if (configEntry == null)
                return;

            if (!PreparePropertyReflection())
                return;

            var from_enumerator = (IDictionaryEnumerator)FromEnumerator.Invoke(configEntry.Value, null);
            if (from_enumerator == null)
                return;

            from_enumerator.Reset();
            while (true) {
                from_enumerator.MoveNext();

                object from = null;
                try {
                    from = from_enumerator.Key;
                } catch (InvalidOperationException) {
                    break;
                }

                Data[from] = new Dictionary<object, string>();

                var to_enumerator = (IDictionaryEnumerator)ToEnumerator.Invoke(from_enumerator.Value, null);
                if (to_enumerator == null)
                    continue;

                to_enumerator.Reset();
                while (true) {
                    to_enumerator.MoveNext();

                    object to = null;
                    try {
                        to = to_enumerator.Key;
                    } catch (InvalidOperationException) {
                        break;
                    }

                    var value = (string)to_enumerator.Value;

                    Data[from][to] = value;
                }

            }
        }

        protected virtual void SaveData(object from, object to, string value) {
            // configEntry.Value[from]
            var abstract_from = FromData.GetValue(configEntry.Value, new object[] { from });

            // configEntry.Value[from][to] = value
            ToData.SetValue(abstract_from, value, new object[] { to });
        }

        protected virtual void SaveData() {
            if (Data == null || Data.Count <= 0)
                return;

            foreach (KeyValuePair<object, Dictionary<object, string>> kv in Data) {
                var from = kv.Key;

                foreach (KeyValuePair<object, string> kv2 in kv.Value) {
                    var to = kv2.Key;
                    var value = kv2.Value;

                    SaveData(from, to, value);
                }
            }
        }

        protected virtual void ShowDataInGridView() {
            if (Data.Count <= 0)
                return;

            bool first_row = true;

            var rowheader_column = new DataGridViewTextBoxColumn();
            rowheader_column.ReadOnly = true;
            Grid.Columns.Add(rowheader_column);

            foreach (KeyValuePair<object, Dictionary<object, string>> kv in Data) {
                var from = kv.Key;

                var rowheader = new DataGridViewTextBoxCell();
                rowheader.Tag = from;
                rowheader.Style = Grid.ColumnHeadersDefaultCellStyle;
                rowheader.Value = configEntry.GetValueAsString(from);

                var row = new DataGridViewRow();
                row.Cells.Add(rowheader);

                foreach (KeyValuePair<object, string> kv2 in kv.Value) {
                    var to = kv2.Key;
                    var value = kv2.Value;

                    if (first_row) {
                        var column = new DataGridViewTextBoxColumn();
                        column.Tag = to;
                        column.HeaderText = configEntry.GetValueAsString(to);
                        Grid.Columns.Add(column);
                    }

                    var cell = new DataGridViewTextBoxCell();
                    cell.Tag = new KeyValuePair<object, object>(from, to);
                    cell.Value = configEntry.GetValueAsString(value);

                    row.Cells.Add(cell);
                }

                Grid.Rows.Add(row);
                first_row = false;
            }
        }

        #endregion

        #region GUI Support

        public void RefreshView() {
            LoadData();
            ShowDataInGridView();
        }

        #endregion

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            e.FormattingApplied = false;

            if (configEntry != null) {
                try {
                    e.Value = configEntry.GetValueAsString(e.Value);
                    e.FormattingApplied = true;
                } catch { }
            }
        }

        private void Grid_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            if (configEntry != null) {
                try {
                    object value = Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    bool valid = configEntry.IsValueValid(ref value);

                    if (valid) {
                        Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";
                        var kv = (KeyValuePair<object, object>)Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag;
                        var from = kv.Key;
                        var to = kv.Value;

                        Data[from][to] = configEntry.GetValueAsString(value);
                        SaveData(from, to, (value == null ? "" : value.ToString()));
                    } else {
                        Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Ungültiger Wert";
                    }
                } catch { }
            }
        }

    }
}
