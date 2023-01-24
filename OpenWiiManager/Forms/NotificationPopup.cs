using OpenWiiManager.Controls;
using OpenWiiManager.Tools;
using OpenWiiManager.Win32;
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
    public partial class NotificationPopup : PopupWindow
    {
        public NotificationPopup()
        {
            InitializeComponent();
            listBoxEx1.HandleCreated += ListBoxEx1_HandleCreated;
        }

        private void ListBoxEx1_HandleCreated(object? sender, EventArgs e)
        {
            User32.SendMessage(listBoxEx1.Handle, Constants.WM_CHANGEUISTATE, InteropUtil.MakeLong(Constants.UIS_SET, Constants.UISF_HIDEFOCUS), 0);
        }
    }
}
