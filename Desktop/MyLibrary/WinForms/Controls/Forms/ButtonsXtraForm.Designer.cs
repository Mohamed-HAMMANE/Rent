namespace Rent.MyLibrary.WinForms.Controls.Forms
{
    partial class ButtonsXtraForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ButtonsGroupControl = new DevExpress.XtraEditors.GroupControl();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.BtnOk = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonsGroupControl)).BeginInit();
            this.ButtonsGroupControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonsGroupControl
            // 
            this.ButtonsGroupControl.Controls.Add(this.BtnCancel);
            this.ButtonsGroupControl.Controls.Add(this.labelControl1);
            this.ButtonsGroupControl.Controls.Add(this.BtnOk);
            this.ButtonsGroupControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonsGroupControl.Location = new System.Drawing.Point(0, 273);
            this.ButtonsGroupControl.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonsGroupControl.Name = "ButtonsGroupControl";
            this.ButtonsGroupControl.Padding = new System.Windows.Forms.Padding(10, 2, 10, 2);
            this.ButtonsGroupControl.ShowCaption = false;
            this.ButtonsGroupControl.Size = new System.Drawing.Size(490, 35);
            this.ButtonsGroupControl.TabIndex = 7;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnCancel.Location = new System.Drawing.Point(299, 4);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(85, 27);
            this.BtnCancel.TabIndex = 3;
            this.BtnCancel.Text = "Annuler";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelControl1.Location = new System.Drawing.Point(384, 4);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(9, 27);
            this.labelControl1.TabIndex = 4;
            // 
            // BtnOk
            // 
            this.BtnOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnOk.Location = new System.Drawing.Point(393, 4);
            this.BtnOk.Margin = new System.Windows.Forms.Padding(0);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(85, 27);
            this.BtnOk.TabIndex = 2;
            this.BtnOk.Text = "Ok";
            // 
            // ButtonsXtraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 308);
            this.Controls.Add(this.ButtonsGroupControl);
            this.Name = "ButtonsXtraForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LayoutXtraForm";
            ((System.ComponentModel.ISupportInitialize)(this.ButtonsGroupControl)).EndInit();
            this.ButtonsGroupControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        public DevExpress.XtraEditors.GroupControl ButtonsGroupControl;
        /// <summary>
        /// 
        /// </summary>
        public DevExpress.XtraEditors.SimpleButton BtnCancel;
        /// <summary>
        /// 
        /// </summary>
        public DevExpress.XtraEditors.SimpleButton BtnOk;
        /// <summary>
        /// 
        /// </summary>
        public DevExpress.XtraEditors.LabelControl labelControl1;
    }
}