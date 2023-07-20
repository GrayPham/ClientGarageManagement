﻿using ManagementStore.Common;
using ManagementStore.Extensions;
using ManagementStore.Model.Static;
using Parking.App.Common.Helper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace ManagementStore.Form.User
{
    public partial class FullName : System.Windows.Forms.UserControl
    {
        public List<string> character;
        private string fileNameAudio;
        public FullName()
        {
            character = new List<String>();

            InitializeComponent();
            Helpers.PlaySound(@"Assets\Audio\FullnameCCCD.wav");
        }

        private async void FullName_Load(object sender, EventArgs e)
        {
            //CustomKeyboardForm();
            fileNameAudio = await AudioConstants.GetListSound(AudioConstants.FullName);
            if (fileNameAudio != null && fileNameAudio != "")
            {
                Helpers.PlaySound(@"Assets\Audio\" + fileNameAudio + ".wav");
            }
            else
            {
                Helpers.PlaySound(@"Assets\Audio\" + AudioConstants.FullName + ".wav");
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            // Handle button click event
            Button button = (Button)sender;
            MessageBox.Show($"You clicked the button with character '{button.Text}'");
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            splashScreenManager2.ShowWaitForm();
            Helpers.StopSound();
            //UserInfo.FullName = birthDayTxt.Text;
            var faceTakenCapture = ParentForm.Controls.Find("FaceTaken", true);
            if (faceTakenCapture.Length > 0)
            {
                var controlToRemove = faceTakenCapture[0] as FaceTaken;
                ParentForm.Controls.Remove(controlToRemove);
                Application.Idle -= controlToRemove.Capture_ImageGrabbed;
                controlToRemove.Dispose();
            }
            ParentForm.Controls.Find("panelSlider", true)[0].Controls.Add(new FaceTaken());

            Utils.Forward(ParentForm, "pictureBoxName", "pictureBoxFace", "FaceTaken");

            UserInfo.FullName = fullNameTxt.Text;

            splashScreenManager2.CloseWaitForm();




        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            splashScreenManager2.ShowWaitForm();

            Utils.Back(ParentForm, "pictureBoxName", "pictureBoxInfo", "InformationUser");
            Thread.Sleep(800);

            splashScreenManager2.CloseWaitForm();

        }
        private void DisplayPhoneNumber()
        {
            fullNameTxt.Text = string.Join("", character.ToArray());
        }
        #region Keyboard


        private void btnCharA_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("A");
            DisplayPhoneNumber();
        }

        private void btnCharB_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("B");
            DisplayPhoneNumber();
        }

        private void btnCharC_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("C");
            DisplayPhoneNumber();
        }

        private void btnCharD_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("D");
            DisplayPhoneNumber();
        }

        private void btnBackSpace_Click(object sender, EventArgs e)
        {
            if (character.Count > 0)
            {
                character.RemoveAt(character.Count - 1);
            }
            else
            {
                fullNameTxt.Text = "";
            }
            DisplayPhoneNumber();
        }

        private void btnCharE_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("E");
            DisplayPhoneNumber();
        }

        private void btnCharF_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("F");
            DisplayPhoneNumber();
        }

        private void btnCharG_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("G");
            DisplayPhoneNumber();
        }

        private void btnCharH_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("H");
            DisplayPhoneNumber();
        }

        private void btnCharI_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("I");
            DisplayPhoneNumber();
        }

        private void btnSpace_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add(" ");
            DisplayPhoneNumber();
        }

        private void btnCharK_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("K");
            DisplayPhoneNumber();
        }

        private void btnCharL_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("L");
            DisplayPhoneNumber();
        }

        private void btnCharM_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("M");
            DisplayPhoneNumber();
        }

        private void btnCharN_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("N");
            DisplayPhoneNumber();
        }

        private void btnCharO_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("O");
            DisplayPhoneNumber();
        }

        private void btnCharP_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("P");
            DisplayPhoneNumber();
        }

        private void btnCharQ_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("Q");
            DisplayPhoneNumber();
        }

        private void btnCharR_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("R");
            DisplayPhoneNumber();
        }

        private void btnCharS_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("S");
            DisplayPhoneNumber();
        }

        private void btnCharT_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("T");
            DisplayPhoneNumber();
        }

        private void btnCharU_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("U");
            DisplayPhoneNumber();
        }

        private void btnCharV_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("V");
            DisplayPhoneNumber();
        }

        private void btnCharZ_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("Z");
            DisplayPhoneNumber();
        }

        private void btnCharY_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("Y");
            DisplayPhoneNumber();
        }

        private void btnCharJ_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("J");
            DisplayPhoneNumber();
        }

        private void btnCharW_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("W");
            DisplayPhoneNumber();
        }

        private void btnCharX_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add("X");
            DisplayPhoneNumber();
        }
        #endregion

        #region

        #endregion
    }
}
