using DevExpress.XtraEditors;
using System;
using System.Reflection;
using System.Windows.Forms;
using Rent.MyLibrary;

namespace Rent
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WindowsFormsSettings.RegisterUserSkins(typeof(DevExpress.UserSkins.SkinKsoft).Assembly);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            WindowsFormsSettings.AllowRoundedWindowCorners = DevExpress.Utils.DefaultBoolean.True;

            WindowsFormsSettings.EnableMdiFormSkins();

            _app.Init(Assembly.GetExecutingAssembly(), Properties.Resources.ResourceManager);

            Application.Run(new Home());
        }
    }
}
