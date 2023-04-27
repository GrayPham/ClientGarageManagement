using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Security.VehicleCheckHttpClient
{
    public class VehicleCheck
    {
        public async Task VehicleCheckPost(String platenum, String typeTransport = "car", String typeLP = "2")
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
            var response = await client.PostAsync("https://localhost:8001/check-vehicle", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Successful");
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
    }
}
