using DevExpress.XtraEditors;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using ManagementStore.Common;
using ManagementStore.DTO;
using ManagementStore.Extensions;
using ManagementStore.Form.Notify;
using ManagementStore.Form.User.ResisterUserSub;
using ManagementStore.Model.ML;
using ManagementStore.Model.Static;
using Parking.App.Common.ApiMethod;
using Parking.App.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementStore.Form.User
{
    public partial class CitizenshipIDCapture : System.Windows.Forms.UserControl
    {
        private VideoCapture capture;
        private int countdownValue;
        private Timer timer;
        //ShowImageCCCD showImage;
        List<DetectionResult> detectionResults;
        ObjectDetectionSSD ssd;

        private string fileNameAudio;
        private const string badImage = "Bad Image";
        private const string badDetect = "Not Detect ID";
        private const string STATUS_CCCD_5 = "Is Unknown";
        private const string STATUS_CCCD_3 = "Not Detect ID";
        private async void CitizenshipIDCapture_Load(object sender, EventArgs e)
        {
            fileNameAudio = await AudioConstants.GetListSound(AudioConstants.AuthenticationCCCD);
            if (fileNameAudio != null && fileNameAudio != "")
            {
                Helpers.PlaySound(@"Assets\Audio\" + fileNameAudio + ".wav");
            }
        }
        public CitizenshipIDCapture()
        {
            InitializeComponent();
            string path = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);

            ssd = new ObjectDetectionSSD(ModelConfig.dataFolderPath + "/mb2-ssd-lite-predict.onnx");
            // Initialize the camera capture
            capture = new VideoCapture(0);
            
            // ShowImage = new ShowImageCCCD();
            Application.Idle += Capture_ImageGrabbed;
            //capture.Start();
            //Set the initial countdown value and Timer interval
            countdownValue = 5;
            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_TickAsync;

            // Start the Timer
            timer.Start();
        }
        private async void  Timer_TickAsync(object sender, EventArgs e)
        {
            countdownValue--;
            showCountDown.Text = $"The photo will be taken in {countdownValue.ToString()} second.";
            // When the countdown reaches 0, stop the Timer and capture the picture
            if (countdownValue == 0)
            {
                var timer = (Timer)sender;
                timer.Stop();
                //Application.Idle -= Capture_ImageGrabbed;
                //capture.Stop();
                if (detectionResults.Count() > 2)
                {
                    var result = XtraMessageBox.Show("Are you sure to use this image?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (DialogResult.No == result)
                    {
                        countdownValue = 5;
                        timer.Start();
                        //Application.Idle += Capture_ImageGrabbed;
                    }
                    else
                    {
                        if (pictureCCCD.Image != null)
                        {
                            splashScreenManager1.ShowWaitForm();
                            //Convert Image to string 
                            string stringImage = ConvertImageToBase64(pictureCCCD.Image);
                            // Send data to ML server
                            string idResult = await ApiMethod.CheckCitizenshipID(stringImage);
                            labelResult.Text = "RESULT:" + idResult;
                            // Check Result to show Immage and compare with the Input CCCD
                            if (idResult != badImage && idResult != badDetect && idResult != STATUS_CCCD_5 && idResult != STATUS_CCCD_3 && idResult == UserCCCD.CCCDNumber)
                            {
                                UserCCCD.PictureCCCD = stringImage;
                                // Convert Image to Byte
                                UserCCCD.PictureCCCDByte = ConvertImageToByte(pictureCCCD.Image);
                                btnDone.Enabled = true;
                                Application.Idle -= Capture_ImageGrabbed;
                                capture.Stop();
                            }
                            else
                            {
                                countdownValue = 5;
                                timer.Start();
                                //capture.Start();
                                //Application.Idle += Capture_ImageGrabbed;
                                labelResult.Text = "Take a photo again";
                            }
                            splashScreenManager1.CloseWaitForm();
                        }

                    }
                }
                else
                {
                    countdownValue = 5;
                    timer.Start();
                    // capture.Start();
                    // Application.Idle += Capture_ImageGrabbed;
                    labelResult.Text = "Not enough information";
                }


            }
            
        }
        private void Capture_ImageGrabbed(object sender, EventArgs e)
        { 
            // Try catch
            if (capture != null && capture.Ptr != IntPtr.Zero)
            {
                using (Mat ImageFrame = capture.QueryFrame())
                {
                    if (ImageFrame != null)
                    {
                        detectionResults = ssd.DetectObjects(ImageFrame);
                        //DrawBoundingBoxesSSD(ImageFrame, detectionResults);
                        Image<Bgr, Byte> image = ImageFrame.ToImage<Bgr, byte>();
                        pictureCCCD.Image = image.ToBitmap();
                    }

                }
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            Application.Idle -= Capture_ImageGrabbed;
            capture?.Dispose();
            var userInfor = ParentForm.Controls.Find("UserInfor", true);
            Helpers.StopSound();
            if (userInfor.Length == 0)
            {
                ParentForm.Controls.Find("panelSlider2", true)[0].Controls.Add(new UserInfor());

            }
            else
            {
                userInfor[0].BringToFront();
            }
            Utils.ForwardCCCD(ParentForm, "pictureBoxVCCCD", "pictureBoxInfo", "UserInfor");
            var citizenCapture = ParentForm.Controls.Find("CitizenshipIDCapture", true);
            if (citizenCapture.Length > 0)
            {
                var controlToRemove = citizenCapture[0];
                ParentForm.Controls.Remove(controlToRemove);
                controlToRemove.Dispose();
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            Application.Idle -= Capture_ImageGrabbed;
            capture.Dispose();
            Helpers.StopSound();
            Utils.BackCCCD(ParentForm, "pictureBoxVCCCD", "pictureBoxCCCD", "CitizenshipID");
            
            //Control CitizenshipIDCapture = ParentForm.Controls.Find("panelSlider", true)[0].Controls.Find("CitizenshipIDCapture", true)[0];
            //ParentForm.Controls.Find("panelSlider2", true)[0].Controls.Remove(CitizenshipIDCapture);
            splashScreenManager1.CloseWaitForm();
        }

        public string ConvertImageToBase64(Image image)
        {
            using (var ms = new MemoryStream())
            {
                // Convert the image to a byte array
                image.Save(ms, ImageFormat.Jpeg);
                var imageBytes = ms.ToArray();

                // Convert the byte array to a base64 string
                var base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        private byte[] ConvertImageToByte(Image img)
        {
            byte[] imageBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Use the appropriate image format
                imageBytes = ms.ToArray();
            }
            return imageBytes;
        }
        private void DrawBoundingBoxesSSD(Mat frame, List<DetectionResult> detectionResults)
        {
            foreach (var detection in detectionResults)
            {
                int x = (int)detection.Top;
                int y = (int)detection.Right;
                int width = (int)(detection.Bottom - detection.Top);
                int height = (int)(detection.Left - detection.Right);

                if (detection.Score > 0.5)
                {
                    // Draw the rectangle
                    Rectangle rect = new Rectangle(x, y, width, height);
                    CvInvoke.Rectangle(frame, rect, new Bgr(Color.Red).MCvScalar, thickness: 2);
                    // Display the class name
                    Point textLocation = new Point(x, y - 10);
                    CvInvoke.PutText(frame, detection.ClassName + " " + (detection.Score * 100).ToString("##.##"), textLocation,
                        FontFace.HersheySimplex, fontScale: 0.5, new Bgr(Color.Red).MCvScalar);
                }
            }
        }


    }
}
