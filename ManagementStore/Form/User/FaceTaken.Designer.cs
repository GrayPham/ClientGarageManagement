
namespace ManagementStore.Form.User
{
    partial class FaceTaken
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
            this.btnDone = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrev = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pictureFace = new System.Windows.Forms.PictureBox();
            this.showCountDown = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFace)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDone
            // 
            this.btnDone.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btnDone.Appearance.BorderColor = System.Drawing.Color.White;
            this.btnDone.Appearance.Font = new System.Drawing.Font("Tahoma", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDone.Appearance.Options.UseBackColor = true;
            this.btnDone.Appearance.Options.UseBorderColor = true;
            this.btnDone.Appearance.Options.UseFont = true;
            this.btnDone.Location = new System.Drawing.Point(233, 512);
            this.btnDone.Margin = new System.Windows.Forms.Padding(2);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(186, 75);
            this.btnDone.TabIndex = 43;
            this.btnDone.Text = "Complete";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Appearance.BackColor = System.Drawing.Color.Gray;
            this.btnPrev.Appearance.BorderColor = System.Drawing.Color.White;
            this.btnPrev.Appearance.Font = new System.Drawing.Font("Tahoma", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Appearance.Options.UseBackColor = true;
            this.btnPrev.Appearance.Options.UseBorderColor = true;
            this.btnPrev.Appearance.Options.UseFont = true;
            this.btnPrev.Location = new System.Drawing.Point(10, 512);
            this.btnPrev.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(176, 75);
            this.btnPrev.TabIndex = 44;
            this.btnPrev.Text = "Previous";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(11, 26);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(369, 27);
            this.labelControl1.TabIndex = 46;
            this.labelControl1.Text = "Vui lòng đưa khuôn mặt trước camera";
            // 
            // pictureFace
            // 
            this.pictureFace.Location = new System.Drawing.Point(11, 104);
            this.pictureFace.Margin = new System.Windows.Forms.Padding(2);
            this.pictureFace.Name = "pictureFace";
            this.pictureFace.Size = new System.Drawing.Size(408, 333);
            this.pictureFace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureFace.TabIndex = 47;
            this.pictureFace.TabStop = false;
            // 
            // showCountDown
            // 
            this.showCountDown.Appearance.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showCountDown.Appearance.Options.UseFont = true;
            this.showCountDown.Location = new System.Drawing.Point(11, 68);
            this.showCountDown.Name = "showCountDown";
            this.showCountDown.Size = new System.Drawing.Size(246, 21);
            this.showCountDown.TabIndex = 48;
            this.showCountDown.Text = "Ảnh sẽ được chụp sau 5 giây nữa";
            // 
            // FaceTaken
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.showCountDown);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.pictureFace);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FaceTaken";
            this.Size = new System.Drawing.Size(433, 602);
            this.Load += new System.EventHandler(this.FaceTaken_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureFace)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnDone;
        private DevExpress.XtraEditors.SimpleButton btnPrev;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.PictureBox pictureFace;
        private System.Windows.Forms.PictureBox countdownPictureBox;
        private DevExpress.XtraEditors.LabelControl showCountDown;
    }
}
