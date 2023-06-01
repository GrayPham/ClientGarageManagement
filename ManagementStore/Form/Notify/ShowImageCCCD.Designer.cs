
namespace ManagementStore.Form.Notify
{
    partial class ShowImageCCCD
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
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnTakeAgain = new DevExpress.XtraEditors.SimpleButton();
            this.pictureBoxTaken = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTaken)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Appearance.Options.UseBackColor = true;
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Location = new System.Drawing.Point(335, 483);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(6);
            this.btnOK.Size = new System.Drawing.Size(156, 70);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            // 
            // btnTakeAgain
            // 
            this.btnTakeAgain.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.btnTakeAgain.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTakeAgain.Appearance.Options.UseBackColor = true;
            this.btnTakeAgain.Appearance.Options.UseFont = true;
            this.btnTakeAgain.Location = new System.Drawing.Point(15, 483);
            this.btnTakeAgain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTakeAgain.Name = "btnTakeAgain";
            this.btnTakeAgain.Padding = new System.Windows.Forms.Padding(6);
            this.btnTakeAgain.Size = new System.Drawing.Size(156, 70);
            this.btnTakeAgain.TabIndex = 4;
            this.btnTakeAgain.Text = "Take Again";
            // 
            // pictureBoxTaken
            // 
            this.pictureBoxTaken.Location = new System.Drawing.Point(15, 24);
            this.pictureBoxTaken.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBoxTaken.Name = "pictureBoxTaken";
            this.pictureBoxTaken.Size = new System.Drawing.Size(476, 410);
            this.pictureBoxTaken.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTaken.TabIndex = 3;
            this.pictureBoxTaken.TabStop = false;
            // 
            // ShowImageCCCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 576);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnTakeAgain);
            this.Controls.Add(this.pictureBoxTaken);
            this.Name = "ShowImageCCCD";
            this.Text = "Image CCCD";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTaken)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.SimpleButton btnOK;
        public DevExpress.XtraEditors.SimpleButton btnTakeAgain;
        public System.Windows.Forms.PictureBox pictureBoxTaken;
    }
}