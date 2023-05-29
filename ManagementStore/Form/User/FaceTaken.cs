﻿using Emgu.CV;
using Emgu.CV.Structure;
using ManagementStore.Common;
using ManagementStore.Extensions;
using System;
using System.Drawing;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System.Collections.Generic;
using System.Linq;
using Emgu.CV.CvEnum;
using ManagementStore.DTO;

namespace ManagementStore.Form.User
{
    public partial class FaceTaken : System.Windows.Forms.UserControl
    {
        private VideoCapture capture;
        //private CascadeClassifier cascadeClassifier;
        private InferenceSession session;
        public FaceTaken()
        {
            InitializeComponent();
            string path = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            //cascadeClassifier = new CascadeClassifier(ModelConfig.settingFolderPath + "/haarcascade_frontalface_default.xml");
            // Load the ONNX model
            session = new InferenceSession(ModelConfig.dataFolderPath + "/ssd_mobilenet_v1_12-int8.onnx");

            // Initialize the camera capture
            capture = new VideoCapture();
            capture.ImageGrabbed += Capture_ImageGrabbed;
            capture.Start();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            Utils.Back(ParentForm, "pictureBoxFace", "pictureBoxName", "FullName");

        }
        
        private void showCam_Click(object sender, EventArgs e)
        {

        }

        private void FaceTaken_Load(object sender, EventArgs e)
        {
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            // Release the resources when closing the form
            capture?.Dispose();
            //cascadeClassifier?.Dispose();
            session?.Dispose();
        }
        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            FPSCounter fpsCounter = new FPSCounter();
            fpsCounter.Start();
            if (capture != null && capture.Ptr != IntPtr.Zero)
            {

                Mat frame = new Mat();
                capture.Retrieve(frame);

                // Preprocess the frame to match the model input requirements
                var inputMeta = session.InputMetadata;
                uint[] inputData = Preprocess(frame, inputMeta);

                // Create an input tensor from the preprocessed data
                var inputName = inputMeta.Keys.FirstOrDefault();
                var tensor = new DenseTensor<uint>(inputData, new int[] {1, 300, 300, 3 });
                // Convert to DenseTensor<byte> (uint8)
                var byteTensor = new DenseTensor<byte>(tensor.Dimensions);

                for (int i = 0; i < tensor.Length; i++)
                {
                    byteTensor.SetValue(i, (byte)(tensor.GetValue(i) & 0xFF));
                }
                // Run the object detection model
                var inputs = new NamedOnnxValue[] { NamedOnnxValue.CreateFromTensor(inputName, byteTensor) };
                var outputs = session.Run(inputs);
                var outputName = session.OutputMetadata.Keys.ToList();
               
                // Get the output tensor with the detected objects
                var outputTensor = outputs.Select(output => output.AsTensor<float>());
                var detectionResults = Postprocess(outputTensor.ToList());

                // Draw bounding boxes around the detected objects
                DrawBoundingBoxes(frame, detectionResults);

                // Display the frame in the PictureBox control
                pictureFace.Image = frame.ToBitmap();
                fpsCounter.Update();
                labelControl1.Text = fpsCounter.CurrentFPS.ToString();
            }
        }
        private uint[] Preprocess(Mat frame, IReadOnlyDictionary<string, NodeMetadata> inputMeta)
        {
            // Resize the frame to match the model input size
            var inputName = inputMeta.Keys.FirstOrDefault();
            var inputSize = 300;
            var resizedFrame = new Mat();
            CvInvoke.Resize(frame, resizedFrame, new Size(inputSize, inputSize));

            // Convert the resized frame to a float array
            var inputData = new uint[inputSize * inputSize * 3];
            int index = 0;

            var data = new byte[resizedFrame.Rows * resizedFrame.Cols * resizedFrame.NumberOfChannels];
            resizedFrame.CopyTo(data);

            for (int row = 0; row < inputSize; row++)
            {
                for (int col = 0; col < inputSize; col++)
                {
                    var offset = (row * inputSize + col) * 3;
                    inputData[index++] = data[offset + 0]; // Blue channel
                    inputData[index++] = data[offset + 1]; // Green channel
                    inputData[index++] = data[offset + 2]; // Red channel
                }
            }

            return inputData;
        }


        private List<DetectionResult> Postprocess(List<Tensor<float>> outputTensor)
        {
            List<DetectionResult> detections = new List<DetectionResult>();
            // Extract the values from the output
            float[] detectionBoxes = outputTensor[0].ToArray();
            float[] detectionClasses = outputTensor[1].ToArray();
            float[] detectionScores = outputTensor[2].ToArray();
            float[] numDetections = outputTensor[3].ToArray();
            // Convert the numDetections value to an integer
            int numDetectionsInt = Convert.ToInt32(numDetections[0]);

            // Slice the arrays to retain only the relevant number of detections
            float[] slicedDetectionBoxes = detectionBoxes.Take(4 * numDetectionsInt).ToArray();
            float[,] slicedDetectionBoxes2D = new float[numDetectionsInt, 4];
            Buffer.BlockCopy(slicedDetectionBoxes, 0, slicedDetectionBoxes2D, 0, slicedDetectionBoxes.Length * sizeof(int));
            float[] slicedDetectionScores = detectionScores.Take(numDetectionsInt).ToArray();
            int[] slicedDetectionClasses = detectionClasses.Select(p => Convert.ToInt32(p)).Take(numDetectionsInt).ToArray();

            var cocoClasses = Utils.GetCoCoClasses();

            List<string> mappedClasses = new List<string>();
            foreach (float classIdx in slicedDetectionClasses)
            {
                int classIdxInt = Convert.ToInt32(classIdx);
                if (cocoClasses.ContainsKey(classIdxInt))
                    mappedClasses.Add(cocoClasses[classIdxInt]);
            }

            for (int i = 0; i < numDetectionsInt; i++)
            {
                // Extract the bounding box coordinates
                float top = slicedDetectionBoxes2D[i, 0];
                float left = slicedDetectionBoxes2D[i, 1];
                float bottom = slicedDetectionBoxes2D[i, 2];
                float right = slicedDetectionBoxes2D[i, 3];

                // Extract the score and class label
                float score = slicedDetectionScores[i];
                int classLabel = slicedDetectionClasses[i];
                string className = cocoClasses.ContainsKey(classLabel) ? cocoClasses[classLabel] : "UnKnown";

                detections.Add(new DetectionResult(score, top, right, bottom, left, classLabel, className));

            }
            
            return detections;
        }
        private void DrawBoundingBoxes(Mat frame, List<DetectionResult> detectionResults)
        {
            // Draw bounding boxes around the detected objects
            // Implement the drawing logic based on your model's output format
            // This is just a placeholder implementation
            foreach (var detection in detectionResults)
            {
                //CvInvoke.Rectangle(frame, result.BoundingBox, new Bgr(Color.Red).MCvScalar, thickness: 2);
                int x = (int)(detection.Left * frame.Width);
                int y = (int)(detection.Top * frame.Height);
                int width = (int)((detection.Right - detection.Left) * frame.Width);
                int height = (int)((detection.Bottom -detection.Top) * frame.Height);

                if(detection.Score > 0.6)
                {
                    // Draw the rectangle
                    Rectangle rect = new Rectangle(x, y, width, height);
                    CvInvoke.Rectangle(frame, rect, new Bgr(Color.Red).MCvScalar, thickness: 2);
                    // Display the class name
                    Point textLocation = new Point(x, y - 10);
                    CvInvoke.PutText(frame, detection.ClassName + " " + detection.Score.ToString("##.##"), textLocation,
                        FontFace.HersheySimplex, fontScale: 0.5, new Bgr(Color.Red).MCvScalar);
                }
            }
        }

    }
}
