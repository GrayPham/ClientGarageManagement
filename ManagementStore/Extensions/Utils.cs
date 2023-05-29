using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Twilio.Rest.Verify.V2.Service;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using ManagementStore.Model.Static;
using System.Collections.Generic;

namespace ManagementStore.Extensions
{
    public static class Utils
    {
        const string accountSid = "AC0e8a283c2cf0564d3e3a02a565d23c45";
        const string authToken = "1ccb383bd206438135ef9ad0dfb1abdd";
        const string serviceSid = "VAffb5b04aee098d3f4ebf3e22346d49f7"; // Replace with your Twilio Verify service SID

        public static Dictionary<int, string> GetCoCoClasses()
        {
            // Map the class indices to class labels using coco_classes dictionary
            Dictionary<int, string> cocoClasses = new Dictionary<int, string>
            {
                { 1, "person" },
                { 2, "bicycle" },
                { 3, "car" },
                { 4, "motorcycle" },
                { 5, "airplane" },
                { 6, "bus" },
                { 7, "train" },
                { 8, "truck" },
                { 9, "boat" },
                { 10, "traffic light" },
                { 11, "fire hydrant" },
                { 12, "stop sign" },
                { 13, "parking meter" },
                { 14, "bench" },
                { 15, "bird" },
                { 16, "cat" },
                { 17, "dog" },
                { 18, "horse" },
                { 19, "sheep" },
                { 20, "cow" },
                { 21, "elephant" },
                { 22, "bear" },
                { 23, "zebra" },
                { 24, "giraffe" },
                { 25, "backpack" },
                { 26, "umbrella" },
                { 27, "handbag" },
                { 28, "tie" },
                { 29, "suitcase" },
                { 30, "frisbee" },
                { 31, "skis" },
                { 32, "snowboard" },
                { 33, "sports ball" },
                { 34, "kite" },
                { 35, "baseball bat" },
                { 36, "baseball glove" },
                { 37, "skateboard" },
                { 38, "surfboard" },
                { 39, "tennis racket" },
                { 40, "bottle" },
                { 41, "wine glass" },
                { 42, "cup" },
                { 43, "fork" },
                { 44, "knife" },
                { 45, "spoon" },
                { 46, "bowl" },
                { 47, "banana" },
                { 48, "apple" },
                { 49, "sandwich" },
                { 50, "orange" },
                { 51, "broccoli" },
                { 52, "carrot" },
                { 53, "hot dog" },
                { 54, "pizza" },
                { 55, "donut" },
                { 56, "cake" },
                { 57, "chair" },
                { 58, "couch" },
                { 59, "potted plant" },
                { 60, "bed" },
                { 61, "dining table" },
                { 62, "toilet" },
                { 63, "tv" },
                { 64, "laptop" },
                { 65, "mouse" },
                { 66, "remote" },
                { 67, "keyboard" },
                { 68, "cell phone" },
                { 69, "microwave" },
                { 70, "oven" },
                { 71, "toaster" },
                { 72, "sink" },
                { 73, "refrigerator" },
                { 74, "book" },
                { 75, "clock" },
                { 76, "vase" },
                { 77, "scissors" },
                { 78, "teddy bear" },
                { 79, "hair drier" },
                { 80, "toothbrush" }
            };
            return cocoClasses;
        }

        public static string GetVideoId(string url)
        {
            var yMatch = new Regex(@"youtu(?:\be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)").Match(url);
            return yMatch.Success ? yMatch.Groups[1].Value : string.Empty;
        }

        public static string SendOTPSMS(string phoneNumber)
        {
            TwilioClient.Init(accountSid, authToken);

            // Generate the OTP code
            Random random = new Random();
            string otpCode = "777777"; // random.Next(100000, 999999).ToString();


            // Send the SMS
            //var message = MessageResource.Create(
            //    from: "+16206464293",
            //    to: phoneNumber,
            //    body: $"Your AI Building verification code is: {otpCode}." // This code will expire in 5 minutes. Don't share this code with anyone; our employees will never ask for the code.
            //);

            //// Send the SMS
            //var message = VerificationResource.Create(
            //    pathServiceSid: serviceSid,
            //    to: phoneNumber,
            //    channel: "sms"
            // body: $"Your AI Building verification code is: {otpCode}." // This code will expire in 5 minutes. Don't share this code with anyone; our employees will never ask for the code.
            //);
            return otpCode;
        }

        public static bool VerifyOTP(string phoneNumber, string verificationCode, string code)
        {
            TwilioClient.Init(accountSid, authToken);


            if (verificationCode.Equals(code))
            {
                return true;
            }
            return false;

            //var verificationCheck = VerificationCheckResource.Create(
            //    pathServiceSid: serviceSid,
            //    to: phoneNumber,
            //    code: verificationCode
            //);

            //return verificationCheck.Status == "approved";
        }

        public static void Forward(System.Windows.Forms.Form parentForm, string currentPictureBox, string nextPictureBox, string nextPageName)
        {
            PictureBox curImage = (PictureBox)parentForm.Controls.Find(currentPictureBox, true)[0];
            curImage.Image = Properties.Resources.completed;
            PictureBox preImage = (PictureBox)parentForm.Controls.Find(nextPictureBox, true)[0];
            preImage.Image = Properties.Resources.current;
            parentForm.Controls.Find("panelSlider", true)[0].Controls.Find(nextPageName, true)[0].BringToFront();
        }

        public static void Back(System.Windows.Forms.Form parentForm, string currentPictureBox, string prevPictureBox, string prevPageName)
        {
            PictureBox curImage = (PictureBox)parentForm.Controls.Find(currentPictureBox, true)[0];
            curImage.Image = Properties.Resources.pending;
            PictureBox preImage = (PictureBox)parentForm.Controls.Find(prevPictureBox, true)[0];
            preImage.Image = Properties.Resources.current;
            parentForm.Controls.Find("panelSlider", true)[0].Controls.Find(prevPageName, true)[0].BringToFront();
        }
    }
}
