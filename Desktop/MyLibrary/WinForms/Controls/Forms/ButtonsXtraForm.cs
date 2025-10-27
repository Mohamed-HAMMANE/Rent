namespace Rent.MyLibrary.WinForms.Controls.Forms
{
    /// <summary>
    /// XtraForm with layoutControl
    /// </summary>
    public partial class ButtonsXtraForm : MyXtraForm
    {
        /// <summary>
        /// 
        /// </summary>
        public ButtonsXtraForm() => InitializeComponent();

        private void BtnCancel_Click(object sender, System.EventArgs e) => Close();
    }
}