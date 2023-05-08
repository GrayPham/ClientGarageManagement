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
        List<string> dataCamera = new List<string>();
        private int countTime = 0;
        private int waitTime = 0;
        //Test FPS 
        int currentFrame = 0;

        // Brightness
        double bright = 0;
        double dark = 0;
        public PictureControl(int index, SocketDetect socket )
        {
            InitializeComponent();
            this.encode = socket;
            loadCamera();
            cameraindex = index;
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
            if (capture == null)
            {
                try
                {
                    capture = new VideoCapture(cameraindex);
                    captureInProgress = true;
                    Application.Idle += ProcessFrame;
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
                // brightness
                double contrast = Math.Pow((100.0 + bright) / 100.0, 2);
                Image<Bgr, Byte> ImageFrame =  capture.QueryFrame().ToImage<Bgr, byte>();  //line 1
                CvInvoke.ConvertScaleAbs(ImageFrame, ImageFrame, contrast, bright);
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
                    dataCamera.Add($"Camera {i}");
                    
                }
            }
            dataCamera.Add("None");
        }
        private void pictureBoxCamera_Paint(object sender, PaintEventArgs e)
        {
            currentFrame++;
        }

  
        #region SubForm Edit
        private Point clickPoint;
        private void pictureBoxSetting_MouseClick(object sender, MouseEventArgs e)
        {
            clickPoint = e.Location;
            SubFormCamera subFormCamera = new SubFormCamera(dataCamera, cameraindex);
            subFormCamera.Location = new Point(this.Location.X + clickPoint.X, this.Location.Y + clickPoint.Y);
            subFormCamera.BringToFront();
            subFormCamera.ComboBoxIndexChanged += SubForm_ComboBoxIndexChanged;
            subFormCamera.ValueDarkChanged += SubForm_ValueDarkChanged;
            subFormCamera.ValueBrightChanged += SubForm_ValueBrightChanged;
            this.Enabled = false;
            subFormCamera.ShowDialog();
            this.Enabled = true;
        }
        private void SubForm_ComboBoxIndexChanged(object sender, EventArgs e)
        {
            // Cập nhật giá trị của comboBoxIndex khi giá trị trên form con thay đổi
            cameraindex = ((SubFormCamera)sender).ComboBoxIndex;
            // Cập nhật các thành phần trên form cha sử dụng giá trị của comboBoxIndex
            if (dataCamera[cameraindex] == "None")
            {
                captureInProgress = false;
                cameraindex = -1;

                Application.Idle -= ProcessFrame;
                pictureBoxCamera.Image = null;
                capture.Dispose();
            }
            else
            {
                capture = new VideoCapture(cameraindex);
                captureInProgress = true;
                //capture = new VideoCapture(cameraindex); // BUG
                Application.Idle += ProcessFrame;
            }
        }
        private void SubForm_ValueDarkChanged(int value)
        {
            dark = value;
        }
        private void SubForm_ValueBrightChanged(int value)
        {
            bright = value;
        }
        #endregion
    }
}
