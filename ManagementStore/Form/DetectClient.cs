using DevExpress.XtraBars;
using Emgu.CV;
using Emgu.CV.Structure;
using ManagementStore.DTO;
using ManagementStore.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementStore.Form
{
    public partial class DetectClient : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        private YoloDetectServices detect = new YoloDetectServices();
        private bool captureInProgress;
        private VideoCapture camera1;
        private YoloModelDto dto;
        public DetectClient()
        {
            InitializeComponent();
        }
        private async void ProcessFrame(object sender, EventArgs arg)
        {
            Image<Bgr, Byte> ImageFrame = camera1.QueryFrame().ToImage<Bgr, byte>();  //line 1
            Image image = ImageFrame.ToBitmap();
            //Thread.Sleep(500);
            dto = detect.detect(image);
            if (dto != null)
            {
                pBoxIn1.Image = dto.getImageDetect();
            }
        }

        private void DetectClient_Load(object sender, EventArgs e)
        {
            if (camera1 == null)
            {
                try
                {
                    camera1 = new VideoCapture(0);
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }

            if (camera1 != null)
            {


                if (captureInProgress)
                {  //if camera is getting frames then stop the capture and set button Text
                   // "Start" for resuming capture
                    Application.Idle -= ProcessFrame;
                }
                else
                {
                    //if camera is NOT getting frames then start the capture and set button
                    // Text to "Stop" for pausing capture
                    Application.Idle += ProcessFrame;
                }
                captureInProgress = !captureInProgress;
            }
        }
    }
}