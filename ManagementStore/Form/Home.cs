using System;
using System.Threading;
using Connect.Common;
using Connect.Common.Common;
using Connect.Common.Contract;
using Connect.Common.Interface;
using Connect.Common.Languages;
using Connect.SocketClient;
using DevExpress.Images;
using ManagementStore.Extensions;
using ManagementStore.Form.User;
using Parking.App.Common.Helper;
using Parking.App.Contract.Common;
using Parking.App.Factory;
using Parking.App.Interface.Common;
using Parking.App.Language;

namespace ManagementStore.Form
{
    public partial class Home : DevExpress.XtraEditors.XtraForm, IProgramController
    {
        ILog _log;
        private ISocketClient _client;
        private System.Timers.Timer _timer;
        private int _counter;
        private static int Counter = 10;

        public Home()
        {
            _log = ProgramFactory.Instance.Log;
           
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            Helpers.PlaySound(@"Assets\Audio\reigsterUser.wav");
            ProgramFactory.Instance.ProgramController = this;
            _log = ProgramFactory.Instance.Log;
            AddEventCommon();
            barItemIP.Caption = "IP:" + ProgramFactory.Instance.IPServer;
            barItemVersion.Caption = LSystem.LVersion + ApplicationInfo.VersionName;
            barItemPort.Caption = string.Format(LSystem.LPort, ApplicationInfo.PortUser);

            //string html = "<html><head>";
            //string url = "https://www.youtube.com/watch?v=Z9uEn2IVPkQ";
            //html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";
            //html += "<iframe id='video' src= 'https://www.youtube.com/embed/{0}?autoplay=1' width='680' height='370' frameborder='0' allowfullscreen></iframe>";
            //html += "</body></html>";
            //this.webBrowserVideo.DocumentText = string.Format(html, Utils.GetVideoId(url));
        }


        private void btnIdentity_Click(object sender, EventArgs e)
        {
            splashScreenManager.ShowWaitForm();
            Thread.Sleep(1000);
            TypeRegister registerUser = new TypeRegister();
            registerUser.Show();
            cameraControl.Stop();
            this.webBrowserVideo.DocumentText = "";
            Helpers.StopSound();
            Hide();
            splashScreenManager.CloseWaitForm();
        }

        public void LoginSuccess(SessionInfo info)
        {
            throw new NotImplementedException();
        }

        public void ConnectSuccess(ServerInfo info)
        {
            if (_client == null) return;

            _client.UpdateIP(info.IPServer);
            _client.UpdatePort(info.Port ?? 0);

            barItemIP.Caption = "IP:" + info.IPServer;
            barItemVersion.Caption = "" + info.Port ?? 0 + "||" + LSystem.LVersion + ApplicationInfo.VersionName;
            _client.Connect();
        }

        public void SetStatus(string description)
        {
            barItemConnect.Caption = description;
        }


        protected virtual void AddEventCommon()
        {
            Onload();
            OnShown();
        }
        protected virtual void OnShown()
        {
            _client.Connect();
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
    }
}