﻿using DevExpress.XtraEditors;
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

namespace ManagementStore.Form.User
{
    public partial class PhoneNumber : System.Windows.Forms.UserControl
    {
        public PhoneNumber()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
             Utils.Forward(ParentForm, "pictureBoxPhone", "pictureBoxInfo", "InformationUser");
        }
    }
}
