﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Diagnostics;

namespace Common.Configuration {
    public class GenericConfiguration : IEnumerable {

        #region Properties

        protected List<ConfigurationEntry> Data { get; private set; }

        public Object BoundObject { get; protected set; }

        #endregion

        #region Constructors / Initialization

        public GenericConfiguration() {
            Data = new List<ConfigurationEntry>();
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

        #region ConfigurationAttribute

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

            if (entry.SortKey == 0)
                entry.SortKey = Data.Count;

            Add(entry);

            return entry;
        }

        void entry_QueryPossibleValues(ConfigurationEntry Sender, out object[] PossibleValues) {
            PossibleValues = null;
            if (BoundObject == null)
                return;

            String property_name = Sender.Name + "PossibleValues";

            Type t = BoundObject.GetType();
            PropertyInfo prop = t.GetProperty(property_name);
            if (prop == null)
                return;

            PossibleValues = (object[])prop.GetValue(BoundObject, null);
        }

        void entry_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (BoundObject == null)
                return;

            if (e.PropertyName != "Value")
                return;

            ConfigurationEntry entry = (ConfigurationEntry)sender;
            
            // Try setting the value of the corresponding property of the BoundObject
            Type t = BoundObject.GetType();
            PropertyInfo prop = t.GetProperty(entry.Name);
            if (prop == null)
                return;

            prop.SetValue(BoundObject, entry.Value, null);
        }

        public static GenericConfiguration CreateFor(Object BoundObject) {
            GenericConfiguration config = new GenericConfiguration();
            config.BoundObject = BoundObject;

            Type t = BoundObject.GetType();
            foreach (PropertyInfo prop in t.GetProperties()) {
                config.AddByPropertyWithAttribute(BoundObject, prop);
            }

            return config;
        }

        #endregion

    }
}
