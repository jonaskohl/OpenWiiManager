namespace OpenWiiManager
{
    public class SettingsCategoryAttribute : Attribute
    {
        private string _category;
        private string? _label;
        private string? _description;

        public string Category => _category;
        public string? Label => _label;
        public string? Description => _description;

        public SettingsCategoryAttribute(string category, string? label = null, string? description = null)
        {
            _category = category;
            _label = label;
            _description = description;
        }
    }
}