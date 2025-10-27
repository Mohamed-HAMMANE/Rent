using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;

namespace Rent.MyLibrary.WinForms.Controls
{
    /// <summary>
    /// Class to manipulate controls
    /// </summary>
    public static class _controlClass
    {
        /// <summary>
        /// Check if a control is not blank
        /// </summary>
        /// <param name="control"></param>
        /// <param name="validationProvider"></param>
        public static void NotBlankValidation(this Control control, DXValidationProvider validationProvider)
        {
            validationProvider.SetValidationRule(control, new ConditionValidationRule
            {
                ConditionOperator = ConditionOperator.IsNotBlank,
                ErrorText = "Champ obligatoire !!!",
                ErrorType = ErrorType.Warning
            });
        }
    }
}
