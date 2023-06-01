using DevExpress.XtraEditors;
using ManagementStore.Extensions;
using ManagementStore.Model.Static;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementStore.Form.User.ResisterUserSub
{
    public partial class FullNameCCCD : DevExpress.XtraEditors.XtraUserControl
    {
        public List<string> character;
        public FullNameCCCD()
        {
            InitializeComponent();
            character = new List<String>();
        }
        private void DisplayPhoneNumber()
        {
            fullNameTxt.Text = string.Join("", character.ToArray());
        }

        #region Keyboard

        private void btnSpace_Click(object sender, EventArgs e)
        {
            if (character.Count != 50) character.Add(" ");
            DisplayPhoneNumber();
        }
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

        private void btnNext_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            UserCCCD.FullName = fullNameTxt.Text;
            ParentForm.Controls.Find("panelSlider2", true)[0].Controls.Add(new FaceTakenCCCD());

            Utils.ForwardCCCD(ParentForm, "pictureBoxName", "pictureBoxInfo", "FaceTakenCCCD");
            splashScreenManager1.CloseWaitForm();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {

        }
    }
}
