using OpenWiiManager.Controls;
using OpenWiiManager.Language.Extensions;
using OpenWiiManager.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWiiManager.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            var fields = typeof(ApplicationConfiguration)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => new KeyValuePair<PropertyInfo, SettingsCategoryAttribute?>(p, p.GetCustomAttribute<SettingsCategoryAttribute>()))
                .WhereNotNull(p => p.Value)
            ;
        }

        private void toolbarButton1_Click(object sender, EventArgs e)
        {
            foreach (var b in flowLayoutPanel2.Controls.OfType<ToolbarButton>())
                b.Checked = false;
            (sender as ToolbarButton).Checked = true;
            SystemSoundPlayer.TryPlay(SystemSoundPlayer.PredefinedSound.Navigating);
        }
    }
}
