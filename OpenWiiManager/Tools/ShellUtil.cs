using OpenWiiManager.Win32;
using OpenWiiManager.Win32.Structures;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Tools
{
    public static class ShellUtil
    {
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
