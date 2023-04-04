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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OpenWiiManager.LICENSE.txt");
            if (stream == null)
                return;
            using var reader = new StreamReader(stream, Encoding.UTF8);
            var text = reader.ReadToEnd();
            Form? f = default;
            using var m = new MenuStrip()
            {
                Padding = Padding.Empty,
                AutoSize = false,
                Height = 20,
                Items = {
                        new ToolStripMenuItem()
                        {
                            Padding = new Padding(2, 0, 2, 0),
                            DropDownItems = {
                                new ToolStripMenuItem("Exit", null, (sender, e) =>
                                {
                                    f?.Close();
                                })
                                {
                                    ShortcutKeyDisplayString = Keys.Escape.ToString()
                                }
                            },
                            Text = "&File"
                        }
                    }
            };
            f = new Form()
            {
                AutoScaleMode = AutoScaleMode.Dpi,
                Size = new Size(600, 400),
                Text = "License",
                ShowIcon = false,
                StartPosition = FormStartPosition.CenterParent,
                ShowInTaskbar = false,
                MaximizeBox = false,
                MinimizeBox = false,
                KeyPreview = true
            };
            using var t = new TextBox()
            {
                Font = new Font("Consolas", 10, FontStyle.Regular, GraphicsUnit.Point),
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                WordWrap = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                Text = text.Replace("\n", Environment.NewLine).Trim()
            };
            t.KeyDown += (sender, e) =>
            {
                Debug.WriteLine(e.KeyData.ToString());
                if (e.KeyData == Keys.Escape)
                {
                    f.Close();
                }
            };
            f.PreviewKeyDown += (sender, e) =>
            {
                Debug.WriteLine(e.KeyData.ToString());
                if (e.KeyData == Keys.Escape)
                {
                    f.Close();
                }
            };
            f.Controls.Add(t);
            f.Controls.Add(m);
            f.MainMenuStrip = m;
            f.ShowDialog(this);
            f.Dispose();
        }
    }
}
