using ManagementStore.Model.Static;
using Parking.App.Common.Helper;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ManagementStore.Form.User
{
    public partial class TypeRegister : DevExpress.XtraEditors.XtraForm
    {
        private Timer timer;
        private int countDown = 0;
        public TypeRegister()
        {
            InitializeComponent();
            Helpers.PlaySound(@"Assets\Audio\typeRegister.wav");
            sidePanel3.BackColor = ColorTranslator.FromHtml("#2980b9");
            sidePanel4.BackColor = ColorTranslator.FromHtml("#2980b9");
            pictureBox5.BackColor = ColorTranslator.FromHtml("#2980b9");
            labelControl2.BackColor = ColorTranslator.FromHtml("#2980b9");
            showCountDown.BackColor = ColorTranslator.FromHtml("#2980b9");
            //btmExit.BackColor = ColorTranslator.FromHtml("#aeb6bf");
            countDown = 30;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            countDown--;
            showCountDown.Text = $"Close form after {countDown} seconds";

            // When the countdown reaches 0, stop the Timer and capture the picture
            if (countDown == 0)
            {
                timer.Stop();

            }
        }

        private void pictureBoxPhone_Click(object sender, System.EventArgs e)
        {
            Helpers.StopSound();
            RegisterUser registerUser = new RegisterUser();
            registerUser.Show();
            this.Hide();
        }

        private void TypeRegister_Load(object sender, System.EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;
            // Start the Timer
            timer.Start();
        }

        private void pictureBoxCard_Click(object sender, EventArgs e)
        {
            Helpers.StopSound();
            RegisterUser2 registerUserCard = new RegisterUser2();
            registerUserCard.Show();
            this.Close();
        }

        private void btmExit_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}