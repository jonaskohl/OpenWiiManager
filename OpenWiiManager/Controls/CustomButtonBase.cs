using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Controls
{
    public abstract class CustomButtonBase : Button
    {
        protected const int FlagMouseOver = 0x0001;
        protected const int FlagMouseDown = 0x0002;
        protected const int FlagMousePressed = 0x0004;
        protected const int FlagInButtonUp = 0x0008;
        protected const int FlagCurrentlyAnimating = 0x0010;
        protected const int FlagAutoEllipsis = 0x0020;
        protected const int FlagIsDefault = 0x0040;
        protected const int FlagUseMnemonic = 0x0080;
        protected const int FlagShowToolTip = 0x0100;

        protected bool IsMouseOver => GetFlag(FlagMouseOver);
        protected bool IsMouseDown => GetFlag(FlagMouseDown);

        protected bool GetFlag(int flag)
        {
            var dynMethod = typeof(ButtonBase).GetMethod("GetFlag", BindingFlags.NonPublic | BindingFlags.Instance);
            return (bool?)dynMethod?.Invoke(this, new object[] { flag }) ?? false;
        }
    }
}
