namespace OpenWiiManager.Language.Attributes
{
    public class EnumValueAttribute : Attribute
    {
        public EnumValueAttribute(object? value)
        {
            Value = value;
        }

        public object? Value { get; init; }
    }
}