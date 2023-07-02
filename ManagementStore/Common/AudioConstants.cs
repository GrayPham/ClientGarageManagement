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
        public static readonly string HomeAudio = "SOUN19";
        public static readonly string TypeRegister = "SOUN28";
        public static readonly string InputCCCD = "SOUN23";
        public static readonly string AuthenticationCCCD = "SOUN24";
        public static readonly string InforUser = "SOUN25";
        public static readonly string FullName = "SOUN26";
        public static readonly string FaceTaken = "SOUN27";
        public static readonly string SuccessfulRegister = "SOUN17";
        public static readonly string RegisteredMember = "SOUN16";
        public static readonly string InputPhone = "SOUN03";
        public static readonly string OTPPhone = "SOUN04";
        public static async Task<string> GetListSound(string typeName)
        {
            try
            {
                
                // Request to Page View 
                string filePath = Path.Combine(basePath, "Settings");
                string soundRequest = await ApiMethod.GetCallSoundAudio();
                if (soundRequest != null && soundRequest != "")
                {

                    List<tblClientSoundMgtInfo> clientSoundMgtInfoNew = JsonConvert.DeserializeObject<List<tblClientSoundMgtInfo>>(soundRequest);
                    tblClientSoundMgtInfo foundItemNew = new tblClientSoundMgtInfo();
                    foundItemNew = clientSoundMgtInfoNew.Find(item => item.SoundType == typeName);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    filePath = Path.Combine(filePath, "audioSettings.json");
                    if (!File.Exists(filePath))
                    {
                        File.Create(filePath).Close();
                        //Convert Json Object and Update New Json File 
                        var jsonObject = JsonConvert.DeserializeObject(soundRequest);
                        File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonObject));
                        GetByteArrayAudio(foundItemNew.SoundNo);
                    }
                    else
                    {
                        // Load Data from older Json file
                        string oldJsonFile = File.ReadAllText(filePath);
                        List<tblClientSoundMgtInfo> clientSoundMgtInfoOld = clientSoundMgtInfoOld = JsonConvert.DeserializeObject<List<tblClientSoundMgtInfo>>(oldJsonFile);
                        tblClientSoundMgtInfo foundItemOld = clientSoundMgtInfoOld.Find(item => item.SoundNo == foundItemNew.SoundNo);
                        if(foundItemOld != null)
                        {
                            if (foundItemOld.Version != foundItemNew.Version)  // Update Type Sound && Version Sound && New Datetime 
                            {
                                //Convert Json Object and Update New Json File 
                                var jsonObject = JsonConvert.DeserializeObject(soundRequest);
                                File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonObject));
                                // Sent API to get New file audio
                                GetByteArrayAudio(foundItemNew.SoundNo);
                            }
                            else
                            {
                                string folderAudioFile = Path.Combine(basePath, "Assets\\Audio\\" + foundItemNew.SoundName + ".wav");
                                if (!File.Exists(folderAudioFile))
                                {
                                    GetByteArrayAudio(foundItemNew.SoundNo);
                                }
                            }
                        }
                        else // If not found oldFile
                        {
                            GetByteArrayAudio(foundItemNew.SoundNo);
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
                    if (!Directory.Exists(folderAudio))
                    {
                        Directory.CreateDirectory(folderAudio);
                    }
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
