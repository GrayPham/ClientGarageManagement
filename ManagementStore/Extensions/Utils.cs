using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Twilio.Rest.Verify.V2.Service;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Collections.Generic;
using System.Text;
using Twilio.Types;
using System.Configuration;

namespace ManagementStore.Extensions
{
    public static class Utils
    {
        public static string AccountSid
        {
            get
            {
                return ConfigurationManager.AppSettings["AccountSid1"];
            }
        }

        public static string AuthToken
        {
            get
            {
                return ConfigurationManager.AppSettings["AuthToken1"];
            }
        }

        public static string ServiceSid
        {
            get
            {
                return ConfigurationManager.AppSettings["ServiceSid1"];
            }
        }

        
        private static Random random = new Random();
        private const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()";


        public static Dictionary<int, string> GetCardClasses()
        {
            // Map the class indices to class labels using coco_classes dictionary
            //Dictionary<int, string> cardClasses = new Dictionary<int, string>
            //{
            //    { 1, "address" },
            //    { 2, "birth" },
            //    { 3, "card" },
            //    { 4, "id" },
            //    { 5, "name" },
            //    { 6, "origin" },
            //    { 7, "title" }
            //};
            Dictionary<int, string> cardClasses = new Dictionary<int, string>
            {
                { 1, "motorcycle" },
                { 2, "character" },
                { 3, "car " },
                { 4, "man" },
                { 5, "human face" },
                { 6, "woman" },
                { 7, "human eye" },
                { 8, "bicycle" },
                { 9, "picture frame" },
                { 10, "vehicle registration plate" },
                { 11, "mobile phone" }
            };
            return cardClasses;
        }

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

        public static string GenerateRandomPassword(int length)
        {
            StringBuilder password = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(allowedChars.Length);
                password.Append(allowedChars[randomIndex]);
            }

            return password.ToString();
        }
        public static bool SendRegisterSuccess(string phoneNumber, string password, string userName)
        {
            try
            {


                TwilioClient.Init(AccountSid, AuthToken);

                var messageOptions = new CreateMessageOptions(
                  new PhoneNumber(phoneNumber));
                messageOptions.From = new PhoneNumber("+15733833092"); // +15733833092 16206464293
                messageOptions.Body = $"Congratulations on your successful registration! Please visit our website at 26.115.12.45 to complete your profile. Your username and password are: ${userName}, ${password}. Welcome aboard!";


                var message = MessageResource.Create(messageOptions);
                Console.WriteLine(message.Body);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public static string SendOTPSMS(string phoneNumber)
        {
            try
            {
                TwilioClient.Init(AccountSid, AuthToken);

                // Generate the OTP code
                Random random = new Random();
                string otpCode = "777777"; // random.Next(100000, 999999).ToString();


                // Send the SMS
                var message = VerificationResource.Create(
                    pathServiceSid: ServiceSid,
                    to: phoneNumber,
                    channel: "sms"
                // body: $"Your AI Building verification code is: {otpCode}." // This code will expire in 5 minutes. Don't share this code with anyone; our employees will never ask for the code.
                );
                return otpCode;
            }
            catch
            {
                return "0";
            }
        }

        public static bool VerifyOTP(string phoneNumber, string verificationCode, string code)
        {
            TwilioClient.Init(AccountSid, AuthToken);


            //if (verificationCode.Equals(code))
            //{
            //    return true;
            //}
            //return false;

            var verificationCheck = VerificationCheckResource.Create(
                pathServiceSid: ServiceSid,
                to: phoneNumber,
                code: verificationCode
            );

            return verificationCheck.Status == "approved";
        }

        public static void Forward(System.Windows.Forms.Form parentForm, string currentPictureBox, string nextPictureBox, string nextPageName)
        {
            PictureBox curImage = (PictureBox)parentForm.Controls.Find(currentPictureBox, true)[0];
            curImage.Image = Properties.Resources.completed;
            PictureBox preImage = (PictureBox)parentForm.Controls.Find(nextPictureBox, true)[0];
            preImage.Image = Properties.Resources.current;
            parentForm.Controls.Find("panelSlider", true)[0].Controls.Find(nextPageName, true)[0].BringToFront();
        }
        public static void ForwardCCCD(System.Windows.Forms.Form parentForm, string currentPictureBox, string nextPictureBox, string nextPageName)
        {
            PictureBox curImage = (PictureBox)parentForm.Controls.Find(currentPictureBox, true)[0];
            curImage.Image = Properties.Resources.completed;
            PictureBox preImage = (PictureBox)parentForm.Controls.Find(nextPictureBox, true)[0];
            preImage.Image = Properties.Resources.current;
            parentForm.Controls.Find("panelSlider2", true)[0].Controls.Find(nextPageName, true)[0].BringToFront();
        }
        public static void Back(System.Windows.Forms.Form parentForm, string currentPictureBox, string prevPictureBox, string prevPageName)
        {
            PictureBox curImage = (PictureBox)parentForm.Controls.Find(currentPictureBox, true)[0];
            curImage.Image = Properties.Resources.pending;
            PictureBox preImage = (PictureBox)parentForm.Controls.Find(prevPictureBox, true)[0];
            preImage.Image = Properties.Resources.current;
            parentForm.Controls.Find("panelSlider", true)[0].Controls.Find(prevPageName, true)[0].BringToFront();
        }
        public static void BackCCCD(System.Windows.Forms.Form parentForm, string currentPictureBox, string prevPictureBox, string prevPageName)
        {
            PictureBox curImage = (PictureBox)parentForm.Controls.Find(currentPictureBox, true)[0];
            curImage.Image = Properties.Resources.pending;
            PictureBox preImage = (PictureBox)parentForm.Controls.Find(prevPictureBox, true)[0];
            preImage.Image = Properties.Resources.current;
            parentForm.Controls.Find("panelSlider2", true)[0].Controls.Find(prevPageName, true)[0].BringToFront();
        }
    }
}
