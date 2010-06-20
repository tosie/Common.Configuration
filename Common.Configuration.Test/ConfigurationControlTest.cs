using Common.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace Common.Configuration.Test
{
    
    
    /// <summary>
    ///This is a test class for ConfigurationControlTest and is intended
    ///to contain all ConfigurationControlTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConfigurationControlTest {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for MultipleConfigs
        ///</summary>
        [TestMethod()]
        public void MultipleConfigsTest() {
            ConfigurationControl target = new ConfigurationControl(); // TODO: Initialize to an appropriate value
            List<GenericConfiguration> expected = null; // TODO: Initialize to an appropriate value
            List<GenericConfiguration> actual;
            target.MultipleConfigs = expected;
            actual = target.MultipleConfigs;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Configuration
        ///</summary>
        [TestMethod()]
        public void ConfigurationTest() {
            ConfigurationControl target = new ConfigurationControl(); // TODO: Initialize to an appropriate value
            GenericConfiguration expected = null; // TODO: Initialize to an appropriate value
            GenericConfiguration actual;
            target.Configuration = expected;
            actual = target.Configuration;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AutoSave
        ///</summary>
        [TestMethod()]
        public void AutoSaveTest() {
            ConfigurationControl target = new ConfigurationControl(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.AutoSave = expected;
            actual = target.AutoSave;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ValidateValueOfControl
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void ValidateValueOfControlTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            Control Control = null; // TODO: Initialize to an appropriate value
            ConfigurationEntry Entry = null; // TODO: Initialize to an appropriate value
            object Value = null; // TODO: Initialize to an appropriate value
            object ValueExpected = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ValidateValueOfControl(Control, Entry, ref Value);
            Assert.AreEqual(ValueExpected, Value);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateTableStyles
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void UpdateTableStylesTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            int FirstRow = 0; // TODO: Initialize to an appropriate value
            target.UpdateTableStyles(FirstRow);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UpdateControlWithNewValue
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void UpdateControlWithNewValueTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            PropertyChangedEventArgs e = null; // TODO: Initialize to an appropriate value
            target.UpdateControlWithNewValue(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UnmapControls
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void UnmapControlsTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            target.UnmapControls();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for sld_FormatValue
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void sld_FormatValueTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            LabelSlider Sender = null; // TODO: Initialize to an appropriate value
            int Value = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.sld_FormatValue(Sender, Value);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ShowSingleConfiguration
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void ShowSingleConfigurationTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            target.ShowSingleConfiguration();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ShowMultipleConfigurations
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void ShowMultipleConfigurationsTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            target.ShowMultipleConfigurations();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest() {
            ConfigurationControl target = new ConfigurationControl(); // TODO: Initialize to an appropriate value
            target.Save();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for rowHeading_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void rowHeading_ClickTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.rowHeading_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for InitializeComponent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void InitializeComponentTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            target.InitializeComponent();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetValueOfControl
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void GetValueOfControlTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            Control Control = null; // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            actual = target.GetValueOfControl(Control);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Finalize
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void FinalizeTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            target.Finalize();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void DisposeTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            bool disposing = false; // TODO: Initialize to an appropriate value
            target.Dispose(disposing);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for control_ValueChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void control_ValueChangedTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.control_ValueChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for control_Leave
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void control_LeaveTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.control_Leave(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ClearTable
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void ClearTableTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            target.ClearTable();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for btn_Click
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void btn_ClickTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.btn_Click(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ApplyBasicControlSettings
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void ApplyBasicControlSettingsTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            Control Control = null; // TODO: Initialize to an appropriate value
            ConfigurationEntry Entry = null; // TODO: Initialize to an appropriate value
            int Row = 0; // TODO: Initialize to an appropriate value
            target.ApplyBasicControlSettings(Control, Entry, Row);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddTextBoxToRow
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void AddTextBoxToRowTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            ConfigurationEntry Entry = null; // TODO: Initialize to an appropriate value
            int Row = 0; // TODO: Initialize to an appropriate value
            TextBox expected = null; // TODO: Initialize to an appropriate value
            TextBox actual;
            actual = target.AddTextBoxToRow(Entry, Row);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddLabelToRow
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void AddLabelToRowTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            ConfigurationEntry Entry = null; // TODO: Initialize to an appropriate value
            int Row = 0; // TODO: Initialize to an appropriate value
            Label expected = null; // TODO: Initialize to an appropriate value
            Label actual;
            actual = target.AddLabelToRow(Entry, Row);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddLabelSliderToRow
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void AddLabelSliderToRowTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            ConfigurationEntry Entry = null; // TODO: Initialize to an appropriate value
            int Row = 0; // TODO: Initialize to an appropriate value
            LabelSlider expected = null; // TODO: Initialize to an appropriate value
            LabelSlider actual;
            actual = target.AddLabelSliderToRow(Entry, Row);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddHeadingToRow
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void AddHeadingToRowTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            string Text = string.Empty; // TODO: Initialize to an appropriate value
            int Row = 0; // TODO: Initialize to an appropriate value
            bool GroupHeader = false; // TODO: Initialize to an appropriate value
            Label expected = null; // TODO: Initialize to an appropriate value
            Label actual;
            actual = target.AddHeadingToRow(Text, Row, GroupHeader);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddDummyRow
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void AddDummyRowTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            target.AddDummyRow();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddConfigurationEntriesToTable
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void AddConfigurationEntriesToTableTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            GenericConfiguration Config = null; // TODO: Initialize to an appropriate value
            target.AddConfigurationEntriesToTable(Config);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddComboBoxToRow
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void AddComboBoxToRowTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            ConfigurationEntry Entry = null; // TODO: Initialize to an appropriate value
            int Row = 0; // TODO: Initialize to an appropriate value
            ComboBox expected = null; // TODO: Initialize to an appropriate value
            ComboBox actual;
            actual = target.AddComboBoxToRow(Entry, Row);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddCheckBoxToRow
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void AddCheckBoxToRowTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            ConfigurationEntry Entry = null; // TODO: Initialize to an appropriate value
            int Row = 0; // TODO: Initialize to an appropriate value
            CheckBox expected = null; // TODO: Initialize to an appropriate value
            CheckBox actual;
            actual = target.AddCheckBoxToRow(Entry, Row);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddButtonToRow
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Common.Configuration.dll")]
        public void AddButtonToRowTest() {
            ConfigurationControl_Accessor target = new ConfigurationControl_Accessor(); // TODO: Initialize to an appropriate value
            ConfigurationEntry Entry = null; // TODO: Initialize to an appropriate value
            int Row = 0; // TODO: Initialize to an appropriate value
            Button expected = null; // TODO: Initialize to an appropriate value
            Button actual;
            actual = target.AddButtonToRow(Entry, Row);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ConfigurationControl Constructor
        ///</summary>
        [TestMethod()]
        public void ConfigurationControlConstructorTest() {
            ConfigurationControl target = new ConfigurationControl();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
