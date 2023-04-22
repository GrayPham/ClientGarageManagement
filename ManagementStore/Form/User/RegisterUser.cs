using Apache.NMS;
using Connect.Common.Contract;
using Connect.Common.Interface;
using Connect.RemoteDataProvider.Interface;
using Connect.SocketClient;
using Parking.App.Common.Helper;
using Parking.App.Contract.Common;
using Parking.App.Factory;
using Parking.App.Interface.Common;
using Parking.Contract.Common;
using System;
using System.Collections.Generic;
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

        public void ConnectSuccess(ServerInfo info)
        {
            throw new System.NotImplementedException();
        }

        public void LoginSuccess(SessionInfo info)
        {
            throw new System.NotImplementedException();
        }

        public void SetStatus(string description)
        {
            throw new System.NotImplementedException();
        }

        private void RegisterUser_Load(object sender, EventArgs e)
        {
            _tblClientSoundMgtService = ProgramFactory.Instance.tblClientSoundMgtService;
            _tblAdMgtService = ProgramFactory.Instance.tblAdMgtService;
            _listSetting = ProgramFactory.Instance.tblClientSoundMgtInfos;
            _tblStoreDeviceInfoService = ProgramFactory.Instance.tblStoreDeviceService;
        }
    }
}