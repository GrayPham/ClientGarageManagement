using Connect.Common;
using Connect.Common.Common;
using Connect.Common.Contract;
using Connect.SocketClient;
using ManagementStore.Model.Static;
using Parking.App.Common.Helper;
using Parking.App.Contract.Common;
using Parking.App.Interface.Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ManagementStore.Form.User
{
    public partial class RegisterUser : DevExpress.XtraEditors.XtraForm, IProgramController
    {
        private Timer timer;
        private Home _home;

        public RegisterUser(Home home)
        {
            _home = home;
            InitializeComponent();
            panelSlider.Controls.Add(new PhoneNumber());
            //panelSlider.Controls.Add(new PhoneOTP());
            //panelSlider.Controls.Add(new InformationUser());
            //panelSlider.Controls.Add(new FullName());
            // panelSlider.Controls.Add(new FaceTaken());
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
            showCountDown.Text = $"Close form after {Settings.countDown.ToString()} seconds";

            // When the countdown reaches 0, stop the Timer and capture the picture
            if (Settings.countDown == 0)
            { 
                timer.Stop();

            }
        }

        private void RegisterUser_Load(object sender, EventArgs e)
        {
            pictureBoxNotify.BackColor = ColorTranslator.FromHtml("#2980b9");
            labelControl2.BackColor = ColorTranslator.FromHtml("#2980b9");
            labelControl3.BackColor = ColorTranslator.FromHtml("#2980b9");
            showCountDown.BackColor = ColorTranslator.FromHtml("#2980b9");
        }
        public void LoginSuccess(SessionInfo info)
        {
            
        }
        public void SetStatus(string description)
        {
           
        }
        protected virtual void OnShown()
        {

        }

        protected virtual void Onload()
        {
           
        }
        private void ClientSessionHandler_SessionError(object sender, EventArgs<ConnectionFailedReason> e)
        {
            
        }

        protected virtual void OnServerConnected(object sender, EventArgs<ITcpClientHandler> e)
        {

        }
        protected virtual void OnServerDisconnected(object sender, EventArgs<string> e)
        {

        }
        protected virtual void TimerOnTick(object sender, EventArgs eventArgs)
        {

        }
        protected virtual void AddEventCommon()
        {

            Onload();
            OnShown();

        }

        protected virtual void FormClosing()
        {
           

        }

        public void ConnectSuccess(ServerInfo info)
        {
            throw new NotImplementedException();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var citizenCapture = panelSlider.Controls.Find("FaceTaken", true);
            if (citizenCapture.Length > 0)
            {
                var controlToRemove = citizenCapture[0] as FaceTaken;
                controlToRemove.capture.Dispose();
                Application.Idle -= controlToRemove.Capture_ImageGrabbedSSD;
                controlToRemove.timer.Tick -= controlToRemove.Timer_Tick;
                panelSlider.Controls.Remove(controlToRemove);
                controlToRemove.Dispose();
            }
            panelSlider.Controls.Clear();
            sidePanel4.Controls.Clear();
            panelSlider.Dispose();
            sidePanel4.Dispose();
            pictureEdit1.Dispose();
            sidePanel1.Dispose();

            _home.Invoke(new Action(() =>
            {
                _home.Show();
                _home.cameraControl.Start();
            }));

            Close();
        }

        private void RegisterUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            var citizenCapture = panelSlider.Controls.Find("FaceTaken", true);
            if (citizenCapture.Length > 0)
            {
                var controlToRemove = citizenCapture[0] as FaceTaken;
                controlToRemove.capture.Dispose();
                Application.Idle -= controlToRemove.Capture_ImageGrabbedSSD;
                controlToRemove.timer.Tick -= controlToRemove.Timer_Tick;
                panelSlider.Controls.Remove(controlToRemove);
                controlToRemove.Dispose();
            }
            panelSlider.Controls.Clear();
            sidePanel4.Controls.Clear();
            panelSlider.Dispose();
            sidePanel4.Dispose();
            pictureEdit1.Dispose();
            sidePanel1.Dispose();

            _home.Invoke(new Action(() =>
            {
                _home.Show();
                _home.cameraControl.Start();
            }));
        }
    }
}