﻿using DevExpress.XtraEditors;
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
        public RegisterUser2()
        {
            InitializeComponent();
            panelSlider2.Controls.Add(new CitizenshipID());
            //panelSlider2.Controls.Add(new CitizenshipIDCapture());

        }
    }
}