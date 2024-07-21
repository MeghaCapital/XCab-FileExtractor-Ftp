using System;

namespace Core.Helpers
{
    public class StringValueAttribute : Attribute
    {
        private string _value;

        private string _description;

        private bool _display = true;

        public StringValueAttribute(string value)
            : this(value, null)
        {

        }

        public StringValueAttribute(string value, string description)
        {
            _value = value;
            _description = description;
        }

        public StringValueAttribute(string value, string description, bool display)
            : this(value, description)
        {
            _display = display;
        }

        public StringValueAttribute(string value, string description, string type)
            : this(value, description)
        {
            Type = type;
        }

        public string Value
        {
            get { return _value; }
        }

        public string Description
        {
            get { return _description; }
        }

        public bool Display
        {
            get { return _display; }
        }

        public string Type { get; private set; }
    }
}
