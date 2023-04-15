using DevExpress.Utils.Text;
using DevExpress.XtraBars;
using DevExpress.XtraCharts;
using Emgu.CV;
using Emgu.CV.CvEnum;
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
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace ManagementStore.Form
{
    public partial class DetectClient : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        VideoCapture capture = new VideoCapture();

        private YoloDetectServices detect = new YoloDetectServices();
        private bool captureInProgress = true;
        private VideoCapture camera1;
        private YoloModelDto dto;

        // Connect Socket 
        private SocketDetect encode = new SocketDetect();
        private ClientWebSocket webSocket = new ClientWebSocket();
        private Uri uri = new Uri(ModelConfig.socketFastAPI);
        //Test FPS 
        int currentFrame = 0;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        int indexCamIn1 = -1;

        private byte bitSent = 1;
        private int countTime = 0;
        private int waitTime = 0;
        public DetectClient()
        {
            InitializeComponent();
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

            // Connect FastAPI
            var cancellationTokenSource = new CancellationTokenSource();
            webSocket.ConnectAsync(uri, cancellationTokenSource.Token);
        }
        private async void ProcessFrame(object sender, EventArgs arg)
        {
            if (captureInProgress)
            {
                Image<Bgr, Byte> ImageFrame = camera1.QueryFrame().ToImage<Bgr, byte>();  //line 1
                Image image = ImageFrame.ToBitmap();
                //Thread.Sleep(500);
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
                            string mess = await encode.request(dto.getImageBase(),dto.yoloPredictions, webSocket);
                            textLPIn1.Text = mess;
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
                        textLPIn1.Text = "";
                    }
                    else if (dto.countListPrediction() <= 1)
                    {
                        countTime++;
                    }
                    pBoxIn1.Image = dto.getImageDetect();
                }
            }

        }

        private void DetectClient_Load(object sender, EventArgs e)
        {
            if (camera1 == null && indexCamIn1 != -1)
            {
                try
                {
                    camera1 = new VideoCapture(indexCamIn1);
                    cBoxIn1.SelectedIndex = 0;
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }
            //if (camera1 != null && indexCamIn1 != -1)
            //{


            //    if (captureInProgress)
            //    {  //if camera is getting frames then stop the capture and set button Text
            //       // "Start" for resuming capture
            //        Application.Idle -= ProcessFrame;
            //    }
            //    else
            //    {
            //        //if camera is NOT getting frames then start the capture and set button
            //        // Text to "Stop" for pausing capture
            //        Application.Idle += ProcessFrame;
            //    }
            //    captureInProgress = !captureInProgress;
            //}
        }
        private void loadCamera()
        {
            for (int i = 0; i < 7; i++)
            {
                capture = new VideoCapture(i);
                
                if (capture.IsOpened)
                {
                    cBoxIn1.Items.Add($"Camera {i}");
                }
            }
            if (cBoxIn1.Items.Count > 0)
            {
                indexCamIn1 = 0; // Get Camera 0;
                
            }
            cBoxIn1.Items.Add("None");
        }
        private void pBoxIn1_Paint(object sender, PaintEventArgs e)
        {
            currentFrame++;
        }

        private void cBoxIn1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBoxIn1.SelectedItem.ToString() != "None" )
            {
                indexCamIn1 = cBoxIn1.SelectedIndex;
                captureInProgress = true;
                camera1 = new VideoCapture(indexCamIn1);
                Application.Idle += ProcessFrame;
            }  
            else
            {

                captureInProgress = false;
                indexCamIn1 = -1;
                Application.Idle -= ProcessFrame;
                pBoxIn1.Image = null;
                camera1.Dispose();

            }
                
            textEdit3.Text = indexCamIn1.ToString();

        }
    }
}