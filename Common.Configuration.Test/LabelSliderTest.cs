using Common.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Common.Configuration.Test
{
    
    
    /// <summary>
    ///This is a test class for LabelSliderTest and is intended
    ///to contain all LabelSliderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LabelSliderTest {


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
        ///A test for Value
        ///</summary>
        [TestMethod()]
        public void ValueTest() {
            LabelSlider target = new LabelSlider(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
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
            LabelSlider target = new LabelSlider(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Text = expected;
            actual = target.Text;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Minimum
        ///</summary>
        [TestMethod()]
        public void MinimumTest() {
            LabelSlider target = new LabelSlider(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
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
            LabelSlider target = new LabelSlider(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.Maximum = expected;
            actual = target.Maximum;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for trackBar_ValueChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GenericConfiguration.dll")]
        public void trackBar_ValueChangedTest() {
            LabelSlider_Accessor target = new LabelSlider_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.trackBar_ValueChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RaiseValueChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GenericConfiguration.dll")]
        public void RaiseValueChangedTest() {
            LabelSlider_Accessor target = new LabelSlider_Accessor(); // TODO: Initialize to an appropriate value
            target.RaiseValueChanged();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RaiseFormatValueEvent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GenericConfiguration.dll")]
        public void RaiseFormatValueEventTest() {
            LabelSlider_Accessor target = new LabelSlider_Accessor(); // TODO: Initialize to an appropriate value
            int Value = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.RaiseFormatValueEvent(Value);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LabelSlider_Enter
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GenericConfiguration.dll")]
        public void LabelSlider_EnterTest() {
            LabelSlider_Accessor target = new LabelSlider_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            EventArgs e = null; // TODO: Initialize to an appropriate value
            target.LabelSlider_Enter(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for InitializeComponent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GenericConfiguration.dll")]
        public void InitializeComponentTest() {
            LabelSlider_Accessor target = new LabelSlider_Accessor(); // TODO: Initialize to an appropriate value
            target.InitializeComponent();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GenericConfiguration.dll")]
        public void DisposeTest() {
            LabelSlider_Accessor target = new LabelSlider_Accessor(); // TODO: Initialize to an appropriate value
            bool disposing = false; // TODO: Initialize to an appropriate value
            target.Dispose(disposing);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for LabelSlider Constructor
        ///</summary>
        [TestMethod()]
        public void LabelSliderConstructorTest() {
            LabelSlider target = new LabelSlider();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
