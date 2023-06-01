
namespace ManagementStore.Form.User
{
    partial class CitizenshipIDCapture
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
            this.pictureCCCD = new System.Windows.Forms.PictureBox();
            this.showCountDown = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnPrev = new DevExpress.XtraEditors.SimpleButton();
            this.btnDone = new DevExpress.XtraEditors.SimpleButton();
            this.labelResult = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCCCD)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureCCCD
            // 
            this.pictureCCCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureCCCD.Location = new System.Drawing.Point(3, 128);
            this.pictureCCCD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureCCCD.Name = "pictureCCCD";
            this.pictureCCCD.Size = new System.Drawing.Size(586, 410);
            this.pictureCCCD.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureCCCD.TabIndex = 48;
            this.pictureCCCD.TabStop = false;
            // 
            // showCountDown
            // 
            this.showCountDown.Appearance.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showCountDown.Appearance.Options.UseFont = true;
            this.showCountDown.Location = new System.Drawing.Point(18, 88);
            this.showCountDown.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.showCountDown.Name = "showCountDown";
            this.showCountDown.Size = new System.Drawing.Size(325, 27);
            this.showCountDown.TabIndex = 50;
            this.showCountDown.Text = "Ảnh sẽ được chụp sau 5 giây nữa";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(18, 36);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(445, 34);
            this.labelControl1.TabIndex = 49;
            this.labelControl1.Text = "Ảnh Căn Cước Công Dân Mặt Trước";
            // 
            // btnPrev
            // 
            this.btnPrev.Appearance.BackColor = System.Drawing.Color.Gray;
            this.btnPrev.Appearance.BorderColor = System.Drawing.Color.White;
            this.btnPrev.Appearance.Font = new System.Drawing.Font("Tahoma", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Appearance.Options.UseBackColor = true;
            this.btnPrev.Appearance.Options.UseBorderColor = true;
            this.btnPrev.Appearance.Options.UseFont = true;
            this.btnPrev.Location = new System.Drawing.Point(1, 630);
            this.btnPrev.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(260, 92);
            this.btnPrev.TabIndex = 52;
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
            this.btnDone.Enabled = false;
            this.btnDone.Location = new System.Drawing.Point(324, 630);
            this.btnDone.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(265, 92);
            this.btnDone.TabIndex = 51;
            this.btnDone.Text = "Complete";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelResult.Location = new System.Drawing.Point(3, 550);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(227, 55);
            this.labelResult.TabIndex = 53;
            this.labelResult.Text = "RESULT:";
            // 
            // CitizenshipIDCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.showCountDown);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.pictureCCCD);
            this.Name = "CitizenshipIDCapture";
            this.Size = new System.Drawing.Size(600, 740);
            ((System.ComponentModel.ISupportInitialize)(this.pictureCCCD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureCCCD;
        private DevExpress.XtraEditors.LabelControl showCountDown;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnPrev;
        private DevExpress.XtraEditors.SimpleButton btnDone;
        private System.Windows.Forms.Label labelResult;
    }
}
