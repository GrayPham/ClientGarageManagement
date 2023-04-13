using ManagementStore.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Camera;
using System.ComponentModel;
using DevExpress.Data.Camera.Interfaces;
using System.Diagnostics;

namespace ManagementStore.Security
{
    [ToolboxItem(true)]
    public class CameraManagement : CameraControl, ICameraDeviceClient
    {
        public CameraManagement()
        {
            
        }
        void ICameraDeviceClient.OnNewFrame()
        {
            if (!this.IsHandleCreated) return;
            this.BeginInvoke(new Action(() => { this.Invalidate(); }));

            Debug.WriteLine("New Frame");
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
}
