using Emgu.CV;
using ManagementStore.DTO;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Emgu.CV.Structure;

namespace ManagementStore.Model.ML
{
    class ObjectDetectionSSD
    {
        private InferenceSession session;
        List<DetectionResult> detections = new List<DetectionResult>();
        public ObjectDetectionSSD(string modelPath)
        {
            session = new InferenceSession(modelPath);
        }

        public float AreaOf(float left, float top, float right, float bottom)
        {
            float width = Math.Max(0, right - left);
            float height = Math.Max(0, bottom - top);
            return width * height;
        }

        private Bitmap ConvertToImage(float[] flattenedImage)
        {
            int inputWidth = 300;
            int inputHeight = 300;
            Bitmap image = new Bitmap(inputWidth, inputHeight, PixelFormat.Format24bppRgb);

            int index = 0;
            for (int y = 0; y < inputHeight; y++)
            {
                for (int x = 0; x < inputWidth; x++)
                {
                    float red = flattenedImage[index++];
                    float green = flattenedImage[index++];
                    float blue = flattenedImage[index++];

                    // Denormalize pixel values
                    int pixelValueR = (int)(red * 128f + 127f);
                    int pixelValueG = (int)(green * 128f + 127f);
                    int pixelValueB = (int)(blue * 128f + 127f);

                    // Clamp pixel values to 0-255 range
                    pixelValueR = Math.Max(0, Math.Min(255, pixelValueR));
                    pixelValueG = Math.Max(0, Math.Min(255, pixelValueG));
                    pixelValueB = Math.Max(0, Math.Min(255, pixelValueB));

                    Color pixelColor = Color.FromArgb(pixelValueR, pixelValueG, pixelValueB);
                    image.SetPixel(x, y, pixelColor);
                }
            }

            return image;
        }
        public List<DetectionResult> DetectObjects(Mat image)
        {
            detections = new List<DetectionResult>();
            float[] preprocessedImage = PreprocessImage(image);
            int height = image.Height;
            int width = image.Width;
            // Create the input tensor
            DenseTensor<float> inputTensor = new DenseTensor<float>(preprocessedImage, new[] { 1, 300, 300, 3 });
            var inputs = new List<NamedOnnxValue> { NamedOnnxValue.CreateFromTensor("onnx::Transpose_0", inputTensor) };
            var outputs = new[] { "scores", "boxes" };
            var results = session.Run(inputs, outputs);
            float[] scores = results.FirstOrDefault().AsEnumerable<float>().ToArray(); // float[24000]
            float[] boxes = results.Skip(1).FirstOrDefault().AsEnumerable<float>().ToArray(); // float[12000]
            float[,] reshapedScores = new float[3000, 8];
            float[,] reshapedBoxes = new float[3000, 4];
            int idx = 0;
            for (int i = 0; i < 3000; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    reshapedScores[i, j] = scores[idx];
                    idx++;
                }
            }
            idx = 0;
            for (int i = 0; i < 3000; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    reshapedBoxes[i, j] = boxes[idx];
                    idx++;
                }
            }
            List<float[]> pickedBoxProbs = new List<float[]>();
            List<int> pickedLabels = new List<int>();

            for (int classIndex = 1; classIndex < reshapedScores.GetLength(1); classIndex++)
            {
                float[] probs = new float[reshapedScores.GetLength(0)];
                for (int i = 0; i < reshapedScores.GetLength(0); i++)
                {
                    probs[i] = reshapedScores[i, classIndex];
                }

                List<int> mask = new List<int>();
                List<float> scoresNew = new List<float>();
                for (int i = 0; i < probs.Length; i++)
                {
                    if (probs[i] > 0.5)
                    {
                        mask.Add(i);
                        scoresNew.Add(probs[i]);
                    }
                }
                if (mask.Count == 0)
                {
                    continue;
                }


                float[,] subsetBoxes = new float[mask.Count, 4];
                for (int i = 0; i < mask.Count; i++)
                {
                    int index = mask[i];
                    for (int j = 0; j < 4; j++)
                    {
                        subsetBoxes[i, j] = reshapedBoxes[index, j];
                    }
                }
                float[,] boxProbs = new float[mask.Count, 5];
                for (int i = 0; i < mask.Count; i++)
                {
                    int index = mask[i];
                    for (int j = 0; j < 4; j++)
                    {
                        boxProbs[i, j] = subsetBoxes[i, j];
                    }
                    boxProbs[i, 4] = scoresNew[i];
                }
                List<int> indexes = new List<int>();
                for (int i = 0; i < mask.Count; i++)
                {
                    indexes.Add(i);
                }
                List<int> picked = new List<int>();
                indexes.Sort((a, b) => scoresNew[b].CompareTo(scoresNew[a]));
                int maxPicked = Math.Min(10, indexes.Count);
                indexes = indexes.GetRange(0, maxPicked);
                while (indexes.Count > 0)
                {
                    int current = indexes[0];
                    picked.Add(current);

                    if (picked.Count >= 10 || indexes.Count == 1)
                    {
                        break;
                    }

                    float[] currentBox = new float[4];
                    for (int j = 0; j < 4; j++)
                    {
                        currentBox[j] = boxProbs[current, j];
                    }

                    indexes.RemoveAt(0);
                    List<int> remainingIndexes = new List<int>();
                    for (int i = 0; i < indexes.Count; i++)
                    {
                        int index = indexes[i];
                        float[] restBox = new float[4];
                        for (int j = 0; j < 4; j++)
                        {
                            restBox[j] = boxProbs[index, j];
                        }

                        float overlapLeft = Math.Max(restBox[0], currentBox[0]);
                        float overlapTop = Math.Max(restBox[1], currentBox[1]);
                        float overlapRight = Math.Min(restBox[2], currentBox[2]);
                        float overlapBottom = Math.Min(restBox[3], currentBox[3]);

                        float overlapArea = AreaOf(overlapLeft, overlapTop, overlapRight, overlapBottom);
                        float area0 = AreaOf(restBox[0], restBox[1], restBox[2], restBox[3]);
                        float area1 = AreaOf(currentBox[0], currentBox[1], currentBox[2], currentBox[3]);

                        float iou = overlapArea / (area0 + area1 - overlapArea + 1e-5f);
                        if (iou <= 0.5f)
                        {
                            remainingIndexes.Add(index);
                        }
                    }
                    indexes = remainingIndexes;
                }

                for (int i = 0; i < picked.Count; i++)
                {
                    pickedBoxProbs.Add(new float[] { boxProbs[i, 0], boxProbs[i, 1], boxProbs[i, 2], boxProbs[i, 3], boxProbs[i, 4] });
                }

                pickedLabels.Add(classIndex);
            }
            float[,] pickedBoxProbsArray;
            int[] pickedLabelsArray;

            if (pickedBoxProbs.Count == 0)
            {
                pickedBoxProbsArray = new float[0, 5];
                pickedLabelsArray = new int[0];
                pickedLabels = new List<int>();
            }
            else
            {
                pickedBoxProbsArray = new float[pickedBoxProbs.Count, 5];
                pickedLabelsArray = pickedLabels.ToArray();

                for (int i = 0; i < pickedBoxProbs.Count; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        pickedBoxProbsArray[i, j] = pickedBoxProbs[i][j];
                    }
                }

                for (int i = 0; i < pickedBoxProbsArray.GetLength(0); i++)
                {
                    pickedBoxProbsArray[i, 0] *= width;
                    pickedBoxProbsArray[i, 1] *= height;
                    pickedBoxProbsArray[i, 2] *= width;
                    pickedBoxProbsArray[i, 3] *= height;
                }
            }


            float[,] newBoxes = new float[pickedBoxProbsArray.GetLength(0), 4];
            float[] newScores = new float[pickedBoxProbsArray.GetLength(0)];

            for (int i = 0; i < pickedBoxProbsArray.GetLength(0); i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    newBoxes[i, j] = pickedBoxProbsArray[i, j];
                }
                newScores[i] = pickedBoxProbsArray[i, 4];

                // Ensure the pickedLabels list has enough elements before accessing them
                if (i < pickedLabels.Count)
                {
                    detections.Add(new DetectionResult(
                        pickedBoxProbsArray[i, 4],
                        newBoxes[i, 0],
                        newBoxes[i, 1],
                        newBoxes[i, 2],
                        newBoxes[i, 3],
                        pickedLabels[i],
                        ""));
                }
            }

            Console.WriteLine($"Detected {detections.Count} in the frame");

            return detections;

        }

        private float[] PreprocessImage(Mat image)
        {
            int inputWidth = 300;
            int inputHeight = 300;
            Mat resizedImage = new Mat();
            CvInvoke.Resize(image, resizedImage, new Size(inputWidth, inputHeight));

            float[] normalizedImage = new float[3 * inputHeight * inputWidth];

            int index = 0;

            using (Image<Bgr, byte> bgrImage = resizedImage.ToImage<Bgr, byte>())
            {
                for (int y = 0; y < inputHeight; y++)
                {
                    for (int x = 0; x < inputWidth; x++)
                    {
                        Bgr pixel = bgrImage[y, x];

                        // Normalize pixel values
                        float red = (float)((pixel.Red - 127.0) / 128.0);
                        float green = (float)((pixel.Green - 127.0) / 128.0);
                        float blue = (float)((pixel.Blue - 127.0) / 128.0);

                        // Set the normalized values in the float array
                        normalizedImage[index++] = red;
                        normalizedImage[index++] = green;
                        normalizedImage[index++] = blue;
                    }
                }
            }

            return normalizedImage;
        }
    }
}
