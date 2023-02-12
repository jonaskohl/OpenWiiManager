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
            label2.Text = @$"Version {fvi.ProductVersion}
{fvi.LegalCopyright}";

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
