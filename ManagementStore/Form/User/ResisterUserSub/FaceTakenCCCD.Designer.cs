
namespace ManagementStore.Form.User.ResisterUserSub
{
    partial class FaceTakenCCCD
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
            this.showCountDown = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnPrev = new DevExpress.XtraEditors.SimpleButton();
            this.btnDone = new DevExpress.XtraEditors.SimpleButton();
            this.pictureFace = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFace)).BeginInit();
            this.SuspendLayout();
            // 
            // showCountDown
            // 
            this.showCountDown.Appearance.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showCountDown.Appearance.Options.UseFont = true;
            this.showCountDown.Location = new System.Drawing.Point(19, 77);
            this.showCountDown.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.showCountDown.Name = "showCountDown";
            this.showCountDown.Size = new System.Drawing.Size(325, 27);
            this.showCountDown.TabIndex = 53;
            this.showCountDown.Text = "Ảnh sẽ được chụp sau 5 giây nữa";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(19, 25);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(475, 34);
            this.labelControl1.TabIndex = 51;
            this.labelControl1.Text = "Vui lòng đưa khuôn mặt trước camera";
            // 
            // btnPrev
            // 
            this.btnPrev.Appearance.BackColor = System.Drawing.Color.Gray;
            this.btnPrev.Appearance.BorderColor = System.Drawing.Color.White;
            this.btnPrev.Appearance.Font = new System.Drawing.Font("Tahoma", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Appearance.Options.UseBackColor = true;
            this.btnPrev.Appearance.Options.UseBorderColor = true;
            this.btnPrev.Appearance.Options.UseFont = true;
            this.btnPrev.Location = new System.Drawing.Point(17, 623);
            this.btnPrev.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(269, 92);
            this.btnPrev.TabIndex = 50;
            this.btnPrev.Text = "Previous";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnDone
            // 
            this.btnDone.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btnDone.Appearance.BorderColor = System.Drawing.Color.White;
            this.btnDone.Appearance.Font = new System.Drawing.Font("Tahoma", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDone.Appearance.Options.UseBackColor = true;
            this.btnDone.Appearance.Options.UseBorderColor = true;
            this.btnDone.Appearance.Options.UseFont = true;
            this.btnDone.Location = new System.Drawing.Point(358, 623);
            this.btnDone.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(283, 92);
            this.btnDone.TabIndex = 49;
            this.btnDone.Text = "Complete";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // pictureFace
            // 
            this.pictureFace.Location = new System.Drawing.Point(19, 121);
            this.pictureFace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureFace.Name = "pictureFace";
            this.pictureFace.Size = new System.Drawing.Size(622, 410);
            this.pictureFace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureFace.TabIndex = 52;
            this.pictureFace.TabStop = false;
            // 
            // FaceTakenCCCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.showCountDown);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.pictureFace);
            this.Name = "FaceTakenCCCD";
            this.Size = new System.Drawing.Size(659, 741);
            ((System.ComponentModel.ISupportInitialize)(this.pictureFace)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl showCountDown;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnPrev;
        private DevExpress.XtraEditors.SimpleButton btnDone;
        private System.Windows.Forms.PictureBox pictureFace;
    }
}
