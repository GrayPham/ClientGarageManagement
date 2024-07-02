using DevExpress.XtraEditors;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using ManagementStore.Common;
using ManagementStore.DTO;
using ManagementStore.Form.Notify;
using ManagementStore.Model.Static;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ManagementStore.Extensions;
using Parking.App.Common.Helper;
using FaceONNX;
using UMapx.Visualization;

namespace ManagementStore.Form.User.ResisterUserSub
{
    public partial class FaceTakenCCCD : System.Windows.Forms.UserControl
    {
        public VideoCapture capture;
        private InferenceSession session;
        private FPSCounter fpsCounter;
        private int countdownValue;
        public Timer timer;
        List<FaceDetectionResult> faceDetectionResults;
        ShowImageTaken image;
        private string fileNameAudio;
        private bool takeAgain = false;
        FaceDetector faceDetector;
        private bool useGPU = false;
        private async void FaceTakenCCCD_Load(object sender, EventArgs e)
        {
            fileNameAudio = await AudioConstants.GetListSound(AudioConstants.FaceTaken);
            if (fileNameAudio != null && fileNameAudio != "")
            {
                Helpers.PlaySound(@"Assets\Audio\" + fileNameAudio + ".wav");
            }
            else
            {
                Helpers.PlaySound(@"Assets\Audio\" + AudioConstants.FaceTaken + ".wav");
            }
        }
        public FaceTakenCCCD()
        {
            InitializeComponent();
            splashScreenManager2.ShowWaitForm();
            using var options = useGPU ? SessionOptions.MakeSessionOptionWithCudaProvider(0) : new SessionOptions();
            faceDetector = new FaceDetector(options);
            faceDetectionResults = new List<FaceDetectionResult>();
            string path = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            // Load the ONNX model
            // session = new InferenceSession(ModelConfig.dataFolderPath + "/ssd_mobilenet_v1_12-int8.onnx");
            splashScreenManager2.CloseWaitForm();

            // Initialize the camera capture
            capture = new VideoCapture();
            capture.ImageGrabbed += Capture_ImageGrabbed;
            capture.Start();
            
            // Set the initial countdown value and Timer interval
            countdownValue = 5;
            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;

            // Start the Timer
            timer.Start();
        }

        private void CloseForm(object sender, FormClosingEventArgs e)
        {
            if(takeAgain == true)
            {
                timer.Start();
                countdownValue = 5;
                capture.ImageGrabbed += Capture_ImageGrabbed;
                capture.Start();
                btnPrev.Enabled = true;
            }
            
            
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            countdownValue--;
            showCountDown.Text = $"Ảnh sẽ được chụp sau {countdownValue.ToString()} giây nữa";

            // When the countdown reaches 0, stop the Timer and capture the picture
            if (countdownValue == 0)
            {
                image = new ShowImageTaken();
                image.FormClosing += CloseForm;
                if (faceDetectionResults.Count == 0)
                {
                    // capture.ImageGrabbed -= Capture_ImageGrabbed;
                    var result = XtraMessageBox.Show("Không thể phát hiện khuôn mặt, vui lòng thử lại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (DialogResult.OK == result)
                    {
                        countdownValue = 5;
                        timer.Start();
                    }
                }
                else
                {
                    image.Show();
                    image.pictureBoxTaken.Image = pictureFace.Image;
                    capture.ImageGrabbed -= Capture_ImageGrabbed;
                    image.btnTakeAgain.Click += btnTakeAgain_Click;

                    image.btnOK.Click += btnOK_Click;
                    btnPrev.Enabled = false;

                }
                timer.Stop();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            capture.ImageGrabbed -= Capture_ImageGrabbed;
            timer.Tick -= Timer_Tick;
            Image img = image.pictureBoxTaken.Image;

            // Convert the image to a byte array
            
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] imageBytes;
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Use the appropriate image format
                imageBytes = ms.ToArray();
                UserCCCD.Picture = Convert.ToBase64String(imageBytes);
                UserCCCD.PictureByte = imageBytes;
            }
            takeAgain = false;
            image.Close();
            btnPrev.Enabled = true;
            btnDone.Enabled = true;
        }
        private void btnTakeAgain_Click(object sender, EventArgs e)
        {
            takeAgain = true;
            countdownValue = 5;
            //capture.ImageGrabbed += Capture_ImageGrabbed;
            timer.Start();
            image.Close();
            
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            splashScreenManager2.ShowWaitForm();
            // Release the resources when closing the form
            capture?.Dispose();
            //cascadeClassifier?.Dispose();
            session?.Dispose();


            Helpers.StopSound();
            Utils.ForwardCCCD(ParentForm, "pictureBoxFace", "pictureBoxFace", "FaceTakenCCCD");

            ConfimRegister confimRegister = new ConfimRegister();
            splashScreenManager2.CloseWaitForm();
            confimRegister.ShowDialog();

            if (confimRegister.CaptureAgain)
            {
                splashScreenManager2.ShowWaitForm();
                btnDone.Enabled = false;
                btnPrev.Enabled = false;
                // session = new InferenceSession(ModelConfig.dataFolderPath + "/ssd_mobilenet_v1_12-int8.onnx");
                // Initialize the camera capture
                capture = new VideoCapture();
                capture.ImageGrabbed += Capture_ImageGrabbed;
                capture.Start();
                countdownValue = 5;
                timer.Start();
                btnDone.Enabled = true;
                btnPrev.Enabled = true;
                splashScreenManager2.CloseWaitForm();
                //Utils.BackCCCD(ParentForm, "pictureBoxName", "pictureBoxFace", "FaceTakenCCCD");

            }

        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            Helpers.StopSound();
            capture.Dispose();
            Utils.BackCCCD(ParentForm, "pictureBoxFace", "pictureBoxName", "FullNameCCCD");
            timer.Tick -= Timer_Tick;
        }
        public void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            if (fpsCounter == null)
            {
                fpsCounter = new FPSCounter();
                fpsCounter.Start();
            }

            if (capture != null && capture.Ptr != IntPtr.Zero)
            {

                Mat frame = new Mat();
                capture.Retrieve(frame);

                //var inputMeta = session.InputMetadata;
                //uint[] inputData = Preprocess(frame, inputMeta);

                //var inputName = inputMeta.Keys.FirstOrDefault();
                //var tensor = new DenseTensor<uint>(inputData, new int[] { 1, 300, 300, 3 });
                //var byteTensor = new DenseTensor<byte>(tensor.Dimensions);

                //for (int i = 0; i < tensor.Length; i++)
                //{
                //    byteTensor.SetValue(i, (byte)(tensor.GetValue(i) & 0xFF));
                //}
                //var inputs = new NamedOnnxValue[] { NamedOnnxValue.CreateFromTensor(inputName, byteTensor) };
                //var outputs = session.Run(inputs);
                //var outputName = session.OutputMetadata.Keys.ToList();

                //var outputTensor = outputs.Select(output => output.AsTensor<float>());
                //var detectionResults = Postprocess(outputTensor.ToList());

                //DrawBoundingBoxes(frame, detectionResults);

                
                Image<Bgr, Byte> image = frame.ToImage<Bgr, byte>();

                var bitmap = image.Clone();
                var imagePlot = image.ToBitmap();
                faceDetectionResults = faceDetector.Forward(bitmap.ToBitmap()).ToList();
                FaceDetectionResult faceDetectionResult = GetBoundingBoxWithLargestArea(faceDetectionResults);
                var res = ClassifyFaceOrientation(faceDetectionResult.Points);
                Console.WriteLine(res);
                pictureFace.Image = imagePlot;

                fpsCounter.Update();
                Console.WriteLine("FPS: " + fpsCounter.CurrentFPS.ToString("F2"));
            }
        }

        private double CalculateAngle(Point p1, Point p2, Point p3)
        {
            double dx1 = p2.X - p1.X;
            double dy1 = p2.Y - p1.Y;
            double dx2 = p3.X - p1.X;
            double dy2 = p3.Y - p1.Y;

            double angle = Math.Atan2(dy2, dx2) - Math.Atan2(dy1, dx1);

            angle = angle * (180 / Math.PI);

            return angle;
        }
        public string ClassifyFaceOrientation(Face5Landmarks landmarks)
        {
            Point leftEye = landmarks.LeftEye;
            Point rightEye = landmarks.RightEye;
            Point nose = landmarks.Nose;

            double angle = CalculateAngle(leftEye, rightEye, nose);

            if (angle < -15)
            {
                return "Xoay trái";
            }
            else if (angle > 15)
            {
                return "Xoay phải";
            }
            else if (landmarks.SymmetryCoefficient < 0.2)
            {
                return "Chính diện";
            }
            else if (angle >= -15 && angle <= 15)
            {
                if (angle < 0)
                {
                    return "Gật xuống";
                }
                else if (angle > 0)
                {
                    return "Ngửa đầu";
                }
            }

            return "Không xác định";
        }

        public FaceDetectionResult GetBoundingBoxWithLargestArea(List<FaceDetectionResult> faceDetectionResults)
        {
            if (faceDetectionResults == null || faceDetectionResults.Count == 0)
            {
                return null;
            }

            int maxArea = 0;
            FaceDetectionResult bboxWithLargestArea = null;

            foreach (var detectionResult in faceDetectionResults)
            {
                int area = detectionResult.Box.Width * detectionResult.Box.Height;

                if (area > maxArea)
                {
                    maxArea = area;
                    bboxWithLargestArea = detectionResult;
                }
            }

            return bboxWithLargestArea;
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
                int height = (int)((detection.Bottom - detection.Top) * frame.Height);

                if (detection.Score > 0.6)
                {
                    // Draw the rectangle
                    Rectangle rect = new Rectangle(x, y, width, height);
                    CvInvoke.Rectangle(frame, rect, new Bgr(Color.Red).MCvScalar, thickness: 2);
                    // Display the class name
                    Point textLocation = new Point(x, y - 10);
                    CvInvoke.PutText(frame, detection.ClassName + " " + (detection.Score * 100).ToString("##.##"), textLocation,
                        FontFace.HersheySimplex, fontScale: 0.5, new Bgr(Color.Red).MCvScalar);
                }
            }
        }


    }
}
