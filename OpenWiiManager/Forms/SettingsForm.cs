using OpenWiiManager.Controls;
using OpenWiiManager.Core;
using OpenWiiManager.Language.Extensions;
using OpenWiiManager.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;

namespace OpenWiiManager.Forms
{
    public partial class SettingsForm : Form
    {
        Dictionary<string, TableLayoutPanel> settingsPages = new();
        Dictionary<string, ToolbarButton> buttons = new();
        List<Action> reapplyFunctions = new();
        Dictionary<string, Image> categoryIcons = new()
        {
            { "General", Properties.Resources.Tools }
        };

        bool isDirty = false;
        bool IsDirty
        {
            get => isDirty;
            set
            {
                isDirty = value;
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            applyButton.Enabled = isDirty;
        }

        public SettingsForm()
        {
            InitializeComponent();

            foreach (
                var property
                    in
                typeof(Core.ApplicationConfiguration)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Select(p => new KeyValuePair<PropertyInfo, SettingsCategoryAttribute?>(p, p.GetCustomAttribute<SettingsCategoryAttribute>()))
                    .WhereNotNull(p => p.Value)
            )
            {
                var category = property.Value?.Category ?? "@DEFAULT";
                if (!settingsPages.ContainsKey(category))
                {
                    var _panel = new TableLayoutPanel()
                    {
                        Dock = DockStyle.Top,
                        AutoSize = true,
                        AutoSizeMode = AutoSizeMode.GrowAndShrink
                    };
                    _panel.ColumnStyles.Add(new(SizeType.Percent, 50f));
                    _panel.ColumnStyles.Add(new(SizeType.AutoSize));
                    settingsPages[category] = _panel;
                }
                var panel = settingsPages[category];
                var controls = GetSetingsControl(property);
                reapplyFunctions.Add(controls.Item3);
                var prevRowCount = panel.RowCount;
                var rsCount = panel.RowStyles.Count;
                panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                if (controls.Item2 != null)
                    panel.Controls.Add(controls.Item2, 1, rsCount);
                panel.Controls.Add(controls.Item1, 0, rsCount);
            }

            foreach (var v in settingsPages)
            {
                var tbb = new ToolbarButton()
                {
                    Text = v.Key,
                    Size = new Size(120, 23),
                    TextAlign = ContentAlignment.MiddleRight,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    Margin = new Padding(3, 3, 3, 0),
                    Image = categoryIcons.GetValueOrDefault(v.Key),
                    Cursor = Cursors.Hand
                };
                tbb.Tag = v.Key;
                tbb.Click += toolbarButton_Click;
                flowLayoutPanel2.Controls.Add(tbb);
                buttons[v.Key] = tbb;

                v.Value.RowStyles.RemoveAt(0);
                v.Value.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            }

            if (settingsPages.Keys.Count > 0)
                SelectCategory(settingsPages.Keys.First());
            else
                MessageBox.Show("no pages");
        }

        private void SelectCategory(string category)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(settingsPages[category]);
            foreach (var b in flowLayoutPanel2.Controls.OfType<ToolbarButton>())
                b.Checked = false;
            buttons[category].Checked = true;
            SystemSoundPlayer.TryPlay(SystemSoundPlayer.PredefinedSound.Navigating);
        }

        private bool IsIntegerType(Type type)
        {
            return type == typeof(byte)
                || type == typeof(sbyte)
                || type == typeof(short)
                || type == typeof(ushort)
                || type == typeof(int)
                || type == typeof(uint)
                || type == typeof(long)
                || type == typeof(ulong)
                || type == typeof(nint)
                || type == typeof(nuint)
            ;
        }

        private bool IsFloatType(Type type)
        {
            return type == typeof(float)
                || type == typeof(double)
                || type == typeof(decimal)
            ;
        }

        private (Control, Control?, Action) GetSetingsControl(KeyValuePair<PropertyInfo, SettingsCategoryAttribute?> property)
        {
            var type = property.Key.PropertyType;

            if (property.Value?.GetEditorCreator != null)
            {
                return property.Value.GetEditorCreator.GetEditor(property, () => IsDirty = true);
            }

            if (type == typeof(string))
            {
                return new StringEditorCreator().GetEditor(property);
            }
            else if (IsFloatType(type))
            {
                return (new NumericEditorCreator() { HasDecimals = true }).GetEditor(property);
            }
            else if (IsIntegerType(type))
            {
                return (new NumericEditorCreator() { HasDecimals = true }).GetEditor(property);
            }
            else
                return (new Label() { Text = $"No editor for type {type.FullName}", ForeColor = Color.Red }, null, () => { });
        }

        private void toolbarButton_Click(object? sender, EventArgs e)
        {
            var tbb = (sender as ToolbarButton);
            if (tbb != null)
            {
                var key = tbb.Tag.ToString() ?? "@DEFAULT";
                SelectCategory(key);
            }
        }

        private void ApplyChanges()
        {
            foreach (var fn in reapplyFunctions)
                fn();
            IsDirty = false;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            ApplyChanges();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
