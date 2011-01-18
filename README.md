The library Common.Configuration is a .NET project that aims to help create user interfaces using WinForms to let a user enter information.



Example
------------------

Suppose you have a class with the following structure:

```c#
    public partial class MainForm : Form
    {
        // [...]
        
        public String NewName {
            get { return Text; }
            set { Text = value; }
        }

        public String Description { get; set; }

        public String Option { get; set; }
        
        public String OptionLink { get; set; }
        
        // [...]
    }
```

Now add declarative information in the form of attributes like here:

```c#
    public partial class MainForm : Form
    {
        // [...]
        
        [Configuration("Name", 11, "First")]
        public String NewName {
            get { return Text; }
            set { Text = value; }
        }

        [Configuration("Description (read only)", 20, "First",
            ReadOnly = true)]
        public String Description { get; set; }

        [Configuration("Option", 30, "First",
            ControlType = ConfigurationEntry.ControlTypes.ComboBox,
            SubText = "Really long long long long long long long long long long long long long long long text.")]
        public String Option { get; set; }
        public object[] OptionPossibleValues {
            get {
                return new object[] { "Option 1", "Option 2", "Option 3" };
            }
        }

        [Configuration("Option", 30, "First",
            ControlType = ConfigurationEntry.ControlTypes.ComboBoxAsLinkLabel)]
        public String OptionLink { get; set; }
        public object[] OptionLinkPossibleValues {
            get {
                return new object[] { "Option 1", "Option 2", "Option 3" };
            }
        }
        
        // [...]
    }
```

And you can get a GUI that looks just like this one:

![Window showing user controls that correspond to the attributes associated with the properties of a class.](https://github.com/tosie/Common.Configuration/wiki/gui-sample.png)
