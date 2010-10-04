using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
                Reset();
                RefreshView();
            }
        }

        protected Dictionary<object, Dictionary<object, string>> Data = null;

        #endregion

        #region Constructor / Initialization

        public ValueMatrix() {
            InitializeComponent();

            Reset();
        }

        #endregion

        #region Data Handling

        protected virtual void Reset() {
            Grid.Columns.Clear();
            Grid.Rows.Clear();

            Data = null;
        }

        protected virtual void LoadData() {
            if (Data != null)
                return;
            
            Data = new Dictionary<object, Dictionary<object, string>>();

            if (configEntry == null)
                return;

            if (!(configEntry.Value is IDictionary))
                return;

            var from_dict = (IDictionary)configEntry.Value;

            foreach (DictionaryEntry kv in from_dict) {
                var from = kv.Key;
                Data[from] = new Dictionary<object, string>();

                var to_dict = (IDictionary)from_dict[from];

                foreach (DictionaryEntry kv2 in to_dict) {
                    var to = kv2.Key;
                    var value = (string)kv2.Value;

                    Data[from][to] = value;
                }

            }
        }

        protected virtual void SaveData(object from, object to, string value) {
            var from_dict = (IDictionary)configEntry.Value;
            var to_dict = (IDictionary)from_dict[from];
            to_dict[to] = value;
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
            if (Data == null || Data.Count <= 0)
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
