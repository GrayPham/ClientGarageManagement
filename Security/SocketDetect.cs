using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yolov5Net.Scorer;

namespace Security
{
    public class SocketDetect
    {
        private byte[] ImageToByteArray(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
        public async Task<string> request(Image imagebase, List<YoloPrediction> predictions, ClientWebSocket webSocket)
        {
            var imageBaseBytes = ImageToByteArray(imagebase);

            var data = new
            {
                Image = imageBaseBytes,
                Predictions = predictions
            };
            // Serialize the JSON object to a string
            var json = JsonConvert.SerializeObject(data);
            var cancellationTokenSource = new CancellationTokenSource();
            var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(json));
            try
            {

                //var message = JsonConvert.SerializeObject(dto);
                //var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                if (webSocket.State == WebSocketState.Open)
                {
                    //// Gửi dữ liệu lên server
                    //var sendBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes("Hello, FastAPI!"));
                    await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, cancellationTokenSource.Token);

                    //// Nhận dữ liệu từ server
                    var bufferGet = new ArraySegment<byte>(new byte[1024]);

                    var receivedResult = await webSocket.ReceiveAsync(bufferGet, cancellationTokenSource.Token);
                    if (receivedResult.MessageType == WebSocketMessageType.Close)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationTokenSource.Token);
                    }
                    var message = Encoding.UTF8.GetString(bufferGet.Array, bufferGet.Offset, receivedResult.Count);

                    return message.ToString();


                }
            }

            catch (Exception ex)
            {
                return "Exception:" + ex.Message;
            }
            // Trả về giá trị mặc định cho kiểu Task<string>
            return default;
        }
    }
}
