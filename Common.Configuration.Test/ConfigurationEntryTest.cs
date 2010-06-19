using Common.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Common.Configuration.Test
{
    
    
    /// <summary>
    ///This is a test class for ConfigurationEntryTest and is intended
    ///to contain all ConfigurationEntryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConfigurationEntryTest {


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
        ///A test for ValueAsString
        ///</summary>
        [TestMethod()]
        public void ValueAsStringTest() {
            ConfigurationEntry target = new ConfigurationEntry(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ValueAsString;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Value
        ///</summary>
        [TestMethod()]
        public void ValueTest() {
            ConfigurationEntry target = new ConfigurationEntry(); // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            target.Value = expected;
            actual = target.Value;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Text
        ///</summary>
        [TestMethod()]
        public void TextTest() {
            ConfigurationEntry target = new ConfigurationEntry(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Text = expected;
            actual = target.Text;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Tag
        ///</summary>
        [TestMethod()]
        public void TagTest() {
            ConfigurationEntry target = new ConfigurationEntry(); // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            target.Tag = expected;
            actual = target.Tag;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SortKey
        ///</summary>
        [TestMethod()]
        public void SortKeyTest() {
            ConfigurationEntry target = new ConfigurationEntry(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.SortKey = expected;
            actual = target.SortKey;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ReadOnly
        ///</summary>
        [TestMethod()]
        public void ReadOnlyTest() {
            ConfigurationEntry target = new ConfigurationEntry(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.ReadOnly = expected;
            actual = target.ReadOnly;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Name
        ///</summary>
        [TestMethod()]
        public void NameTest() {
            ConfigurationEntry target = new ConfigurationEntry(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Minimum
        ///</summary>
        [TestMethod()]
        public void MinimumTest() {
            ConfigurationEntry target = new ConfigurationEntry(); // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            target.Minimum = expected;
            actual = target.Minimum;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Maximum
        ///</summary>
        [TestMethod()]
        public void MaximumTest() {
            ConfigurationEntry target = new ConfigurationEntry(); // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            target.Maximum = expected;
            actual = target.Maximum;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InputType
        ///</summary>
        [TestMethod()]
        public void InputTypeTest() {
            ConfigurationEntry target = new ConfigurationEntry(); // TODO: Initialize to an appropriate value
            ConfigurationEntry.ControlTypes expected = new ConfigurationEntry.ControlTypes(); // TODO: Initialize to an appropriate value
            ConfigurationEntry.ControlTypes actual;
            target.ControlType = expected;
            actual = target.ControlType;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GroupKey
        ///</summary>
        [TestMethod()]
        public void GroupKeyTest() {
            ConfigurationEntry target = new ConfigurationEntry(); // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            target.GroupKey = expected;
            actual = target.GroupKey;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ValidateInt32Value
        ///</summary>
        [TestMethod()]
        public void ValidateInt32ValueTest() {
            ConfigurationEntry Sender = null; // TODO: Initialize to an appropriate value
            object Value = null; // TODO: Initialize to an appropriate value
            object ValueExpected = null; // TODO: Initialize to an appropriate value
            bool Valid = false; // TODO: Initialize to an appropriate value
            bool ValidExpected = false; // TODO: Initialize to an appropriate value
            ConfigurationEntry.ValidateInt32Value(Sender, ref Value, out Valid);
            Assert.AreEqual(ValueExpected, Value);
            Assert.AreEqual(ValidExpected, Valid);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ValidateDoubleValue
        ///</summary>
        [TestMethod()]
        public void ValidateDoubleValueTest() {
            ConfigurationEntry Sender = null; // TODO: Initialize to an appropriate value
            object Value = null; // TODO: Initialize to an appropriate value
            object ValueExpected = null; // TODO: Initialize to an appropriate value
            bool Valid = false; // TODO: Initialize to an appropriate value
            bool ValidExpected = false; // TODO: Initialize to an appropriate value
            ConfigurationEntry.ValidateDoubleValue(Sender, ref Value, out Valid);
            Assert.AreEqual(ValueExpected, Value);
            Assert.AreEqual(ValidExpected, Valid);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ValidateBooleanValue
        ///</summary>
        [TestMethod()]
        public void ValidateBooleanValueTest() {
            ConfigurationEntry Sender = null; // TODO: Initialize to an appropriate value
            object Value = null; // TODO: Initialize to an appropriate value
            object ValueExpected = null; // TODO: Initialize to an appropriate value
            bool Valid = false; // TODO: Initialize to an appropriate value
            bool ValidExpected = false; // TODO: Initialize to an appropriate value
            ConfigurationEntry.ValidateBooleanValue(Sender, ref Value, out Valid);
            Assert.AreEqual(ValueExpected, Value);
            Assert.AreEqual(ValidExpected, Valid);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ToInt32
        ///</summary>
        [TestMethod()]
        public void ToInt32Test() {
            object Value = null; // TODO: Initialize to an appropriate value
            int Default = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = ConfigurationEntry.ToInt32(Value, Default);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToDouble
        ///</summary>
        [TestMethod()]
        public void ToDoubleTest() {
            object Value = null; // TODO: Initialize to an appropriate value
            double Default = 0F; // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = ConfigurationEntry.ToDouble(Value, Default);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RaisePropertyChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GenericConfiguration.dll")]
        public void RaisePropertyChangedTest() {
            ConfigurationEntry_Accessor target = new ConfigurationEntry_Accessor(); // TODO: Initialize to an appropriate value
            string PropertyName = string.Empty; // TODO: Initialize to an appropriate value
            target.RaisePropertyChanged(PropertyName);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RaiseFormatValue
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GenericConfiguration.dll")]
        public void RaiseFormatValueTest() {
            ConfigurationEntry_Accessor target = new ConfigurationEntry_Accessor(); // TODO: Initialize to an appropriate value
            object Value = null; // TODO: Initialize to an appropriate value
            string ValueAsString = string.Empty; // TODO: Initialize to an appropriate value
            string ValueAsStringExpected = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.RaiseFormatValue(Value, out ValueAsString);
            Assert.AreEqual(ValueAsStringExpected, ValueAsString);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RaiseComplexEditor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GenericConfiguration.dll")]
        public void RaiseComplexEditorTest() {
            ConfigurationEntry_Accessor target = new ConfigurationEntry_Accessor(); // TODO: Initialize to an appropriate value
            target.RaiseComplexEditor();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for IsValueValid
        ///</summary>
        [TestMethod()]
        public void IsValueValidTest() {
            ConfigurationEntry target = new ConfigurationEntry(); // TODO: Initialize to an appropriate value
            object Value = null; // TODO: Initialize to an appropriate value
            object ValueExpected = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsValueValid(ref Value);
            Assert.AreEqual(ValueExpected, Value);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetValueAsString
        ///</summary>
        [TestMethod()]
        public void GetValueAsStringTest() {
            ConfigurationEntry target = new ConfigurationEntry(); // TODO: Initialize to an appropriate value
            object Value = null; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetValueAsString(Value);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPossibleValues
        ///</summary>
        [TestMethod()]
        public void GetPossibleValuesTest() {
            ConfigurationEntry target = new ConfigurationEntry(); // TODO: Initialize to an appropriate value
            object[] expected = null; // TODO: Initialize to an appropriate value
            object[] actual;
            actual = target.GetPossibleValues();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ConfigurationEntry Constructor
        ///</summary>
        [TestMethod()]
        public void ConfigurationEntryConstructorTest() {
            ConfigurationEntry target = new ConfigurationEntry();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
