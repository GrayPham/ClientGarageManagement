using Connect.Common.Contract;
using Connect.Common.Interface;
using Connect.SocketClient;
using Kiosk.App.Factory;
using ManagementStore.Services;
using Parking.App.Contract.Common;
using Parking.App.Interface.Common;
using Parking.App.Language;
using System;

namespace ManagementStore.Form
{
    public partial class DetectClient : DevExpress.XtraBars.Ribbon.RibbonForm, IProgramController
    {

        private YoloDetectServices detect = new YoloDetectServices();
        ILog _log;
        protected ISocketClient _client;
        protected System.Timers.Timer _timer;
        protected int _counter;
        public static int Counter = 10;
        
        public DetectClient()
        {
            _log = ProgramFactory.Instance.Log;
            InitializeComponent();
        }
        private void DetectClient_Load(object sender, EventArgs e)
        {
            ProgramFactory.Instance.ProgramController = this;
            _log = ProgramFactory.Instance.Log;
            barItemIP.Caption = " IP:" + ProgramFactory.Instance.IPServer;
            barItemVersion.Caption = LSystem.LVersion + ApplicationInfo.VersionName;
            barItemPort.Caption = string.Format(LSystem.LPort, ApplicationInfo.PortUser);
        }
        private void ProcessFrame(object sender, EventArgs arg)
        {
            //Image image = new Image();
            //detect.detect(image);
        }
        #region IProgramController
        void IProgramController.Close()
        {

        }
        public void ConnectSuccess(ServerInfo info)
        {
            if (_client == null) return;

            _client.UpdateIP(info.IPServer);
            _client.UpdatePort(info.Port ?? 0);

            barItemIP.Caption = " IP:" + info.IPServer;
            barItemVersion.Caption = "" + info.Port ?? 0 + "||" + LSystem.LVersion + ApplicationInfo.VersionName;
            _client.Connect();
        }

        public void LoginSuccess(SessionInfo info)
        {

        }

        public void SetStatus(string description)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}