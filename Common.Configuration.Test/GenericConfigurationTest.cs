using Common.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;

namespace Common.Configuration.Test
{
    
    
    /// <summary>
    ///This is a test class for GenericConfigurationTest and is intended
    ///to contain all GenericConfigurationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GenericConfigurationTest {


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
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest() {
            GenericConfiguration target = new GenericConfiguration(); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            target[name] = expected;
            actual = target[name];
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Data
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GenericConfiguration.dll")]
        public void DataTest() {
            GenericConfiguration_Accessor target = new GenericConfiguration_Accessor(); // TODO: Initialize to an appropriate value
            List<ConfigurationEntry> expected = null; // TODO: Initialize to an appropriate value
            List<ConfigurationEntry> actual;
            target.Data = expected;
            actual = target.Data;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BoundObject
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GenericConfiguration.dll")]
        public void BoundObjectTest() {
            GenericConfiguration_Accessor target = new GenericConfiguration_Accessor(); // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            target.BoundObject = expected;
            actual = target.BoundObject;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for System.Collections.IEnumerable.GetEnumerator
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GenericConfiguration.dll")]
        public void GetEnumeratorTest() {
            IEnumerable target = new GenericConfiguration(); // TODO: Initialize to an appropriate value
            IEnumerator expected = null; // TODO: Initialize to an appropriate value
            IEnumerator actual;
            actual = target.GetEnumerator();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void RemoveTest() {
            GenericConfiguration target = new GenericConfiguration(); // TODO: Initialize to an appropriate value
            ConfigurationEntry key = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Remove(key);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FindEntryByName
        ///</summary>
        [TestMethod()]
        public void FindEntryByNameTest() {
            GenericConfiguration target = new GenericConfiguration(); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            ConfigurationEntry expected = null; // TODO: Initialize to an appropriate value
            ConfigurationEntry actual;
            actual = target.FindEntryByName(name);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for entry_QueryPossibleValues
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GenericConfiguration.dll")]
        public void entry_QueryPossibleValuesTest() {
            GenericConfiguration_Accessor target = new GenericConfiguration_Accessor(); // TODO: Initialize to an appropriate value
            ConfigurationEntry Sender = null; // TODO: Initialize to an appropriate value
            object[] PossibleValues = null; // TODO: Initialize to an appropriate value
            object[] PossibleValuesExpected = null; // TODO: Initialize to an appropriate value
            target.entry_QueryPossibleValues(Sender, out PossibleValues);
            Assert.AreEqual(PossibleValuesExpected, PossibleValues);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for entry_PropertyChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GenericConfiguration.dll")]
        public void entry_PropertyChangedTest() {
            GenericConfiguration_Accessor target = new GenericConfiguration_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            PropertyChangedEventArgs e = null; // TODO: Initialize to an appropriate value
            target.entry_PropertyChanged(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for CreateFor
        ///</summary>
        [TestMethod()]
        public void CreateForTest() {
            object BoundObject = null; // TODO: Initialize to an appropriate value
            GenericConfiguration expected = null; // TODO: Initialize to an appropriate value
            GenericConfiguration actual;
            actual = GenericConfiguration.CreateFor(BoundObject);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Contains
        ///</summary>
        [TestMethod()]
        public void ContainsTest() {
            GenericConfiguration target = new GenericConfiguration(); // TODO: Initialize to an appropriate value
            ConfigurationEntry key = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Contains(key);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Clear
        ///</summary>
        [TestMethod()]
        public void ClearTest() {
            GenericConfiguration target = new GenericConfiguration(); // TODO: Initialize to an appropriate value
            target.Clear();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddByPropertyWithAttribute
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GenericConfiguration.dll")]
        public void AddByPropertyWithAttributeTest() {
            GenericConfiguration_Accessor target = new GenericConfiguration_Accessor(); // TODO: Initialize to an appropriate value
            object BoundObject = null; // TODO: Initialize to an appropriate value
            PropertyInfo Property = null; // TODO: Initialize to an appropriate value
            ConfigurationEntry expected = null; // TODO: Initialize to an appropriate value
            ConfigurationEntry actual;
            actual = target.AddByPropertyWithAttribute(BoundObject, Property);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddTest1() {
            GenericConfiguration target = new GenericConfiguration(); // TODO: Initialize to an appropriate value
            ConfigurationEntry key = null; // TODO: Initialize to an appropriate value
            object value = null; // TODO: Initialize to an appropriate value
            ConfigurationEntry expected = null; // TODO: Initialize to an appropriate value
            ConfigurationEntry actual;
            actual = target.Add(key, value);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddTest() {
            GenericConfiguration target = new GenericConfiguration(); // TODO: Initialize to an appropriate value
            ConfigurationEntry key = null; // TODO: Initialize to an appropriate value
            ConfigurationEntry expected = null; // TODO: Initialize to an appropriate value
            ConfigurationEntry actual;
            actual = target.Add(key);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GenericConfiguration Constructor
        ///</summary>
        [TestMethod()]
        public void GenericConfigurationConstructorTest() {
            GenericConfiguration target = new GenericConfiguration();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
