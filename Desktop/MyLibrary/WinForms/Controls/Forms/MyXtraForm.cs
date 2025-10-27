using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Rent.MyLibrary.WinForms.Controls.Forms
{
    /// <summary>
    /// XtraForm Class
    /// </summary>
    public class MyXtraForm : XtraForm
    {
        /// <summary>
        /// Escape to close
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData != Keys.Escape) return base.ProcessCmdKey(ref msg, keyData);
            Close();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="showInTaskbar"></param>
        /// <returns></returns>
        public DialogResult ShowDialog(XtraForm parent = null, bool showInTaskbar = false) => this.ShowHiddenDialog(parent, showInTaskbar);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="showInTaskbar"></param>
        public void Show(XtraForm parent = null, bool showInTaskbar = false) => this.ShowHidden(parent, showInTaskbar);
    }
    /// <summary>
    /// Class to manipulate XtraForm
    /// </summary>
    public static class _xtraFormClass
    {
        /// <summary>
        /// Show dialog XtraForm hidden in taskBar
        /// </summary>
        /// <param name="form"></param>
        /// <param name="parent"></param>
        /// <param name="showInTaskbar"></param>
        /// <returns></returns>
        internal static DialogResult ShowHiddenDialog(this XtraForm form, XtraForm parent = null, bool showInTaskbar = false)
        {
            form.ShowInTaskbar = showInTaskbar;
            return parent != null ? form.ShowDialog(parent) : form.ShowDialog();
        }

        /// <summary>
        /// Show XtraForm hidden in taskBar
        /// </summary>
        /// <param name="form"></param>
        /// <param name="parent"></param>
        /// <param name="showInTaskbar"></param>
        internal static void ShowHidden(this XtraForm form, XtraForm parent = null, bool showInTaskbar = false)
        {
            form.ShowInTaskbar = showInTaskbar;
            if (parent != null) form.Show(parent);
            else form.Show();
        }
    }
}
