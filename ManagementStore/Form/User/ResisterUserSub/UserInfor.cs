using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;
using ManagementStore.Extensions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManagementStore.Model.Static;

namespace ManagementStore.Form.User.ResisterUserSub
{
    public partial class UserInfor : System.Windows.Forms.UserControl
    {
        public List<string> Num;
        public UserInfor()
        {
            InitializeComponent();
            Num = new List<String>();
        }
        private void UserInfor_Load(object sender, EventArgs e)
        {
            ccbSelectGender.Properties.Items.Add("Male");
            ccbSelectGender.Properties.Items.Add("Female");
            ccbSelectGender.Properties.Items.Add("Other");
            ccbSelectGender.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

            ccbSelectGender.Properties.DropDownRows = 5;
            ccbSelectGender.SelectedIndex = 0;
        }
        #region Number
        private void btnNum8_Click(object sender, EventArgs e)
        {

            if (Num.Count != 8) Num.Add("8");
            DisplayPhoneNumber();
        }

        private void btnNum2_Click(object sender, EventArgs e)
        {
            if (Num.Count != 8) Num.Add("2");
            DisplayPhoneNumber();
        }

        private void btnNum3_Click(object sender, EventArgs e)
        {
            if (Num.Count != 8) Num.Add("3");
            DisplayPhoneNumber();
        }

        private void btnNum6_Click(object sender, EventArgs e)
        {
            if (Num.Count != 8) Num.Add("6");
            DisplayPhoneNumber();
        }

        private void btnNum5_Click(object sender, EventArgs e)
        {
            if (Num.Count != 8) Num.Add("5");
            DisplayPhoneNumber();
        }

        private void btnNum4_Click(object sender, EventArgs e)
        {
            if (Num.Count != 8) Num.Add("4");
            DisplayPhoneNumber();
        }

        private void btnNum7_Click(object sender, EventArgs e)
        {
            if (Num.Count != 8) Num.Add("7");
            DisplayPhoneNumber();
        }

        private void btnNum1_Click(object sender, EventArgs e)
        {
            if (Num.Count != 8) Num.Add("1");
            DisplayPhoneNumber();
        }

        private void btnNum9_Click(object sender, EventArgs e)
        {
            if (Num.Count != 8) Num.Add("9");
            DisplayPhoneNumber();
        }

        private void btnNum0_Click(object sender, EventArgs e)
        {
            if (Num.Count != 8) Num.Add("0");
            DisplayPhoneNumber();
        }

        #endregion
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (Num.Count > 0)
            {
                Num.RemoveAt(Num.Count - 1);
            }
            else
            {
                birthDayTxt.Text = "";
            }
            DisplayPhoneNumber();
        }
        private void btnAC_Click(object sender, EventArgs e)
        {
            Num = new List<string>();
            DisplayPhoneNumber();
        }

        private void DisplayPhoneNumber()
        {
            birthDayTxt.Text = string.Join("", Num.ToArray());
        }

        private void birthDayTxt_TextChanged(object sender, EventArgs e)
        {
            string input = birthDayTxt.Text.Replace("-", ""); // Remove existing hyphens
            string formattedInput = string.Empty;

            if (input.Length > 0)
            {
                formattedInput += input.Substring(0, Math.Min(2, input.Length));

                if (input.Length > 2)
                    formattedInput += "-" + input.Substring(2, Math.Min(2, input.Length - 2));

                if (input.Length > 4)
                    formattedInput += "-" + input.Substring(4, Math.Min(4, input.Length - 4));
            }

            birthDayTxt.Text = formattedInput;
            birthDayTxt.SelectionStart = birthDayTxt.Text.Length;

            if (formattedInput.Length == 10)
            {
                // Parse the formatted input as a date
                if (DateTime.TryParseExact(formattedInput, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    // Check if the day is valid
                    int day = parsedDate.Day;
                    int month = parsedDate.Month;
                    int year = parsedDate.Year;

                    if (month < 1 || month > 12)
                    {
                        // Invalid month
                        birthDayTxt.ForeColor = Color.Red;
                        // XtraMessageBox.Show("Invalid month, please input again!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (day < 1 || day > DateTime.DaysInMonth(year, month))
                    {
                        // Invalid day
                        birthDayTxt.ForeColor = Color.Red;
                        // XtraMessageBox.Show("Invalid day, please input again!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else if (year < 1900 || year > 2100)
                    {
                        // Invalid year
                        birthDayTxt.ForeColor = Color.Red;
                        // XtraMessageBox.Show("Invalid year, please input again!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        // Valid day
                        birthDayTxt.ForeColor = SystemColors.ControlText;
                    }
                }
                else
                {
                    // Invalid date
                    birthDayTxt.ForeColor = Color.Red;
                }
            }
            else
            {
                // Reset color if the input length is not 10
                birthDayTxt.ForeColor = SystemColors.ControlText;
            }
        }


        private void ccbSelectGender_DrawItem(object sender, DevExpress.XtraEditors.ListBoxDrawItemEventArgs e)
        {
            //if (e.Index < 0)
            //    return;

            //e.Appearance.Options.UseTextOptions = true;
            //e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            //// Customize the height of the item
            //int itemHeight = 30; // Set the desired item height
            //e.Appearance.Options.UseFont = true;
            //e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, 12, FontStyle.Regular);

            //Rectangle rect = e.Bounds;
            //rect.Height = itemHeight;

            //e.Cache.FillRectangle(e.Cache.GetSolidBrush(SystemColors.Window), rect);
            //e.Appearance.DrawString(e.Cache, e.Item.ToString(), rect);
            //e.Handled = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            ParentForm.Controls.Find("panelSlider2", true)[0].Controls.Add(new FullNameCCCD());
            UserCCCD.BirthDay = birthDayTxt.Text;
            UserCCCD.Gender = ccbSelectGender.SelectedItem.ToString();
            Utils.ForwardCCCD(ParentForm, "pictureBoxInfo", "pictureBoxName", "FullNameCCCD");
            splashScreenManager1.CloseWaitForm();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            Utils.ForwardCCCD(ParentForm, "pictureBoxInfo", "pictureBoxVCCCD", "CitizenshipIDCapture");
            splashScreenManager1.CloseWaitForm();
        }
    }
}
