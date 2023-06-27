using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Connect.Common;
using Connect.Common.Common;
using Connect.Common.Contract;
using Connect.Common.Interface;
using Connect.Common.Languages;
using Connect.SocketClient;
using DevExpress.Images;
using ManagementStore.Common;
using ManagementStore.DTO;
using ManagementStore.Extensions;
using ManagementStore.Form.User;
using NAudio.Wave;
using Newtonsoft.Json;
using Parking.App.Common.ApiMethod;
using Parking.App.Common.Helper;
using Parking.App.Contract.Common;
using Parking.App.Factory;
using Parking.App.Interface.Common;
using Parking.App.Language;
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

        public Home()
        {
            _log = ProgramFactory.Instance.Log;
           
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            GetListSound(AudioConstants.HomeAudio);
            if(fileNameAudio != null || fileNameAudio != "")
            {
                Helpers.PlaySound(@"Assets\Audio\"+ fileNameAudio + ".wav");
            }
            
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
            splashScreenManager1.ShowWaitForm();
            Thread.Sleep(1000);
            
            cameraControl.Stop();
            this.webBrowserVideo.DocumentText = "";
            Helpers.StopSound();
            
            TypeRegister typeRegister = new TypeRegister();
            
            Hide();

            splashScreenManager1.CloseWaitForm();
            typeRegister.ShowDialog();
            Show();

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

        #region Audio
        private async void GetListSound(int audioId)
        {
            try
            {
                // Request to Page View 
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings\\audioSettings.json");
                string soundRequest = await ApiMethod.GetCallSoundAudio();
                if (soundRequest != null)
                {
                    
                    List<tblClientSoundMgtInfo> clientSoundMgtInfoNew = JsonConvert.DeserializeObject<List<tblClientSoundMgtInfo>>(soundRequest);
                    tblClientSoundMgtInfo foundItemNew = clientSoundMgtInfoNew.Find(item => item.SoundNo == audioId);
                    if (!File.Exists(filePath))
                    {
                        File.Create(filePath);
                    }
                    else
                    {
                        // Load Data from older Json file
                        string oldJsonFile = File.ReadAllText(filePath);
                        List<tblClientSoundMgtInfo> clientSoundMgtInfoOld = clientSoundMgtInfoOld = JsonConvert.DeserializeObject<List<tblClientSoundMgtInfo>>(oldJsonFile);
                        tblClientSoundMgtInfo foundItemOld = clientSoundMgtInfoOld.Find(item => item.SoundNo == audioId);
                        
                        if (foundItemOld.Version != foundItemNew.Version)
                        {
                            // Sent API to get New file audio
                            string result = await ApiMethod.GetSourceAudio("123");
                            if (result != "")
                            {
                                ResultAudioDto resultAudioDto = JsonConvert.DeserializeObject<ResultAudioDto>(result);
                                if (resultAudioDto.Success == true)
                                {
                                    string folderAudio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\Audio");
                                    ByteArrayToWaveFile(resultAudioDto.Data, folderAudio+"\\"+ resultAudioDto.SoundName);
                                    string tempSound = Path.Combine(folderAudio, folderAudio + "\\" + resultAudioDto.SoundName);
                                    if (File.Exists(tempSound))
                                    {
                                        using (var reader = new MediaFoundationReader(tempSound))
                                        {
                                            WaveFileWriter.CreateWaveFile(Path.Combine(tempSound + ".wav"), reader);
                                            File.Delete(tempSound);
                   
                                        }
                                    }
                                }
                            }
                        }
                        
                    }
                    fileNameAudio = foundItemNew.SoundName;
                    //Convert Json Object and Update New Json File 
                    var jsonObject = JsonConvert.DeserializeObject(soundRequest);
                    File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonObject));

                    
                }
                
            }
            catch
            {
                throw;
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
        #endregion
    }
}