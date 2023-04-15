using ManagementStore.Common;
using ManagementStore.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov5Net.Scorer;
using Yolov5Net.Scorer.Models;

namespace ManagementStore.Services
{
    public class YoloDetectServices
    {

        private YoloScorer<YoloCocoP5Model> scorer = new YoloScorer<YoloCocoP5Model>(ModelConfig.dataFolderPath + "/best.onnx");
        public YoloModelDto detect(Image image)
        {
            Image imagebase = new Bitmap(image);
            //Mat frame = new Mat();
            //VideoCapture camera1 = new VideoCapture(0); // thiết lập camera đầu tiên
            //camera1.Read(frame); // đọc khung hình từ camera đầu tiên
            List<YoloPrediction> predictions = scorer.Predict(image);
            YoloModelDto yolo = new YoloModelDto(imagebase, predictions);
            using var graphics = Graphics.FromImage(image);
            foreach (var prediction in predictions) // iterate predictions to draw results
            {
                double score = Math.Round(prediction.Score, 2);

                graphics.DrawRectangles(new Pen(prediction.Label.Color, 1),
                    new[] { prediction.Rectangle });

                var (x, y) = (prediction.Rectangle.X - 3, prediction.Rectangle.Y - 23);

                graphics.DrawString($"{prediction.Label.Name} ({score})",
                    new Font("Arial", 9, GraphicsUnit.Pixel), new SolidBrush(prediction.Label.Color),
                    new PointF(x, y));
            }
            yolo.setImageDetect(image);
            return yolo;
        }
    }
}
