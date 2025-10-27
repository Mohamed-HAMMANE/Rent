using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Rent.MyLibrary.WinForms
{
    /// <summary>
    /// WinForm Helper
    /// </summary>
    public static class WinHelpers
    {
        /// <summary>
        /// All conf to be edited
        /// </summary>
        public static List<string> OtherConfigsToChange { get; set; }

        /// <summary>
        /// Show log file
        /// </summary>
        public static void ShowLogFile()
        {
            var path = _file.Log();
            if (File.Exists(path)) Process.Start(path);
            else XtraMsg.Show("Le fichier log de est vide.");
        }

        /// <summary>
        /// Help buttons click
        /// </summary>
        /// <param name="buttonName"></param>
        public static void BtnHelpClick(string buttonName)
        {
            switch (buttonName)
            {
                case "BtnWebSite":
                    Process.Start($"https://{_app.Company}.ma/produits/{_app.Name}.html");
                    break;
                case "BtnSupport":
                    Process.Start($"mailto:contact@{_app.Company}.ma");
                    break;
                case "BtnVersion":
                    XtraMsg.Show($"Version {_app.Name} : {_app.Version}");
                    break;
                case "BtnCompanyWebsite":
                    Process.Start($"https://{_app.Company}.ma/");
                    break;
                case "BtnLogFile":
                    ShowLogFile();
                    break;
            }
        }
    }
}