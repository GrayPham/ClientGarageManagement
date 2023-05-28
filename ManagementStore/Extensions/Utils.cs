using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Twilio.Rest.Verify.V2.Service;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using ManagementStore.Model.Static;

namespace ManagementStore.Extensions
{
    public static class Utils
    {
        const string accountSid = "AC0e8a283c2cf0564d3e3a02a565d23c45";
        const string authToken = "1ccb383bd206438135ef9ad0dfb1abdd";
        const string serviceSid = "VAffb5b04aee098d3f4ebf3e22346d49f7"; // Replace with your Twilio Verify service SID

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
            //    // body: $"Your AI Building verification code is: {otpCode}." // This code will expire in 5 minutes. Don't share this code with anyone; our employees will never ask for the code.
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
