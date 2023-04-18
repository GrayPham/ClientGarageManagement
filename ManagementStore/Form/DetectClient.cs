using Connect.Common;
using Connect.Common.Common;
using Connect.Common.Contract;
using Connect.Common.Interface;
using Connect.Common.Languages;
using Connect.SocketClient;
using DevExpress.Images;
using Parking.App.Factory;
using DevExpress.XtraCharts;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using ManagementStore.Common;
using ManagementStore.DTO;
using ManagementStore.Form.Camera;
using ManagementStore.Services;
using Parking.App.Contract.Common;
using Parking.App.Interface.Common;
using Parking.App.Language;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace ManagementStore.Form
{
    public partial class DetectClient : DevExpress.XtraBars.Ribbon.RibbonForm, IProgramController
    {
        private VideoCapture capture = new VideoCapture();
        private int count = 0;

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
            loadCamera();
            //loadCamera();
            // Connect FastAPI
            if (encode.OpenConnect())
            {
                ModelConfig.socketOpen = true;
            }
            //webSocket.ConnectAsync(uri, cancellationTokenSource.Token);
        }
        private void DetectClient_Load(object sender, EventArgs e)
        {
            ProgramFactory.Instance.ProgramController = this;
            _log = ProgramFactory.Instance.Log;
            AddEventCommon();
            barItemIP.Caption = " IP:" + ProgramFactory.Instance.IPServer;
            barItemVersion.Caption = LSystem.LVersion + ApplicationInfo.VersionName;
            barItemPort.Caption = string.Format(LSystem.LPort, ApplicationInfo.PortUser);
        }
        private void ProcessFrame(object sender, EventArgs arg)
        {
            //Image image = new Image();
            //detect.detect(image);

        private void DetectClient_Load(object sender, EventArgs e)
        {
            if(count > 1)
            {
                PictureControl pictureControl = new PictureControl(0, encode);
                panelIn.Controls.Add(pictureControl);
                PictureControl pictureControl1 = new PictureControl(1, encode);
                panelOut.Controls.Add(pictureControl1);
            }else if (count == 1)
            {
                PictureControl pictureControl = new PictureControl(0, encode);
                panelIn.Controls.Add(pictureControl);
            }

        }
        private void loadCamera()
        {
            for (int i = 0; i < 7; i++)
            {
                capture = new VideoCapture(i);

                if (capture.IsOpened)
                {
                    count++;
                }
            }

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
            _client.Disconnect();
            _timer.Stop();
        }
        #endregion


    }
}