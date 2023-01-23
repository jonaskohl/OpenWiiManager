using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace OpenWiiManager.Language.Extensions
{
    public static class XmlExtensions
    {
        public static void SaveWithoutFormatting(this XDocument doc, string filename)
        {
            doc.SaveWithoutFormatting(filename, Encoding.UTF8);
        }

        public static void SaveWithoutFormatting(this XDocument doc, string filename, Encoding encoding)
        {
            using var wr = new XmlTextWriter(filename, encoding);
            wr.Formatting = Formatting.None;
            doc.Save(wr);
        }
        public static void SaveWithoutFormatting(this XDocument doc, Stream w)
        {
            doc.SaveWithoutFormatting(w, null);
        }

        public static void SaveWithoutFormatting(this XDocument doc, Stream w, Encoding? encoding)
        {
            using var wr = new XmlTextWriter(w, encoding);
            wr.Formatting = Formatting.None;
            doc.Save(wr);
        }

        public static void SaveWithoutFormatting(this XDocument doc, TextWriter w)
        {
            using var wr = new XmlTextWriter(w);
            wr.Formatting = Formatting.None;
            doc.Save(wr);
        }
    }
}
