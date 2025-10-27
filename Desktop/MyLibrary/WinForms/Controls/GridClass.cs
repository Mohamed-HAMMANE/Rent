using System.Data;
using System.Drawing;
using System.Linq;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Layout;

namespace Rent.MyLibrary.WinForms.Controls
{
    /// <summary>
    /// GridClass
    /// </summary>
    public class GridClass
    {
        private readonly ColumnView _view;
        private readonly GridControl _gc;
        /// <summary>
        /// ImageCollection of a column
        /// </summary>
        public ImageCollection Collection { get; set; }
        /// <summary>
        /// column of images
        /// </summary>
        public string ImagesCol { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gc"></param>
        public GridClass(GridControl gc)
        {
            _view = (ColumnView)gc.MainView;
            _gc = gc;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="view"></param>
        public GridClass(ColumnView view)
        {
            _gc = view.GridControl;
            _view = view;
        }

        /// <summary>
        /// Customize showing columnView
        /// </summary>
        /// <param name="dateFormat"></param>
        public void Customize(string dateFormat = "dd/MM/yyyy")
        {
            if (_view.Columns["id"] != null) _view.Columns["id"].Visible = false;
            if (_view.Columns["Id"] != null) _view.Columns["Id"].Visible = false;
            foreach (GridColumn dc in _view.Columns)
            {
                if (_view is not LayoutView)
                {
                    dc.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    dc.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
                    dc.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                }
                switch (dc.ColumnType.ToString())
                {
                    case "System.Boolean":

                        break;
                    case "System.Byte":

                        break;
                    case "System.Int16":

                        break;
                    case "System.Int32":

                        break;
                    case "System.Int64":

                        break;
                    case "System.Byte[]":

                        break;
                    case "System.Char[]":

                        break;
                    case "System.String":

                        break;
                    case "System.Single":

                        break;
                    case "System.Double":
                    case "System.Decimal":
                        dc.DisplayFormat.FormatString = "n";
                        dc.DisplayFormat.FormatType = FormatType.Custom;
                        dc.SortMode = ColumnSortMode.Value;
                        var exists = false;
                        foreach (var tmp in dc.Summary)
                        {
                            if (!(tmp is GridColumnSummaryItem s)) continue;
                            if (s.Mode != SummaryMode.Mixed) continue;
                            exists = true;
                            break;
                        }
                        if (!exists) dc.Summary.Add(new GridColumnSummaryItem(SummaryItemType.Sum, SummaryMode.Mixed, dc.FieldName, "{0:N}"));
                        break;
                    case "System.DateTime":
                        dc.DisplayFormat.FormatString = "{0:" + dateFormat + "}";
                        dc.DisplayFormat.FormatType = FormatType.Custom;
                        dc.SortMode = ColumnSortMode.Value;
                        break;
                    case "System.Guid":

                        break;
                    case "System.Object":

                        break;
                }
            }
            if (Collection == null || ImagesCol == null || _view.Columns[ImagesCol] == null) return;
            var riImages = new RepositoryItemImageComboBox();
            riImages.Fill(Collection);
            _view.Columns[ImagesCol].ColumnEdit = riImages;
        }

        /// <summary>
        /// Fill Grid with dataSource
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="populateColumns"></param>
        public void Fill(DataTable dt, bool populateColumns = false)
        {
            foreach (DataColumn col in dt.Columns) col.ReadOnly = false;
            _gc.DataSource = dt;
            if (populateColumns) _view.PopulateColumns();
        }
    }
    /// <summary>
    /// Manipulating Win ColumnView
    /// </summary>
    public static class _gridClass
    {
        /// <summary>
        /// Fill GridControl
        /// </summary>
        /// <param name="gc">GridControl</param>
        /// <param name="dt">DataTable</param>
        /// <param name="populateColumns"></param>
        public static void Fill(this GridControl gc, DataTable dt, bool populateColumns = false) => new GridClass(gc).Fill(dt, populateColumns);
        /// <summary>
        /// Fill GridControl
        /// </summary>
        /// <param name="view">ColumnView</param>
        /// <param name="dt">DataTable</param>
        /// <param name="populateColumns"></param>
        public static void Fill(this ColumnView view, DataTable dt, bool populateColumns = false) => new GridClass(view).Fill(dt, populateColumns);

        /// <summary>
        /// Get the first selected row
        /// </summary>
        /// <param name="view">ColumnView</param>
        /// <returns>DataRowView ? null</returns>
        public static DataRowView SelectedRow(this ColumnView view) => (DataRowView)view.GetFocusedRow();
        /// <summary>
        /// Get row by index
        /// </summary>
        /// <param name="view">ColumnView</param>
        /// <param name="index">index</param>
        /// <returns>DataRowView</returns>
        public static DataRowView GetARow(this ColumnView view, int index) => (DataRowView)view.GetRow(index);
        /// <summary>
        /// Check if a row is selected
        /// </summary>
        /// <param name="view">ColumnView</param>
        /// <returns>boolean</returns>
        public static bool Selected(this ColumnView view) => view.GetSelectedRows().Length > 0 && view.GetSelectedRows()[0] >= 0;

        /// <summary>
        /// Customize ColumnView
        /// </summary>
        /// <param name="view"></param>
        /// <param name="dateFormat"></param>
        public static void Customize(this ColumnView view, string dateFormat = "dd/MM/yyyy") => new GridClass(view).Customize(dateFormat);

        /// <summary>
        /// set icons of PopupMenu
        /// </summary>
        /// <param name="e"></param>
        public static bool PopupMenuShowing(this PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == GridMenuType.Row || e.MenuType == GridMenuType.User) return false;
            if (e.Menu == null) return true;
            var items = e.Menu.Items.Cast<DXMenuItem>();
            foreach (var item in items)
            {
                //var tmp = Properties.Resources.ResourceManager.GetObject($"{item.Tag}_{WinHelpers.GetPalette(UserLookAndFeel.Default)}");
                var tmp = _app.ResourceManager.GetObject($"{item.Tag}");
                if (!(tmp is Bitmap img)) continue;
                item.ImageOptions.SvgImage = null;
                item.ImageOptions.Image = img;
            }
            return true;
        }
    }
}
