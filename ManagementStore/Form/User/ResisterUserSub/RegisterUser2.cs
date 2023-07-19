using DevExpress.XtraEditors;
using ManagementStore.Form.User.ResisterUserSub;
using ManagementStore.Model.Static;
using Parking.App.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementStore.Form.User
{
    public partial class RegisterUser2 : DevExpress.XtraEditors.XtraForm
    {
        private TypeRegister _typeRegister;
        private Timer timer;
        public RegisterUser2(TypeRegister typeRegister)
        {
            _typeRegister = typeRegister;
            InitializeComponent();
            panelSlider2.Controls.Add(new CitizenshipID());
            //panelSlider2.Controls.Add(new CitizenshipIDCapture());
            Settings.countDown = 120;
            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;

            // Start the Timer
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            Settings.countDown--;
            showCountDown.Text = $"Đóng biểu mẫu sau {Settings.countDown.ToString()} giây";

            // When the countdown reaches 0, stop the Timer and capture the picture
            if (Settings.countDown == 0)
            {
                timer.Stop();

            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            

            var citizenCapture = panelSlider2.Controls.Find("CitizenshipIDCapture", true);
            if (citizenCapture.Length > 0)
            {
                var controlToRemove = citizenCapture[0] as CitizenshipIDCapture;
                controlToRemove.capture.Dispose();
                Application.Idle -= controlToRemove.Capture_ImageGrabbed;
                controlToRemove.timer.Tick -= controlToRemove.Timer_TickAsync;
                panelSlider2.Controls.Remove(controlToRemove);
                controlToRemove.Dispose();
            }
            var citizenCaptureFace = panelSlider2.Controls.Find("FaceTakenCCCD", true);
            if (citizenCapture.Length > 0)
            {
                var controlToRemove = citizenCapture[0] as FaceTakenCCCD;
                controlToRemove.capture.Dispose();
                Application.Idle -= controlToRemove.Capture_ImageGrabbed;
                controlToRemove.timer.Tick -= controlToRemove.Timer_Tick;
                panelSlider2.Controls.Remove(controlToRemove);
                controlToRemove.Dispose();
            }
            panelSlider2.Controls.Clear();
            sidePanel4.Controls.Clear();
            panelSlider2.Dispose();
            sidePanel4.Dispose();
            pictureEdit1.Dispose();
            sidePanel1.Dispose();

            _typeRegister.Invoke(new Action(() =>
            {
                Helpers.StopSound();
                _typeRegister.Show();

            }));
            timer.Tick -= Timer_Tick;
            Close();
        }

        private void RegisterUser2_FormClosed(object sender, FormClosedEventArgs e)
        {
            panelSlider2.Controls.Clear();
            sidePanel4.Controls.Clear();


            panelSlider2.Dispose();
            sidePanel4.Dispose();
            pictureEdit1.Dispose();
            sidePanel1.Dispose();

            _typeRegister.Invoke(new Action(() =>
            {
                Helpers.StopSound();
                _typeRegister.Show();
            }));
            timer.Tick -= Timer_Tick;

        }
    }
}