using Security.VehicleCheckHttpClient.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace Security.VehicleCheckHttpClient
{
    public class VehicleCheck: IVehicleManagement
    {
        public async Task<string> CheckInVehicleAsync(string platenum, string typeTransport = "car", string typeLP = "2")
        {
            HttpClient client = new HttpClient();
            

            var values = new Dictionary<string, string>
            {
                { "platenum", platenum },
                { "typeTransport", typeTransport },
                { "typeLP", typeLP }
            };

            var content = new FormUrlEncodedContent(values);
            // Gọi đến API kiểm tra xe ra vào
            var response = await client.PostAsync(Common.Common.api + "/check-vehicle", content);
            //string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return "Successful";
            }
            else
            {
                return "Error";
            }
        }
        public async Task<string> VehicleReportAsync(string ImagePlate, string ImageFace)
        {
            HttpClient client = new HttpClient();


            var values = new Dictionary<string, string>
            {
                { "imagePlate", ImagePlate },
                { "imageFace", ImageFace }
            };

            var content = new FormUrlEncodedContent(values);
            // Gọi đến API kiểm tra xe ra vào
            var response = await client.PostAsync("https://localhost:8001/report", content);
            //string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return "Successful";
            }
            else
            {
                return "Error";
            }
        }
    }
}
