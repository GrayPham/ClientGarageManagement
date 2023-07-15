
namespace ManagementStore.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cameraControlHome = new DevExpress.XtraEditors.Camera.CameraControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnIdentity = new DevExpress.XtraEditors.SimpleButton();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barItemIP = new DevExpress.XtraBars.BarStaticItem();
            this.barItemConnect = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem3 = new DevExpress.XtraBars.BarStaticItem();
            this.barItemPort = new DevExpress.XtraBars.BarStaticItem();
            this.barItemVersion = new DevExpress.XtraBars.BarStaticItem();
            this.repositoryItemRatingControl1 = new DevExpress.XtraEditors.Repository.RepositoryItemRatingControl();
            this.repositoryItemTimeEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.webBrowserVideo = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRatingControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.cameraControlHome);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.btnIdentity);
            this.panelControl1.Controls.Add(this.ribbonControl1);
            this.panelControl1.Controls.Add(this.ribbonStatusBar1);
            this.panelControl1.Controls.Add(this.webBrowserVideo);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(708, 861);
            this.panelControl1.TabIndex = 0;
            // 
            // cameraControlHome
            // 
            this.cameraControlHome.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cameraControlHome.Location = new System.Drawing.Point(2, 2);
            this.cameraControlHome.Name = "cameraControlHome";
            this.cameraControlHome.Size = new System.Drawing.Size(704, 352);
            this.cameraControlHome.TabIndex = 16;
            this.cameraControlHome.Text = "cameraControlHome";
            this.cameraControlHome.VideoStretchMode = DevExpress.XtraEditors.Camera.VideoStretchMode.Stretch;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseBackColor = true;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.ImageOptions.SvgImageSize = new System.Drawing.Size(40, 40);
            this.simpleButton1.Location = new System.Drawing.Point(366, 766);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(225, 51);
            this.simpleButton1.TabIndex = 10;
            this.simpleButton1.Text = "Map";
            // 
            // btnIdentity
            // 
            this.btnIdentity.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnIdentity.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIdentity.Appearance.Options.UseBackColor = true;
            this.btnIdentity.Appearance.Options.UseFont = true;
            this.btnIdentity.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnIdentity.ImageOptions.Image")));
            this.btnIdentity.ImageOptions.SvgImageSize = new System.Drawing.Size(40, 40);
            this.btnIdentity.Location = new System.Drawing.Point(120, 766);
            this.btnIdentity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnIdentity.Name = "btnIdentity";
            this.btnIdentity.Size = new System.Drawing.Size(225, 51);
            this.btnIdentity.TabIndex = 5;
            this.btnIdentity.Text = "Xác minh danh tính";
            this.btnIdentity.Click += new System.EventHandler(this.btnIdentity_Click);
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.ExpandCollapseItem.VisibleInSearchMenu = false;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.barItemIP,
            this.barItemConnect,
            this.barStaticItem3,
            this.barItemPort,
            this.barItemVersion});
            this.ribbonControl1.Location = new System.Drawing.Point(2, 2);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ribbonControl1.MaxItemId = 10;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemRatingControl1,
            this.repositoryItemTimeEdit1,
            this.repositoryItemDateEdit1});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.MacOffice;
            this.ribbonControl1.Size = new System.Drawing.Size(704, 47);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // barItemIP
            // 
            this.barItemIP.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barItemIP.Caption = "IP: 192.168.10.1";
            this.barItemIP.Id = 5;
            this.barItemIP.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barItemIP.ImageOptions.Image")));
            this.barItemIP.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barItemIP.ImageOptions.LargeImage")));
            this.barItemIP.Name = "barItemIP";
            this.barItemIP.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barItemConnect
            // 
            this.barItemConnect.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barItemConnect.Caption = "Kết nối";
            this.barItemConnect.Id = 6;
            this.barItemConnect.Name = "barItemConnect";
            // 
            // barStaticItem3
            // 
            this.barStaticItem3.Caption = "Thông tin tòa nhà AI";
            this.barStaticItem3.Id = 7;
            this.barStaticItem3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barStaticItem3.ImageOptions.Image")));
            this.barStaticItem3.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barStaticItem3.ImageOptions.LargeImage")));
            this.barStaticItem3.Name = "barStaticItem3";
            this.barStaticItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barItemPort
            // 
            this.barItemPort.Id = 8;
            this.barItemPort.Name = "barItemPort";
            // 
            // barItemVersion
            // 
            this.barItemVersion.Id = 9;
            this.barItemVersion.Name = "barItemVersion";
            // 
            // repositoryItemRatingControl1
            // 
            this.repositoryItemRatingControl1.AutoHeight = false;
            this.repositoryItemRatingControl1.Name = "repositoryItemRatingControl1";
            // 
            // repositoryItemTimeEdit1
            // 
            this.repositoryItemTimeEdit1.AutoHeight = false;
            this.repositoryItemTimeEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemTimeEdit1.Name = "repositoryItemTimeEdit1";
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this.barItemConnect);
            this.ribbonStatusBar1.ItemLinks.Add(this.barItemIP);
            this.ribbonStatusBar1.ItemLinks.Add(this.barStaticItem3);
            this.ribbonStatusBar1.ItemLinks.Add(this.barItemPort);
            this.ribbonStatusBar1.ItemLinks.Add(this.barItemVersion);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(2, 831);
            this.ribbonStatusBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(704, 28);
            // 
            // webBrowserVideo
            // 
            this.webBrowserVideo.Location = new System.Drawing.Point(-16, 347);
            this.webBrowserVideo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.webBrowserVideo.MinimumSize = new System.Drawing.Size(17, 16);
            this.webBrowserVideo.Name = "webBrowserVideo";
            this.webBrowserVideo.Size = new System.Drawing.Size(740, 424);
            this.webBrowserVideo.TabIndex = 3;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 861);
            this.Controls.Add(this.panelControl1);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Home_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRatingControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.WebBrowser webBrowserVideo;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraEditors.SimpleButton btnIdentity;
        private DevExpress.XtraBars.BarStaticItem barItemIP;
        private DevExpress.XtraEditors.Repository.RepositoryItemRatingControl repositoryItemRatingControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraBars.BarStaticItem barItemConnect;
        private DevExpress.XtraBars.BarStaticItem barStaticItem3;
        private DevExpress.XtraBars.BarStaticItem barItemPort;
        private DevExpress.XtraBars.BarStaticItem barItemVersion;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManage;
        private DevExpress.XtraEditors.Camera.CameraControl cameraControlHome;
    }
}