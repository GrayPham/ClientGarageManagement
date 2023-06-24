
namespace ManagementStore.Form.Notify
{
    partial class ShowImageTaken
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
            this.btnTakeAgain = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.pictureBoxTaken = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTaken)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTakeAgain
            // 
            this.btnTakeAgain.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.btnTakeAgain.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTakeAgain.Appearance.Options.UseBackColor = true;
            this.btnTakeAgain.Appearance.Options.UseFont = true;
            this.btnTakeAgain.Location = new System.Drawing.Point(14, 474);
            this.btnTakeAgain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTakeAgain.Name = "btnTakeAgain";
            this.btnTakeAgain.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnTakeAgain.Size = new System.Drawing.Size(156, 70);
            this.btnTakeAgain.TabIndex = 1;
            this.btnTakeAgain.Text = "Take Again";
            // 
            // btnOK
            // 
            this.btnOK.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Appearance.Options.UseBackColor = true;
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Location = new System.Drawing.Point(334, 474);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnOK.Size = new System.Drawing.Size(156, 70);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            // 
            // pictureBoxTaken
            // 
            this.pictureBoxTaken.Location = new System.Drawing.Point(14, 15);
            this.pictureBoxTaken.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBoxTaken.Name = "pictureBoxTaken";
            this.pictureBoxTaken.Size = new System.Drawing.Size(476, 410);
            this.pictureBoxTaken.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTaken.TabIndex = 0;
            this.pictureBoxTaken.TabStop = false;
            // 
            // ShowImageTaken
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 576);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnTakeAgain);
            this.Controls.Add(this.pictureBoxTaken);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowImageTaken";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Photo Taken";
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTaken)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public DevExpress.XtraEditors.SimpleButton btnTakeAgain;
        public DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        public System.Windows.Forms.PictureBox pictureBoxTaken;
    }
}