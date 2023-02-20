using OpenWiiManager.Win32;
using OpenWiiManager.Win32.Structures;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Tools
{
    public static class ShellUtil
    {
        public enum ShellIconSize
        {
            Small = Constants.SHIL_SMALL,
            Large = Constants.SHIL_LARGE,
            ExtraLarge = Constants.SHIL_EXTRALARGE,
            SystemSmall = Constants.SHIL_SYSSMALL,
            Jumbo = Constants.SHIL_JUMBO,
        }

        private static int GetIconIndex(string pszFile)
        {
            SHFILEINFO sfi = new SHFILEINFO();
            Shell32.SHGetFileInfo(pszFile
                , 0
                , ref sfi
                , (uint)Marshal.SizeOf(sfi)
                , (uint)(SHGFI.SysIconIndex | SHGFI.LargeIcon | SHGFI.UseFileAttributes));
            return sfi.iIcon;
        }

        private static IntPtr GetHIcon(string filename, ShellIconSize iconSize)
        {
            var iImage = GetIconIndex(filename);
            IImageList spiml = null;
            var guil = new Guid(Constants.IID_IImageList2);//or IID_IImageList

            Shell32.SHGetImageList((int)iconSize, ref guil, ref spiml);
            var hIcon = IntPtr.Zero;
            spiml.GetIcon(iImage, Constants.ILD_TRANSPARENT | Constants.ILD_IMAGE, ref hIcon);

            return hIcon;
        }

        public static Icon GetIcon(string filename, ShellIconSize iconSize)
        {
            var hIcon = GetHIcon(filename, iconSize);
            var icon = Icon.FromHandle(hIcon);
            User32.DestroyIcon(hIcon);
            return icon;
        }

        public static Image GetIconAsImage(string filename, ShellIconSize iconSize)
        {
            var hIcon = GetHIcon(filename, iconSize);
            var bitmap = Bitmap.FromHicon(hIcon);
            User32.DestroyIcon(hIcon);
            return bitmap;
        }

        public static bool ShowFileProperties(string filename)
        {
            ArgumentNullException.ThrowIfNull(filename, nameof(filename));

            SHELLEXECUTEINFO info = new();
            info.cbSize = Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = filename;
            info.nShow = Constants.SW_SHOW;
            info.fMask = Constants.SEE_MASK_INVOKEIDLIST;
            return Shell32.ShellExecuteEx(ref info);
        }
        public static bool ShowFileProperties(string[] filenames)
        {
            if (filenames.Length < 1) return false;
            if (filenames.Length == 1) return ShowFileProperties(filenames[0]);

            var files = new StringCollection();
            files.AddRange(filenames);
            var data = new DataObject();
            data.SetFileDropList(files);
            var dropEffectStream = new MemoryStream(new byte[] { 5, 0, 0, 0 });
            var idListStream = CreateShellIDList(files);
            data.SetData("Preferred DropEffect", true, dropEffectStream);
            data.SetData("Shell IDList Array", true, idListStream);
            return Shell32.SHMultiFileProperties(data, 0) == 0;
        }

        public static void ShowFilesInExplorer(string[] filenames)
        {
            if (filenames.Length < 1)
                return;
            else if (filenames.Length == 1)
                ShowFilesInExplorerSingle(filenames[0]);
            else
                ShowFilesInExplorerMultiple(filenames);
        }

        private static void ShowFilesInExplorerSingle(string filename)
        {
            var pILFile = IntPtr.Zero;

            try
            {
                pILFile = Shell32.ILCreateFromPath(filename);
                var res = Shell32.SHOpenFolderAndSelectItems(pILFile, 0, new IntPtr[0], 0);
                if (res != 0)
                    throw new Win32Exception(res);
            }
            finally
            {
                Shell32.ILFree(pILFile);
            }
        }

        private static void ShowFilesInExplorerMultiple(string[] filenames)
        {
            if (filenames.Length < 2)
                throw new ArgumentException("Must be 2 or more items", nameof(filenames));

            IntPtr pILFolder = IntPtr.Zero;
            var pILFiles = new IntPtr[filenames.Length];
            try
            {
                var commonFolderName = IOUtil.GetLongestCommonPrefix(filenames);
                pILFolder = Shell32.ILCreateFromPath(commonFolderName);

                for (var i = 0; i < filenames.Length; ++i)
                    pILFiles[i] = Shell32.ILCreateFromPath(filenames[i]);

                var res = Shell32.SHOpenFolderAndSelectItems(pILFolder, unchecked((uint)filenames.Length), pILFiles, 0);
                if (res != 0)
                    throw new Win32Exception(res);
            }
            finally
            {
                Shell32.ILFree(pILFolder);
                foreach (var pILFile in pILFiles)
                    Shell32.ILFree(pILFile);
            }
        }

        private static MemoryStream CreateShellIDList(StringCollection filenames)
        {
            var pos = 0;
            var pidls = new byte[filenames.Count][];
            foreach (var filename in filenames)
            {
                var pidl = Shell32.ILCreateFromPath(filename!);
                var pidlSize = Shell32.ILGetSize(pidl);
                pidls[pos] = new byte[pidlSize];
                Marshal.Copy(pidl, pidls[pos++], 0, pidlSize);
                Shell32.ILFree(pidl);
            }

            var pidlOffset = 4 * (filenames.Count + 2);
            var memStream = new MemoryStream();
            var sw = new BinaryWriter(memStream);
            sw.Write(filenames.Count);
            sw.Write(pidlOffset);
            pidlOffset += 4;
            foreach (var pidl in pidls)
            {
                sw.Write(pidlOffset);
                pidlOffset += pidl.Length;
            }

            sw.Write(0);
            foreach (var pidl in pidls)
                sw.Write(pidl);

            return memStream;
        }
    }
}
