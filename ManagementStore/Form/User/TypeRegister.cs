using Emgu.CV;
using ManagementStore.Common;
using ManagementStore.Model.Static;
using Parking.App.Common.Helper;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementStore.Form.User
{
    public partial class TypeRegister : DevExpress.XtraEditors.XtraForm
    {
        private Timer timer;
        private int countDown = 0;
        private string fileNameAudio;
        private Home _home;
        public TypeRegister(Home home)
        {
            _home = home;
            InitializeComponent();
            sidePanel3.BackColor = ColorTranslator.FromHtml("#2980b9");
            sidePanel4.BackColor = ColorTranslator.FromHtml("#2980b9");
            pictureBox5.BackColor = ColorTranslator.FromHtml("#2980b9");
            labelControl2.BackColor = ColorTranslator.FromHtml("#2980b9");
            showCountDown.BackColor = ColorTranslator.FromHtml("#2980b9");
            btmExit.BackColor = ColorTranslator.FromHtml("#aeb6bf");
            countDown = 30;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            countDown--;
            showCountDown.Text = $"Đóng biểu mẫu sau {countDown} giây";

            // When the countdown reaches 0, stop the Timer and capture the picture
            if (countDown == 0)
            {
                timer.Stop();

            }
        }

        private void pictureBoxPhone_Click(object sender, System.EventArgs e)
        {
            Helpers.StopSound();
            RegisterUser registerUser = new RegisterUser(this);
            registerUser.Show();
            this.Hide();
        }

        public async Task Load_Audio()
        {
            fileNameAudio = await AudioConstants.GetListSound(AudioConstants.TypeRegister);
            if (fileNameAudio != null && fileNameAudio != "")
            {
                Helpers.PlaySound(@"Assets\Audio\" + fileNameAudio + ".wav");
            }
            else
            {
                Helpers.PlaySound(@"Assets\DefaultAudio\" + AudioConstants.TypeRegister + ".wav");
            }
        }
        private async void TypeRegister_Load(object sender, System.EventArgs e)
        {
            await Load_Audio();
            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;
            // Start the Timer
            timer.Start();
        }

        private void pictureBoxCard_Click(object sender, EventArgs e)
        {
            Helpers.StopSound();
            RegisterUser2 registerUserCard = new RegisterUser2(this);
            registerUserCard.Show();
            this.Hide();
        }

        private void btmExit_Click(object sender, EventArgs e)
        {
            
            this.Close();
            //_home.cameraControlHome.Start();
            //_home.DisplayAd();
            //_home.Show();

        }


        private async void TypeRegister_FormClosed(object sender, FormClosedEventArgs e)
        {
            // _home.cameraControlHome.Start();
            await _home.DisplayAd();
            await _home.Load_Home_Audio();

            _home.Show();
            _home.timerAd.Tick -= _home.Timer_Tick;
            _home.timerAudio.Tick -= _home.Timer_Tick_Audio;
            _home.timerAd.Start();
            _home.timerAudio.Start();
            _home.capture = new VideoCapture(0);
            _home.capture.Start();
            
        }
    }
}