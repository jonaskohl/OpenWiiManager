namespace OpenWiiManager.Language.Attributes
{
    public class EnumValueAttribute : Attribute
    {
        public EnumValueAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; init; }
    }
}