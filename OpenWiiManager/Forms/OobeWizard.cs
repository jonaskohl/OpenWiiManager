using OpenWiiManager.Language.Extensions;
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
    public partial class OobeWizard : Form
    {
        public OobeWizard()
        {
            InitializeComponent();

            browseFolderButton.Image = StockIcons.GetStockIconAsImage(StockIcons.SHSTOCKICONID.SIID_FOLDEROPEN, new Size(16, 16), StockIcons.IconSize.Small);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.DisableCloseButton();
        }

        private void browseFolderButton_Click(object sender, EventArgs e)
        {
            if (vistaFolderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                pathLabel.Text = vistaFolderBrowserDialog1.SelectedPath;
                pathLabel.Font = pathLabel.Parent.Font;
                wizardPage2.AllowNext = true;
            }
        }
    }
}
