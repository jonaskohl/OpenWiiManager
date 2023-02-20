using Flurl.Http;
using Ookii.Dialogs.WinForms;
using OpenWiiManager.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWiiManager.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            var fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            label2.Text = @$"Open Wii Mananger {fvi.ProductVersion}
{fvi.LegalCopyright}

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.";

            textBox1.Text = $@"Flurl: {GetAssemblyVersion<Flurl.Url>()}
Flurl.Http: {GetAssemblyVersion<FlurlCall>()}
AeroWizard: {GetAssemblyVersion<AeroWizard.ThemedLabel>()}
System.ServiceModel.Syndication: {GetAssemblyVersion<SyndicationFeed>()}
Ookii.Dialogs.WinForms: {GetAssemblyVersion<VistaFileDialog>()}";
        }

        private static string? GetAssemblyVersion<T>()
        {
            return FileVersionInfo.GetVersionInfo(typeof(T).Assembly.Location).ProductVersion;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(ApplicationEnviornment.WebUrl) { UseShellExecute = true });
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.gametdb.com/") { UseShellExecute = true });
        }
    }
}
