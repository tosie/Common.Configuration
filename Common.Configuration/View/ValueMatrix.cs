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

        protected bool synchronizeTwoWayAssignments = true;

        [DefaultValue(true)]
        public bool SynchronizeTwoWayAssignments {
            get {
                return synchronizeTwoWayAssignments;
            }

            set {
                if (synchronizeTwoWayAssignments == value)
                    return;

                synchronizeTwoWayAssignments = value;
                SetStateOfTwoWaySynchronization(synchronizeTwoWayAssignments);
            }
        }

        protected bool allowSelfToSelfAssignments = false;

        [DefaultValue(false)]
        public bool AllowSelfToSelfAssignments {
            get {
                return allowSelfToSelfAssignments;
            }

            set {
                if (allowSelfToSelfAssignments == value)
                    return;

                allowSelfToSelfAssignments = value;
                SetStateOfSelfToSelfAssignment(value);
            }
        }

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

            // Add the row header column.
            var rowheader_column = new DataGridViewTextBoxColumn();
            rowheader_column.ReadOnly = true;
            Grid.Columns.Add(rowheader_column);
            
            // Build up a list of columns and add them to the grid view.
            var columns_without_rowheader = new Dictionary<object, int>();
            foreach (KeyValuePair<object, Dictionary<object, string>> kv in Data) {
                var from = kv.Key;

                foreach (KeyValuePair<object, string> kv2 in kv.Value) {
                    var to = kv2.Key;
                    var value = kv2.Value;

                    if (!columns_without_rowheader.ContainsKey(to)) {
                        var column = new DataGridViewTextBoxColumn();
                        column.Tag = to;
                        column.HeaderText = configEntry.GetValueAsString(to);

                        columns_without_rowheader[to] = Grid.Columns.Add(column);
                    }
                }
            }

            // Create a row template that prepares each cell with an empty string.
            var row_template = new object[Grid.Columns.Count];
            for (int i = 0; i < row_template.Length; i++) {
                row_template[i] = "";
            }

            foreach (KeyValuePair<object, Dictionary<object, string>> kv in Data) {
                var from = kv.Key;

                var rowindex = Grid.Rows.Add(row_template);
                var row = Grid.Rows[rowindex];

                var rowheader = new DataGridViewTextBoxCell();
                rowheader.Tag = from;
                rowheader.Style = Grid.ColumnHeadersDefaultCellStyle;
                rowheader.Value = configEntry.GetValueAsString(from);

                row.Cells[0] = rowheader;

                foreach (KeyValuePair<object, string> kv2 in kv.Value) {
                    var to = kv2.Key;
                    var value = kv2.Value;

                    var cell = new DataGridViewTextBoxCell();
                    cell.Tag = new KeyValuePair<object, object>(from, to);
                    cell.Value = configEntry.GetValueAsString(value);

                    var columnindex = columns_without_rowheader[to];
                    row.Cells[columnindex] = cell;
                }
            }

            SetStateOfSelfToSelfAssignment(AllowSelfToSelfAssignments);
        }

        #endregion

        #region GUI Support

        public void RefreshView() {
            LoadData();
            ShowDataInGridView();
        }

        protected DataGridViewTextBoxCell FindCellOfRelation(object from, object to) {
            for (int r = 0; r < Grid.Rows.Count; r++) {
                var row = Grid.Rows[r];
                if (row.Cells[0].Tag != from)
                    continue;

                var cell = FindCellOfTarget(row, to);
                return cell;
            }

            return null;
        }

        protected DataGridViewTextBoxCell FindCellOfTarget(DataGridViewRow row, object target) {
            // The first column is the row header column, thus start with "1" (the second column).
            for (int column = 1; column < row.Cells.Count; column++) {
                var cell = (DataGridViewTextBoxCell)row.Cells[column];
                if (cell.Tag == null)
                    continue;

                var kv = (KeyValuePair<object, object>)cell.Tag;
                var from = kv.Key;
                var to = kv.Value;

                if (to != target)
                    continue;

                return cell;
            }

            return null;
        }

        protected virtual void SetStateOfTwoWaySynchronization(bool enabled) {
            //
        }

        protected virtual void SetStateOfSelfToSelfAssignment(bool enabled) {
            for (int row = 0; row < Grid.Rows.Count; row++) {
                var cell = FindCellOfTarget(Grid.Rows[row], Grid.Rows[row].Cells[0].Tag);
                if (cell == null)
                    continue;

                cell.ReadOnly = !enabled;
                cell.Style.BackColor = (enabled ? SystemColors.Window : SystemColors.ButtonFace);
            }
        }

        #endregion

        #region GUI Events

        private void Grid_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            if (configEntry != null) {
                try {
                    var cell = Grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    object value = cell.Value;
                    bool valid = configEntry.IsValueValid(ref value);

                    if (valid) {
                        cell.ErrorText = "";
                        var from = Grid.Rows[e.RowIndex].Cells[0].Tag;
                        var to = Grid.Columns[e.ColumnIndex].Tag;

                        var new_value = configEntry.GetValueAsString(value);
                        if (!Data.ContainsKey(from))
                            Data[from] = new Dictionary<object, string>();

                        bool notify_about_changes = false;

                        if (!Data[from].ContainsKey(to) || Data[from][to] != new_value) {
                            Data[from][to] = new_value;
                            cell.Value = new_value;
                            SaveData(from, to, new_value);
                            notify_about_changes = true;
                        }

                        // Two-Way Synchronization
                        if (SynchronizeTwoWayAssignments) {
                            // Vice-versa, _not_ "from => to"!

                            if (!Data.ContainsKey(to))
                                Data[to] = new Dictionary<object, string>();

                            if (!Data[to].ContainsKey(to) || Data[to][from] != new_value) {
                                Data[to][from] = new_value;
                                var cell2 = FindCellOfRelation(to, from);
                                if (cell2 != null)
                                    cell2.Value = new_value;
                                SaveData(to, from, new_value);
                                notify_about_changes = true;
                            }
                        }

                        if (notify_about_changes)
                            configEntry.ValueHasChanged();
                    } else {
                        cell.ErrorText = "Ungültiger Wert";
                    }
                } catch { }
            }
        }

        #endregion

    }
}
