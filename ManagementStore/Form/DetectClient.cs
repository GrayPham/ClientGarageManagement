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
    public partial class DetectClient : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private VideoCapture _capture = new VideoCapture();
        private int _count = 0;

        // Connect Socket 
        private SocketDetect Encode = new SocketDetect();
        public DetectClient()
        {
            InitializeComponent();
            LoadCamera();

            // Connect FastAPI
            if (Encode.OpenConnect())
            {
                ModelConfig.socketOpen = true;
            }
 
        }
        private void DetectClient_Load(object sender, EventArgs e)
        {
            if (_count > 1)
            {
                PictureControl pictureControl = new PictureControl(0, Encode);
                panelIn.Controls.Add(pictureControl);
                PictureControl pictureControl1 = new PictureControl(1, Encode);
                panelOut.Controls.Add(pictureControl1);
            }
            else if (_count == 1)
            {
                PictureControl pictureControl = new PictureControl(0, Encode);
                panelIn.Controls.Add(pictureControl);
            }
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
    }
}