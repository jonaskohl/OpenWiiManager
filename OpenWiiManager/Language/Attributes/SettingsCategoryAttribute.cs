using OpenWiiManager.Core;

namespace OpenWiiManager
{
    public class SettingsCategoryAttribute : Attribute
    {
        private string _category;
        private string? _label;
        private string? _description;
        private Type? _editorCreatorType;

        public string Category => _category;
        public string? Label => _label;
        public string? Description => _description;
        public IEditorCreator? GetEditorCreator
        {
            get
            {
                if (_editorCreatorType == null) return null;
                return (IEditorCreator?)Activator.CreateInstance(_editorCreatorType);
            }
        }

        public SettingsCategoryAttribute(string category, string? label = null, string? description = null, Type? editorCreatorType = null)
        {
            if (editorCreatorType != null && !editorCreatorType.IsAssignableTo(typeof(IEditorCreator)))
                throw new ArgumentException(editorCreatorType.FullName + " needs to inherit " + nameof(IEditorCreator), nameof(editorCreatorType));

            _category = category;
            _label = label;
            _description = description;
            _editorCreatorType = editorCreatorType;
        }
    }
}