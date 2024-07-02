using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.Model.Static
{
    public static class UserCCCD
    {
        public static string CCCDNumber { get; set; }
        public static string Gender { get; set; }
        public static string BirthDay { get; set; }
        public static string FullName { get; set; }
        public static string Picture { get; set; }
        public static byte[] PictureByte { get; set; } 
        public static string PictureCCCD { get; set; }
        public static byte[] PictureCCCDByte { get; set; }
    }

    public class CCCDResult
    {
        public string Card { get; set; }
        public string Id { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string Origin { get; set; }
        public string Name { get; set; }
        public string Birth { get; set; }
    }
}
