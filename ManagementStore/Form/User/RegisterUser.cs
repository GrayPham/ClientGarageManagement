using Connect.Common;
using Connect.Common.Common;
using Connect.Common.Contract;
using Connect.SocketClient;
using Parking.App.Contract.Common;
using Parking.App.Interface.Common;
using System;
namespace ManagementStore.Form.User
{
    public partial class RegisterUser : DevExpress.XtraEditors.XtraForm, IProgramController
    {

        public RegisterUser()
        {
            InitializeComponent();
            panelSlider.Controls.Add(new FullName());

            panelSlider.Controls.Add(new PhoneNumber());
            panelSlider.Controls.Add(new InformationUser());
            panelSlider.Controls.Add(new FaceTaken());
            panelSlider.Controls.Add(new PhoneOTP());
        }
        private void RegisterUser_Load(object sender, EventArgs e)
        {
            
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