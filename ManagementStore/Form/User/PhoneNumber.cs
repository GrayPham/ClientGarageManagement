using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSplashScreen;
using ManagementStore.Common;
using ManagementStore.Extensions;
using ManagementStore.Model.Static;
using Parking.App.Common.Helper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace ManagementStore.Form.User
{
    public partial class PhoneNumber : System.Windows.Forms.UserControl
    {
        public List<string> Num;
        private Dictionary<string, string> phoneCodes;
        private string fileNameAudio;
        public PhoneNumber()
        {
            Num = new List<String>();
            InitializeComponent();
            // btnNext.Enabled = false;
            

        }

        private async void PhoneNumber_Load(object sender, EventArgs e)
        {
            phoneTxt.Text = "0365858975";
            // splashScreenManager.ShowWaitForm();
            phoneCodes = InitializePhoneCodes();
            ccbCountryNumber.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;      
            AddFormattedPhoneCodes();
            ccbCountryNumber.Select(0, 1);
            fileNameAudio = await AudioConstants.GetListSound(AudioConstants.InputPhone);
            if (fileNameAudio != null && fileNameAudio != "")
            {
                Helpers.PlaySound(@"Assets\Audio\" + fileNameAudio + ".wav");
            }
            else
            {
                Helpers.PlaySound(@"Assets\Audio\" + AudioConstants.InputPhone + ".wav");
            }
            // splashScreenManager.CloseWaitForm();

        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if(ccbCountryNumber.Text == "" || ccbCountryNumber.Text == null)
            {
                XtraMessageBox.Show("Invalid phone code, please select it before go to next page!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ccbCountryNumber.Focus();
                return;

            }
            string text = ccbCountryNumber.SelectedItem.ToString().Split(' ')[0];
            VerifyPhoneNumber.PhoneNumber = text + phoneTxt.Text.Trim();

            // splashScreenManager.ShowWaitForm();
            VerifyPhoneNumber.OTPCode = Utils.SendOTPSMS(VerifyPhoneNumber.PhoneNumber);
            
            var citizenCapture = ParentForm.Controls.Find("PhoneOTP", true);
            if (citizenCapture.Length > 0)
            {
                var controlToRemove = citizenCapture[0];
                ParentForm.Controls.Remove(controlToRemove);
                controlToRemove.Dispose();
            }
            ParentForm.Controls.Find("panelSlider", true)[0].Controls.Add(new PhoneOTP());
            Utils.Forward(ParentForm, "pictureBoxPhone", "pictureBoxOTP", "PhoneOTP");
            
            // splashScreenManager.CloseWaitForm();
            UserInfo.PhoneNumber = VerifyPhoneNumber.PhoneNumber;
        }
        #region Number
        private void btnNum1_Click(object sender, EventArgs e)
        {
            if(Num.Count!=10) Num.Add("1");
            DisplayPhoneNumber();
        }

        private void btnNum2_Click(object sender, EventArgs e)
        {
            if (Num.Count != 10) Num.Add("2");
            DisplayPhoneNumber();

        }

        private void btnNum3_Click(object sender, EventArgs e)
        {
            if (Num.Count != 10) Num.Add("3");
            DisplayPhoneNumber();

        }

        private void btnNum4_Click(object sender, EventArgs e)
        {
            if (Num.Count != 10) Num.Add("4");
            DisplayPhoneNumber();

        }

        private void btnNum5_Click(object sender, EventArgs e)
        {
            if (Num.Count != 10) Num.Add("5");
            DisplayPhoneNumber();

        }

        private void btnNum6_Click(object sender, EventArgs e)
        {
            if (Num.Count != 10) Num.Add("6");
            DisplayPhoneNumber();

        }

        private void btnNum7_Click(object sender, EventArgs e)
        {
            if (Num.Count != 10) Num.Add("7");
            DisplayPhoneNumber();

        }

        private void btnNum8_Click(object sender, EventArgs e)
        {
            if (Num.Count != 10) Num.Add("8");
            DisplayPhoneNumber();

        }

        private void btnNum9_Click(object sender, EventArgs e)
        {
            if (Num.Count != 10) Num.Add("9");
            DisplayPhoneNumber();

        }

        private void btnNum0_Click(object sender, EventArgs e)
        {
            if (Num.Count != 10) Num.Add("0");
            DisplayPhoneNumber();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if(Num.Count > 0)
            {
                Num.RemoveAt(Num.Count - 1);
            }
            else
            {
                phoneTxt.Text = "";
            }
            DisplayPhoneNumber();

        }

        private void btnAC_Click(object sender, EventArgs e)
        {
            Num = new List<string>();
            DisplayPhoneNumber();
        }

        #endregion
        private void DisplayPhoneNumber()
        {
            phoneTxt.Text = string.Join("", Num.ToArray());
            if (phoneTxt.Text.Length == 10)
            {
                // Phone number is valid
                btnNext.Enabled = true;
            }
            else
            {
                // Phone number is invalid
                btnNext.Enabled = false;
            }
        }
        private Dictionary<string, string> InitializePhoneCodes()
        {
            Dictionary<string, string> codes = new Dictionary<string, string>()
        {
            { "+84", "VN" },
            { "+1", "US" },      // United States
            { "+44", "UK" },     // United Kingdom
            { "+61", "AU" },     // Australia
            { "+86", "CN" },     // China
            { "+91", "IN" },     // India
            { "+81", "JP" },     // Japan
            { "+27", "ZA" },     // South Africa
            { "+49", "DE" },     // Germany
            { "+33", "FR" },     // France
            { "+39", "IT" },     // Italy
            { "+7", "RU" },      // Russia
            { "+31", "NL" },     // Netherlands
            { "+46", "SE" },     // Sweden
            { "+34", "ES" },     // Spain
            { "+55", "BR" },     // Brazil
        };

            return codes;
        }
        private void AddFormattedPhoneCodes()
        {
            foreach (KeyValuePair<string, string> entry in phoneCodes)
            {
                string formattedDisplay = $"{entry.Key} ({entry.Value})";
        
                ccbCountryNumber.Properties.Items.Add(formattedDisplay);
            }
        }
    }
}
