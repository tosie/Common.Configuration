using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Configuration {
    public partial class LabelSlider : UserControl {

        #region Properties / Class Variables

        Boolean first = true;

        public Int32 Value {
            get { return trackBar.Value; }
            set {
                trackBar.Value = value;
                if (first) {
                    first = false;
                    UpdateLabel();
                }
            }
        }

        public Int32 Minimum {
            get { return trackBar.Minimum; }
            set { trackBar.Minimum = value; }
        }

        public Int32 Maximum {
            get { return trackBar.Maximum; }
            set { trackBar.Maximum = value; }
        }

        public Int32 SmallChange {
            get { return trackBar.SmallChange; }
            set { trackBar.SmallChange = value; }
        }

        public Int32 LargeChange {
            get { return trackBar.LargeChange; }
            set { trackBar.LargeChange = value; }
        }

        public override String Text {
            get { return valueLabel.Text; }
            set { valueLabel.Text = value; }
        }

        #endregion

        #region Events

        public event EventHandler ValueChanged;
        protected void RaiseValueChanged() {
            if (ValueChanged != null)
                ValueChanged(this, new EventArgs());
        }

        public delegate String FormatValueEvent(LabelSlider Sender, Int32 Value);
        public event FormatValueEvent FormatValue;
        protected String RaiseFormatValueEvent(Int32 Value) {
            if (FormatValue != null)
                return FormatValue(this, Value);
            else
                return Value.ToString();
        }

        #endregion

        #region Constructors / Initialization

        public LabelSlider() {
            InitializeComponent();
        }

        private void LabelSlider_Enter(object sender, EventArgs e) {
            trackBar.Focus();
        }

        #endregion

        #region Label and TrackBar Handling

        private void UpdateLabel() {
            valueLabel.Text = RaiseFormatValueEvent(trackBar.Value);
        }

        private void trackBar_ValueChanged(object sender, EventArgs e) {
            RaiseValueChanged();
            UpdateLabel();
        }

        #endregion

    }
}
