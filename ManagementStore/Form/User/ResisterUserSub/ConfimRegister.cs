using DevExpress.XtraEditors;
using ManagementStore.Model.Static;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementStore.Form.User.ResisterUserSub
{
    public partial class ConfimRegister : DevExpress.XtraEditors.XtraForm
    {
        public ConfimRegister()
        {
            InitializeComponent();
            fullNameTxt.Text = UserCCCD.FullName;
            birthdayTxt.Text = UserCCCD.BirthDay;
            genderTxt.Text = UserCCCD.Gender;
            pictureTaken.Image = ConvertBase64ToImage(UserCCCD.Picture);
            pictureBoxCCCD.Image = ConvertBase64ToImage(UserCCCD.PictureCCCD);
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

        private void buttonConfirm_Click(object sender, EventArgs e)
        {

        }
    }
}