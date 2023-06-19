using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using ManagementStore.Extensions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraSplashScreen;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManagementStore.Form.User.ResisterUserSub;
using ManagementStore.Model.Static;

namespace ManagementStore.Form.User
{
    public partial class CitizenshipID : System.Windows.Forms.UserControl
    {
        public List<string> Num;
        private int number_character = 12;
        public CitizenshipID()
        {
            InitializeComponent();
            Num = new List<String>();
        }
        private void CCCDNumber_Load(object sender, EventArgs e)
        {
            //splashScreenManager.ShowWaitForm();
            cccdTxt.Text = "066201000447";
            //phoneCodes = InitializePhoneCodes();
            //ccbCountryNumber.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            //AddFormattedPhoneCodes();
            //ccbCountryNumber.Select(0, 1);
            Thread.Sleep(1000);
            //splashScreenManager.CloseWaitForm();

        }
        #region Button click
        private void btnNum1_Click(object sender, EventArgs e)
        {
            if (Num.Count != 12) Num.Add("1");
            DisplayCCCDNumber();
        }
        private void btnNum2_Click(object sender, EventArgs e)
        {
            if (Num.Count != 12) Num.Add("2");
            DisplayCCCDNumber();
        }
        private void btnNum3_Click(object sender, EventArgs e)
        {
            if (Num.Count != 12) Num.Add("3");
            DisplayCCCDNumber();
        }
        private void btnNum4_Click(object sender, EventArgs e)
        {
            if (Num.Count != 12) Num.Add("4");
            DisplayCCCDNumber();
        }
        private void btnNum5_Click(object sender, EventArgs e)
        {
            if (Num.Count != 12) Num.Add("5");
            DisplayCCCDNumber();
        }
        private void btnNum6_Click(object sender, EventArgs e)
        {
            if (Num.Count != 12) Num.Add("6");
            DisplayCCCDNumber();
        }
        private void btnNum7_Click(object sender, EventArgs e)
        {
            if (Num.Count != 12) Num.Add("7");
            DisplayCCCDNumber();
        }
        private void btnNum8_Click(object sender, EventArgs e)
        {
            if (Num.Count != 12) Num.Add("8");
            DisplayCCCDNumber();
        }
        private void btnNum9_Click(object sender, EventArgs e)
        {
            if (Num.Count != 12) Num.Add("9");
            DisplayCCCDNumber();
        }
        private void btnNum0_Click(object sender, EventArgs e)
        {
            if (Num.Count != 12) Num.Add("0");
            DisplayCCCDNumber();
        }
        private void btnAC_Click(object sender, EventArgs e)
        {
            Num = new List<string>();
            DisplayCCCDNumber();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (Num.Count > 0)
            {
                Num.RemoveAt(Num.Count - 1);
            }
            else
            {
                cccdTxt.Text = "";
            }
            DisplayCCCDNumber();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            UserCCCD.CCCDNumber = cccdTxt.Text;
            var citizenCapture = ParentForm.Controls.Find("CitizenshipIDCapture", true);
            if(citizenCapture.Length > 0)
            {
                var controlToRemove = citizenCapture[0];
                ParentForm.Controls.Remove(controlToRemove);
                controlToRemove.Dispose();
            }
            ParentForm.Controls.Find("panelSlider2", true)[0].Controls.Add(new CitizenshipIDCapture());
            Utils.ForwardCCCD(ParentForm, "pictureBoxCCCD", "pictureBoxVCCCD", "CitizenshipIDCapture");
            splashScreenManager1.CloseWaitForm();

        }
        #endregion
        private void DisplayCCCDNumber()
        {
            cccdTxt.Text = string.Join("", Num.ToArray());
            if (cccdTxt.Text.Length == number_character)
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


    }
}
