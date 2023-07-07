using DevExpress.XtraEditors;
using ManagementStore.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementStore.Form.Ad
{
    public partial class TopAd : DevExpress.XtraEditors.XtraUserControl
    {
        public TopAd()
        {
            InitializeComponent();

            string html = "<html><head>";
            string url = "https://www.youtube.com/watch?v=Z9uEn2IVPkQ";
            html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";
            html += "<iframe id='video' src= 'https://www.youtube.com/embed/{0}?autoplay=1;mute=1' width='100%' height='350px' frameborder='0' allowfullscreen></iframe>";
            html += "</body></html>";
            this.webBrowserVideo.DocumentText = string.Format(html, Utils.GetVideoId(url));
        }
    }
}
