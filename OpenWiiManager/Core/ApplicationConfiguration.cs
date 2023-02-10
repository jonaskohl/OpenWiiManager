using Ookii.Dialogs.WinForms;
using OpenWiiManager.Forms;
using OpenWiiManager.Language.Attributes;
using OpenWiiManager.Language.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Core
{
    public class ApplicationConfiguration : SerializableStateHolder
    {
        protected override string FilePath => ApplicationEnviornment.ConfigFilePath;

        [StateSerialization]
        private string? isoPath;

        [SettingsCategory("General",
            label: "Game directory",
            description: "The directory where all of your Wii ISO files live",
            editorCreatorType: typeof(FolderBrowserEditorCreator)
        )]
        public string? IsoPath { get => isoPath; set { isoPath = value; Serialize(); } }
    }
    public interface IEditorCreator
    {
        (Control, Control?, Action) GetEditor(KeyValuePair<PropertyInfo, SettingsCategoryAttribute?> property, Action? triggerDirty = null);
    }

    public abstract class BaseEditorCreator : IEditorCreator
    {
        protected Label GetDefaultLabel(KeyValuePair<PropertyInfo, SettingsCategoryAttribute?> property)
        {
            return new Label()
            {
                Text = $"{property.Value?.Label ?? property.Key.Name}: ",
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Margin = Padding.Empty
            };
        }

        public abstract (Control, Control?, Action) GetEditor(KeyValuePair<PropertyInfo, SettingsCategoryAttribute?> property, Action? triggerDirty = null);
    }

    public class NumericEditorCreator : BaseEditorCreator
    {
        public bool HasDecimals { get; init; } = false;

        public override (Control, Control?, Action) GetEditor(KeyValuePair<PropertyInfo, SettingsCategoryAttribute?> property, Action? triggerDirty = null)
        {
            var editControl = new NumericUpDown()
            {
                Width = 100,
                Anchor = AnchorStyles.Right,
                DecimalPlaces = HasDecimals ? 2 : 0,
                Margin = new Padding(0, 1, 0, 1),
                Value = (decimal?)Convert.ChangeType(property.Key.GetValue(ApplicationConfigurationSingleton.Instance), typeof(decimal)) ?? 0m
            };

            editControl.ValueChanged += (sender, e) => triggerDirty?.Invoke();

            return (GetDefaultLabel(property), editControl, () => property.Key.SetValue(ApplicationConfigurationSingleton.Instance, Convert.ChangeType(editControl.Value, property.Key.PropertyType)));
        }
    }

    public class StringEditorCreator : BaseEditorCreator
    {
        public override (Control, Control?, Action) GetEditor(KeyValuePair<PropertyInfo, SettingsCategoryAttribute?> property, Action? triggerDirty = null)
        {
            var editControl = new TextBox()
            {
                Width = 300,
                Anchor = AnchorStyles.Right,
                Margin = new Padding(0, 1, 0, 1),
                Text = property.Key.GetValue(ApplicationConfigurationSingleton.Instance)?.ToString()
            };

            editControl.TextChanged += (sender, e) => triggerDirty?.Invoke();

            return (GetDefaultLabel(property), editControl, () => property.Key.SetValue(ApplicationConfigurationSingleton.Instance, editControl.Text));
        }
    }

    public class FolderBrowserEditorCreator : StringEditorCreator
    {
        public override (Control, Control?, Action) GetEditor(KeyValuePair<PropertyInfo, SettingsCategoryAttribute?> property, Action? triggerDirty = null)
        {
            var ctrls = base.GetEditor(property, triggerDirty);

            var tbl = new TableLayoutPanel()
            {
                Width = 300,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Anchor = AnchorStyles.Right,
                Margin = new Padding(0, 1, 0, 1)
            };
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tbl.Controls.Add(ctrls.Item2, 0, 0);
            ctrls.Item2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            ctrls.Item2.Padding = Padding.Empty;
            (ctrls.Item2 as TextBox).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            (ctrls.Item2 as TextBox).AutoCompleteSource = AutoCompleteSource.FileSystem;

            var btn = new Button()
            {
                Width = 24,
                Height = 24,
                Margin = new Padding(3, 0, 0, 0),
                Image = Properties.Resources.Folder
            };
            btn.Click += (sender, e) =>
            {
                using var d = new VistaFolderBrowserDialog()
                {
                    SelectedPath = ctrls.Item2.Text,
                    ShowNewFolderButton = true
                };
                if (d.ShowDialog() == DialogResult.OK)
                     ctrls.Item2.Text = d.SelectedPath;
            };
            tbl.Controls.Add(btn, 1, 0);

            return (ctrls.Item1, tbl, ctrls.Item3);
        }
    }

    public class ApplicationConfigurationSingleton : Singleton<ApplicationConfiguration> { }
}
