namespace Rent.MyLibrary.WinForms.Controls.Forms
{
    /// <summary>
    /// XtraForm with layoutcontrol
    /// </summary>
    public partial class LayoutXtraForm : ButtonsXtraForm
    {
        /// <summary>
        /// 
        /// </summary>
        public LayoutXtraForm() => InitializeComponent();

        private void BtnCancel_Click(object sender, System.EventArgs e) => Close();
    }
}