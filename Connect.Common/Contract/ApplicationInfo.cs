using Connect.Common.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.Common.Contract
{
    public class ApplicationInfo
    {
        public static string VersionName = @"1.0.0.0 (271002022)";
        public static int VersionCode = 1010;
        public static string VersionBy = @"Nguyễn Thanh Tín";
        public static string VersionDate = @"27/10/2022";
        public static string Company = @"TinNT";
        public static string Copyright = @"Copyright";
        public static string FirstName = @"KIOSK";
        public static string LastName = @"Management System";
        public static string AppName = @"KIOSK Management System";
        public const string Author = @"Nguyễn Thanh Tín";
        public static Guid AppKey = Guid.NewGuid();
        public static string GuiD = "";
        public static string Phone = "0782252279";
        public static string SeriKey = @"151219900968696391";
        public static string LicenseKey = @"";
        public static string SoftwareKey = @"E43E527ED23C381D00FE6DA731C5D64D";
        public static string PCID = "42E6-BB34-447F-A080-3891-30B3-50AF-AC49";
        public static string CpuIdKey = @"";
        public static string InstallID = @"";
        public static EClientType eClientType = EClientType.None;
        public static int PortUser = 9000;
        public static string IPServer = "127.0.0.1";
        public static string AccessKey = "@@##151290";
        public static bool AllowGetServer { get; set; } = true;
        public static bool AllowTranslate { get; set; } = false;
    }
}
