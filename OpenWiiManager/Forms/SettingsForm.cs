using OpenWiiManager.Controls;
using OpenWiiManager.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
