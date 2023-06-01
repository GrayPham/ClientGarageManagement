﻿using DevExpress.XtraEditors;
using Emgu.CV;
using ManagementStore.Form.Notify;
using ManagementStore.Model.Static;
using Parking.App.Common.ApiMethod;
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
        ShowImageCCCD showImage;
        private const string badImage = "Bad Image";
        private const string badDetect = "Not Detect ID";
        private const string STATUS_CCCD_5 = "Is Unknown";
        private const string STATUS_CCCD_3 = "Not Detect ID";

        public CitizenshipIDCapture()
        {
            InitializeComponent();
            // Initialize the camera capture
            capture = new VideoCapture();
            showImage = new ShowImageCCCD();
            capture.ImageGrabbed += Capture_ImageGrabbed;
            capture.Start();
            // Set the initial countdown value and Timer interval
            countdownValue = 5;
            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_TickAsync;

            // Start the Timer
            timer.Start();
        }
        private async  void  Timer_TickAsync(object sender, EventArgs e)
        {
            countdownValue--;
            showCountDown.Text = $"Ảnh sẽ được chụp sau {countdownValue.ToString()} giây nữa";

            // When the countdown reaches 0, stop the Timer and capture the picture
            if (countdownValue == 0)
            {
                
                var timer = (Timer)sender;
                timer.Stop();
                capture.ImageGrabbed -= Capture_ImageGrabbed;
                var result = XtraMessageBox.Show("Are you sure to use this image?", "Warning",  MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (DialogResult.No == result)
                {
                    countdownValue = 5;
                    timer.Start();
                    capture.ImageGrabbed += Capture_ImageGrabbed;
                }
                else
                {
                    if (pictureCCCD.Image != null)
                    {
                        //Convert Image to string 
                        string  stringImage = ConvertImageToBase64(pictureCCCD.Image);
                        UserCCCD.PictureCCCD = stringImage;
                        // Send data to ML server
                        string idResult = await ApiMethod.CheckCitizenshipID(UserCCCD.PictureCCCD);
                        labelResult.Text = "RESULT:" + idResult;
                        // Check Result to show Immage and compare with the Input CCCD
                        if (idResult != badImage || idResult != badDetect || idResult != STATUS_CCCD_5 || idResult != STATUS_CCCD_3)
                        {
                            btnDone.Enabled = true;
                        }
                        else
                        {
                            countdownValue = 5;
                            timer.Start();
                            capture.ImageGrabbed += Capture_ImageGrabbed;
                            labelResult.Text = "Take a photo again";
                        }
                        
                    }
                        
                }

            }
            
        }
        private void btnTakeAgain_Click(object sender, EventArgs e)
        {
            countdownValue = 5;
            capture.ImageGrabbed += Capture_ImageGrabbed;
            timer.Start();
            showImage.Close();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            capture.ImageGrabbed -= Capture_ImageGrabbed;
            Image img = showImage.pictureBoxTaken.Image;

            // Convert the image to a byte array
            //byte[] imageBytes;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Use the appropriate image format
            //    imageBytes = ms.ToArray();
            //}
            // Save Image and CCCD INFOR
            //UserInfo.Picture = Convert.ToBase64String(imageBytes);
            showImage.Close();
        }
        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            if (capture != null && capture.Ptr != IntPtr.Zero)
            {
                Mat frame = new Mat();
                capture.Retrieve(frame);
                pictureCCCD.Image = frame.ToBitmap();
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {

        }

        private void btnPrev_Click(object sender, EventArgs e)
        {

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
    }
}