using Emgu.CV;
using Emgu.CV.Structure;
using ManagementStore.Common;
using ManagementStore.Extensions;
using System;
using System.Drawing;
using Microsoft.ML.OnnxRuntime;
using System.Collections.Generic;
using Emgu.CV.CvEnum;
using ManagementStore.DTO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ManagementStore.Form.Notify;
using ManagementStore.Model.Static;
using System.IO;
using Connect.Common.Contract;
using Parking.App.Common.ApiMethod;
using Parking.App.Common.ViewModels;
using Parking.App.Contract.Common;
using ManagementStore.Model.ML;
using System.Net.NetworkInformation;
using Parking.App.Common;
using Parking.App.Common.Constants;
using Parking.App.Common.Helper;

namespace ManagementStore.Form.User
{
    public partial class FaceTaken : System.Windows.Forms.UserControl
    {
        private VideoCapture capture;
        private FPSCounter fpsCounter;
        private int countdownValue;
        private Timer timer;
        ShowImageTaken image;
        ConfirmInfo info;
        List<DetectionResult> detectionResults;
        ObjectDetectionSSD ssd;
        ObjectDetectionMB mb;
        public static string fullPathMainForm = Helpers.GetFullPathOfMainForm();
        public FaceTaken()
        {
            InitializeComponent();
            string path = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);

            ssd = new ObjectDetectionSSD(ModelConfig.dataFolderPath + "/mb2-ssd-lite-predict.onnx");
            mb = new ObjectDetectionMB(ModelConfig.dataFolderPath + "/ssd_mobilenet_v1_10.onnx");
            capture = new VideoCapture();
            capture.ImageGrabbed += Capture_ImageGrabbed;
            capture.Start();
            // Set the initial countdown value and Timer interval
            countdownValue = 5;
            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;

            // Start the Timer
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            countdownValue--;
            showCountDown.Text = $"The photo will be taken in {countdownValue.ToString()} seconds";

            // When the countdown reaches 0, stop the Timer and capture the picture
            if (countdownValue == 0)
            {
                image = new ShowImageTaken();
                if (detectionResults.Count == 0)
                {
                    //capture.ImageGrabbed -= Capture_ImageGrabbed;
                    capture.Stop();
                    var result = XtraMessageBox.Show("Can not detect the face, please try again", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (DialogResult.OK == result)
                    {
                        //capture.ImageGrabbed += Capture_ImageGrabbed;
                        capture.Start();
                        countdownValue = 5;
                        timer.Start();
                    }
                }
                else
                {
                    image.Show();
                    image.pictureBoxTaken.Image = pictureFace.Image;
                    //capture.ImageGrabbed -= Capture_ImageGrabbed;
                    capture.Stop();
                    image.btnTakeAgain.Click += btnTakeAgain_Click;
                    image.btnOK.Click += btnOK_Click;
                }
                timer.Stop();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // capture.ImageGrabbed -= Capture_ImageGrabbed;
            capture.Stop();
            Image img = image.pictureBoxTaken.Image;
            info = new ConfirmInfo();
            // Convert the image to a byte array
            byte[] imageBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Use the appropriate image format
                imageBytes = ms.ToArray();
            }
            UserInfo.Picture = Convert.ToBase64String(imageBytes);
            info.fullNameTxt.Text = UserInfo.FullName;
            info.phoneTxt.Text = UserInfo.PhoneNumber;
            info.birthdayTxt.Text = UserInfo.BirthDay;
            info.genderTxt.Text = UserInfo.Gender;
            info.pictureTaken.Image = img;
            info.Show();
            info.btnBack.Click += btnBack_Click;
            info.btnConfirm.Click += btnConfirm_Click;
            image.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            capture?.Dispose();
            splashRegisterUser.ShowWaitForm();
            onCreateUser();
            splashRegisterUser.CloseWaitForm();
        }

        public static string GetLocalIPv4()
        {
            string ipAddress = "";
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                    netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach (UnicastIPAddressInformation ip in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ipAddress = ip.Address.ToString();
                            return ipAddress;
                        }
                    }
                }
            }
            return ipAddress;
        }


        private async void onCreateUser()
        {
            RequestInfo request = new RequestInfo();
            var uInfo = new object[5];
            uInfo[0] = UserInfo.FullName == null ? "Nguyen Ngoc Thien" : UserInfo.FullName;
            uInfo[1] = UserInfo.PhoneNumber == null ? "0365858975" : UserInfo.PhoneNumber;
            uInfo[2] = UserInfo.Gender == null ? "Male" : UserInfo.Gender;
            uInfo[3] = UserInfo.Picture == null ? "124" : UserInfo.Picture;
            uInfo[4] = UserInfo.BirthDay == null ? "2023-06-23" : UserInfo.BirthDay;

            string userid = DateTime.Now.ToString("yyyyMMddHHmmss");
            tblUserInfo user = new tblUserInfo()
            {
                UserID = userid,
                UserType = "USR001",
                Password = Helpers.HashCodePassword("DPSS01"),
                UserName = userid,
                PhoneNumber = UserInfo.PhoneNumber,
                Birthday = Convert.ToDateTime(UserInfo.BirthDay),
                Email = String.Empty,
                Gender = UserInfo.Gender == "Male" ? true : false,
                ApproveReject = true,
                UserStatus = "USST01",
                RegistDate = DateTime.Now,
                Desc = String.Empty,
                UseYN = false,
                LoginIP = GetLocalIPv4() ?? " ",
                LastSimilarityRate = ConfigClass.SimilarityRate,
                AuthMethod = "Phone Number" == Constants.PhoneMethod ? "APPTP1" : "APPTP2"
            };

            tblUserMgtStoreInfo userMgt = new tblUserMgtStoreInfo()
            {
                UserID = userid,
                RegistDate = DateTime.Now,
                Memo = "",
                StoreNo = 19
            };

            tblStoreUseHistoryInfo storeUseHistory = new tblStoreUseHistoryInfo()
            {
                UserID = userid,
                UseDate = DateTime.Now,
                StoreNo = 19
            };

            tblUserPhotoInfo photo = new tblUserPhotoInfo();
            var facePhotoPath = "";
            var IdcardPhoto = "";


            if ("Phone Number" == Constants.CardMethod)
            {
                // TODO: save image taken and image in card ID
                //var checkImage = Helpers.SaveToFolderImage(ImageShoot, userid, Helpers.GetFullPathOfMainForm());
                //location = checkImage.Item2;

                //IdcardPhoto = fullPathMainForm + Constants.CardImage;
                //photo = new tblUserPhotoInfo()
                //{
                //    UserID = userid,
                //    TakenPhoto = File.ReadAllBytes(location),
                //    IdCardPhoto = File.ReadAllBytes(IdcardPhoto)
                //};
            }
            else
            {

                photo = new tblUserPhotoInfo()
                {
                    UserID = userid,
                   
                };
            }

            tblUserInfo userInfo = new tblUserInfo()
            {

            };

            user.TblUserMgtStoreInfo = userMgt;
            user.TblStoreUseHistoryInfo = storeUseHistory;
            user.TblUserPhotoInfo = photo;
            user.isRemoveTempUser = true;

            var dataObject = new RequestInfo()
            {
                Data = user
            };

            request.Data = user;
            DataRequest userMgtData = new DataRequest()
            {
                Signature = 104,
                FrameID = 0,
                FunctionCode = 4104,
                DataLength = 10000,
                Data = dataObject
            };

            var repose = await ApiMethod.PostCall(userMgtData);
            if (repose.StatusCode == System.Net.HttpStatusCode.OK)
            {
                XtraMessageBox.Show("Registed account successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // TODO: send message register successfully
            }
            else
            {
                XtraMessageBox.Show("Register user failed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the Image to the MemoryStream as JPEG format
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                // Convert the MemoryStream to a byte array
                return ms.ToArray();
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            Settings.countDown = 120;
            info.Close();
            Utils.Back(ParentForm, "pictureBoxOTP", "pictureBoxPhone", "PhoneNumber");
        }

        private void btnTakeAgain_Click(object sender, EventArgs e)
        {
            countdownValue = 5;
            capture.ImageGrabbed += Capture_ImageGrabbed;
            timer.Start();
            image.Close();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            Utils.Back(ParentForm, "pictureBoxFace", "pictureBoxName", "FullName");

        }

        private void FaceTaken_Load(object sender, EventArgs e)
        {
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            capture?.Dispose();
        }

        private void Capture_ImageGrabbedSSD(object send, EventArgs e)
        {
            if (fpsCounter == null)
            {
                fpsCounter = new FPSCounter();
                fpsCounter.Start();
            }
            if (capture != null && capture.Ptr != IntPtr.Zero)
            {
                Mat frame = new Mat();
                capture.Retrieve(frame);
                detectionResults = ssd.DetectObjects(frame);
                DrawBoundingBoxesSSD(frame, detectionResults);
                pictureFace.Image = frame.ToBitmap();
                fpsCounter.Update();
                Console.WriteLine("FPS: " + fpsCounter.CurrentFPS.ToString("F2"));
            }

        }
        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            if (fpsCounter == null)
            {
                fpsCounter = new FPSCounter();
                fpsCounter.Start();
            }

            if (capture != null && capture.Ptr != IntPtr.Zero)
            {
                Mat frame = new Mat();
                capture.Retrieve(frame);
                detectionResults = mb.DetectObjects(frame);
                mb.DrawBoundingBoxes(frame, detectionResults);
                pictureFace.Image = frame.ToBitmap();
                fpsCounter.Update();
                Console.WriteLine("FPS: " + fpsCounter.CurrentFPS.ToString("F2"));
            }
        }
        private void DrawBoundingBoxes(Mat frame, List<DetectionResult> detectionResults)
        {
            foreach (var detection in detectionResults)
            {
                int x = (int)(detection.Left * frame.Width);
                int y = (int)(detection.Top * frame.Height);
                int width = (int)((detection.Right - detection.Left) * frame.Width);
                int height = (int)((detection.Bottom - detection.Top) * frame.Height);

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