using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Diagnostics;

namespace ManagementStore.Form.Camera
{
    public partial class FaceCameraControl : DevExpress.XtraEditors.XtraUserControl
    {
        #region Test FPS
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        int currentFrame = 0;
        #endregion
        #region Model detect
        // Declare Variables
        CascadeClassifier faceDetect;
        Image<Bgr, Byte> frame;
        VideoCapture _camera;
        Image<Gray, byte> result;
        public bool startDetect = false;
        string ipMac = "127.1.1.1";
        int countFace = 0;
        #endregion
        public FaceCameraControl(int cameraIndex)
        {
            InitializeComponent();
            if (cameraIndex >= 0)
            {
                _camera = new VideoCapture(cameraIndex);
                faceDetect = new CascadeClassifier(Application.StartupPath + "/Assets/HaarCascade/haarcascade_frontalface_default.xml");
                Application.Idle += ProcessFrame;
            }
            timer.Interval = 1000;
            timer.Tick += (sender, e) =>
            {
                int previousFrame = currentFrame;
                currentFrame = 0;

                // Calculate and display the number of FPS
                double fps = (double)(previousFrame) / 1;
                lbFPS.Text = string.Format("{0:0.00} FPS", fps);
            };
            // Start Timer
            timer.Start();

        }
        
        private void ProcessFrame(object sender, EventArgs arg)
        {
            frame = _camera.QueryFrame().ToImage<Bgr, Byte>();
            if (frame != null)
            {
                if(startDetect == true)
                {
                    var grayframe = frame.Convert<Gray, byte>();
                    var faces = faceDetect.DetectMultiScale(grayframe, 1.1, 10, Size.Empty); //the actual face detection happens here
                    
                    foreach (var face in faces)
                    {
                        countFace++;
                        frame.Draw(face, new Bgr(Color.BurlyWood), 3); //the detected face(s) is highlighted here using a box that is drawn around it/them
                    }
                }
                pBoxFace.Image = frame.ToBitmap();

            }
            
        }
        public bool startFaceDetect()
        {
            startDetect = true;
            return true;
        }
        public void endCameraFaceDetect()
        {
            startDetect = false;
            return;
        }
        public void endCameraFace(string ip)
        {
            if(ipMac == ip)
            {
                Application.Idle -= ProcessFrame;
                return;
            }    
        }
        
        private void pBoxFace_Paint(object sender, PaintEventArgs e)
        {
            currentFrame++;
        }
        #region Get Face Image Test
        public Image getFaceImage()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.Elapsed.TotalSeconds < 5)
            {
                if (pBoxFace.Image != null && countFace > 0)
                    break;
            }
            stopwatch.Stop();
            stopwatch.Reset();
            if (pBoxFace.Image != null)
                return pBoxFace.Image;
            return null;
        }
        #endregion
    }
}
