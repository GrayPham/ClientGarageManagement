﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.Common
{
    public class ModelConfig
    {
        public static string dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/Weights");
        public static string settingFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Setting");
        public static string imageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\Images");

        public static string socketFastAPI = "ws://26.115.12.45:8001/ws";
        public static bool socketOpen = false;
    }
}
