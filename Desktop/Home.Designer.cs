namespace Rent
{
    partial class Home
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            DevExpress.DataAccess.Sql.SelectQuery selectQuery2 = new DevExpress.DataAccess.Sql.SelectQuery();
            DevExpress.DataAccess.Sql.AllColumns allColumns2 = new DevExpress.DataAccess.Sql.AllColumns();
            DevExpress.DataAccess.Sql.Table table2 = new DevExpress.DataAccess.Sql.Table();
            DevExpress.DataAccess.Sql.Sorting sorting2 = new DevExpress.DataAccess.Sql.Sorting();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression2 = new DevExpress.DataAccess.Sql.ColumnExpression();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.BtnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.BtnEdit = new DevExpress.XtraBars.BarButtonItem();
            this.BtnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.layoutView1 = new DevExpress.XtraGrid.Views.Layout.LayoutView();
            this.colId = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_colId = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colImage = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.ImageRepo = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.layoutViewField_colImage = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colLink = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.LinkRepo = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.layoutViewField_colLink = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colPrice = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_colPrice = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colAddress = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_colPlace = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colQuality = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_colQuality = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colAesthetics = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_colAesthetics = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colFurniture = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_colFurniture = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colPhone = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_colPhone = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colObservation = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.ObservationRepo = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.layoutViewField_colObservation = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colChatLink = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.ChatLinkRepo = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.layoutViewField_colChatLink = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colLocation = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.colMark = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1_1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.Group1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.Group = new DevExpress.XtraLayout.LayoutControlGroup();
            this.Group2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.item1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.Group3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.item2 = new DevExpress.XtraLayout.SimpleSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageRepo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LinkRepo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPlace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colAesthetics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colFurniture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObservationRepo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colObservation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChatLinkRepo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colChatLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.item1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.item2)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.AllowKeyTips = false;
            this.ribbonControl1.AllowMdiChildButtons = false;
            this.ribbonControl1.AllowMinimizeRibbon = false;
            this.ribbonControl1.AllowMinimizeRibbonOnDoubleClick = false;
            this.ribbonControl1.AllowTrimPageText = false;
            this.ribbonControl1.CommandLayout = DevExpress.XtraBars.Ribbon.CommandLayout.Simplified;
            this.ribbonControl1.DrawGroupCaptions = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.DrawGroupsBorderMode = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.BtnAdd,
            this.BtnEdit,
            this.BtnDelete});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 4;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowMoreCommandsButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.ShowQatLocationSelector = false;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(1303, 97);
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Caption = "Add";
            this.BtnAdd.Id = 1;
            this.BtnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnAdd.ImageOptions.Image")));
            this.BtnAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BtnAdd.ImageOptions.LargeImage")));
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Caption = "Edit";
            this.BtnEdit.Enabled = false;
            this.BtnEdit.Id = 2;
            this.BtnEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnEdit.ImageOptions.Image")));
            this.BtnEdit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BtnEdit.ImageOptions.LargeImage")));
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Caption = "Delete";
            this.BtnDelete.Enabled = false;
            this.BtnDelete.Id = 3;
            this.BtnDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnDelete.ImageOptions.Image")));
            this.BtnDelete.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BtnDelete.ImageOptions.LargeImage")));
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.BtnAdd);
            this.ribbonPageGroup1.ItemLinks.Add(this.BtnEdit);
            this.ribbonPageGroup1.ItemLinks.Add(this.BtnDelete);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "DevelopmentString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            table2.MetaSerializable = "<Meta X=\"30\" Y=\"30\" Width=\"125\" Height=\"363\" />";
            table2.Name = "BachelorPadView";
            allColumns2.Table = table2;
            selectQuery2.Columns.Add(allColumns2);
            selectQuery2.Name = "Query";
            sorting2.Direction = System.ComponentModel.ListSortDirection.Descending;
            columnExpression2.ColumnName = "Mark";
            columnExpression2.Table = table2;
            sorting2.Expression = columnExpression2;
            selectQuery2.Sorting.Add(sorting2);
            selectQuery2.Tables.Add(table2);
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            selectQuery2});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // gridControl1
            // 
            this.gridControl1.DataMember = "Query";
            this.gridControl1.DataSource = this.sqlDataSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 97);
            this.gridControl1.MainView = this.layoutView1;
            this.gridControl1.MenuManager = this.ribbonControl1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.LinkRepo,
            this.ChatLinkRepo,
            this.ObservationRepo,
            this.ImageRepo});
            this.gridControl1.Size = new System.Drawing.Size(1303, 853);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutView1});
            // 
            // layoutView1
            // 
            this.layoutView1.Appearance.FieldCaption.FontSizeDelta = 1;
            this.layoutView1.Appearance.FieldCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.layoutView1.Appearance.FieldCaption.Options.UseFont = true;
            this.layoutView1.CardHorzInterval = 1;
            this.layoutView1.CardMinSize = new System.Drawing.Size(667, 427);
            this.layoutView1.CardVertInterval = 0;
            this.layoutView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.colId,
            this.colImage,
            this.colLink,
            this.colPrice,
            this.colAddress,
            this.colQuality,
            this.colAesthetics,
            this.colFurniture,
            this.colPhone,
            this.colObservation,
            this.colChatLink,
            this.colLocation,
            this.colMark});
            this.layoutView1.GridControl = this.gridControl1;
            this.layoutView1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_colId});
            this.layoutView1.Name = "layoutView1";
            this.layoutView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.layoutView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.layoutView1.OptionsBehavior.AllowExpandCollapse = false;
            this.layoutView1.OptionsBehavior.AllowPanCards = false;
            this.layoutView1.OptionsBehavior.AllowRuntimeCustomization = false;
            this.layoutView1.OptionsView.ShowCardExpandButton = false;
            this.layoutView1.OptionsView.ShowCardFieldBorders = true;
            this.layoutView1.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.MultiRow;
            this.layoutView1.TemplateCard = this.layoutViewCard1;
            this.layoutView1.DoubleClick += new System.EventHandler(this.layoutView1_DoubleClick);
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.LayoutViewField = this.layoutViewField_colId;
            this.colId.Name = "colId";
            this.colId.OptionsColumn.ReadOnly = true;
            // 
            // layoutViewField_colId
            // 
            this.layoutViewField_colId.EditorPreferredWidth = 20;
            this.layoutViewField_colId.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colId.Name = "layoutViewField_colId";
            this.layoutViewField_colId.Size = new System.Drawing.Size(647, 389);
            this.layoutViewField_colId.TextSize = new System.Drawing.Size(66, 13);
            // 
            // colImage
            // 
            this.colImage.ColumnEdit = this.ImageRepo;
            this.colImage.FieldName = "Image";
            this.colImage.LayoutViewField = this.layoutViewField_colImage;
            this.colImage.Name = "colImage";
            this.colImage.OptionsColumn.ReadOnly = true;
            // 
            // ImageRepo
            // 
            this.ImageRepo.ContextButtonOptions.ItemCursor = System.Windows.Forms.Cursors.Hand;
            this.ImageRepo.ContextButtonOptions.PanelCursor = System.Windows.Forms.Cursors.Hand;
            this.ImageRepo.Name = "ImageRepo";
            this.ImageRepo.ShowMenu = false;
            this.ImageRepo.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.ImageRepo.Click += new System.EventHandler(this.ImageRepo_Click);
            // 
            // layoutViewField_colImage
            // 
            this.layoutViewField_colImage.EditorPreferredWidth = 365;
            this.layoutViewField_colImage.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colImage.MaxSize = new System.Drawing.Size(369, 369);
            this.layoutViewField_colImage.MinSize = new System.Drawing.Size(369, 369);
            this.layoutViewField_colImage.Name = "layoutViewField_colImage";
            this.layoutViewField_colImage.Size = new System.Drawing.Size(369, 389);
            this.layoutViewField_colImage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutViewField_colImage.StartNewLine = true;
            this.layoutViewField_colImage.TextVisible = false;
            // 
            // colLink
            // 
            this.colLink.ColumnEdit = this.LinkRepo;
            this.colLink.FieldName = "Link";
            this.colLink.LayoutViewField = this.layoutViewField_colLink;
            this.colLink.Name = "colLink";
            // 
            // LinkRepo
            // 
            this.LinkRepo.AutoHeight = false;
            this.LinkRepo.Name = "LinkRepo";
            this.LinkRepo.SingleClick = true;
            // 
            // layoutViewField_colLink
            // 
            this.layoutViewField_colLink.EditorPreferredWidth = 73;
            this.layoutViewField_colLink.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colLink.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutViewField_colLink.MinSize = new System.Drawing.Size(95, 24);
            this.layoutViewField_colLink.Name = "layoutViewField_colLink";
            this.layoutViewField_colLink.Size = new System.Drawing.Size(134, 24);
            this.layoutViewField_colLink.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutViewField_colLink.TextSize = new System.Drawing.Size(51, 13);
            // 
            // colPrice
            // 
            this.colPrice.FieldName = "Price";
            this.colPrice.LayoutViewField = this.layoutViewField_colPrice;
            this.colPrice.Name = "colPrice";
            this.colPrice.OptionsColumn.ReadOnly = true;
            // 
            // layoutViewField_colPrice
            // 
            this.layoutViewField_colPrice.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.layoutViewField_colPrice.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutViewField_colPrice.EditorPreferredWidth = 68;
            this.layoutViewField_colPrice.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colPrice.Name = "layoutViewField_colPrice";
            this.layoutViewField_colPrice.Size = new System.Drawing.Size(122, 24);
            this.layoutViewField_colPrice.TextSize = new System.Drawing.Size(44, 13);
            // 
            // colAddress
            // 
            this.colAddress.Caption = "Address";
            this.colAddress.FieldName = "Address";
            this.colAddress.LayoutViewField = this.layoutViewField_colPlace;
            this.colAddress.Name = "colAddress";
            this.colAddress.OptionsColumn.ReadOnly = true;
            // 
            // layoutViewField_colPlace
            // 
            this.layoutViewField_colPlace.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutViewField_colPlace.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutViewField_colPlace.EditorPreferredWidth = 80;
            this.layoutViewField_colPlace.Location = new System.Drawing.Point(122, 0);
            this.layoutViewField_colPlace.Name = "layoutViewField_colPlace";
            this.layoutViewField_colPlace.Size = new System.Drawing.Size(134, 24);
            this.layoutViewField_colPlace.TextSize = new System.Drawing.Size(44, 13);
            // 
            // colQuality
            // 
            this.colQuality.FieldName = "Quality";
            this.colQuality.LayoutViewField = this.layoutViewField_colQuality;
            this.colQuality.Name = "colQuality";
            this.colQuality.OptionsColumn.ReadOnly = true;
            // 
            // layoutViewField_colQuality
            // 
            this.layoutViewField_colQuality.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.layoutViewField_colQuality.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutViewField_colQuality.EditorPreferredWidth = 57;
            this.layoutViewField_colQuality.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colQuality.Name = "layoutViewField_colQuality";
            this.layoutViewField_colQuality.Size = new System.Drawing.Size(122, 24);
            this.layoutViewField_colQuality.TextSize = new System.Drawing.Size(55, 13);
            // 
            // colAesthetics
            // 
            this.colAesthetics.FieldName = "Aesthetics";
            this.colAesthetics.LayoutViewField = this.layoutViewField_colAesthetics;
            this.colAesthetics.Name = "colAesthetics";
            this.colAesthetics.OptionsColumn.ReadOnly = true;
            // 
            // layoutViewField_colAesthetics
            // 
            this.layoutViewField_colAesthetics.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.layoutViewField_colAesthetics.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutViewField_colAesthetics.EditorPreferredWidth = 57;
            this.layoutViewField_colAesthetics.Location = new System.Drawing.Point(0, 25);
            this.layoutViewField_colAesthetics.Name = "layoutViewField_colAesthetics";
            this.layoutViewField_colAesthetics.Size = new System.Drawing.Size(122, 24);
            this.layoutViewField_colAesthetics.TextSize = new System.Drawing.Size(55, 13);
            // 
            // colFurniture
            // 
            this.colFurniture.FieldName = "Furniture";
            this.colFurniture.LayoutViewField = this.layoutViewField_colFurniture;
            this.colFurniture.Name = "colFurniture";
            this.colFurniture.OptionsColumn.ReadOnly = true;
            // 
            // layoutViewField_colFurniture
            // 
            this.layoutViewField_colFurniture.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.layoutViewField_colFurniture.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutViewField_colFurniture.EditorPreferredWidth = 69;
            this.layoutViewField_colFurniture.Location = new System.Drawing.Point(122, 25);
            this.layoutViewField_colFurniture.Name = "layoutViewField_colFurniture";
            this.layoutViewField_colFurniture.Size = new System.Drawing.Size(134, 24);
            this.layoutViewField_colFurniture.TextSize = new System.Drawing.Size(55, 13);
            // 
            // colPhone
            // 
            this.colPhone.FieldName = "Phone";
            this.colPhone.LayoutViewField = this.layoutViewField_colPhone;
            this.colPhone.Name = "colPhone";
            this.colPhone.OptionsColumn.ReadOnly = true;
            // 
            // layoutViewField_colPhone
            // 
            this.layoutViewField_colPhone.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.layoutViewField_colPhone.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutViewField_colPhone.EditorPreferredWidth = 52;
            this.layoutViewField_colPhone.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colPhone.Name = "layoutViewField_colPhone";
            this.layoutViewField_colPhone.Size = new System.Drawing.Size(128, 24);
            this.layoutViewField_colPhone.TextSize = new System.Drawing.Size(66, 13);
            // 
            // colObservation
            // 
            this.colObservation.ColumnEdit = this.ObservationRepo;
            this.colObservation.FieldName = "Observation";
            this.colObservation.LayoutViewField = this.layoutViewField_colObservation;
            this.colObservation.Name = "colObservation";
            this.colObservation.OptionsColumn.ReadOnly = true;
            // 
            // ObservationRepo
            // 
            this.ObservationRepo.Name = "ObservationRepo";
            // 
            // layoutViewField_colObservation
            // 
            this.layoutViewField_colObservation.EditorPreferredWidth = 250;
            this.layoutViewField_colObservation.Location = new System.Drawing.Point(0, 25);
            this.layoutViewField_colObservation.Name = "layoutViewField_colObservation";
            this.layoutViewField_colObservation.Size = new System.Drawing.Size(256, 71);
            this.layoutViewField_colObservation.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutViewField_colObservation.TextSize = new System.Drawing.Size(66, 13);
            // 
            // colChatLink
            // 
            this.colChatLink.ColumnEdit = this.ChatLinkRepo;
            this.colChatLink.FieldName = "ChatLink";
            this.colChatLink.LayoutViewField = this.layoutViewField_colChatLink;
            this.colChatLink.Name = "colChatLink";
            // 
            // ChatLinkRepo
            // 
            this.ChatLinkRepo.AutoHeight = false;
            this.ChatLinkRepo.Name = "ChatLinkRepo";
            this.ChatLinkRepo.SingleClick = true;
            // 
            // layoutViewField_colChatLink
            // 
            this.layoutViewField_colChatLink.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.layoutViewField_colChatLink.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutViewField_colChatLink.EditorPreferredWidth = 61;
            this.layoutViewField_colChatLink.Location = new System.Drawing.Point(134, 0);
            this.layoutViewField_colChatLink.Name = "layoutViewField_colChatLink";
            this.layoutViewField_colChatLink.Size = new System.Drawing.Size(122, 24);
            this.layoutViewField_colChatLink.TextSize = new System.Drawing.Size(51, 13);
            // 
            // colLocation
            // 
            this.colLocation.Caption = "Location";
            this.colLocation.FieldName = "Location";
            this.colLocation.LayoutViewField = this.layoutViewField_layoutViewColumn1;
            this.colLocation.Name = "colLocation";
            // 
            // layoutViewField_layoutViewColumn1
            // 
            this.layoutViewField_layoutViewColumn1.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.layoutViewField_layoutViewColumn1.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutViewField_layoutViewColumn1.EditorPreferredWidth = 69;
            this.layoutViewField_layoutViewColumn1.Location = new System.Drawing.Point(122, 0);
            this.layoutViewField_layoutViewColumn1.Name = "layoutViewField_layoutViewColumn1";
            this.layoutViewField_layoutViewColumn1.Size = new System.Drawing.Size(134, 24);
            this.layoutViewField_layoutViewColumn1.TextSize = new System.Drawing.Size(55, 13);
            // 
            // colMark
            // 
            this.colMark.Caption = "Mark";
            this.colMark.DisplayFormat.FormatString = "N2";
            this.colMark.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMark.FieldName = "Mark";
            this.colMark.LayoutViewField = this.layoutViewField_layoutViewColumn1_1;
            this.colMark.Name = "colMark";
            this.colMark.OptionsColumn.AllowEdit = false;
            this.colMark.OptionsColumn.ReadOnly = true;
            // 
            // layoutViewField_layoutViewColumn1_1
            // 
            this.layoutViewField_layoutViewColumn1_1.EditorPreferredWidth = 52;
            this.layoutViewField_layoutViewColumn1_1.Location = new System.Drawing.Point(128, 0);
            this.layoutViewField_layoutViewColumn1_1.Name = "layoutViewField_layoutViewColumn1_1";
            this.layoutViewField_layoutViewColumn1_1.Size = new System.Drawing.Size(128, 24);
            this.layoutViewField_layoutViewColumn1_1.TextSize = new System.Drawing.Size(66, 13);
            // 
            // layoutViewCard1
            // 
            this.layoutViewCard1.CustomizationFormText = "TemplateCard";
            this.layoutViewCard1.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutViewCard1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_colImage,
            this.Group1,
            this.Group,
            this.Group2,
            this.Group3});
            this.layoutViewCard1.Name = "layoutViewCard1";
            this.layoutViewCard1.OptionsItemText.TextToControlDistance = 5;
            this.layoutViewCard1.Text = "TemplateCard";
            // 
            // Group1
            // 
            this.Group1.CustomizationFormText = "Identification";
            this.Group1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_colPrice,
            this.layoutViewField_colPlace});
            this.Group1.Location = new System.Drawing.Point(369, 0);
            this.Group1.Name = "Group1";
            this.Group1.OptionsItemText.TextToControlDistance = 3;
            this.Group1.Size = new System.Drawing.Size(280, 73);
            this.Group1.Text = "Identification";
            // 
            // Group
            // 
            this.Group.CustomizationFormText = "Links";
            this.Group.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_colChatLink,
            this.layoutViewField_colLink});
            this.Group.Location = new System.Drawing.Point(369, 73);
            this.Group.Name = "Group";
            this.Group.OptionsItemText.TextToControlDistance = 3;
            this.Group.Size = new System.Drawing.Size(280, 73);
            this.Group.Text = "Links";
            // 
            // Group2
            // 
            this.Group2.CustomizationFormText = "Marks";
            this.Group2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_colQuality,
            this.layoutViewField_layoutViewColumn1,
            this.layoutViewField_colAesthetics,
            this.layoutViewField_colFurniture,
            this.item1});
            this.Group2.Location = new System.Drawing.Point(369, 146);
            this.Group2.Name = "Group2";
            this.Group2.OptionsItemText.TextToControlDistance = 3;
            this.Group2.Size = new System.Drawing.Size(280, 98);
            this.Group2.Text = "Marks";
            // 
            // item1
            // 
            this.item1.CustomizationFormText = "item1";
            this.item1.Location = new System.Drawing.Point(0, 24);
            this.item1.Name = "item1";
            this.item1.Size = new System.Drawing.Size(256, 1);
            // 
            // Group3
            // 
            this.Group3.CustomizationFormText = "Info";
            this.Group3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_colPhone,
            this.layoutViewField_colObservation,
            this.item2,
            this.layoutViewField_layoutViewColumn1_1});
            this.Group3.Location = new System.Drawing.Point(369, 244);
            this.Group3.Name = "Group3";
            this.Group3.OptionsItemText.TextToControlDistance = 3;
            this.Group3.Size = new System.Drawing.Size(280, 145);
            this.Group3.Text = "Info";
            // 
            // item2
            // 
            this.item2.CustomizationFormText = "item2";
            this.item2.Location = new System.Drawing.Point(0, 24);
            this.item2.Name = "item2";
            this.item2.Size = new System.Drawing.Size(256, 1);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 950);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.ribbonControl1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("Home.IconOptions.SvgImage")));
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageRepo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LinkRepo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPlace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colAesthetics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colFurniture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObservationRepo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colObservation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChatLinkRepo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colChatLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.item1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Group3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.item2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem BtnAdd;
        private DevExpress.XtraBars.BarButtonItem BtnEdit;
        private DevExpress.XtraBars.BarButtonItem BtnDelete;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Layout.LayoutView layoutView1;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colId;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colImage;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colLink;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colPrice;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colAddress;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colQuality;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colAesthetics;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colFurniture;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colPhone;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colObservation;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colChatLink;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit LinkRepo;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit ChatLinkRepo;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit ObservationRepo;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit ImageRepo;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colLocation;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colMark;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colId;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colImage;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colLink;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colPrice;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colPlace;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colQuality;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colAesthetics;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colFurniture;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colPhone;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colObservation;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colChatLink;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1_1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
        private DevExpress.XtraLayout.LayoutControlGroup Group1;
        private DevExpress.XtraLayout.LayoutControlGroup Group;
        private DevExpress.XtraLayout.LayoutControlGroup Group2;
        private DevExpress.XtraLayout.SimpleSeparator item1;
        private DevExpress.XtraLayout.LayoutControlGroup Group3;
        private DevExpress.XtraLayout.SimpleSeparator item2;
    }
}

