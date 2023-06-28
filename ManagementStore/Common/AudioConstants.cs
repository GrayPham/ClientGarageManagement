using ManagementStore.DTO;
using NAudio.Wave;
using Newtonsoft.Json;
using Parking.App.Common.ApiMethod;
using Parking.Contract.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.Common
{
    class AudioConstants
    {
        private static string basePath = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly int HomeAudio = 123;
        public static readonly int TypeRegister = 100;
        public static readonly int InputCCCD = 1;
        public static readonly int AuthenticationCCCD = 201;
        public static readonly int InforUser = 202;
        public static readonly int FullName = 203;
        public static readonly int FaceTaken = 204;
        public static readonly int SuccessfulRegister = 303;
        public static readonly int RegisteredMember = 101;
        public static readonly int InputPhone = 301;
        public static readonly int OTPPhone = 302;
        public static async Task<string> GetListSound(int audioId)
        {
            try
            {
                
                // Request to Page View 
                string filePath = Path.Combine(basePath, "Settings\\audioSettings.json");
                string soundRequest = await ApiMethod.GetCallSoundAudio();
                if (soundRequest != null && soundRequest != "")
                {

                    List<tblClientSoundMgtInfo> clientSoundMgtInfoNew = JsonConvert.DeserializeObject<List<tblClientSoundMgtInfo>>(soundRequest);
                    tblClientSoundMgtInfo foundItemNew = new tblClientSoundMgtInfo();
                    foundItemNew = clientSoundMgtInfoNew.Find(item => item.SoundNo == audioId);
                    if (!File.Exists(filePath))
                    {
                        File.Create(filePath).Close();
                        //Convert Json Object and Update New Json File 
                        var jsonObject = JsonConvert.DeserializeObject(soundRequest);
                        File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonObject));
                        GetByteArrayAudio(audioId);


                    }
                    else
                    {
                        // Load Data from older Json file
                        string oldJsonFile = File.ReadAllText(filePath);
                        List<tblClientSoundMgtInfo> clientSoundMgtInfoOld = clientSoundMgtInfoOld = JsonConvert.DeserializeObject<List<tblClientSoundMgtInfo>>(oldJsonFile);
                        tblClientSoundMgtInfo foundItemOld = clientSoundMgtInfoOld.Find(item => item.SoundNo == audioId);
                        // ? Neu nhu khong khac version nhung chua co file trong he thong thi sao
                        if (foundItemOld.Version != foundItemNew.Version)
                        {
                            //Convert Json Object and Update New Json File 
                            var jsonObject = JsonConvert.DeserializeObject(soundRequest);
                            File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonObject));
                            // Sent API to get New file audio
                            GetByteArrayAudio(audioId);
                        }
                        else
                        {
                            string folderAudioFile = Path.Combine(basePath, "Assets\\Audio\\"+ foundItemNew.SoundName + ".wav");
                            if (!File.Exists(folderAudioFile))
                            {
                                GetByteArrayAudio(audioId);
                            }
                        }

                    }
                    


                    return foundItemNew.SoundName;
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }


        }
        public static async  void GetByteArrayAudio(int audioNo)
        {
            string audioNoString = audioNo.ToString();
            string result = await ApiMethod.GetSourceAudio(audioNoString);
            if (result != "")
            {
                ResultAudioDto resultAudioDto = JsonConvert.DeserializeObject<ResultAudioDto>(result);
                if (resultAudioDto.Success == true)
                {
                    string folderAudio = Path.Combine(basePath, "Assets\\Audio");
                    ByteArrayToWaveFile(resultAudioDto.Data, folderAudio + "\\" + resultAudioDto.SoundName);
                    string tempSound = Path.Combine(folderAudio, folderAudio + "\\" + resultAudioDto.SoundName);
                    if (File.Exists(tempSound))
                    {
                        using (var reader = new MediaFoundationReader(tempSound))
                        {
                            WaveFileWriter.CreateWaveFile(Path.Combine(tempSound + ".wav"), reader);
                            File.Delete(tempSound);

                        }
                    }
                }
            }
            
        }
        public static void ByteArrayToWaveFile(byte[] byteArray, string fileName)
        {
            // Create a FileStream object to write the byte array to a file
            FileStream fileStream = new FileStream(fileName, FileMode.Create);

            try
            {
                // Write the byte array to the FileStream
                fileStream.Write(byteArray, 0, byteArray.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing wave file: " + ex.Message);
            }
            finally
            {
                // Close the FileStream
                fileStream.Close();
            }
        }
    }
}
