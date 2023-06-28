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
        public static string fileNameAudio;
        public static readonly int HomeAudio = 123;
        public static readonly int TypeRegister = 100;

        public static async Task<string> GetListSound(int audioId)
        {
            try
            {
                // Request to Page View 
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings\\audioSettings.json");
                string soundRequest = await ApiMethod.GetCallSoundAudio();
                if (soundRequest != null && soundRequest != "")
                {

                    List<tblClientSoundMgtInfo> clientSoundMgtInfoNew = JsonConvert.DeserializeObject<List<tblClientSoundMgtInfo>>(soundRequest);
                    tblClientSoundMgtInfo foundItemNew = new tblClientSoundMgtInfo();
                    foundItemNew = clientSoundMgtInfoNew.Find(item => item.SoundNo == audioId);
                    if (!File.Exists(filePath))
                    {
                        File.Create(filePath);
                        GetByteArrayAudio(audioId);

                    }
                    else
                    {
                        // Load Data from older Json file
                        string oldJsonFile = File.ReadAllText(filePath);
                        List<tblClientSoundMgtInfo> clientSoundMgtInfoOld = clientSoundMgtInfoOld = JsonConvert.DeserializeObject<List<tblClientSoundMgtInfo>>(oldJsonFile);
                        tblClientSoundMgtInfo foundItemOld = clientSoundMgtInfoOld.Find(item => item.SoundNo == audioId);

                        if (foundItemOld.Version != foundItemNew.Version)
                        {
                            // Sent API to get New file audio
                            GetByteArrayAudio(audioId);
                            
                        }

                    }
                    return foundItemNew.SoundName;
                    //Convert Json Object and Update New Json File 
                    var jsonObject = JsonConvert.DeserializeObject(soundRequest);
                    File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonObject));


                }
                return "";
            }
            catch
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
                    string folderAudio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\Audio");
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
