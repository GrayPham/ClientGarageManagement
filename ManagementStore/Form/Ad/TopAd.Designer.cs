
namespace ManagementStore.Form.Ad
{
    partial class TopAd
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.webBrowserVideo = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowserVideo
            // 
            this.webBrowserVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserVideo.Location = new System.Drawing.Point(0, 0);
            this.webBrowserVideo.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserVideo.Name = "webBrowserVideo";
            this.webBrowserVideo.Size = new System.Drawing.Size(706, 370);
            this.webBrowserVideo.TabIndex = 0;
            // 
            // TopAd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.webBrowserVideo);
            this.Name = "TopAd";
            this.Size = new System.Drawing.Size(706, 370);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserVideo;
    }
}
