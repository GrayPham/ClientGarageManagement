
namespace ManagementStore.Form.Camera
{
    partial class PictureControl
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxSetting = new System.Windows.Forms.PictureBox();
            this.pictureBoxCamera = new System.Windows.Forms.PictureBox();
            this.lbFPS = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textEditLP = new DevExpress.XtraEditors.TextEdit();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBoxSetting);
            this.panel1.Controls.Add(this.pictureBoxCamera);
            this.panel1.Location = new System.Drawing.Point(0, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(423, 232);
            this.panel1.TabIndex = 1;
            // 
            // pictureBoxSetting
            // 
            this.pictureBoxSetting.Image = global::ManagementStore.Properties.Resources.technology_32x32;
            this.pictureBoxSetting.Location = new System.Drawing.Point(375, 3);
            this.pictureBoxSetting.Name = "pictureBoxSetting";
            this.pictureBoxSetting.Size = new System.Drawing.Size(45, 45);
            this.pictureBoxSetting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxSetting.TabIndex = 1;
            this.pictureBoxSetting.TabStop = false;
            this.pictureBoxSetting.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxSetting_MouseClick);
            // 
            // pictureBoxCamera
            // 
            this.pictureBoxCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxCamera.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxCamera.Name = "pictureBoxCamera";
            this.pictureBoxCamera.Size = new System.Drawing.Size(423, 232);
            this.pictureBoxCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCamera.TabIndex = 0;
            this.pictureBoxCamera.TabStop = false;
            this.pictureBoxCamera.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxCamera_Paint);
            // 
            // lbFPS
            // 
            this.lbFPS.AutoSize = true;
            this.lbFPS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFPS.Location = new System.Drawing.Point(3, 251);
            this.lbFPS.Name = "lbFPS";
            this.lbFPS.Size = new System.Drawing.Size(32, 18);
            this.lbFPS.TabIndex = 2;
            this.lbFPS.Text = "FPS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "LP IN:";
            // 
            // textEditLP
            // 
            this.textEditLP.Location = new System.Drawing.Point(77, 298);
            this.textEditLP.Name = "textEditLP";
            this.textEditLP.Size = new System.Drawing.Size(218, 22);
            this.textEditLP.TabIndex = 6;
            // 
            // PictureControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textEditLP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbFPS);
            this.Controls.Add(this.panel1);
            this.Name = "PictureControl";
            this.Size = new System.Drawing.Size(423, 326);
            this.Load += new System.EventHandler(this.PictureControl_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxCamera;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbFPS;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit textEditLP;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private System.Windows.Forms.PictureBox pictureBoxSetting;
    }
}
