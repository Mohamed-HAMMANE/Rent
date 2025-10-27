using System;

namespace Rent.MyLibrary.WinForms
{
    /// <summary>
    /// Loading form
    /// </summary>
    public partial class LoadingForm : DevExpress.XtraWaitForm.WaitForm
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LoadingForm()
        {
            InitializeComponent();
            this.progressPanel1.AutoHeight = true;
        }

        #region Overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="caption"></param>
        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="arg"></param>
        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public enum WaitFormCommand
        {
        }
    }
}