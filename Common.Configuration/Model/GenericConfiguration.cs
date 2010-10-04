using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace Common.Configuration {
    [XmlRoot("genericconfiguration")]
    public class GenericConfiguration : IEnumerable {

        #region Properties

        protected List<ConfigurationEntry> Data { get; private set; }

        protected object boundObject = null;

        /// <summary>
        /// Represents the object that this configuration is bound to.
        /// </summary>
        public object BoundObject {
            get {
                return boundObject;
            }

            set {
                if (boundObject == value)
                    return;

                boundObject = value;
                UpdateValues();
            }
        }

        protected bool LoadingConfiguration { get; set; }

        #endregion

        #region Constructors / Initialization

        public GenericConfiguration() {
            Data = new List<ConfigurationEntry>();
            LoadingConfiguration = false;
        }

        #endregion

        #region List-like Members

        public ConfigurationEntry Add(ConfigurationEntry key, object value) {
            key.Value = value;
            Data.Add(key);
            return key;
        }

        public ConfigurationEntry Add(ConfigurationEntry key) {
            Data.Add(key);
            return key;
        }

        public bool Contains(ConfigurationEntry key) {
            return Data.Contains(key);
        }

        public bool Remove(ConfigurationEntry key) {
            return Data.Remove(key);
        }

        public void Clear() {
            Data.Clear();
        }

        public ConfigurationEntry FindEntryByName(String name) {
            foreach (ConfigurationEntry item in Data) {
                if (item.Name.Equals(name))
                    return item;
            }

            return null;
        }

        public Object this[String name] {
            get {
                ConfigurationEntry key = FindEntryByName(name);
                if (key == null)
                    throw new ArgumentException(String.Format("Unknown property: {0}", name));

                return key.Value;
            }
            set {
                ConfigurationEntry key = FindEntryByName(name);
                if (key == null)
                    throw new ArgumentException(String.Format("Unknown property: {0}", name));

                key.Value = value;
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return Data.GetEnumerator();
        }

        #endregion

        #region Bound Object Handling

        void UpdateValues() {
            LoadingConfiguration = true;

            try {

                // Walk over each configuration entry that has a PropertyOfBoundObject ...
                Data.ForEach(entry => {
                    Type t = GetDirectType(BoundObject);
                    PropertyInfo prop = t.GetProperty(entry.Name);
                    if (prop == null)
                        return;

                    object value = prop.GetValue(BoundObject, null);
                    entry.Value = value;
                });

            } finally {

                LoadingConfiguration = false;

            }
        }

        #endregion

        #region ConfigurationAttribute

        /// <summary>
        /// Returns the type of the object. If the type is of a Castle.Proxies type, the base type is returned.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static Type GetDirectType(Object obj) {
            Type t = obj.GetType();

            if (t.Namespace == "Castle.Proxies")
                t = t.BaseType;

            return t;
        }

        protected ConfigurationEntry AddByPropertyWithAttribute(Object BoundObject, PropertyInfo Property) {
            Object[] attrs = Property.GetCustomAttributes(typeof(ConfigurationAttribute), true);
            if (attrs.Length <= 0)
                return null;

            ConfigurationAttribute attr = (ConfigurationAttribute)attrs[0];

            ConfigurationEntry entry = new ConfigurationEntry();

            entry.Name = Property.Name;
            attr.ApplyToConfigurationEntry(entry);
            entry.Value = Property.GetValue(BoundObject, null);

            entry.PropertyChanged += new PropertyChangedEventHandler(entry_PropertyChanged);
            entry.QueryPossibleValues += new ConfigurationEntry.QueryPossibleValuesEvent(entry_QueryPossibleValues);
            entry.Editor += new ConfigurationEntry.EditorHandler(entry_Editor);
            entry.FormatValue += new ConfigurationEntry.FormatValueHandler(entry_FormatValue);

            if (entry.SortKey == 0)
                entry.SortKey = Data.Count;

            Add(entry);

            return entry;
        }

        bool entry_Editor(ConfigurationEntry Sender, IWin32Window Owner) {
            if (BoundObject == null)
                return false;

            String method_name = Sender.Name + "Editor";

            Type t = GetDirectType(BoundObject);
            MethodInfo method = t.GetMethod(method_name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (method == null)
                return false;

            return (bool)DirectInvoker.InvokeDirect(method, BoundObject, new object[] { Sender, Owner });
        }

        void entry_QueryPossibleValues(ConfigurationEntry Sender, out object[] PossibleValues) {
            PossibleValues = null;
            if (BoundObject == null)
                return;

            String property_name = Sender.Name + "PossibleValues";

            Type t = GetDirectType(BoundObject);
            PropertyInfo prop = t.GetProperty(property_name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (prop == null)
                return;

            PossibleValues = (object[])prop.GetValue(BoundObject, null);
        }

        void entry_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (LoadingConfiguration)
                return;

            if (BoundObject == null)
                return;

            if (e.PropertyName != "Value")
                return;

            ConfigurationEntry entry = (ConfigurationEntry)sender;
            
            // Try setting the value of the corresponding property of the BoundObject
            Type t = GetDirectType(BoundObject);
            PropertyInfo prop = t.GetProperty(entry.Name);
            if (prop == null)
                return;

            prop.SetValue(BoundObject, entry.Value, null);
        }

        string entry_FormatValue(ConfigurationEntry Sender, object Value) {
            String value_as_string = (Value == null ? "" : Value.ToString());

            if (BoundObject == null)
                return value_as_string;

            String method_name = Sender.Name + "FormatValue";

            Type t = GetDirectType(BoundObject);
            MethodInfo method = t.GetMethod(method_name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (method == null)
                return value_as_string;

            return (string)method.Invoke(BoundObject, new object[] { Sender, Value });
        }

        public static GenericConfiguration CreateFor(Object BoundObject) {
            GenericConfiguration config = new GenericConfiguration();
            config.BoundObject = BoundObject;

            Type t = GetDirectType(BoundObject);
            foreach (PropertyInfo prop in t.GetProperties()) {
                config.AddByPropertyWithAttribute(BoundObject, prop);
            }

            return config;
        }

        #endregion

    }
}
