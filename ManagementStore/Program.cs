﻿using DevExpress.UserSkins;
using ManagementStore.Form;
using ManagementStore.Form.User;
using ManagementStore.Form.User.ResisterUserSub;
using Parking.App.Factory;
using System;
using System.Windows.Forms;

namespace ManagementStore
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            Application.Run(new Home(ProgramFactory.Instance.tblAdMgtService));
        }
    }
}

