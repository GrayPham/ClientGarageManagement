﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Connect.Common;
using Connect.Common.Common;
using Connect.Common.Contract;
using Connect.Common.Helper;
using Connect.Common.Interface;
using Connect.Common.Languages;
using Connect.RemoteDataProvider.Interface;
using Connect.SocketClient;
using DevExpress.Images;
using Emgu.CV;
using Emgu.CV.Structure;
using ManagementStore.Common;
using ManagementStore.Extensions;
using ManagementStore.Form.User;
using NAudio.Wave;
using Newtonsoft.Json;
using Parking.App.Common;
using Parking.App.Common.ApiMethod;
using Parking.App.Common.Helper;
using Parking.App.Common.ViewModels;
using Parking.App.Contract.Common;
using Parking.App.Factory;
using Parking.App.Interface.Common;
using Parking.App.Language;
using Parking.App.Service.Common;
using Parking.Contract.Common;

namespace ManagementStore.Form
{
    public partial class Home : DevExpress.XtraEditors.XtraForm, IProgramController
    {
        ILog _log;
        private ISocketClient _client;
        private System.Timers.Timer _timer;
        private int _counter;
        private static int Counter = 10;
        private string fileNameAudio;
        bool active = true;
        Mat frame;
        //private const int soundAudioNo = 123;

        private static string fullPathMainForm = Helpers.GetFullPathOfMainForm();
        //**------------------------------------------------------------------------
        private ICacheDataService<tblClientSoundMgtInfo> _tblClientSoundMgtService;
        private ICacheDataService<tblStoreDeviceInfo> _tblStoreDeviceInfoService;
        private ICacheDataService<tblAdMgtInfo> _tblAdMgtServiceInfo;
        private readonly tblAdMgtService _tblAdMgtService;
        private IList<tblClientSoundMgtInfo> _listSetting;

        private readonly XMLReader _xml = new XMLReader();
        private int _clientSound;
        private int _clientStoreDevice;
        public bool IsReConnect { get; set; } = true;
        public static bool isCanChangeTheAd = true;
        public static bool isCanModify { get; set; } = false;
        public static bool isShouldOpenCamera { get; set; } = false;
        //**------------------------------------------------------

        public TimeSpan timeTop;
        public DispatcherTimer dispatcherTimerTop;

        public TimeSpan timeBot;
        public DispatcherTimer dispatcherTimerBot;
        public Home(tblAdMgtService tblAdMgtService)
        {
            _log = ProgramFactory.Instance.Log;
            _tblAdMgtService = tblAdMgtService;
            InitializeComponent();
        }

        private string AdHtml()
        {
            string html = "<html><head>";
            html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";
            html += "</head><body>";
            html += "<iframe id='video' src='https://www.youtube.com/embed/{0}?autoplay=1&mute=1' width='725px' height='400px' frameborder='0' allowfullscreen></iframe>";
            html += "</body></html>";
            return html;
        }
        private async void Home_Load(object sender, EventArgs e)
        {
            fileNameAudio= await AudioConstants.GetListSound(AudioConstants.HomeAudio);
            if(fileNameAudio != null && fileNameAudio != "")
            {
                Helpers.PlaySound(@"Assets\Audio\"+ fileNameAudio + ".wav");
            }
            else
            {
                Helpers.PlaySound(@"Assets\DefaultAudio\" + AudioConstants.HomeAudio + ".wav");
            }
            
            ProgramFactory.Instance.ProgramController = this;
            _log = ProgramFactory.Instance.Log;
            AddEventCommon();
            barItemIP.Caption = "IP:" + ProgramFactory.Instance.IPServer;
            barItemVersion.Caption = LSystem.LVersion + ApplicationInfo.VersionName;
            barItemPort.Caption = string.Format(LSystem.LPort, ApplicationInfo.PortUser);
            _tblClientSoundMgtService = ProgramFactory.Instance.tblClientSoundMgtService;
            _tblAdMgtServiceInfo = ProgramFactory.Instance.tblAdMgtService;
            _listSetting = ProgramFactory.Instance.tblClientSoundMgtInfos;
            _tblStoreDeviceInfoService = ProgramFactory.Instance.tblStoreDeviceService;

            _clientStoreDevice = _tblStoreDeviceInfoService.RegisterClient(_tblStoreDeviceInfoService.GetType().Name, StoreDeviceSynchronized);
            //_clientSound = _tblClientSoundMgtService.RegisterClient(_tblClientSoundMgtService.GetType().Name, SoundSynchronized);
            //_tblClientSoundMgtService.SetAddedListener(_clientSound, ClientSoundAdd);
            //_tblClientSoundMgtService.SetListAddedListener(_clientSound, ClientSoundListAdd);
            //_tblClientSoundMgtService.SetRemovedListener(_clientSound, ClientSoundRemoved);
            //_tblClientSoundMgtService.SetUpdatedListener(_clientSound, ClientSoundUpdated);
            var _adMgtClient = _tblAdMgtService.RegisterClient(_tblAdMgtService.GetType().Name, AdMgtSynchronized);
            _tblAdMgtService.AddCompleted += AdMgt_Added;
            _tblAdMgtService.UpdateCompleted += AdMgt_Updated;
            _tblAdMgtService.RemoveCompleted += AdMgt_Removed;
           //  _tblClientSoundMgtService.SetCustomizedListener(_clientSound, SendSoundEvent);
            _tblStoreDeviceInfoService.SetCustomizedListener(_clientStoreDevice, SendStoreEvent);


            
            string url = "https://www.youtube.com/watch?v=Z9uEn2IVPkQ";
            this.webBrowserVideo.DocumentText = string.Format(AdHtml(), Utils.GetVideoId(url));


        }
       
        private void AdMgtSynchronized(object sender, EventArgs<int> e)
        {

            if (isCanChangeTheAd)
                DisplayAd();
        }
        private void AdMgt_Removed(object sender, EventArgs<object> e)
        {

            if (isCanChangeTheAd)
                DisplayAd();
        }
        private void AdMgt_Updated(object sender, EventArgs<tblAdMgtInfo> e)
        {

            if (isCanChangeTheAd)
                DisplayAd();
        }
        private void AdMgt_Added(object sender, EventArgs<tblAdMgtInfo> e)
        {

            if (isCanChangeTheAd)
                DisplayAd();
        }
        public Task DisplayAd()
        {
            return Task.Factory.StartNew(async () =>
            {
                try
                {
                    tblAdMgtInfo adMgtInfo = new tblAdMgtInfo();
                    tblAdStoreMgtInfo tblAdStore = new tblAdStoreMgtInfo();

                    Datas dataAdMgt = new Datas
                    {
                        Data = adMgtInfo
                    };

                    Datas dataAdStore = new Datas
                    {
                        Data = tblAdStore
                    };

                    DataRequest dataAdMgtRequest = new DataRequest()
                    {
                        Signature = 101,
                        FrameID = 0,
                        FunctionCode = 12292,
                        DataLength = 0,
                        Data = dataAdMgt
                    };

                    DataRequest dataAdStoreRequest = new DataRequest()
                    {
                        Signature = 115,
                        FrameID = 0,
                        FunctionCode = 12292,
                        DataLength = 0,
                        Data = dataAdStore
                    };
                    var data1 = await ApiMethod.PostCall(dataAdMgtRequest);
                    var data2 = await ApiMethod.PostCall(dataAdStoreRequest);

                    if (data1.StatusCode != HttpStatusCode.OK || data2.StatusCode != HttpStatusCode.OK)
                        return;

                    RequestInfo data1Get = JsonConvert.DeserializeObject<RequestInfo>(data1.Content.ReadAsStringAsync().Result);
                    List<tblAdMgtInfo> AdMgtData = Helpers.ConvertObjectToListModel<tblAdMgtInfo>(data1Get.Data);


                    RequestInfo data2Get = JsonConvert.DeserializeObject<RequestInfo>(data2.Content.ReadAsStringAsync().Result);
                    List<tblAdStoreMgtInfo> AdStoreData = JsonHelper.JsonToListInfo<tblAdStoreMgtInfo>(string.Empty + data2Get.Data);

                    tblAdMgtInfo AdTopShow = null;
                    tblAdMgtInfo AdBotShow = null;

                    List<tblAdMgtInfo> listAd = new List<tblAdMgtInfo>();
                    var innerJoin = from s in AdMgtData // outer sequence
                                    join st in AdStoreData //inner sequence 
                                    on s.AdNo equals st.AdNo
                                    where st.StoreNo == ConfigClass.StoreNo
                                    select s;

                    var dateNow = DateTime.Now;

                    // isShouldOpenCamera = (BetweenTimeOfDayCamera(DateTime.Now, ConfigClass.TimeStart, ConfigClass.TimeEnd));


                    var AdValid = innerJoin.Where(x => (Between(dateNow, x.PeriodStartDate, x.PeriodEndDate) && x.AdStatus)).ToList();
                    if (AdValid != null)
                    {
                        var ListAdTopShow = AdValid.Where(x => (BetweenTimeOfDay(dateNow, x.DayStartTime, x.DayEndTime) && x.AdLocation)).ToList();
                        if (ListAdTopShow != null && ListAdTopShow.Any())
                        {
                            AdTopShow = ListAdTopShow.OrderByDescending(dt => dt.RegistDate).FirstOrDefault();
                        }
                        else
                        {
                           
                        }

                        var ListAdBotShow = AdValid.Where(x => (BetweenTimeOfDay(dateNow, x.DayStartTime, x.DayEndTime) && !x.AdLocation)).ToList();
                        if (ListAdBotShow != null && ListAdBotShow.Any())
                        {
                            AdBotShow = ListAdBotShow.OrderByDescending(dt => dt.RegistDate).FirstOrDefault();
                        }
                        else
                        {
                           
                        }
                    }

                    if (AdTopShow != null && !isShouldOpenCamera)
                    {
                        if (AdTopShow.AdType == "AD0001")
                        {


                            var filePath = CheckPathExtension(Helpers.fullPathMainForm + @"AdMgt\" + AdTopShow.AdName);
                            if (!string.IsNullOrWhiteSpace(filePath))
                            {

                            }


                        }
                        else if (AdTopShow.AdType == "AD0002")
                        {
                            if (AdTopShow.AttachFilePath.Contains("youtu"))
                            {
                                Task.Factory.StartNew(() =>
                                {

                                });
                            }
                            else
                            {
                                var filePath = CheckPathExtension(Helpers.fullPathMainForm + @"AdMgt\" + AdTopShow.AdName);
                                if (!string.IsNullOrWhiteSpace(AdTopShow.AttachFilePath))
                                {

                                }
                            }
                        }
                    }
                    else
                    {

                    }


                    if (AdBotShow != null)
                    {
                        if (AdBotShow.AdType == "AD0001")
                        {
                            var filePath = CheckPathExtension(Helpers.fullPathMainForm + @"AdMgt\" + AdBotShow.AdName);
                            if (!string.IsNullOrWhiteSpace(filePath))
                            {
                               
                            }

                        }
                        else if (AdBotShow.AdType == "AD0002")
                        {
                            if (AdBotShow.AttachFilePath.Contains("youtube"))
                            {
                                Task.Factory.StartNew(() =>
                                {

                                   
                                });
                            }
                            else if (!string.IsNullOrWhiteSpace(AdBotShow.AttachFilePath))
                            {
                               
                            }
                        }
                    }
                    else
                    {
                       
                    }


                }
                catch (Exception e)
                {

                }
            });
        }

        #region Compare Daytime

        private string CheckPathExtension(string filePath)
        {
            try
            {
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
                string fileExtension = allowedExtensions.FirstOrDefault(ext => File.Exists(filePath + ext));

                if (fileExtension != null)
                {
                    return filePath + fileExtension; ;
                }
                return null;
            }
            catch (Exception ex)
            {

                _log.SError(this.GetType().Name, ex.Message, ex.StackTrace, ex.Message);
                return null;
            }

        }
        public void StartTimerBot(DateTime dateEnd, bool isHaveValue = true)
        {
            if (isHaveValue == false)
            {
                timeBot = TimeSpan.FromSeconds(600);
                dispatcherTimerBot.Interval = TimeSpan.FromSeconds(1);

                dispatcherTimerBot.Start();
                return;
            }

            var dateNow = DateTime.Now;
            TimeSpan DateNow = dateNow.TimeOfDay;
            TimeSpan DateEnd = dateEnd.TimeOfDay;

            int totalTime = (int)DateEnd.TotalSeconds - (int)DateNow.TotalSeconds;

            timeBot = TimeSpan.FromSeconds(totalTime);

            dispatcherTimerBot.Interval = TimeSpan.FromSeconds(1);

            dispatcherTimerBot.Start();
        }
        public bool BetweenTimeOfDayCamera(DateTime input, DateTime? date1, DateTime? date2)
        {
            try
            {
                if (date1?.TimeOfDay < date2?.TimeOfDay)
                {
                    return (input.TimeOfDay >= date1?.TimeOfDay && input.TimeOfDay < date2?.TimeOfDay);
                }

                return (input.TimeOfDay >= date1?.TimeOfDay || input.TimeOfDay < date2?.TimeOfDay);
            }
            catch (Exception ex)
            {

                _log.SError(this.GetType().Name, ex.Message, ex.StackTrace, ex.Message);
                return false; ;
            }
        }

        public bool Between(DateTime input, DateTime date1, DateTime date2)
        {
            try
            {
                return (input.Date >= date1.Date && input.Date < date2.Date);

            }
            catch (Exception ex)
            {

                _log.SError(this.GetType().Name, ex.Message, ex.StackTrace, ex.Message);
                return false; ;
            }
        }

        public bool BetweenTimeOfDay(DateTime input, DateTime? date1, DateTime? date2)
        {
            try
            {
                return (input.TimeOfDay >= date1?.TimeOfDay && input.TimeOfDay < date2?.TimeOfDay);

            }
            catch (Exception ex)
            {

                _log.SError(this.GetType().Name, ex.Message, ex.StackTrace, ex.Message);
                return false; ;
            }
        }

        #endregion

        private void btnIdentity_Click(object sender, EventArgs e)
        {
 
            //Thread.Sleep(1000);

            webBrowserVideo.Stop();

            webBrowserVideo.Dispose();
            Helpers.StopSound();
            TypeRegister typeRegister = new TypeRegister(this);
            typeRegister.Show();
            Hide();
            //Show();
            cameraControlHome.Stop();

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
                                    if (!string.IsNullOrEmpty(item.ListDeviceKeyNo) && item.StoreNo == ConfigClass.StoreNo && (item.DeviceType == "DVC002" || (item.DeviceType == "DVC001")))
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
                            catch (Exception ex)
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

        #region Audio
        private void SoundSynchronized(object sender, EventArgs<int> e)
        {
            try
            {
                _tblClientSoundMgtService.GetDataIsActivityAsync(999999).ContinueWith(t =>
                {
                    if (!t.IsFaulted)
                    {
                        if (t.Result.Status)
                        {
                            var listData = t.Result.Data as IList<tblClientSoundMgtInfo>;
                            var listUpdateDeployStatus = new List<tblClientSoundMgtInfo>();
                            if (listData != null)
                            {
                                try
                                {

                                    List<tblClientSoundMgtInfo> listMissing = new List<tblClientSoundMgtInfo>();

                                    var listSoundNo = new List<int>();

                                    if (_listSetting != null && listData != null)
                                    {
                                        listSoundNo = _listSetting.Select(x => x.SoundNo).ToList();
                                        var dataMissing = listData.Where(x => (!listSoundNo.Contains(x.SoundNo))).ToList();
                                        listMissing.AddRange(dataMissing);

                                        foreach (tblClientSoundMgtInfo item in _listSetting)
                                        {
                                            var data = listData.FirstOrDefault((x => x.SoundNo == item.SoundNo && x.Version != item.Version));
                                            if (data != null)
                                            {
                                                listMissing.Add(data);
                                            }
                                        }
                                    }


                                    List<SoundResult> soundResults = new List<SoundResult>();

                                    foreach (var item in listMissing)
                                    {
                                        strNo strNo = new strNo() { soundNo = item.SoundNo };


                                        var dataGet = ApiMethod.PostCallSound(item.SoundNo).Result;
                                        var k = dataGet.Content.ReadAsStringAsync();
                                        if (dataGet.StatusCode == HttpStatusCode.OK)
                                        {
                                            SoundResult dataRespond = JsonConvert.DeserializeObject<SoundResult>(dataGet.Content.ReadAsStringAsync().Result);
                                            soundResults.Add(dataRespond);
                                        }


                                    }
                                    List<tblClientSoundDeployHistInfo> listTblClientSoundHis = new List<tblClientSoundDeployHistInfo>();

                                    foreach (var item in soundResults)
                                    {

                                        var typeName = Helpers.FindNameDictionaryByKey(item.soundType);
                                        if (string.IsNullOrEmpty(typeName))
                                            continue;

                                        ByteArrayToWaveFile(item.source, fullPathMainForm + @"TempSound\" + typeName + item.data);

                                        if (File.Exists(fullPathMainForm + @"TempSound\" + typeName + item.data))
                                        {
                                            using (var reader = new MediaFoundationReader(Path.Combine(fullPathMainForm + @"TempSound\", typeName + item.data)))
                                            {
                                                WaveFileWriter.CreateWaveFile(Path.Combine(fullPathMainForm + @"Sounds\", typeName + ".wav"), reader);
                                                File.Delete(Path.Combine(fullPathMainForm + @"TempSound\", typeName + item.data));
                                                var soundAlreadyDeployed = listData.FirstOrDefault(x => x.SoundNo.ToString() == item.soundNo);
                                                if (soundAlreadyDeployed != null)
                                                {
                                                    soundAlreadyDeployed.DeployStatus = true;
                                                    listUpdateDeployStatus.Add(soundAlreadyDeployed);
                                                }
                                            }
                                        }


                                        tblClientSoundDeployHistInfo tblClientSoundDeployHistInfo = new tblClientSoundDeployHistInfo()
                                        {
                                            SoundNo = int.Parse(item.soundNo),
                                            DeployTime = DateTime.Now,
                                            TargetStoreNo = ConfigClass.StoreNo,
                                            DeployResult = true,
                                            TargetDeviceNo = ConfigClass.StoreDeviceNo,
                                            TargetNo = ConfigClass.StoreDeviceNo
                                        };
                                        listTblClientSoundHis.Add(tblClientSoundDeployHistInfo);
                                    }

                                    _tblClientSoundMgtService.RequestUpdateListAsyn(0, listUpdateDeployStatus);


                                    Datas SoundHisData = new Datas
                                    {
                                        Data = listTblClientSoundHis
                                    };
                                    DataRequest soundHisRequestData = new DataRequest()
                                    {
                                        Signature = 114,
                                        FrameID = 0,
                                        FunctionCode = 4101,
                                        DataLength = 0,
                                        Data = SoundHisData
                                    };

                                    var soundHistRequest = ApiMethod.PostCall(soundHisRequestData).Result;
                                    _log.Info("SoundHisStatus status:" + soundHistRequest.StatusCode);
                                    if (soundHistRequest.IsSuccessStatusCode)
                                    {
                                        _xml.WriteXml(listData, @"Setting\SettingView.xml");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _log.SError(this.GetType().Name, ex.Message, ex.StackTrace, ex.Message);
                                }
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                _log.SError(this.GetType().Name, ex.Message, ex.StackTrace, ex.Message);
                Debug.WriteLine("Deploy Sound" + ex.Message);
            }
        }
        public static void ByteArrayToWaveFile(byte[] byteArray, string fileName)
        {
            // Create a FileStream object to write the byte array to a file
            FileStream fileStream = new FileStream(fileName, FileMode.Create);

            try
            {
                // Write the byte array to the FileStream
                fileStream.Write(byteArray, 0, byteArray.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing wave file: " + ex.Message);
            }
            finally
            {
                // Close the FileStream
                fileStream.Close();
            }
        }
        private void SendSoundEvent(object sender, EventArgs<ResultInfo> e)
        {
            try
            {
                var items = e.Data;
                List<GetSoundsVm> soundItem = JsonHelper.JsonToListInfo<GetSoundsVm>(String.Empty + items.Data);

                List<tblClientSoundMgtInfo> tblClientSoundMgtToAdd = new List<tblClientSoundMgtInfo>();
                List<tblClientSoundDeployHistInfo> listTblClientSound = new List<tblClientSoundDeployHistInfo>();
                tblClientSoundMgtInfo tblClientSoundMgtInfo = new tblClientSoundMgtInfo();

                _tblClientSoundMgtService.GetDataIsActivityAsync(999999).ContinueWith(t =>
                {
                    if (!t.IsFaulted)
                    {
                        if (t.Result.Status)
                        {
                            var SoundInfoDatas = t.Result.Data as IList<tblClientSoundMgtInfo>;
                            foreach (var item in soundItem)
                            {
                                var soundData = SoundInfoDatas.FirstOrDefault(x => x.SoundNo == item.soundNo);
                                if (soundData != null)
                                {
                                    var typeName = Helpers.FindNameDictionaryByKey(item.soundType);
                                    if (string.IsNullOrEmpty(typeName))
                                        continue;

                                    ByteArrayToWaveFile(item.source, fullPathMainForm + @"TempSound\" + typeName + item.extension);

                                    if (File.Exists(fullPathMainForm + @"TempSound\" + typeName + item.extension))
                                    {
                                        using (var reader = new MediaFoundationReader(Path.Combine(fullPathMainForm + @"TempSound\", typeName + item.extension)))
                                        {
                                            WaveFileWriter.CreateWaveFile(Path.Combine(fullPathMainForm + @"Sounds\", typeName + ".wav"), reader);
                                            File.Delete(Path.Combine(fullPathMainForm + @"TempSound\", typeName + item.extension));
                                        }
                                    }

                                    //using (FileStream fs = File.Create(fullPathMainForm + @"Sounds\" + typeName + ".wav"))
                                    //{
                                    //    fs.Write(item.source, 0, item.source.Length);
                                    //    Debug.WriteLine("Ok");

                                    //}

                                    tblClientSoundDeployHistInfo tblClientSoundDeployHistInfo = new tblClientSoundDeployHistInfo()
                                    {
                                        SoundNo = item.soundNo,
                                        DeployTime = DateTime.Now,
                                        TargetStoreNo = ConfigClass.StoreNo,
                                        DeployResult = true,
                                        TargetDeviceNo = ConfigClass.StoreDeviceNo,
                                        TargetNo = ConfigClass.StoreDeviceNo
                                    };
                                    listTblClientSound.Add(tblClientSoundDeployHistInfo);
                                }

                            }

                            Datas SoundData = new Datas
                            {
                                Data = tblClientSoundMgtToAdd
                            };

                            DataRequest SoundRequestData = new DataRequest()
                            {
                                Signature = 100,
                                FrameID = 0,
                                FunctionCode = 4103,
                                DataLength = 0,
                                Data = SoundData
                            };

                            var soundRequest = ApiMethod.PostCall(SoundRequestData);
                            Debug.WriteLine("SoundReques status:");


                            ////
                            Datas SoundHisData = new Datas
                            {
                                Data = listTblClientSound
                            };
                            DataRequest soundHisRequestData = new DataRequest()
                            {
                                Signature = 114,
                                FrameID = 0,
                                FunctionCode = 4101,
                                DataLength = 0,
                                Data = SoundHisData
                            };

                            var soundHistRequest = ApiMethod.PostCall(soundHisRequestData);
                            Debug.WriteLine("SoundHisStatus status:" + soundHistRequest);
                        }
                    }
                });
            }


            catch (Exception ex)
            {
                Debug.WriteLine("Sound Exception" + ex.Message);
            }

        }
        private void ClientSoundRemoved(object sender, EventArgs<IList<object>> e)
        {

        }
        private void ClientSoundRemoved(object sender, EventArgs<object> e)
        {

        }
        private void ClientSoundListAdd(object sender, EventArgs<IList<tblClientSoundMgtInfo>> e)
        {

        }
        private void ClientSoundUpdated(object sender, EventArgs<tblClientSoundMgtInfo> e)
        {
        }
        private void ClientSoundAdd(object sender, EventArgs<tblClientSoundMgtInfo> e)
        {
        }
        #endregion
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

                    if (InvokeRequired)
                    {
                        // Invoke the method on the UI thread
                        Invoke(new Action(() => ToggleDeviceStatus(deviceItem.DeviceStatus, ref active)));
                    }
                    else
                    {
                        // Access the UI control directly from the UI thread
                        ToggleDeviceStatus(deviceItem.DeviceStatus, ref active);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Sound Exception" + ex.Message);
            }
        }

        // Method to toggle device status
        private void ToggleDeviceStatus(bool deviceStatus, ref bool active)
        {
            if (deviceStatus && active == false)
            {
                active = true;
                this.Show();
                //webBrowserVideo.Stop();
                cameraControlHome.Start();
            }
            else
            {
                cameraControlHome.Stop();
                
                webBrowserVideo.Stop();
                webBrowserVideo.Dispose();
                this.Hide();
                active = false;
            }
        }
    }
}