using DevExpress.XtraBars;
using ManagementStore.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementStore.Form
{
    public partial class DetectClient : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        private YoloDetectServices detect = new YoloDetectServices();
        public DetectClient()
        {
            InitializeComponent();
        }
        private void ProcessFrame(object sender, EventArgs arg)
        {
            //Image image = new Image();
            //detect.detect(image);
        }

    }
}