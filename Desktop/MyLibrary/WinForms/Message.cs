using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;

namespace Rent.MyLibrary.WinForms
{
    /// <summary>
    /// Xtra messages
    /// </summary>
    public static class XtraMsg
    {
        /// <summary>
        /// Show message box
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="messageBoxIcon">the type of message</param>
        public static bool Show(string message, MessageBoxIcon messageBoxIcon = MessageBoxIcon.Information)
        {
            string caption;
            switch (messageBoxIcon)
            {
                case MessageBoxIcon.Error:
                    caption = "Erreur";
                    break;
                case MessageBoxIcon.Question:
                    caption = "Question";
                    break;
                case MessageBoxIcon.Information:
                    caption = "Information";
                    break;
                case MessageBoxIcon.Exclamation:
                    caption = "Exclamation";
                    break;
                case MessageBoxIcon.None:
                    caption = "Information";
                    break;
                default:
                    caption = "Information";
                    break;
            }
            var btn = messageBoxIcon == MessageBoxIcon.Question ? MessageBoxButtons.YesNo : MessageBoxButtons.OK;
            var res = XtraMessageBox.Show(message, caption, btn, messageBoxIcon);
            if (messageBoxIcon == MessageBoxIcon.Question) return res == DialogResult.Yes || res == DialogResult.OK;
            return true;
        }

        /// <summary>
        /// Show Delayed message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="seconds">delay</param>
        public static void ShowDelayed(string message, int seconds = 1)
        {
            var args = new XtraMessageBoxArgs
            {
                AutoCloseOptions =
                {
                    Delay = seconds * 1000,
                    ShowTimerOnDefaultButton = true
                },
                DefaultButtonIndex = 0,
                Caption = _app.Name,
                Text = message,
                Buttons = [DialogResult.OK]
            };
            XtraMessageBox.Show(args);
        }

        /// <summary>
        /// show exception message
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="message"></param>
        /// <returns>bool</returns>
        public static bool Show(this Exception ex, string message)
        {
            ex.Log();
            return Show(message.IsNull() ? ex.Message : message, MessageBoxIcon.Error);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static bool Show(this Exception ex) => ex.Show(null);

        /// <summary>
        /// Show exception message
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="closeWaiting"></param>
        /// <returns></returns>
        public static bool Show(this Exception ex, bool closeWaiting)
        {
            if (closeWaiting) SplashScreenManager.CloseForm(false);
            return ex.Show();
        }

        /// <summary>
        /// Confirm message
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>bool</returns>
        public static bool Confirm(string message = null)
        {
            message ??= "Vous êtes sur le point de supprimer cet élément !!!";
            return Show(message, MessageBoxIcon.Question);
        }

        /*// <summary>
        /// Get path from file dialogue
        /// </summary>
        /// <param name="pattern">img or (file extension),csv or direct filter</param>
        /// <param name="isFilter"></param>
        /// <returns></returns>
        public static (string path, bool ok) GetFilePathFromDialog(string pattern, bool isFilter = false)
        {
            string filter;
            if (!isFilter)
            {
                if (pattern != "img") filter = "Files(*." + pattern + ")| *." + pattern;
                else filter = "Bitmap (*.bmp)|*.bmp|GIF (*.gif)|*.gif|JPEG (*.jpeg)|*.jp*g|PNG (*.png)|*.png";
            }
            else filter = pattern;
            var openFileDialog = new XtraOpenFileDialog
            {
                Title = @"Choix de fichier",
                InitialDirectory = "",
                Filter = filter,
                RestoreDirectory = true,
                FilterIndex = 1,
                CheckFileExists = true,
                Multiselect = false,
                StartPosition = FormStartPosition.CenterScreen
            };
            return openFileDialog.ShowDialog() != DialogResult.OK ? (null, false) : (openFileDialog.FileName.IsNotNull() ? openFileDialog.FileName : null, true);
        }
        /// <summary>
        /// get path of save as file
        /// </summary>
        /// <param name="pattern">img or (file extension-csv)</param>
        /// <returns>path</returns>
        public static (string path, bool ok) GetSaveAsFilePathFromDialog(string pattern = "img")
        {
            string filter;
            if (pattern != "img") filter = "Files(*." + pattern + ")| *." + pattern;
            else filter = "Bitmap (*.bmp)|*.bmp|GIF (*.gif)|*.gif|JPEG (*.jpeg)|*.jp*g|PNG (*.png)|*.png";
            var openFileDialog = new XtraSaveFileDialog
            {
                Title = @"Enregistrer comme :",
                Filter = filter,
                RestoreDirectory = true,
                FilterIndex = 1,
                CheckFileExists = false,
                StartPosition = FormStartPosition.CenterScreen
            };
            //return openFileDialog.ShowDialog() == DialogResult.OK ? openFileDialog.FileName : null;
            return openFileDialog.ShowDialog() != DialogResult.OK ? (null, false) : (openFileDialog.FileName.IsNotNull() ? openFileDialog.FileName : null, true);
        }*/
    }
}
