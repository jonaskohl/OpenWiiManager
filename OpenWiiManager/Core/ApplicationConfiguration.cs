using Ookii.Dialogs.WinForms;
using OpenWiiManager.Forms;
using OpenWiiManager.Language.Attributes;
using OpenWiiManager.Language.Types;
using OpenWiiManager.Media;
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
        public bool SettingsFileExists => File.Exists(FilePath);

        [StateSerialization]
        private string? isoPath;

        //[StateSerialization]
        //private Color? searchHighlightBackgroundColor;

        //[StateSerialization]
        //private Color? searchHighlightForegroundColor;

        [StateSerialization]
        private bool upperCaseHashes;

        [StateSerialization]
        private string? dolphinPath;

        [StateSerialization]
        private string? dolphinArgs = @"--batch ""--exec=%1""";

        [StateSerialization]
        private string? wiiDbZipUrl = "https://www.gametdb.com/wiitdb.zip";

        [StateSerialization]
        private string? wiiDbRssUrl = "https://www.gametdb.com/Main/LatestGames?action=rss";

        [SettingsCategory("General",
            label: "Game directory",
            description: "The directory where all of your Wii ISO files live",
            editorCreatorType: typeof(FolderBrowserEditorCreator)
        )]
        public string? IsoPath { get => isoPath; set { isoPath = value; Serialize(); } }

        //[SettingsCategory("General",
        //    label: "Search highlight background color",
        //    description: "The background color for highlighted search results"
        //)]
        //public Color? SearchHighlightBackgroundColor { get => searchHighlightBackgroundColor; set { searchHighlightBackgroundColor = value; Serialize(); } }

        //[SettingsCategory("General",
        //    label: "Search highlight text color",
        //    description: "The text color for highlighted search results"
        //)]
        //public Color? SearchHighlightForegroundColor { get => searchHighlightForegroundColor; set { searchHighlightForegroundColor = value; Serialize(); } }

        [SettingsCategory("Details",
            label: "Use uppercase letters in file hashes",
            description: "If checked, letters in the hexadecimal strings of file hashes will be upper case"
        )]
        public bool UpperCaseHashes { get => upperCaseHashes; set { upperCaseHashes = value; Serialize(); } }

        [SettingsCategory("Emulation",
            label: "Path to Dolphin",
            description: "The path to the main executable of Dolphin",
            editorCreatorType: typeof(FileBrowserEditorCreator)
        )]
        [FileBrowserEditorCreatorParams(filter: "Executable files|*.exe|All files|*")]
        public string? DolphinPath { get => dolphinPath; set { dolphinPath = value; Serialize(); } }

        [SettingsCategory("Emulation",
            label: "Dolphin command line arguments",
            description: "The command line arguments passed to Dolphin (%1 is the game's file name)"
        )]
        public string? DolphinArgs { get => dolphinArgs; set { dolphinArgs= value; Serialize(); } }

        [SettingsCategory("Advanced",
            label: "WiiTDB Zipped XML location",
            description: "The URL where to download the WiiTDB database from"
        )]
        public string? WiiDbZipUrl { get => wiiDbZipUrl; set { wiiDbZipUrl = value; Serialize(); } }

        [SettingsCategory("Advanced",
            label: "WiiTDB RSS feed location",
            description: "The URL where to download the WiiTDB database rss feed from"
        )]
        public string? WiiDbRssUrl { get => wiiDbRssUrl; set { wiiDbRssUrl = value; Serialize(); } }
    }

    public interface IEditorCreator
    {
        (Control, Control?, Action) GetEditor(KeyValuePair<PropertyInfo, SettingsCategoryAttribute?> property, SettingsForm.SettingsFormCommonObjects commonObjects, Action? triggerDirty = null);
    }

    public abstract class BaseEditorCreator : IEditorCreator
    {
        protected Label GetDefaultLabel(KeyValuePair<PropertyInfo, SettingsCategoryAttribute?> property, SettingsForm.SettingsFormCommonObjects commonObjects)
        {
            var label = new Label()
            {
                Text = $"{property.Value?.Label ?? property.Key.Name}: ",
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Margin = Padding.Empty
            };

            if (!string.IsNullOrEmpty(property.Value?.Description))
                commonObjects.MainToolTip.SetToolTip(label, property.Value?.Description);

            return label;
        }

        public abstract (Control, Control?, Action) GetEditor(KeyValuePair<PropertyInfo, SettingsCategoryAttribute?> property, SettingsForm.SettingsFormCommonObjects commonObjects, Action? triggerDirty = null);
    }

    public class BooleanEditorCreator : BaseEditorCreator
    {
        public override (Control, Control?, Action) GetEditor(KeyValuePair<PropertyInfo, SettingsCategoryAttribute?> property, SettingsForm.SettingsFormCommonObjects commonObjects, Action? triggerDirty = null)
        {
            var editControl = new CheckBox()
            {
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(3, 1, 3, 1),
                Text = property.Value?.Label ?? property.Key.Name,
                Checked = (bool?)Convert.ChangeType(property.Key.GetValue(ApplicationConfigurationSingleton.Instance), typeof(bool)) ?? false
            };

            editControl.CheckedChanged += (sender, e) => triggerDirty?.Invoke();

            if (!string.IsNullOrEmpty(property.Value?.Description))
                commonObjects.MainToolTip.SetToolTip(editControl, property.Value?.Description);

            return (editControl, null, () => property.Key.SetValue(ApplicationConfigurationSingleton.Instance, Convert.ChangeType(editControl.Checked, property.Key.PropertyType)));
        }
    }

    public class NumericEditorCreator : BaseEditorCreator
    {
        public bool HasDecimals { get; init; } = false;

        public override (Control, Control?, Action) GetEditor(KeyValuePair<PropertyInfo, SettingsCategoryAttribute?> property, SettingsForm.SettingsFormCommonObjects commonObjects, Action? triggerDirty = null)
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

            return (GetDefaultLabel(property, commonObjects), editControl, () => property.Key.SetValue(ApplicationConfigurationSingleton.Instance, Convert.ChangeType(editControl.Value, property.Key.PropertyType)));
        }
    }

    public class StringEditorCreator : BaseEditorCreator
    {
        public override (Control, Control?, Action) GetEditor(KeyValuePair<PropertyInfo, SettingsCategoryAttribute?> property, SettingsForm.SettingsFormCommonObjects commonObjects, Action? triggerDirty = null)
        {
            var editControl = new TextBox()
            {
                Width = 200,
                Anchor = AnchorStyles.Right,
                Margin = new Padding(0, 1, 0, 1),
                Text = property.Key.GetValue(ApplicationConfigurationSingleton.Instance)?.ToString()
            };

            editControl.TextChanged += (sender, e) => triggerDirty?.Invoke();

            return (GetDefaultLabel(property, commonObjects), editControl, () => property.Key.SetValue(ApplicationConfigurationSingleton.Instance, editControl.Text));
        }
    }

    public abstract class PathBrowserEditorCreator : StringEditorCreator
    {
        public override (Control, Control?, Action) GetEditor(KeyValuePair<PropertyInfo, SettingsCategoryAttribute?> property, SettingsForm.SettingsFormCommonObjects commonObjects, Action? triggerDirty = null)
        {
            var ctrls = base.GetEditor(property, commonObjects, triggerDirty);

            var tbl = new TableLayoutPanel()
            {
                Width = 200,
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
                Image = StockIcons.GetStockIconAsImage(StockIcons.SHSTOCKICONID.SIID_FOLDEROPEN, new Size(16, 16), StockIcons.IconSize.Small)//Properties.Resources.Folder
            };

            ctrls.Item2.Width -= btn.Width + btn.Margin.Horizontal;
            tbl.Controls.Add(btn, 1, 0);

            return (ctrls.Item1, tbl, ctrls.Item3);
        }
    }

    public class FolderBrowserEditorCreator : PathBrowserEditorCreator
    {
        public override (Control, Control?, Action) GetEditor(KeyValuePair<PropertyInfo, SettingsCategoryAttribute?> property, SettingsForm.SettingsFormCommonObjects commonObjects, Action? triggerDirty = null)
        {
            var controls = base.GetEditor(property, commonObjects, triggerDirty);
            var btn = controls.Item2.Controls.OfType<Button>().First();
            var txt = controls.Item2.Controls.OfType<TextBox>().First();
            commonObjects.MainToolTip.SetToolTip(btn, "Browse for folder...");
            btn.Click += (sender, e) =>
            {
                using var d = new VistaFolderBrowserDialog()
                {
                    SelectedPath = txt.Text,
                    ShowNewFolderButton = true
                };
                if (d.ShowDialog() == DialogResult.OK)
                    txt.Text = d.SelectedPath;
            };
            return controls;
        }
    }

    public class FileBrowserEditorCreator : PathBrowserEditorCreator
    {
        public override (Control, Control?, Action) GetEditor(KeyValuePair<PropertyInfo, SettingsCategoryAttribute?> property, SettingsForm.SettingsFormCommonObjects commonObjects, Action? triggerDirty = null)
        {
            var paramsAttrib = property.Key.GetCustomAttribute<FileBrowserEditorCreatorParamsAttribute>();
            var filter = "";
            if (paramsAttrib != null)
                filter = paramsAttrib.Filter;

            var controls = base.GetEditor(property, commonObjects, triggerDirty);
            var btn = controls.Item2.Controls.OfType<Button>().First();
            var txt = controls.Item2.Controls.OfType<TextBox>().First();
            commonObjects.MainToolTip.SetToolTip(btn, "Browse for file...");
            btn.Click += (sender, e) =>
            {
                using var d = new OpenFileDialog()
                {
                    FileName = txt.Text,
                    Filter = filter
                };
                if (d.ShowDialog() == DialogResult.OK)
                    txt.Text = d.FileName;
            };
            return controls;
        }
    }

    public class FileBrowserEditorCreatorParamsAttribute : Attribute
    {
        public string Filter { get; private set; }

        public FileBrowserEditorCreatorParamsAttribute(string filter)
        {
            Filter = filter;
        }
    }

    public class ApplicationConfigurationSingleton : Singleton<ApplicationConfiguration> { }
}
