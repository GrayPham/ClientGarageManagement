using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementStore.Form.User
{
    public partial class RegisterUser2 : DevExpress.XtraEditors.XtraForm
    {
        private Home _home; 
        public RegisterUser2(Home home)
        {
            _home = home;
            InitializeComponent();
            panelSlider2.Controls.Add(new CitizenshipID());
            //panelSlider2.Controls.Add(new CitizenshipIDCapture());

        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            foreach (Control control in panelSlider2.Controls)
            {
                if (control is UserControl)
                {
                    
                    panelSlider2.Controls.Remove(control);
                    
                }
            }
            _home.Show();
            _home.cameraControl.Start();
            this.Close();
        }

        private void RegisterUser2_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            _home.Show();
            _home.cameraControl.Start();
            //panelSlider2.Controls.RE
            foreach (Control control in panelSlider2.Controls)
            {
                if (control is UserControl)
                {
                    panelSlider2.Controls.Remove(control);
                    control.Dispose();
                }
            }
            this.Close();
        }
    }
}