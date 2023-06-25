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

        public RegisterUser()
        {
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
    }
}