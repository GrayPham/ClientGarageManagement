
namespace ManagementStore.Form.Camera
{
    partial class FaceCameraControl
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
            this.panelFace = new System.Windows.Forms.Panel();
            this.pictureBoxSetting = new System.Windows.Forms.PictureBox();
            this.pBoxFace = new System.Windows.Forms.PictureBox();
            this.lbFPS = new System.Windows.Forms.Label();
            this.panelFace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxFace)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFace
            // 
            this.panelFace.Controls.Add(this.pictureBoxSetting);
            this.panelFace.Controls.Add(this.pBoxFace);
            this.panelFace.Location = new System.Drawing.Point(0, 3);
            this.panelFace.Name = "panelFace";
            this.panelFace.Size = new System.Drawing.Size(423, 232);
            this.panelFace.TabIndex = 0;
            // 
            // pictureBoxSetting
            // 
            this.pictureBoxSetting.Image = global::ManagementStore.Properties.Resources.technology_32x32;
            this.pictureBoxSetting.Location = new System.Drawing.Point(375, 3);
            this.pictureBoxSetting.Name = "pictureBoxSetting";
            this.pictureBoxSetting.Size = new System.Drawing.Size(45, 45);
            this.pictureBoxSetting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxSetting.TabIndex = 4;
            this.pictureBoxSetting.TabStop = false;
            // 
            // pBoxFace
            // 
            this.pBoxFace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBoxFace.Location = new System.Drawing.Point(0, 0);
            this.pBoxFace.Name = "pBoxFace";
            this.pBoxFace.Size = new System.Drawing.Size(423, 232);
            this.pBoxFace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBoxFace.TabIndex = 5;
            this.pBoxFace.TabStop = false;
            this.pBoxFace.Paint += new System.Windows.Forms.PaintEventHandler(this.pBoxFace_Paint);
            // 
            // lbFPS
            // 
            this.lbFPS.AutoSize = true;
            this.lbFPS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFPS.Location = new System.Drawing.Point(3, 296);
            this.lbFPS.Name = "lbFPS";
            this.lbFPS.Size = new System.Drawing.Size(32, 18);
            this.lbFPS.TabIndex = 3;
            this.lbFPS.Text = "FPS";
            // 
            // FaceCameraControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbFPS);
            this.Controls.Add(this.panelFace);
            this.Name = "FaceCameraControl";
            this.Size = new System.Drawing.Size(423, 326);
            this.panelFace.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxFace)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelFace;
        private System.Windows.Forms.Label lbFPS;
        private System.Windows.Forms.PictureBox pictureBoxSetting;
        private System.Windows.Forms.PictureBox pBoxFace;
    }
}
