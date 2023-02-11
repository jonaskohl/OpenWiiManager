using OpenWiiManager.Win32;
using OpenWiiManager.Win32.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Language.Extensions
{
    public static class ListViewExtensions
    {
        public static void SetSortIcon(this ListView listViewControl, int columnIndex, SortOrder order)
        {
            IntPtr columnHeader = User32.SendMessage(listViewControl.Handle, Constants.LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);
            for (int columnNumber = 0; columnNumber <= listViewControl.Columns.Count - 1; columnNumber++)
            {
                var columnPtr = new IntPtr(columnNumber);
                var item = new HDITEM
                {
                    mask = HDITEM.Mask.Format
                };

                if (User32.SendMessage(columnHeader, Constants.HDM_GETITEM, columnPtr, ref item) == IntPtr.Zero)
                {
                    throw new Win32Exception();
                }

                if (order != SortOrder.None && columnNumber == columnIndex)
                {
                    switch (order)
                    {
                        case SortOrder.Ascending:
                            item.fmt &= ~HDITEM.Format.SortDown;
                            item.fmt |= HDITEM.Format.SortUp;
                            break;
                        case SortOrder.Descending:
                            item.fmt &= ~HDITEM.Format.SortUp;
                            item.fmt |= HDITEM.Format.SortDown;
                            break;
                    }
                }
                else
                {
                    item.fmt &= ~HDITEM.Format.SortDown & ~HDITEM.Format.SortUp;
                }

                if (User32.SendMessage(columnHeader, Constants.HDM_SETITEM, columnPtr, ref item) == IntPtr.Zero)
                {
                    throw new Win32Exception();
                }
            }
        }
    }
}
