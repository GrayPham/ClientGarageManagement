using DevExpress.UserSkins;
using ManagementStore.Form;
using ManagementStore.Form.User;
using ManagementStore.Form.User.ResisterUserSub;
using Parking.App.Factory;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ManagementStore
{

    static class Program
    {
        [DllImport("onnxruntime.dll")]
        public static extern IntPtr OrtSessionOptionsAppendExecutionProvider_CUDA(IntPtr options, int device_id);

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

