using DevExpress.XtraEditors;
using ManagementStore.Extensions;
using ManagementStore.Model.Static;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace ManagementStore.Form.User
{
    public partial class PhoneOTP : System.Windows.Forms.UserControl
    {
        public List<string> Num;
        public PhoneOTP()
        {
            Num = new List<String>();
            InitializeComponent();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            bool status = Utils.VerifyOTP(VerifyPhoneNumber.PhoneNumber, otpTxt.Text, VerifyPhoneNumber.OTPCode);
            Thread.Sleep(1000);
            splashScreenManager1.CloseWaitForm();
            if (status)
            {
                Utils.Forward(ParentForm, "pictureBoxOTP", "pictureBoxInfo", "InformationUser");              
            }
            else
            {
                XtraMessageBox.Show("Incorrect OTP code, please input again or resend OTP!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void btnResendOTP_Click(object sender, EventArgs e)
        {
            Utils.SendOTPSMS(VerifyPhoneNumber.PhoneNumber);
            XtraMessageBox.Show("Resend OTP successfull", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnOTP_Click(object sender, EventArgs e)
        {
            Utils.Back(ParentForm, "pictureBoxOTP", "pictureBoxPhone", "PhoneNumber");
        }


        private void btnNum1_Click(object sender, EventArgs e)
        {
            if (Num.Count != 10) Num.Add("1");
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (Num.Count > 0)
            {
                Num.RemoveAt(Num.Count - 1);
            }
            else
            {
                otpTxt.Text = "";
            }
            DisplayPhoneNumber();
        }

        private void btnNum0_Click(object sender, EventArgs e)
        {
            if (Num.Count != 10) Num.Add("0");
            DisplayPhoneNumber();
        }

        private void btnAC_Click(object sender, EventArgs e)
        {
            Num = new List<string>();
            DisplayPhoneNumber();
        }
        private void DisplayPhoneNumber()
        {
            otpTxt.Text = string.Join("", Num.ToArray());
            if (otpTxt.Text.Length == 6)
            {
                btnNext.Enabled = true;
            }
            else
            {
                btnNext.Enabled = false;
            }
        }

    }
}
