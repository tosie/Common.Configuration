using Common.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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

        /// <summary>
        /// A test for Serialize
        ///</summary>
        [TestMethod()]
        public void SerializeDeserializeTest() {
            var testObject = new ConfigurableClass();
            
            testObject.NewName = "testname";
            testObject.MultilineText = "a lot of text";
            testObject.Age = 12;

            var source = GenericConfiguration.CreateFor(testObject);
            var serialized = source.Serialize();

            testObject.NewName = "testname_two";
            testObject.MultilineText = "a lot of text two";
            testObject.Age = 22;

            var target = GenericConfiguration.CreateFor(testObject);
            target.Deserialize(serialized);

            Assert.AreEqual("testname", target["NewName"]);
            Assert.AreEqual("a lot of text", target["MultilineText"]);
            Assert.AreEqual(12, target["Age"]);
        }
    }
}
