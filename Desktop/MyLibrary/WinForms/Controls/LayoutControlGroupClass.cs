using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraRichEdit;

namespace Rent.MyLibrary.WinForms.Controls
{
    /// <summary>
    /// LayoutControlGroupClass
    /// </summary>
    public class LayoutControlGroupClass
    {
        private LayoutControlGroup[] Roots { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roots"></param>
        public LayoutControlGroupClass(params LayoutControlGroup[] roots)
        {
            Roots = roots;
        }

        /// <summary>
        /// Save a root data to database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validationProvide"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        public bool Save<T>(DXValidationProvider validationProvide, bool autoClose = true)
        {
            if (validationProvide != null)
            {
                if (!validationProvide.Validate()) return false;
            }
            try
            {
                /*if (checkModification)
                {
                    if (Roots.Modified<T>()) InnerSave<T>(/*duplication**);
                }
                else */InnerSave<T>(/*duplication*/);
                if (autoClose) Roots[0].ParentForm().DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                ex.Show(true);
                return false;
            }
            return true;
        }
        private void InnerSave<T>()
        {
            var tObj = Roots[0].Current<T>();
            var obj = tObj.Copy();
            foreach(var root in Roots) FillSave(root.Items, obj);
            obj.Save(/*duplication*/);
            Roots[0].Current<T>(obj);
        }
        private static void FillSave<T>(IEnumerable layoutControlGroupItems, T obj)
        {
            foreach (var layoutItem in layoutControlGroupItems)
            {
                switch (layoutItem)
                {
                    case LayoutControlItem lci:
                        {
                            if (lci.Control != null)
                            {
                                var ctr = lci.Control;
                                if (ctr.Tag.Has("avoid") || lci.Visibility != LayoutVisibility.Always) continue;
                                var fieldName = ctr.FieldName();
                                var field = _attr.GetField<T>(fieldName);
                                if (field == null)
                                {
                                    field = _attr.GetField<T>($"_{fieldName.FirstCharToLower()}");
                                    if (field == null) continue;
                                }
                                object val = null;
                                switch (ctr)
                                {
                                    case SearchLookUpEdit sle:
                                    {
                                        if (sle.EditValue.IsNotNull()) val = sle.EditValue;
                                        else val = sle.Text.IsNull() ? null : sle.Text;
                                        break;
                                    }
                                    case MemoEdit me:
                                    {
                                        val = me.Text.IsNull() ? null : me.Text;
                                        break;
                                    }
                                    case PictureEdit pictureEdit:
                                    {
                                        if (pictureEdit.Image == null) val = null;
                                        else
                                        {
                                            var path = _file.Temp("png");
                                            pictureEdit.Image.Save(path);
                                            val = _file.ToBytes(path);
                                        }
                                        break;
                                    }
                                    case ImageComboBoxEdit imageComboBox:
                                    {
                                        val = imageComboBox.EditValue is ComboboxItem comboboxItem ? comboboxItem?.Key : imageComboBox.EditValue;
                                        break;
                                    }
                                    case CheckedComboBoxEdit ccbe:
                                    {
                                        var list = new List<string>();
                                        var items = ccbe.Properties.Items;
                                        foreach (CheckedListBoxItem item in items)
                                        {
                                            if (item.CheckState == CheckState.Checked) list.Add(item.Value is ComboboxItem cb ? cb.Key.ToString() : item.Value.ToString());
                                        }
                                        val = list.Count <= 0 ? null : string.Join(",", list);
                                        break;
                                    }
                                    case ComboBoxEdit cb:
                                    {
                                        if (cb.SelectedIndex != -1)
                                        {
                                            if (cb.SelectedItem is ComboboxItem ci) val = ci.Key;
                                            else val = cb.Text;
                                        }
                                        else val = cb.Text.IsNull() ? null : cb.Text;
                                        break;
                                    }
                                    case CheckEdit ce:
                                    {
                                        val = ce.Checked;
                                        break;
                                    }
                                    case DateEdit de:
                                    {
                                        val = de.Text.IsNull() ? null : de.DateTime;
                                        break;
                                    }
                                    case TimeEdit te:
                                    {
                                        val = te.Text.IsNull() ? null : te.Time;
                                        break;
                                    }
                                    case RatingControl rc:
                                    {
                                        val = rc.Rating.ToInt();
                                        break;
                                    }
                                    case SpinEdit se:
                                    {
                                        if (se.Text.IsNull()) val = null;
                                        else switch (field.FieldType.Name)
                                        {
                                            case "Decimal":
                                                val = se.Value;
                                                break;
                                            case "Double":
                                                val = se.Value.ToDouble();
                                                break;
                                            case "Int16":
                                            case "Int32":
                                                val = se.Value.ToInt();
                                                break;
                                            case "Int64":
                                                val = se.Value.ToLong();
                                                break;
                                            }
                                        break;
                                    }
                                    case TextEdit te:
                                    {
                                        val = te.Text.IsNull() ? null : te.Text;
                                        break;
                                    }
                                }

                                var cAttr = field.CustomAttribute();
                                if (cAttr != null && cAttr.Action.Contains(MyAttribute.ActionType.ForeignKey)) field.SetValue(obj, field.FieldType.SetPkValue(val));
                                else field.SetValue(obj, val);
                            }
                            break;
                        }
                    case LayoutControlGroup lcg:
                        if(lcg.Tag?.ToString() != "avoid") FillSave(lcg.Items, obj);
                        break;
                    case TabbedControlGroup tcg:
                        if (tcg.Tag?.ToString() != "avoid") FillSave(tcg.TabPages, obj);
                        break;
                }
            }
        }

        /// <summary>
        /// Fill a root from object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Fill<T>()
        {
            try
            {
                var obj = Roots[0].Current<T>();
                if (obj.Exists())
                {
                    var form = Roots[0].ParentForm();
                    form.Text += @" : " + obj;
                }
                foreach(var root in Roots) FillH(root.Items, obj);
            }
            catch (Exception ex)
            {
                ex.Show();
            }
        }
        private static void FillH<T>(IEnumerable layoutControlGroupItems, T obj)
        {
            foreach (var layoutItem in layoutControlGroupItems)
            {
                switch (layoutItem)
                {
                    case LayoutControlItem lci:
                        {
                            if (lci.Control != null)
                            {
                                var ctr = lci.Control;
                                if (ctr.Tag.Has("avoid") || lci.Visibility != LayoutVisibility.Always) continue;
                                var fieldName = ctr.FieldName();
                                var field = _attr.GetField<T>(fieldName);
                                if (field == null)
                                {
                                    field = _attr.GetField<T>($"_{fieldName.FirstCharToLower()}");
                                    if (field == null) continue;
                                }
                                var value = field.GetValue(obj);
                                if (value != null && value.IsNotNull())
                                {
                                    var cAttr = field.CustomAttribute();
                                    if (cAttr != null && cAttr.Action.Contains(MyAttribute.ActionType.ForeignKey)) value = field.FieldType.GetId().GetValue(value);

                                    switch (ctr)
                                    {
                                        case SearchLookUpEdit sle:
                                            {
                                                sle.EditValue = value;
                                                /*if (sle.Properties.View.RowCount > 0) sle.EditValue = value;
                                                else sle.Text = value.ToString();*/
                                                break;
                                            }
                                        case MemoEdit me:
                                            {
                                                me.Text = value.ToString();
                                                break;
                                            }
                                        case PictureEdit pictureEdit:
                                            {
                                                pictureEdit.Image = Image.FromFile(((byte[])value).ToFile(_file.Temp("png")));
                                                break;
                                            }
                                        case ImageComboBoxEdit imageComboBox:
                                            {
                                                imageComboBox.SelectedIndex = (int)value;
                                                break;
                                            }
                                        case CheckedComboBoxEdit ccbe:
                                            {
                                                var items = ccbe.Properties.Items;
                                                foreach (CheckedListBoxItem item in items)
                                                {
                                                    var lst = value.ToString().Split(',').Select(e => e.Trim());
                                                    if (item.Value is ComboboxItem cb)
                                                    {
                                                        if (lst.Contains(cb.Key.ToString())) item.CheckState = CheckState.Checked;
                                                    }
                                                    else if (lst.Contains(item.Value.ToString())) item.CheckState = CheckState.Checked;
                                                }
                                                break;
                                            }
                                        case ComboBoxEdit cb:
                                            {
                                                if (cb.Properties.Items.Count > 0)
                                                {
                                                    if (cb.Properties.Items[0] is ComboboxItem) cb.Select(new ComboboxItem { Key = value });
                                                    else cb.Text = value.ToString();
                                                }
                                                else cb.Text = value.ToString();
                                                break;
                                            }
                                        case CheckEdit ce:
                                            {
                                                ce.Checked = Convert.ToBoolean(value);
                                                break;
                                            }
                                        case DateEdit de:
                                            {
                                                if (value.IsDate()) de.DateTime = Convert.ToDateTime(value);
                                                break;
                                            }
                                        case TimeEdit te:
                                            {
                                                if (value.IsDate()) te.Time = Convert.ToDateTime(value);
                                                break;
                                            }
                                        case RatingControl rc:
                                            {
                                                rc.Rating = value.ToDecimal();
                                                break;
                                            }
                                        case SpinEdit se:
                                            {
                                                se.Value = value.ToDecimal();
                                                break;
                                            }
                                        case TextEdit te:
                                            {
                                                te.Text = value.ToString();
                                                break;
                                            }
                                    }
                                }
                            }
                            break;
                        }
                    case LayoutControlGroup lcg:
                        if (lcg.Tag?.ToString() != "avoid") FillH(lcg.Items, obj);
                        break;
                    case TabbedControlGroup tcg:
                        if (tcg.Tag?.ToString() != "avoid") FillH(tcg.TabPages, obj);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Class to manipulate LayoutControlGroup
    /// </summary>
    public static class _layoutControlGroupClass
    {
        /// <summary>
        /// For saving root data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <param name="validationProvide"></param>
        /// <param name="autoClose">save and verify duplication and close form</param>
        /// <returns></returns>
        public static bool Save<T>(this LayoutControlGroup root, DXValidationProvider validationProvide = null, bool autoClose = true) => new LayoutControlGroupClass(root).Save<T>(validationProvide, autoClose/*, checkModification, duplication*/);
        /// <summary>
        /// For saving root data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root">LayoutControlGroup</param>
        /// <param name="autoClose">close form</param>
        /// <returns>bool</returns>
        public static bool Save<T>(this LayoutControlGroup root, bool autoClose) => Save<T>(root, null, autoClose);
        /// <summary>
        /// Fill root from object data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        public static void Fill<T>(this LayoutControlGroup root) => new LayoutControlGroupClass(root).Fill<T>();
        /// <summary>
        /// Get current root's object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        public static T Current<T>(this LayoutControlGroup root) => root.Tag is null or string ? (T)Activator.CreateInstance(typeof(T)) : (T)root.Tag;
        /// <summary>
        /// Set current root's object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <param name="obj"></param>
        public static void Current<T>(this LayoutControlGroup root, object obj) => root.Tag = obj ?? Activator.CreateInstance(typeof(T));
        /// <summary>
        /// Get parent form
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static XtraForm ParentForm(this LayoutControlGroup root)
        {
            var obj = root.Owner.Parent;
            while (!(obj is XtraForm)) obj = obj.Parent;
            return (XtraForm)obj;
        }
        /// <summary>
        /// Get class name of a control
        /// </summary>
        /// <param name="ctr"></param>
        /// <returns></returns>
        public static string FieldName(this Control ctr)
        {
            var name = ctr.Tag;
            if (!name.IsNull()) return name.ToString();
            var str = ctr.Name;
            var lastCapital = Array.FindLastIndex(str.ToCharArray(), char.IsUpper);
            if (lastCapital > 0) str = str.Substring(0, str.Length - str.Substring(lastCapital).Length);
            name = str;
            return name.ToString();
        }
    }
}