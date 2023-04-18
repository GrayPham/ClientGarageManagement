using DevExpress.XtraEditors;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;
using ManagementStore.Common;
using ManagementStore.DTO;
using ManagementStore.Services;
using Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementStore.Form.Camera
{
    public partial class PictureControl : DevExpress.XtraEditors.XtraUserControl
    {
        VideoCapture capture;
        private VideoCapture camera1;
        private SocketDetect encode;
        private YoloDetectServices detect = new YoloDetectServices(CudaInvoke.HasCuda);
        private YoloModelDto dto;
        private bool captureInProgress = false;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private byte bitSent = 1;
        int cameraindex = -1;

        private int countTime = 0;
        private int waitTime = 0;
        //Test FPS 
        int currentFrame = 0;
        public PictureControl(int index, SocketDetect socket )
        {
            InitializeComponent();
            this.encode = socket;
            cameraindex = index;
            loadCamera();
            timer.Interval = 1000;
            timer.Tick += (sender, e) =>
            {
                // Lưu trữ số khung hình hiện tại
                int previousFrame = currentFrame;

                // Cập nhật số khung hình hiện tại
                currentFrame = 0;

                // Tính toán và hiển thị số FPS
                double fps = (double)(previousFrame) / 1;
                lbFPS.Text = string.Format("{0:0.00} FPS", fps);
            };

            // Bắt đầu Timer
            timer.Start();

        }

        private void PictureControl_Load(object sender, EventArgs e)
        {
            if (capture == null && cameraindex != -1)
            {
                try
                {
                    capture = new VideoCapture(cameraindex);
                    cBoxIn1.SelectedIndex = cameraindex;
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }
        }
        private async void ProcessFrame(object sender, EventArgs arg)
        {
            if (captureInProgress)
            {
                double brightness = trackBarBrightness.Value;
                // brightness
                double contrast = Math.Pow((100.0 + brightness) / 100.0, 2);
                Image<Bgr, Byte> ImageFrame =  capture.QueryFrame().ToImage<Bgr, byte>();  //line 1
                CvInvoke.ConvertScaleAbs(ImageFrame, ImageFrame, contrast, brightness);
                Image image = ImageFrame.ToBitmap();
                dto = detect.detect(image);
                if (dto != null)
                {
                    // Check Velhike input output
                    if (bitSent == 1 && dto.countListPrediction() > 7)
                    {
                        // Check Result if Error
                        if (waitTime == 10)
                        {
                            bitSent = 0;
                            // return LP
                            string mess = await encode.request(dto.getImageBase(), dto.yoloPredictions);
                            textEditLP.Text = mess;
                            
                            countTime = 0;
                            waitTime = 0;
                        }
                        else
                        {
                            waitTime++;
                        }
                    }
                    // Kiem tra sau 2.5s không nhận được phát hiện xe trong client --> set bitSent = 1
                    if (countTime == 10)
                    {
                        bitSent = 1;
                        textEditLP.Text = "";
                    }
                    else if (dto.countListPrediction() <= 1)
                    {
                        countTime++;
                    }
                    pictureBoxCamera.Image = dto.getImageDetect();
                    ModelConfig.socketOpen = encode.SocketStatus();


                }
            }
        }
        private void loadCamera()
        {
            for (int i = 0; i < 7; i++)
            {
                camera1 = new VideoCapture(i);

                if (camera1.IsOpened)
                {
                    cBoxIn1.Items.Add($"Camera {i}");
                }
            }
            cBoxIn1.Items.Add("None");
        }
        private void pictureBoxCamera_Paint(object sender, PaintEventArgs e)
        {
            currentFrame++;
        }

        private void cBoxIn1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBoxIn1.SelectedItem.ToString() != "None")
            {
                cameraindex = cBoxIn1.SelectedIndex;
                captureInProgress = true;
                //capture = new VideoCapture(cameraindex); // BUG
                Application.Idle += ProcessFrame;
            }
            else
            {
                captureInProgress = false;
                cameraindex = -1;
                Application.Idle -= ProcessFrame;
                pictureBoxCamera.Image = null;
                capture.Dispose();
            }
        }
    }
}
