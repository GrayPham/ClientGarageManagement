using Connect.Common;
using Connect.Common.Common;
using Connect.Common.Contract;
using Connect.Common.Interface;
using Connect.Common.Languages;
using Connect.SocketClient;
using DevExpress.Images;
using ManagementStore.Form.User;
using Parking.App.Contract.Common;
using Parking.App.Factory;
using Parking.App.Interface.Common;
using Parking.App.Language;
using System;
using System.Threading;

namespace ManagementStore.Form
{
    public partial class Main : DevExpress.XtraBars.Ribbon.RibbonForm, IProgramController
    {

        ILog _log;
        private ISocketClient _client;
        private System.Timers.Timer _timer;
        private int _counter;
        private static int Counter = 10;

        public Main()
        {
            _log = ProgramFactory.Instance.Log;
            InitializeComponent();
         
        }
        private void Main_Load(object sender, EventArgs e)
        {
            ProgramFactory.Instance.ProgramController = this;
            _log = ProgramFactory.Instance.Log;
            AddEventCommon();
            barItemIP.Caption = "IP:" + ProgramFactory.Instance.IPServer;
            barItemVersion.Caption = LSystem.LVersion + ApplicationInfo.VersionName;
            barItemPort.Caption = string.Format(LSystem.LPort, ApplicationInfo.PortUser);
        }
        private void pictureBoxParking_Click(object sender, EventArgs e)
        {
            // splashScreenManager1.ShowWaitForm();
            DetectClient form = new DetectClient();
            form.Show();
            Hide();
            Thread.Sleep(1000);
            // splashScreenManager1.CloseWaitForm();
        }

        private void pictureBoxDepart_Click(object sender, EventArgs e)
        {
            // splashScreenManager1.ShowWaitForm();
            Home home = new Home(ProgramFactory.Instance.tblAdMgtService);
            home.Show();
            Hide();
            Thread.Sleep(1000);
            // splashScreenManager1.CloseWaitForm();
        }

        private void pictureBoxTracking_Click(object sender, EventArgs e)
        {

        }

        #region IProgramController

        public void ConnectSuccess(ServerInfo info)
        {
            if (_client == null) return;

            _client.UpdateIP(info.IPServer);
            _client.UpdatePort(info.Port ?? 0);

            barItemIP.Caption = "IP:" + info.IPServer;
            barItemVersion.Caption = "" + info.Port ?? 0 + "||" + LSystem.LVersion + ApplicationInfo.VersionName;
            _client.Connect();
        }

        public void LoginSuccess(SessionInfo info)
        {

        }

        public void SetStatus(string description)
        {
            barItemConnect.Caption = description;
        }

        protected virtual void OnShown()
        {
            _client.Connect();
        }

        protected virtual void AddEventCommon()
        {
            Onload();
            OnShown();
        }
        protected virtual void Onload()
        {
            barItemConnect.Caption = FWLanguages.LSetupConnect;
            _client = ProgramFactory.Instance.SocketClientServer;
            _client.Connected += OnServerConnected;
            _client.Disconnected += OnServerDisconnected;
            ProgramFactory.Instance.ClientSessionHandler.SessionError += ClientSessionHandler_SessionError;
        }
        protected virtual void TimerOnTick(object sender, EventArgs eventArgs)
        {
            _counter--;

            barItemConnect.Caption = string.Format(FWLanguages.LReConnect, _counter);
            barItemConnect.ItemAppearance.Normal.ForeColor = System.Drawing.Color.HotPink;
            barItemConnect.Glyph = ImageResourceCache.Default.GetImage("images/communication/radio_16x16.png");
            if (_counter > 0) return;

            barItemConnect.Caption = FWLanguages.LSetupConnect + " ";
            barItemConnect.ItemAppearance.Normal.ForeColor = System.Drawing.Color.White;
            barItemConnect.Glyph = ImageResourceCache.Default.GetImage("images/programming/technology_16x16.png");

            _timer.Stop();
            _client.ReConnect();

        }
        protected virtual void OnServerConnected(object sender, EventArgs<ITcpClientHandler> e)
        {
            barItemConnect.Caption = FWLanguages.LConnectSuccessfully;
            barItemConnect.ItemAppearance.Normal.ForeColor = System.Drawing.Color.GreenYellow;
            barItemConnect.Glyph = ImageResourceCache.Default.GetImage("images/tasks/status_16x16.png");
        }
        protected virtual void OnServerDisconnected(object sender, EventArgs<string> e)
        {
            _counter = Counter;
            barItemConnect.Caption = e.Data + string.Format(FWLanguages.LReConnect, _counter);
            barItemConnect.ItemAppearance.Normal.ForeColor = System.Drawing.Color.HotPink;
            barItemConnect.Glyph = ImageResourceCache.Default.GetImage("images/communication/wifi_16x16.png");

            _timer = new System.Timers.Timer { Interval = 1000 };
            _timer.Elapsed -= TimerOnTick;
            _timer.Elapsed += TimerOnTick;
            _timer.Start();


        }
        private void ClientSessionHandler_SessionError(object sender, EventArgs<ConnectionFailedReason> e)
        {
            string mes = "";
            switch (e.Data)
            {
                case ConnectionFailedReason.Unknown:
                    mes = LSystem.LConnectionFailedReason_Unknown;
                    break;
                case ConnectionFailedReason.InvalidSerialNumber:
                    mes = LSystem.LConnectionFailedReason_InvalidSerialNumber;
                    break;
                case ConnectionFailedReason.InvalidLicenseKey:
                    mes = LSystem.LConnectionFailedReason_InvalidLicenseKey;
                    break;
                case ConnectionFailedReason.MacError:
                    mes = LSystem.LConnectionFailedReason_Unknown;
                    break;
                default:
                    mes = LSystem.LConnectionFailedReason_Default;
                    break;
            }
            _client.Disconnect(mes);
            _timer.Stop();
        }
        #endregion


    }
}