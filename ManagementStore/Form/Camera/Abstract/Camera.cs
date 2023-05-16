using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.Form.Camera.Abstract
{
    public abstract class Camera
    {
        protected VideoCapture _camera;
        protected abstract void  ProcessFrame(object sender, EventArgs arg);
        #region Test FPS
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        int currentFrame = 0;
        #endregion
    }
}
