using System;
using Rent.MyLibrary.WinForms.Controls;

namespace Rent
{
    public partial class DetailForm : MyLibrary.WinForms.Controls.Forms.LayoutXtraForm
    {
        public DetailForm(BachelorPad bachelorPad = null)
        {
            InitializeComponent();
            Root.Current<BachelorPad>(bachelorPad);
        }

        private void DetailForm_Load(object sender, EventArgs e) => Root.Fill<BachelorPad>();

        private void BtnOk_Click(object sender, EventArgs e) => Root.Save<BachelorPad>(validationProvider);
    }
}