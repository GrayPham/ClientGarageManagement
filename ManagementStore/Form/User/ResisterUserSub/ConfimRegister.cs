using Connect.Common.Contract;
using DevExpress.XtraEditors;
using ManagementStore.Model.Static;
using Parking.App.Common;
using Parking.App.Common.ApiMethod;
using Parking.App.Common.Constants;
using Parking.App.Common.Helper;
using Parking.App.Common.ViewModels;
using Parking.App.Contract.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementStore.Form.User.ResisterUserSub
{
    public partial class ConfimRegister : DevExpress.XtraEditors.XtraForm
    {
        public bool CaptureAgain { get; private set; }
        public ConfimRegister()
        {
            InitializeComponent();
            fullNameTxt.Text = UserCCCD.FullName != null ? UserCCCD.FullName : "Pham Van Manh Hung";
            birthdayTxt.Text = UserCCCD.BirthDay;
            genderTxt.Text = UserCCCD.Gender != null ? UserCCCD.Gender : "Male"; ;
            pictureTaken.Image = UserCCCD.Picture != null ? ConvertBase64ToImage(UserCCCD.Picture): pictureTaken.Image;
            pictureBoxCCCD.Image = UserCCCD.PictureCCCD != null ? ConvertBase64ToImage(UserCCCD.PictureCCCD) : pictureBoxCCCD.Image;
        }
        public Image ConvertBase64ToImage(string base64String)
        {
            // Chuyển chuỗi Base64 thành mảng byte
            byte[] imageBytes = Convert.FromBase64String(base64String);

            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                // Tạo đối tượng hình ảnh từ mảng byte
                Image image = Image.FromStream(ms);
                return image;
            }
        }
        public byte[] ConvertImageToByte(Image image)
        {
            // Chuyển chuỗi Base64 thành mảng byte
            

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
        public static string GetLocalIPv4()
        {
            string ipAddress = "";
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                    netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach (UnicastIPAddressInformation ip in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ipAddress = ip.Address.ToString();
                            return ipAddress;
                        }
                    }
                }
            }
            return ipAddress;
        }
        private async void buttonConfirm_Click(object sender, EventArgs e)
        {
            RequestInfo request = new RequestInfo();
            var uInfo = new object[5];
            uInfo[0] = UserCCCD.FullName == null ? "Nguyen Ngoc Thien" : UserCCCD.FullName;
            uInfo[1] = UserCCCD.CCCDNumber == null ? "066201000447" : UserCCCD.CCCDNumber;
            uInfo[2] = UserCCCD.Gender == null ? "Male" : UserCCCD.Gender;
            uInfo[3] = UserCCCD.Picture == null ? "124" : UserCCCD.Picture;
            uInfo[4] = UserCCCD.BirthDay == null ? "2023-06-23" : UserCCCD.BirthDay;

            string userid = DateTime.Now.ToString("yyyyMMddHHmmss");
            tblUserInfo user = new tblUserInfo()
            {
                UserID = userid,
                UserType = "USR001",
                Password = Helpers.HashCodePassword("DPSS01"),
                UserName = userid,
                IdentityNo = UserCCCD.CCCDNumber != null ? UserCCCD.CCCDNumber : "066201000447" ,
                Birthday = UserCCCD.BirthDay != null ? Convert.ToDateTime(UserCCCD.BirthDay) : DateTime.Now,
                Email = String.Empty,
                Gender = UserCCCD.Gender == "Male" ? true : false,
                ApproveReject = true,
                UserStatus = "USST01",
                RegistDate = DateTime.Now,
                Desc = String.Empty,
                UseYN = false,
                LoginIP = GetLocalIPv4() ?? " ",
                LastSimilarityRate = ConfigClass.SimilarityRate,
                AuthMethod = "Card Id" == Constants.CardMethod ? "APPTP1" : "APPTP2"
            };

            tblUserMgtStoreInfo userMgt = new tblUserMgtStoreInfo()
            {
                UserID = userid,
                RegistDate = DateTime.Now,
                Memo = "",
                StoreNo = 19 // Need update
            };

            tblStoreUseHistoryInfo storeUseHistory = new tblStoreUseHistoryInfo()
            {
                UserID = userid,
                UseDate = DateTime.Now,
                StoreNo = 19
            };

            tblUserPhotoInfo photo;
            //var facePhotoPath = "";
            //var IdcardPhoto = "";


            if ("IdCardAuth" == Constants.CardMethod)
            {
                // TODO: save image taken and image in card ID
                
                photo = new tblUserPhotoInfo()
                {
                    UserID = userid,
                    // TEST

                    TakenPhoto = UserCCCD.PictureByte,
                    IdCardPhoto = UserCCCD.PictureCCCDByte

                    //TakenPhoto = ConvertImageToByte(pictureTaken.Image),
                    //IdCardPhoto = ConvertImageToByte(pictureBoxCCCD.Image)
                };
            }
            else
            {

                photo = new tblUserPhotoInfo()
                {
                    UserID = userid,
                    TakenPhoto = UserInfo.PictureByte,
                    // TODO: add image taken 
                };
            }

            //tblUserInfo userInfo = new tblUserInfo()
            //{

            //};

            user.TblUserMgtStoreInfo = userMgt;
            user.TblStoreUseHistoryInfo = storeUseHistory;
            user.TblUserPhotoInfo = photo;
            user.isRemoveTempUser = true;

            var dataObject = new RequestInfo()
            {
                Data = user
            };

            request.Data = user;
            DataRequest userMgtData = new DataRequest()
            {
                Signature = 104,
                FrameID = 0,
                FunctionCode = 4104,
                DataLength = 10000,
                Data = dataObject
            };

            var repose = await ApiMethod.PostCall(userMgtData);
            if (repose.StatusCode == System.Net.HttpStatusCode.OK)
            {
                XtraMessageBox.Show("Registed account successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // TODO: send message register successfully
            }
            else
            {
                XtraMessageBox.Show("Register user failed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void btnReturnRegis_Click(object sender, EventArgs e)
        {
            CaptureAgain = true;
            this.Close();
        }
    }
}