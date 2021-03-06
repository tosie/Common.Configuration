﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Common.Configuration {

    /// <summary>GUI editor for instances of <see cref="GenericConfiguration"/> sets.</summary>
    public partial class ConfigurationControl : UserControl {
        
        #region Properties / Class Variables

        protected GenericConfiguration configuration;

        /// <summary>
        /// A single instance of <see cref="GenericConfiguration"/> that should be shown on the control.
        /// Overrides <see cref="MultipleConfigs"/> if set.
        /// </summary>
        public GenericConfiguration Configuration {
            get {
                return configuration;
            }

            set {
                configuration = value;
                multipleConfigs = null;
                ShowSingleConfiguration();
            }
        }

        protected List<GenericConfiguration> multipleConfigs;

        /// <summary>
        /// Multiple instances of <see cref="GenericConfiguration"/> that should be shown on the control.
        /// Overrides <see cref="Configuration"/> is set.
        /// </summary>
        public List<GenericConfiguration> MultipleConfigs {
            get {
                return multipleConfigs;
            }

            set {
                multipleConfigs = value;
                configuration = null;
                ShowMultipleConfigurations();
            }
        }

        /// <summary>
        /// If true all changes of the controls are immediately reflected back to their corresponding
        /// <see cref="ConfigurationEntry"/>. If false a call to <see cref="Save"/> is required to
        /// assign the new values entered by a user to the entries.
        /// </summary>
        [DefaultValue(false)]
        public Boolean AutoSave { get; set; }

        protected Boolean Loading = false;
        protected Boolean Saving = false;

        protected Dictionary<ConfigurationEntry, Control> ControlMapping;

        #endregion

        #region Constructors / Initialization

        /// <summary>
        /// Create a new instance of this class.
        /// </summary>
        public ConfigurationControl() {
            AutoSave = false;

            ControlMapping = new Dictionary<ConfigurationEntry, Control>();

            InitializeComponent();
        }

        ~ConfigurationControl() {
            UnmapControls();
        }

        #endregion

        #region Table Handling

        private void AddConfigurationEntriesToTable(GenericConfiguration Config) {
            // Build entry groups (GroupKey)
            Int32 first_row = Table.RowCount;
            Int32 total_rows = 0;
            Dictionary<Object, List<ConfigurationEntry>> groups = new Dictionary<Object, List<ConfigurationEntry>>();
            foreach (ConfigurationEntry entry in Config) {
                // Ignore entries that don't want to be edited or shown
                if (entry.ControlType == ConfigurationEntry.ControlTypes.None)
                    continue;

                // Make sure that the object used as key in the dictionary is not null
                Object group = (entry.GroupKey == null ? "" : entry.GroupKey);

                // Initialize a new group in the dictionary
                if (!groups.ContainsKey(group)) {
                    groups[group] = new List<ConfigurationEntry>();
                    total_rows++;
                }

                // Add the entry to the dictionary
                groups[group].Add(entry);
                total_rows++;

                // If there is a SubText, it needs to be counted, too
                if (!String.IsNullOrEmpty(entry.SubText))
                    total_rows++;
            }

            // Sort the grouped lists (SortKey)
            foreach (List<ConfigurationEntry> list in groups.Values) {
                list.Sort((a, b) => a.SortKey - b.SortKey);
            }

            // Increase the row counter for the final label (see the end of this method)
            total_rows++;

            // Setup the container panel
            Table.RowCount += total_rows;
            UpdateTableStyles(first_row);

            // Create the necessary controls
            Int32 row = first_row;
            foreach (KeyValuePair<Object, List<ConfigurationEntry>> kv in groups) {
                // Add a group heading, if it is not empty
                // (it is made sure that group is not null further up)
                String group = kv.Key.ToString();
                if (!String.IsNullOrEmpty(group)) {
                    AddHeadingToRow(group, row, true);
                    row++;
                }

                // Add the value rows
                foreach (ConfigurationEntry entry in kv.Value) {
                    switch (entry.ControlType) {
                        case ConfigurationEntry.ControlTypes.TextBox:
                            AddHeadingToRow(entry.Text, row, false);
                            ControlMapping[entry] = AddTextBoxToRow(entry, row);
                            break;
                        case ConfigurationEntry.ControlTypes.MultiLineTextBox:
                            AddHeadingToRow(entry.Text, row, false);
                            ControlMapping[entry] = AddMultilineBoxToRow(entry, row);
                            break;
                        case ConfigurationEntry.ControlTypes.ComboBox:
                            AddHeadingToRow(entry.Text, row, false);
                            ControlMapping[entry] = AddComboBoxToRow(entry, row);
                            break;
                        case ConfigurationEntry.ControlTypes.ComboBoxAsLinkLabel:
                        case ConfigurationEntry.ControlTypes.ButtonAsLinkLabel: 
                            AddHeadingToRow(entry.Text, row, false);
                            ControlMapping[entry] = AddLinkLabelToRow(entry, row);
                            break;
                        case ConfigurationEntry.ControlTypes.CheckBox:
                            ControlMapping[entry] = AddCheckBoxToRow(entry, row);
                            break;
                        case ConfigurationEntry.ControlTypes.Label:
                            AddHeadingToRow(entry.Text, row, false);
                            ControlMapping[entry] = AddLabelToRow(entry, row);
                            break;
                        case ConfigurationEntry.ControlTypes.GenericConfiguration:
                        case ConfigurationEntry.ControlTypes.Button:
                            AddHeadingToRow(entry.Text, row, false);
                            ControlMapping[entry] = AddButtonToRow(entry, row);
                            break;
                        case ConfigurationEntry.ControlTypes.Slider:
                            AddHeadingToRow(entry.Text, row, false);
                            ControlMapping[entry] = AddLabelSliderToRow(entry, row);
                            break;
                        case ConfigurationEntry.ControlTypes.File:
                        case ConfigurationEntry.ControlTypes.Directory:
                            AddHeadingToRow(entry.Text, row, false);
                            ControlMapping[entry] = AddFileSystemBrowserToRow(entry, row,
                                entry.ControlType == ConfigurationEntry.ControlTypes.Directory);
                            break;
                        default:
                            throw new ArgumentException(String.Format("Unknown input type: {0}", entry.ControlType));
                    }
                    entry.PropertyChanged += UpdateControlWithNewValue;
                    row++;

                    if (!String.IsNullOrEmpty(entry.SubText)) {
                        AddSubTextLabelToRow(entry, row);
                        row++;
                    }
                }
            }

            // Add a final label with no text to add some extra spacing
            AddHeadingToRow("", row, false);
        }

        private void ClearTable() {
            // Clear everything
            Table.Controls.Clear();
            UnmapControls();

            // Setup the container panel
            Table.ColumnCount = 2;
            //Table.ColumnStyles[0].SizeType = SizeType.AutoSize;
            Table.ColumnStyles[0].Width = 0.25f;
            Table.ColumnStyles[1].Width = 1.0f - Table.ColumnStyles[0].Width;
            Table.RowStyles.Clear();
            Table.RowCount = 0;
            UpdateTableStyles(0);
        }

        private void AddDummyRow() {
            // the extra row is there to fill any remaining space
            Table.RowCount += 1;
            UpdateTableStyles(Table.RowCount - 2);
        }

        private void UpdateTableStyles(Int32 FirstRow) {
            for (int i = FirstRow; i < Table.RowCount; i++) {
                Table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }
        }

        #endregion

        #region Control Creation

        void ApplyBasicControlSettings(Control Control, ConfigurationEntry Entry, Int32 Column, Int32 Row) {
            if (Entry != null) {
                Control.Tag = Entry;
                Control.Enabled = !Entry.ReadOnly;
                Control.Leave += new EventHandler(control_Leave);
            }

            Table.Controls.Add(Control);
            Table.SetCellPosition(Control, new TableLayoutPanelCellPosition(Column, Row));
        }

        Label AddHeadingToRow(String Text, Int32 Row, Boolean GroupHeader) {
            Label lbl = new Label();
            ApplyBasicControlSettings(lbl, null, 0, Row);
            
            // Layout
            lbl.AutoSize = true;
            lbl.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            lbl.TextAlign = ContentAlignment.TopRight;
            lbl.Margin = new Padding(
                lbl.Margin.Left,
                lbl.Margin.Top + (int)lbl.Font.Size,
                lbl.Margin.Right,
                lbl.Margin.Bottom);
            lbl.AutoEllipsis = true;
            
            // Content
            lbl.Text = Text;
            lbl.Click += new EventHandler(rowHeading_Click);

            // Position inside table
            if (GroupHeader) {
                lbl.Margin = new Padding(10, (Row == 0 ? 0 : 10), 0, 0);
                lbl.Font = new Font(lbl.Font.FontFamily, lbl.Font.Size * 1.2f, FontStyle.Bold);
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                Table.SetColumnSpan(lbl, 2);
            }

            return lbl;
        }

        TextBox AddTextBoxToRow(ConfigurationEntry Entry, Int32 Row) {
            TextBox control = new TextBox();
            ApplyBasicControlSettings(control, Entry, 1, Row);
            
            // Layout
            control.Anchor = AnchorStyles.Right | AnchorStyles.Left;

            // Content
            control.Text = Entry.ValueAsString;
            control.TextChanged += new EventHandler(control_ValueChanged);

            return control;
        }

        TextBox AddMultilineBoxToRow(ConfigurationEntry Entry, Int32 Row) {
            TextBox control = AddTextBoxToRow(Entry, Row);

            control.Multiline = true;
            control.Height = control.Height * 3;
            control.ScrollBars = ScrollBars.Vertical;
            control.WordWrap = true;

            return control;
        }

        ComboBox AddComboBoxToRow(ConfigurationEntry Entry, Int32 Row) {
            ComboBox cbx = new ComboBox();
            ApplyBasicControlSettings(cbx, Entry, 1, Row);
            
            // Layout
            cbx.Anchor = AnchorStyles.Right | AnchorStyles.Left;

            // Content
            cbx.DropDownStyle = ComboBoxStyle.DropDownList;
            object[] values = Entry.GetPossibleValues();
            if (values != null && values.Length > 0)
                cbx.Items.AddRange(values);
            if (Entry.Value != null)
                cbx.SelectedIndex = cbx.Items.IndexOf(Entry.Value);
            cbx.SelectedIndexChanged += new EventHandler(control_ValueChanged);

            return cbx;
        }

        LinkLabel AddLinkLabelToRow(ConfigurationEntry Entry, Int32 Row) {
            LinkLabel control = new LinkLabel();
            ApplyBasicControlSettings(control, Entry, 1, Row);

            // Layout
            control.AutoSize = true;
            control.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            control.TextAlign = ContentAlignment.TopLeft;
            control.Margin = new Padding(
                control.Margin.Left,
                control.Margin.Top + (int)control.Font.Size,
                control.Margin.Right,
                control.Margin.Bottom);
            control.AutoEllipsis = true;

            // Content
            var default_text = "<Bitte auswählen>";
            control.Text = Entry.GetValueAsString(Entry.Value, default_text);
            if (String.IsNullOrEmpty(control.Text)) // Empty strings hide the label and the user will not
                control.Text = default_text;        // be able to click on it anymore -- catch that here.
            control.LinkClicked += new LinkLabelLinkClickedEventHandler(linklabel_LinkClicked);

            return control;
        }

        CheckBox AddCheckBoxToRow(ConfigurationEntry Entry, Int32 Row) {
            CheckBox cb = new CheckBox();
            ApplyBasicControlSettings(cb, Entry, 1, Row);
            
            // Layout
            cb.Anchor = AnchorStyles.Right | AnchorStyles.Left;

            // Content
            cb.Text = Entry.Text;
            cb.Checked = (Entry.Value == null ? false : Entry.Value is Boolean ? (Boolean)Entry.Value : Boolean.Parse(Entry.Value.ToString()));
            cb.CheckedChanged += new EventHandler(control_ValueChanged);

            return cb;
        }

        Label AddLabelToRow(ConfigurationEntry Entry, Int32 Row) {
            Label lbl = new Label();
            ApplyBasicControlSettings(lbl, Entry, 1, Row);

            // Layout
            lbl.AutoSize = true;
            lbl.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            lbl.TextAlign = ContentAlignment.TopLeft;
            lbl.Margin = new Padding(
                lbl.Margin.Left,
                lbl.Margin.Top + (int)lbl.Font.Size,
                lbl.Margin.Right,
                lbl.Margin.Bottom);
            lbl.AutoEllipsis = true;
            
            // Content
            lbl.Text = Entry.ValueAsString;

            return lbl;
        }

        Label AddSubTextLabelToRow(ConfigurationEntry Entry, Int32 Row) {
            Label lbl = new Label();
            ApplyBasicControlSettings(lbl, Entry, 1, Row);

            // Layout
            lbl.AutoSize = true;
            lbl.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            lbl.TextAlign = ContentAlignment.TopLeft;
            lbl.ForeColor = SystemColors.GrayText;
            lbl.Margin = new Padding(lbl.Margin.Left, 0, lbl.Margin.Right,
                9);

            // Content
            lbl.Text = Entry.SubText;

            return lbl;
        }

        Button AddButtonToRow(ConfigurationEntry Entry, Int32 Row) {
            Button btn = new Button();
            ApplyBasicControlSettings(btn, Entry, 1, Row);
            
            // Layout
            btn.BackColor = SystemColors.ButtonFace;

            // Content
            btn.Text = "Ändern";
            btn.Click += new EventHandler(btn_Click);

            return btn;
        }

        LabelSlider AddLabelSliderToRow(ConfigurationEntry Entry, Int32 Row) {
            LabelSlider sld = new LabelSlider();
            ApplyBasicControlSettings(sld, Entry, 1, Row);

            // Layout
            sld.Anchor = AnchorStyles.Right | AnchorStyles.Left;

            // Content
            sld.FormatValue += new LabelSlider.FormatValueEvent(sld_FormatValue);
            sld.Minimum = ConfigurationEntry.ToInt32(Entry.Minimum, 0);
            sld.Maximum = ConfigurationEntry.ToInt32(Entry.Maximum, 10);
            sld.SmallChange = 1;
            sld.LargeChange = Convert.ToInt32(Math.Round((Convert.ToDouble(sld.Maximum - sld.Minimum)) / 10.0));
            sld.Value = ConfigurationEntry.ToInt32(Entry.Value, sld.Minimum);
            sld.ValueChanged += new EventHandler(control_ValueChanged);

            return sld;
        }

        FileSystemBrowser AddFileSystemBrowserToRow(ConfigurationEntry Entry, Int32 Row, Boolean IsDirectoryBrowser) {
            FileSystemBrowser control = new FileSystemBrowser();
            ApplyBasicControlSettings(control, Entry, 1, Row);

            // Layout
            control.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            // Content
            control.IsDirectoryBrowser = IsDirectoryBrowser;
            control.Value = Entry.ValueAsString;
            control.ValueChanged += new EventHandler(control_ValueChanged);

            return control;
        }

        #endregion

        #region User Interaction

        void rowHeading_Click(object sender, EventArgs e) {
            Label lbl = (Label)sender;
            TableLayoutPanelCellPosition position = Table.GetCellPosition(lbl);
            Control control = Table.GetControlFromPosition(1, position.Row);
            if (control != null)
                control.Focus();
        }

        void btn_Click(object sender, EventArgs e) {
            Button control = (Button)sender;
            ConfigurationEntry entry = (ConfigurationEntry)control.Tag;

            if (entry.ControlType == ConfigurationEntry.ControlTypes.GenericConfiguration && entry.Value != null) {
                GenericConfiguration value = (GenericConfiguration)entry.Value;
                ConfigurationForm.EditConfiguration(null, value);
            } else if (entry.ControlType == ConfigurationEntry.ControlTypes.Button) {
                entry.RaiseEditor(this.ParentForm);
            }
        }

        string sld_FormatValue(LabelSlider Sender, int Value) {
            ConfigurationEntry entry = (ConfigurationEntry)Sender.Tag;
            return entry.GetValueAsString(GetValueOfControl(Sender));
        }

        private void ShowPopupMenu(Control sender, List<String> items, List<Object> tags, String selected, EventHandler handler) {
            if (tags != null && tags.Count != items.Count) {
                throw new ArgumentOutOfRangeException("tags", "Die Anzahl von +items+ und +tags+ muss gleich sein.");
            }

            cmsDynamicPopup.Items.Clear();
            items.ForEach(text => {
                ToolStripMenuItem item = new ToolStripMenuItem(text, null, handler);
                if (tags != null) {
                    item.Tag = tags[0];
                    tags.RemoveAt(0);
                }

                item.Checked = (text == selected);
                cmsDynamicPopup.Items.Add(item);
            });
            cmsDynamicPopup.Show(sender, 2, sender.Height + 2);
        }

        void linklabel_ItemSelected(object sender, EventArgs e) {
            object[] options = (object[])(sender as ToolStripMenuItem).Tag;
            LinkLabel control = (LinkLabel)options[0];
            ConfigurationEntry entry = (ConfigurationEntry)control.Tag;
            object value = options[1];

            control.Text = entry.GetValueAsString(value, "<Bitte auswählen>");

            if (ValidateValueOfControl(control, entry, ref value) && !AutoSave) {
                // This control always saves after changing the value
                Saving = true;
                entry.Value = value;
                Saving = false;
            }
        }

        void linklabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            LinkLabel control = (LinkLabel)sender;
            ConfigurationEntry entry = (ConfigurationEntry)control.Tag;

            if (entry.ControlType == ConfigurationEntry.ControlTypes.ButtonAsLinkLabel) {

                if (entry.RaiseEditor(this.ParentForm)) {
                    control.Text = entry.GetValueAsString(entry.Value, "<Bitte auswählen>");
                }
            
            } else if (entry.ControlType == ConfigurationEntry.ControlTypes.ComboBoxAsLinkLabel) {
            
                object[] values = entry.GetPossibleValues();
                if (values == null || values.Length <= 0)
                    return;

                string[] strings = new string[values.Length];
                object[] tags = new object[values.Length];
                for (int i = 0; i < values.Length; i++) {
                    strings[i] = entry.GetValueAsString(values[i], "---");
                    tags[i] = new object[] { sender, values[i] };
                }

                ShowPopupMenu(
                    control,
                    strings.ToList(),
                    tags.ToList(),
                    entry.ValueAsString,
                    new EventHandler(linklabel_ItemSelected));

            }
        }

        #endregion

        #region Input Validation

        Boolean ValidateValueOfControl(Control Control, ConfigurationEntry Entry, ref Object Value) {
            Boolean result = Entry.IsValueValid(ref Value);
            
            if (result) {
                Control.BackColor = SystemColors.Window;

                if (AutoSave) {
                    Saving = true;
                    Entry.Value = Value;
                    Saving = false;
                }
            } else {
                Control.BackColor = Color.Red;
            }

            return result;
        }

        Object GetValueOfControl(Control Control) {
            if (Control == null) {
                return null;
            } else if (Control is TextBox) {
                return (Control as TextBox).Text;
            } else if (Control is ComboBox) {
                ComboBox cbx = (Control as ComboBox);
                return (cbx.SelectedIndex >= 0 ? cbx.Items[cbx.SelectedIndex] : null);
            } else if (Control is LinkLabel) {
                return (Control as LinkLabel).Tag;
            } else if (Control is CheckBox) {
                return (Control as CheckBox).Checked;
            } else if (Control is LabelSlider) {
                return (Control as LabelSlider).Value;
            } else if (Control is FileSystemBrowser) {
                return (Control as FileSystemBrowser).Value;
            } else {
                throw new ArgumentException(String.Format("Unknown control type: {0}", Control), "Control");
            }
        }

        void control_ValueChanged(object sender, EventArgs e) {
            Control control = (Control)sender;

            // Validate the new value
            if (control.Tag == null || !(control.Tag is ConfigurationEntry))
                return;

            ConfigurationEntry entry = (ConfigurationEntry)control.Tag;
            Object value = GetValueOfControl(control);
            ValidateValueOfControl(control, entry, ref value);
        }

        void control_Leave(object sender, EventArgs e) {
            if (AutoSave)
                UpdateControlWithNewValue((sender as Control).Tag, new PropertyChangedEventArgs("Value"));
        }

        #endregion

        #region Change Management

        private void UpdateControlWithNewValue(object sender, PropertyChangedEventArgs e) {
            if (Saving)
                return;

            if (e.PropertyName != "Value")
                return;

            ConfigurationEntry entry = (ConfigurationEntry)sender;
            if (!ControlMapping.ContainsKey(entry))
                return;

            Control control = ControlMapping[entry];

            switch (entry.ControlType) {
                case ConfigurationEntry.ControlTypes.TextBox:
                case ConfigurationEntry.ControlTypes.MultiLineTextBox:
                    ((TextBox)control).Text = entry.ValueAsString;
                    break;
                case ConfigurationEntry.ControlTypes.ComboBox:
                    ((ComboBox)control).SelectedItem = ((ComboBox)control).Items.IndexOf(entry.Value);
                    break;
                case ConfigurationEntry.ControlTypes.ComboBoxAsLinkLabel:
                case ConfigurationEntry.ControlTypes.ButtonAsLinkLabel:
                    ((LinkLabel)control).Text = entry.GetValueAsString(entry.Value, "<Bitte auswählen>");
                    break;
                case ConfigurationEntry.ControlTypes.CheckBox:
                    ((CheckBox)control).Checked = (entry.Value == null ? false : entry.Value is Boolean ? (Boolean)entry.Value : Boolean.Parse(entry.Value.ToString()));
                    break;
                case ConfigurationEntry.ControlTypes.Label:
                    ((Label)control).Text = entry.ValueAsString;
                    break;
                case ConfigurationEntry.ControlTypes.GenericConfiguration:
                    // Ignore this one
                    break;
                case ConfigurationEntry.ControlTypes.Button:
                    // Ignore this one
                    break;
                case ConfigurationEntry.ControlTypes.Slider:
                    ((LabelSlider)control).Value = ConfigurationEntry.ToInt32(entry.Value, ((LabelSlider)control).Minimum);
                    break;
                case ConfigurationEntry.ControlTypes.File:
                case ConfigurationEntry.ControlTypes.Directory:
                    ((FileSystemBrowser)control).Value = entry.ValueAsString;
                    break;
                default:
                    break;
            }
        }

        private void UnmapControls() {
            foreach (ConfigurationEntry entry in ControlMapping.Keys) {
                entry.PropertyChanged -= UpdateControlWithNewValue;
            }
            ControlMapping.Clear();
        }

        #endregion

        #region Configuration Handling

        private void ShowSingleConfiguration() {
            Loading = true;
            Table.Hide();

            try {
                ClearTable();

                if (configuration == null)
                    return;

                AddConfigurationEntriesToTable(Configuration);
                AddDummyRow();
            } finally {
                Table.Show();
                Loading = false;
            }
        }

        private void ShowMultipleConfigurations() {
            Loading = true;
            Table.Hide();

            try {
                ClearTable();

                if (multipleConfigs == null)
                    return;

                Loading = true;

                MultipleConfigs.ForEach(c => AddConfigurationEntriesToTable(c));
                AddDummyRow();
            } finally {
                Table.Show();
                Loading = false;
            }
        }

        /// <summary>
        /// Stores all values of the GUI controls back to their respective <see cref="ConfigurationEntry"/>.
        /// Calling this method is not required when <see cref="AutoSave"/> is true.
        /// </summary>
        public void Save() {
            Saving = true;

            foreach (Control control in Table.Controls) {
                if (control.Tag == null || !(control.Tag is ConfigurationEntry))
                    continue;

                ConfigurationEntry entry = (ConfigurationEntry)control.Tag;
                Object value = null;
                Boolean value_extracted = false;
                
                try {
                    value = GetValueOfControl(control);
                    value_extracted = true;
                } catch { }

                if (value_extracted) {
                    try {
                        entry.Value = value;
                    } catch (ArgumentException ex) {
                        Trace.WriteLine(ex.Message, "ConfigurationControl");
                    }

                    UpdateControlWithNewValue(entry, new PropertyChangedEventArgs("Value"));
                }
            }

            Saving = false;
        }

        #endregion
    }
}
