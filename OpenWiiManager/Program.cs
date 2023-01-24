using OpenWiiManager.Language.Types;
using System.ComponentModel;

namespace OpenWiiManager
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TypeDescriptor.AddAttributes(
                typeof(Keys),
                new TypeConverterAttribute(typeof(ExtraKeysConverter))
            );

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Forms.MainForm());
        }
    }
}