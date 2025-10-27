using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Rent
{
    public partial class Home : DevExpress.XtraEditors.XtraForm
    {
        public Home()
        {
            InitializeComponent();
            sqlDataSource1.Fill();
        }

        private void ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item == BtnAdd)
            {
                if (new DetailForm().ShowDialog(this) == DialogResult.OK) sqlDataSource1.Fill();
            }
        }

        private void layoutView1_DoubleClick(object sender, EventArgs e) => Edit();

        private void ImageRepo_Click(object sender, EventArgs e) => Process.Start(layoutView1.GetFocusedRowCellValue(colLink).ToString());

        private void Edit()
        {
            var tbl = layoutView1.GetSelectedRows();
            if (tbl.Length <= 0) return;
            var view = layoutView1;

            var dto = new BachelorPad
            {
                Id = Convert.ToInt32(view.GetFocusedRowCellValue(colId)),
                Price = Convert.ToInt32(view.GetFocusedRowCellValue(colPrice)),
                Link = view.GetFocusedRowCellValue(colLink)?.ToString(),
                ChatLink = view.GetFocusedRowCellValue(colChatLink)?.ToString(),
                Address = view.GetFocusedRowCellValue(colAddress)?.ToString(),
                Quality = Convert.ToInt32(view.GetFocusedRowCellValue(colQuality)),
                Aesthetics = Convert.ToInt32(view.GetFocusedRowCellValue(colAesthetics)),
                Furniture = Convert.ToInt32(view.GetFocusedRowCellValue(colFurniture)),
                Phone = view.GetFocusedRowCellValue(colPhone)?.ToString(),
                Image = view.GetFocusedRowCellValue(colImage) as byte[],
                Location = Convert.ToInt32(view.GetFocusedRowCellValue(colLocation)),
                Observation = view.GetFocusedRowCellValue(colObservation)?.ToString()
            };

            if (new DetailForm(dto).ShowDialog(this) == DialogResult.OK) sqlDataSource1.Fill();
        }
    }
}
