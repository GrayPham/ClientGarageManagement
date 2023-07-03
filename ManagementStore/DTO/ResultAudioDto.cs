using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.DTO
{
    public class ResultAudioDto
    {
        public bool Success { get; set; }
        public string SoundName { get; set; }
        public string SoundNo { get; set; }
        public string Message { get; set; }
        public byte[] Data { get; set; }


    }
}
