using Emgu.CV;
using ManagementStore.Common;
using ManagementStore.Form.Camera;
using Security;
using System;


namespace ManagementStore.Form
{
    public partial class DetectClient : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private VideoCapture capture = new VideoCapture();
        private int count = 0;


        // Connect Socket 
        public SocketDetect encode = new SocketDetect();
        public DetectClient()
        {
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
            if (count > 1)
            {
                PictureControl pictureControl = new PictureControl(0, encode);
                panelIn.Controls.Add(pictureControl);
                PictureControl pictureControl1 = new PictureControl(1, encode);
                panelOut.Controls.Add(pictureControl1);
            }
            else if (count == 1)
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



    }
}