using Connect.Common;
using Connect.Common.Common;
using Connect.Common.Contract;
using Connect.Common.Interface;
using Connect.Common.Languages;
using Connect.SocketClient;
using DevExpress.Images;
using Parking.App.Factory;
using Emgu.CV;
using ManagementStore.Common;
using ManagementStore.Form.Camera;
using Parking.App.Contract.Common;
using Parking.App.Interface.Common;
using Parking.App.Language;
using System;
using Security;

namespace ManagementStore.Form
{
    public partial class DetectClient : DevExpress.XtraBars.Ribbon.RibbonForm, IProgramController
    {
        private VideoCapture _capture = new VideoCapture();
        private int _count = 0;

        ILog _log;
        private ISocketClient _client;
        private System.Timers.Timer _timer;
        private int _counter;
        private static int Counter = 10;

        // Connect Socket 
        private SocketDetect Encode = new SocketDetect();
        public DetectClient()
        {
            _log = ProgramFactory.Instance.Log;
            InitializeComponent();
            // LoadCamera();

            // Connect FastAPI
            //if (Encode.OpenConnect())
            //{
            //    ModelConfig.socketOpen = true;
            //}
            //webSocket.ConnectAsync(uri, cancellationTokenSource.Token);
        }
        private void DetectClient_Load(object sender, EventArgs e)
        {
            ProgramFactory.Instance.ProgramController = this;
            _log = ProgramFactory.Instance.Log;
            AddEventCommon();
            barItemIP.Caption = "IP:" + ProgramFactory.Instance.IPServer;
            barItemVersion.Caption = LSystem.LVersion + ApplicationInfo.VersionName;
            barItemPort.Caption = string.Format(LSystem.LPort, ApplicationInfo.PortUser);
            //if (_count > 1)
            //{
            //    PictureControl pictureControl = new PictureControl(0, Encode);
            //    panelIn.Controls.Add(pictureControl);
            //    PictureControl pictureControl1 = new PictureControl(1, Encode);
            //    panelOut.Controls.Add(pictureControl1);
            //}
            //else if (_count == 1)
            //{
            //    PictureControl pictureControl = new PictureControl(0, Encode);
            //    panelIn.Controls.Add(pictureControl);
            //}
        }

        private void LoadCamera()
        {
            for (int i = 0; i < 7; i++)
            {
                _capture = new VideoCapture(i);

                if (_capture.IsOpened)
                {
                    _count++;
                }
            }

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
            Console.WriteLine(mes);
            _client.Disconnect();
            _timer.Stop();
        }
        #endregion


    }
}