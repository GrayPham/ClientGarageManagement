using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Windows.Forms;

namespace ManagementStore.Form.Camera
{
    public partial class HomeCamera : DevExpress.XtraEditors.XtraUserControl
    {
        private VideoCapture capture;
        public HomeCamera()
        {
            InitializeComponent();
            capture = new VideoCapture();
            Application.Idle += Capture_Home;
            capture.Start(); 
        }

        private void HomeCamera_Click(object sender, EventArgs e)
        {

        }
        private void Capture_Home(object send, EventArgs e)
        {

            if (capture != null && capture.Ptr != IntPtr.Zero)
            {
                using (Mat frame = capture.QueryFrame())
                {
                    try
                    {
                        Image<Bgr, byte> image = frame.ToImage<Bgr, byte>();
                        pictureBoxHome.Image = image.ToBitmap();
                    }
                    catch
                    {
                        
                    }

                }
            }
        }
    }
}
