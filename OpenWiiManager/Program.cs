using OpenWiiManager.Controls;
using OpenWiiManager.Forms;
using OpenWiiManager.Language.Types;
using System.ComponentModel;
using System.Globalization;

namespace OpenWiiManager
{
    internal static class Program
    {
        static ApplicationContext? context;
        static Forms.SplashForm? splashForm;
        static MainForm? mainForm;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US"); 
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            TypeDescriptor.AddAttributes(
                typeof(Keys),
                new TypeConverterAttribute(typeof(ExtraKeysConverter))
            );

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            splashForm = new();
            context = new();
            Application.Idle += Application_Idle;
            splashForm.Show();

            Application.Run(context);
        }

        private static void Application_Idle(object? sender, EventArgs e)
        {
            if (context?.MainForm == null)
            {
                Application.Idle -= Application_Idle;

                mainForm = new();
                mainForm.InitializeWork();
                context!.MainForm = mainForm;
                mainForm.Show();
                splashForm?.Close();
            }
        }
    }
}