using Apache.NMS;
using Connect.Common;
using Connect.Common.Common;
using Connect.Common.Contract;
using Connect.Common.Helper;
using Connect.Common.Interface;
using Connect.RemoteDataProvider.Interface;
using Connect.SocketClient;
using Newtonsoft.Json;
using Parking.App.Common;
using Parking.App.Common.Helper;
using Parking.App.Contract.Common;
using Parking.App.Factory;
using Parking.App.Interface.Common;
using Parking.App.Language;
using Parking.Contract.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
namespace ManagementStore.Form.User
{
    public partial class RegisterUser : DevExpress.XtraEditors.XtraForm, IProgramController
    {
        protected static AutoResetEvent semaphore = new AutoResetEvent(false);
        protected static ITextMessage message = null;
        protected static TimeSpan receiveTimeout = TimeSpan.FromSeconds(10);
        private static string fullPathMainForm = Helpers.GetFullPathOfMainForm();
        public TimeSpan time;
        public DispatcherTimer dispatcherTimer;
        ILog _log;
        protected ISocketClient _client;
        protected System.Timers.Timer _timer;
        protected int _counter;
        public static int Counter = 10;
        private ICacheDataService<tblClientSoundMgtInfo> _tblClientSoundMgtService;
        private ICacheDataService<tblStoreDeviceInfo> _tblStoreDeviceInfoService;
        private ICacheDataService<tblAdMgtInfo> _tblAdMgtService;
        private IList<tblClientSoundMgtInfo> _listSetting;

        private int _clientSound;
        private int _clientStoreDevice;
        private readonly XMLReader _xml = new XMLReader();
        public bool IsReConnect { get; set; } = true;
        public RegisterUser()
        {
            InitializeComponent();
            panelSlider.Controls.Add(new PhoneNumber());
            panelSlider.Controls.Add(new InformationUser());
            panelSlider.Controls.Add(new FaceTaken());
            panelSlider.Controls.Add(new PhoneOTP());
            panelSlider.Controls.Add(new FullName());
        }
        private void RegisterUser_Load(object sender, EventArgs e)
        {
            try
            {
                ProgramFactory.Instance.ProgramController = this;
                _log = ProgramFactory.Instance.Log;
                AddEventCommon();
                _tblClientSoundMgtService = ProgramFactory.Instance.tblClientSoundMgtService;
                _tblAdMgtService = ProgramFactory.Instance.tblAdMgtService;
                _listSetting = ProgramFactory.Instance.tblClientSoundMgtInfos;
                _tblStoreDeviceInfoService = ProgramFactory.Instance.tblStoreDeviceService;


                //_clientStoreDevice = _tblStoreDeviceInfoService.RegisterClient(_tblStoreDeviceInfoService.GetType().Name, StoreDeviceSynchronized);

                //_tblStoreDeviceInfoService.SetCustomizedListener(_clientStoreDevice, SendStoreEvent);

            }
            catch (Exception ex)
            {
                _log.SError(this.GetType().Name, ex.Message, ex.StackTrace, ex.Message);
            }
        }

        private void SendStoreEvent(object sender, EventArgs<ResultInfo> e)
        {
            try
            {
                var items = e.Data;
                tblStoreDeviceInfo deviceItem = JsonHelper.JsonToInfo<tblStoreDeviceInfo>(String.Empty + items.Data);

                if (deviceItem != null && deviceItem.StoreNo == ConfigClass.StoreNo)
                {
                    if (!string.IsNullOrEmpty(deviceItem.ListDeviceKeyNo))
                    {
                        var ListDeviceKey = JsonConvert.DeserializeObject<List<string>>(deviceItem.ListDeviceKeyNo);
                        if (ListDeviceKey.Contains(ConfigClass.DeviceKey))
                        {
                            ConfigClass.FaceOkDeviceKey = deviceItem.DeviceKeyNo;
                        }
                    }

                    if (deviceItem.DeviceKeyNo == ConfigClass.DeviceKey && deviceItem.DeviceStatus == false)
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Sound Exception" + ex.Message);
            }
        }
        private void StoreDeviceSynchronized(object sender, EventArgs<int> e)
        {

            _tblStoreDeviceInfoService.GetDataIsActivityAsync(999999).ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    if (t.Result.Status)
                    {
                        var listData = t.Result.Data as IList<tblStoreDeviceInfo>;
                        if (listData != null)
                        {
                            try
                            {

                                if (ConfigClass.StoreNo == 0 || string.IsNullOrEmpty(ConfigClass.DeviceKey) || ConfigClass.StoreDeviceNo == 0)
                                {

                                    return;
                                }

                                foreach (var item in listData)
                                {
                                    if (!string.IsNullOrEmpty(item.ListDeviceKeyNo) && item.StoreNo == ConfigClass.StoreNo && item.DeviceType == "DVC002")
                                    {
                                        var ListDeviceKey = JsonConvert.DeserializeObject<List<string>>(item.ListDeviceKeyNo);
                                        if (ListDeviceKey.Contains(ConfigClass.DeviceKey))
                                        {
                                            ConfigClass.FaceOkDeviceKey = item.DeviceKeyNo;
                                            break;
                                        }
                                    }
                                }
                            }
                            catch
                            {

                                throw;
                            }
                        }
                    }
                    else
                    {

                    }
                }
            });
        }
        public void StartTimer()
        {
            time = TimeSpan.FromSeconds(60);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();
        }

        public void StopTimer()
        {
            if (dispatcherTimer != null && dispatcherTimer.IsEnabled)
                dispatcherTimer.Stop();
        }
        public void ConnectSuccess(ServerInfo info)
        {
            if (_client == null) return;
            try
            {
                _client.UpdateIP(info.IPServer);
                _client.UpdatePort(info.Port ?? 0);
                _client.Connect();
            }
            catch (Exception ex)
            {
                _log.SError(this.GetType().Name, ex.Message, ex.StackTrace, ex.Message);
            }
        }

        public void LoginSuccess(SessionInfo info)
        {
            
        }

        public void SetStatus(string description)
        {
           
        }
        protected virtual void OnShown()
        {
            _client.Connect();
        }

        protected virtual void Onload()
        {
            _client = ProgramFactory.Instance.SocketClientServer;
            _client.Connected += OnServerConnected;
            _client.Disconnected += OnServerDisconnected;
            ProgramFactory.Instance.ClientSessionHandler.SessionError += ClientSessionHandler_SessionError;
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
                    IsReConnect = false;
                    break;
                case ConnectionFailedReason.InvalidLicenseKey:
                    mes = LSystem.LConnectionFailedReason_InvalidLicenseKey;
                    {
                        IsReConnect = false;
                        break;
                    }
                case ConnectionFailedReason.MacError:
                    mes = LSystem.LConnectionFailedReason_Unknown;
                    IsReConnect = false;
                    break;
                default:
                    mes = LSystem.LConnectionFailedReason_Default;
                    break;
            }
            _client.Disconnect(mes);
            if (_timer != null)
            {
                _timer.Stop();
            }
        }

        protected virtual void OnServerConnected(object sender, EventArgs<ITcpClientHandler> e)
        {

        }
        protected virtual void OnServerDisconnected(object sender, EventArgs<string> e)
        {
            if (IsReConnect)
            {


                _timer = new System.Timers.Timer { Interval = 1000 };
                _timer.Elapsed -= TimerOnTick;
                _timer.Elapsed += TimerOnTick;
                _timer.Start();
            }
            else
            {

            }
        }
        protected virtual void TimerOnTick(object sender, EventArgs eventArgs)
        {
            _timer.Stop();
            _client.ReConnect();
        }
        protected virtual void AddEventCommon()
        {

            Onload();
            OnShown();
            //FormClosing();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(60);

        }

        protected virtual void FormClosing()
        {
            try
            {
                Process[] runingProcess = Process.GetProcesses();
                // _faceDetect.Dispose();
                var process = runingProcess.Where(t => t.ProcessName.Contains("ManagementStore") || t.ProcessName.Contains("Kiosk.Update")).ToList();
                if (process != null && process.Any())
                {
                    foreach (var item in process)
                    {
                        item.Kill();
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                _log.SError(this.GetType().Name, ex.Message, ex.StackTrace, ex.Message);
                this.Close();
            }

        }
    }
}