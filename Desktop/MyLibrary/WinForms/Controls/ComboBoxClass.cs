using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace Rent.MyLibrary.WinForms.Controls
{
    /// <summary>
    /// ComboboxItem
    /// </summary>
    public class ComboboxItem
    {
        /// <summary>
        /// Key of item
        /// </summary>
        public object Key { get; set; }
        /// <summary>
        /// value of item
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public ComboboxItem()
        {

        }
        /// <summary>
        /// Constructor with params
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public ComboboxItem(object key, string value) => (Key, Value) = (key, value);
        /// <summary>
        /// To string
        /// </summary>
        /// <returns>value</returns>
        public override string ToString() => Value;
    }
    /// <summary>
    /// Class to manipulate combobox items
    /// </summary>
    public static class _combobox
    {
        /// <summary>
        /// Fill ComboBoxEdit by DataTable
        /// </summary>
        /// <param name="cb">ComboBoxEdit</param>
        /// <param name="dt">DataTable</param>
        public static void Fill(this ComboBoxEdit cb, DataTable dt)
        {
            cb.Text = null;
            cb.Properties.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                if(dr[0].IsNotNull()) cb.Properties.Items.Add(new ComboboxItem { Key = dr[0], Value = (dt.Columns.Count >= 2 ? dr[1] : dr[0]).ToString() });
            }
        }

        /// <summary>
        /// Fill CheckedComboBoxEdit by DataTable
        /// </summary>
        /// <param name="cb">CheckedComboBoxEdit</param>
        /// <param name="dt">DataTable</param>
        public static void Fill(this CheckedComboBoxEdit cb, DataTable dt)
        {
            cb.Text = null;
            cb.Properties.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                if(dr[0].IsNotNull()) cb.Properties.Items.Add(new CheckedListBoxItem { Value = dr[0], Description = (dt.Columns.Count >= 2 ? dr[1] : dr[0]).ToString() });
            }
        }
        /// <summary>
        /// Get selected items
        /// </summary>
        /// <param name="cb">CheckedComboBoxEdit</param>
        /// <returns>List&lt;ComboboxItem&gt;</returns>
        public static IEnumerable<CheckedListBoxItem> SelectedItems(this CheckedComboBoxEdit cb) => cb.Properties.Items.Where(i => i.CheckState == CheckState.Checked);

        /// <summary>
        /// Get selected item
        /// </summary>
        /// <param name="cb">ComboBoxEdit</param>
        /// <returns>ComboboxItem</returns>
        public static ComboboxItem SelectedItem(this ComboBoxEdit cb)
        {
            if (cb.SelectedIndex == -1) return null;
            if (cb.SelectedItem is ComboboxItem item) return item;
            return new ComboboxItem { Key = cb.SelectedItem, Value = cb.SelectedItem.ToString() };
        }
        /// <summary>
        /// Select an item
        /// </summary>
        /// <param name="cb">ComboBoxEdit</param>
        /// <param name="item">value</param>
        public static void Select(this ComboBoxEdit cb, ComboboxItem item)
        {
            if (cb.Properties.Items.Count <= 0) return;
            if (item != null)
            {
                var key = item.Key.IsNotNull() ? item.Key : item.Value;
                if (key.IsNotNull())
                {
                    var res = cb.Properties.Items.Cast<ComboboxItem>().FirstOrDefault(obj => obj.Key.Is(key.ToString()));
                    if (res != null)
                    {
                        cb.SelectedItem = res;
                        return;
                    }
                }
            }
            cb.SelectedIndex = -1;
            cb.Text = null;
        }
        /// <summary>
        /// Fill RepositoryItemImageComboBox using ImageCollection
        /// </summary>
        /// <param name="repositoryItemImageComboBox">RepositoryItemImageComboBox</param>
        /// <param name="imageCollection">ImageCollection</param>
        public static void Fill(this RepositoryItemImageComboBox repositoryItemImageComboBox, ImageCollection imageCollection)
        {
            var keys = imageCollection.Images.Keys;
            for (var i = 0; i < keys.Count; i++) repositoryItemImageComboBox.Items.Add(new ImageComboBoxItem(keys[i], i, i));
            repositoryItemImageComboBox.SmallImages = imageCollection;
        }
        /// <summary>
        /// Fill LookUpEdit using DataTable
        /// </summary>
        /// <param name="lu">LookUpEdit</param>
        /// <param name="dt">DataTable</param>
        public static void Fill(this LookUpEdit lu, DataTable dt)
        {
            lu.Properties.DataSource = null;
            lu.Properties.DataSource = dt;
            lu.Properties.ForceInitialize();
            lu.Properties.PopulateColumns();
            lu.Properties.ValueMember = dt.Columns[0].ColumnName;
            lu.Properties.DisplayMember = dt.Columns.Count > 1 ? dt.Columns[1].ColumnName : dt.Columns[0].ColumnName;
            if (lu.Properties.Columns.Count > 1) lu.Properties.Columns[0].Visible = false;
            lu.Properties.ShowHeader = false;
        }
        /// <summary>
        /// Fill SearchLookUpEdit using DataTable
        /// </summary>
        /// <param name="lu">SearchLookUpEdit</param>
        /// <param name="dt">DataTable</param>
        public static void Fill(this SearchLookUpEdit lu, DataTable dt)
        {
            lu.Properties.DataSource = dt;
            lu.Properties.ValueMember = dt.Columns[0].ColumnName;
            lu.Properties.DisplayMember = dt.Columns.Count > 1 ? dt.Columns[1].ColumnName : dt.Columns[0].ColumnName;
            lu.Properties.PopulateViewColumns();
            if (lu.Properties.View.Columns.Count > 1) lu.Properties.View.Columns[0].Visible = false;
        }
        /// <summary>
        /// Fill RepositoryItemSearchLookUpEdit using DataTable
        /// </summary>
        /// <param name="lu">RepositoryItemSearchLookUpEdit</param>
        /// <param name="dt">DataTable</param>
        public static void Fill(this RepositoryItemSearchLookUpEdit lu, DataTable dt)
        {
            lu.DataSource = dt;
            lu.ValueMember = dt.Columns[0].ColumnName;
            lu.DisplayMember = dt.Columns.Count > 1 ? dt.Columns[1].ColumnName : dt.Columns[0].ColumnName;
            lu.PopulateViewColumns();
            if(lu.View.Columns.Count > 1) lu.View.Columns[0].Visible = false;
        }

    }
}
