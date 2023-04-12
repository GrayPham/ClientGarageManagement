
namespace ManagementStore.Form
{
    partial class DetectClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetectClient));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barHeaderItem1 = new DevExpress.XtraBars.BarHeaderItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup_System = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.barBtn_Profile = new DevExpress.XtraBars.BarButtonItem();
            this.panelControlHandle = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.simpleButtonReport = new DevExpress.XtraEditors.SimpleButton();
            this.panelControlMain = new DevExpress.XtraEditors.PanelControl();
            this.panelControlOut = new DevExpress.XtraEditors.PanelControl();
            this.cameraControlOut3 = new DevExpress.XtraEditors.Camera.CameraControl();
            this.cameraControlOut2 = new DevExpress.XtraEditors.Camera.CameraControl();
            this.cameraControlOut1 = new DevExpress.XtraEditors.Camera.CameraControl();
            this.panelControlInPut = new DevExpress.XtraEditors.PanelControl();
            this.cameraControlIn3 = new DevExpress.XtraEditors.Camera.CameraControl();
            this.cameraControlIn2 = new DevExpress.XtraEditors.Camera.CameraControl();
            this.cameraControlIn1 = new DevExpress.XtraEditors.Camera.CameraControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHandle)).BeginInit();
            this.panelControlHandle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlOut)).BeginInit();
            this.panelControlOut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlInPut)).BeginInit();
            this.panelControlInPut.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.barHeaderItem1});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 2;
            this.ribbon.Name = "ribbon";
            this.ribbon.PageHeaderItemLinks.Add(this.barHeaderItem1);
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1384, 181);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // barHeaderItem1
            // 
            this.barHeaderItem1.Caption = "04/12/2023";
            this.barHeaderItem1.Id = 1;
            this.barHeaderItem1.Name = "barHeaderItem1";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup_System});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Detect Page";
            // 
            // ribbonPageGroup_System
            // 
            this.ribbonPageGroup_System.Name = "ribbonPageGroup_System";
            this.ribbonPageGroup_System.Text = "ribbonPageGroup1";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 735);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1384, 29);
            // 
            // barBtn_Profile
            // 
            this.barBtn_Profile.Caption = "Profile";
            this.barBtn_Profile.Hint = "Edit profile";
            this.barBtn_Profile.Id = 2;
            this.barBtn_Profile.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtn_Profile.ImageOptions.SvgImage")));
            this.barBtn_Profile.Name = "barBtn_Profile";
            // 
            // panelControlHandle
            // 
            this.panelControlHandle.Controls.Add(this.labelControl2);
            this.panelControlHandle.Controls.Add(this.textEdit3);
            this.panelControlHandle.Controls.Add(this.labelControl1);
            this.panelControlHandle.Controls.Add(this.textEdit1);
            this.panelControlHandle.Controls.Add(this.simpleButtonReport);
            this.panelControlHandle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlHandle.Location = new System.Drawing.Point(0, 666);
            this.panelControlHandle.Name = "panelControlHandle";
            this.panelControlHandle.Size = new System.Drawing.Size(1384, 69);
            this.panelControlHandle.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(483, 22);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(89, 33);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "LP OUT";
            // 
            // textEdit3
            // 
            this.textEdit3.Location = new System.Drawing.Point(593, 19);
            this.textEdit3.MenuManager = this.ribbon;
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit3.Properties.Appearance.Options.UseFont = true;
            this.textEdit3.Size = new System.Drawing.Size(325, 34);
            this.textEdit3.TabIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(38, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 33);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "LP IN";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(157, 19);
            this.textEdit1.MenuManager = this.ribbon;
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Size = new System.Drawing.Size(304, 34);
            this.textEdit1.TabIndex = 1;
            // 
            // simpleButtonReport
            // 
            this.simpleButtonReport.Appearance.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButtonReport.Appearance.Options.UseFont = true;
            this.simpleButtonReport.Location = new System.Drawing.Point(1215, 18);
            this.simpleButtonReport.Name = "simpleButtonReport";
            this.simpleButtonReport.Size = new System.Drawing.Size(122, 45);
            this.simpleButtonReport.TabIndex = 0;
            this.simpleButtonReport.Text = "Report";
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.panelControlOut);
            this.panelControlMain.Controls.Add(this.panelControlInPut);
            this.panelControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlMain.Location = new System.Drawing.Point(0, 181);
            this.panelControlMain.Name = "panelControlMain";
            this.panelControlMain.Size = new System.Drawing.Size(1384, 485);
            this.panelControlMain.TabIndex = 3;
            // 
            // panelControlOut
            // 
            this.panelControlOut.Controls.Add(this.cameraControlOut3);
            this.panelControlOut.Controls.Add(this.cameraControlOut2);
            this.panelControlOut.Controls.Add(this.cameraControlOut1);
            this.panelControlOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlOut.Location = new System.Drawing.Point(2, 245);
            this.panelControlOut.Name = "panelControlOut";
            this.panelControlOut.Size = new System.Drawing.Size(1380, 238);
            this.panelControlOut.TabIndex = 1;
            // 
            // cameraControlOut3
            // 
            this.cameraControlOut3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cameraControlOut3.Location = new System.Drawing.Point(922, 2);
            this.cameraControlOut3.Name = "cameraControlOut3";
            this.cameraControlOut3.Size = new System.Drawing.Size(413, 236);
            this.cameraControlOut3.TabIndex = 3;
            this.cameraControlOut3.Text = "cameraControlOut3";
            // 
            // cameraControlOut2
            // 
            this.cameraControlOut2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cameraControlOut2.Location = new System.Drawing.Point(465, 1);
            this.cameraControlOut2.Name = "cameraControlOut2";
            this.cameraControlOut2.Size = new System.Drawing.Size(451, 237);
            this.cameraControlOut2.TabIndex = 2;
            this.cameraControlOut2.Text = "cameraControlOut2";
            // 
            // cameraControlOut1
            // 
            this.cameraControlOut1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cameraControlOut1.Location = new System.Drawing.Point(36, 1);
            this.cameraControlOut1.Name = "cameraControlOut1";
            this.cameraControlOut1.Size = new System.Drawing.Size(423, 237);
            this.cameraControlOut1.TabIndex = 1;
            this.cameraControlOut1.Text = "cameraControlOut1";
            // 
            // panelControlInPut
            // 
            this.panelControlInPut.Controls.Add(this.cameraControlIn3);
            this.panelControlInPut.Controls.Add(this.cameraControlIn2);
            this.panelControlInPut.Controls.Add(this.cameraControlIn1);
            this.panelControlInPut.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlInPut.Location = new System.Drawing.Point(2, 2);
            this.panelControlInPut.Name = "panelControlInPut";
            this.panelControlInPut.Size = new System.Drawing.Size(1380, 243);
            this.panelControlInPut.TabIndex = 0;
            // 
            // cameraControlIn3
            // 
            this.cameraControlIn3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cameraControlIn3.Location = new System.Drawing.Point(922, -2);
            this.cameraControlIn3.Name = "cameraControlIn3";
            this.cameraControlIn3.Size = new System.Drawing.Size(413, 239);
            this.cameraControlIn3.TabIndex = 2;
            this.cameraControlIn3.Text = "cameraControlIn3";
            // 
            // cameraControlIn2
            // 
            this.cameraControlIn2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cameraControlIn2.Location = new System.Drawing.Point(465, -2);
            this.cameraControlIn2.Name = "cameraControlIn2";
            this.cameraControlIn2.Size = new System.Drawing.Size(451, 239);
            this.cameraControlIn2.TabIndex = 1;
            this.cameraControlIn2.Text = "cameraControlIn2";
            // 
            // cameraControlIn1
            // 
            this.cameraControlIn1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cameraControlIn1.Location = new System.Drawing.Point(36, 0);
            this.cameraControlIn1.Name = "cameraControlIn1";
            this.cameraControlIn1.Size = new System.Drawing.Size(423, 237);
            this.cameraControlIn1.TabIndex = 0;
            this.cameraControlIn1.Text = "cameraControlIn1";
            // 
            // DetectClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 764);
            this.Controls.Add(this.panelControlMain);
            this.Controls.Add(this.panelControlHandle);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "DetectClient";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "DetectClient";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHandle)).EndInit();
            this.panelControlHandle.ResumeLayout(false);
            this.panelControlHandle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlOut)).EndInit();
            this.panelControlOut.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlInPut)).EndInit();
            this.panelControlInPut.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup_System;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem barBtn_Profile;
        private DevExpress.XtraEditors.PanelControl panelControlHandle;
        private DevExpress.XtraEditors.PanelControl panelControlMain;
        private DevExpress.XtraEditors.PanelControl panelControlInPut;
        private DevExpress.XtraEditors.PanelControl panelControlOut;
        private DevExpress.XtraEditors.Camera.CameraControl cameraControlOut3;
        private DevExpress.XtraEditors.Camera.CameraControl cameraControlOut2;
        private DevExpress.XtraEditors.Camera.CameraControl cameraControlOut1;
        private DevExpress.XtraEditors.Camera.CameraControl cameraControlIn3;
        private DevExpress.XtraEditors.Camera.CameraControl cameraControlIn2;
        private DevExpress.XtraEditors.Camera.CameraControl cameraControlIn1;
        private DevExpress.XtraBars.BarHeaderItem barHeaderItem1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonReport;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEdit3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}