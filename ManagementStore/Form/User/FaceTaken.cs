using Emgu.CV;
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
using System.Windows.Forms;
using ManagementStore.Form.Customize;
using DevExpress.XtraEditors;
using ManagementStore.Form.Notify;
using ManagementStore.Model.Static;
using System.IO;
using Connect.Common.Contract;
using Parking.App.Common.ApiMethod;
using Parking.App.Common.ViewModels;
using Parking.App.Contract.Common;
using ManagementStore.Model.ML;

namespace ManagementStore.Form.User
{
    public partial class FaceTaken : System.Windows.Forms.UserControl
    {
        private VideoCapture capture;
        private InferenceSession session;
        private FPSCounter fpsCounter;
        private int countdownValue;
        private Timer timer;
        private int countObject = 0;
        ShowImageTaken image;
        ConfirmInfo info;
        List<DetectionResult> detectionResults;
        ObjectDetectionSSD ssd;
        public FaceTaken()
        {
            InitializeComponent();
            string path = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);

            ssd = new ObjectDetectionSSD(ModelConfig.dataFolderPath + "/mb2-ssd-lite-predict.onnx");

            // Load the ONNX model
            // session = new InferenceSession(ModelConfig.dataFolderPath + "/mb1-ssd-predict.onnx");
            // Initialize the camera capture
            capture = new VideoCapture();
            capture.ImageGrabbed += Capture_ImageGrabbedSSD;
            capture.Start();
            // Set the initial countdown value and Timer interval
            countdownValue = 5;
            timer = new Timer();
            timer.Interval = 1000; // 1 second
            //timer.Tick += Timer_Tick;

            // Start the Timer
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            countdownValue--;
            showCountDown.Text = $"The photo will be taken in {countdownValue.ToString()} seconds";

            // When the countdown reaches 0, stop the Timer and capture the picture
            if (countdownValue == 0)
            {
                image = new ShowImageTaken();
                if (countObject == 0)
                {
                    //capture.ImageGrabbed -= Capture_ImageGrabbed;
                    capture.Stop();
                    var result = XtraMessageBox.Show("Can not detect the face, please try again", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (DialogResult.OK == result)
                    {
                        //capture.ImageGrabbed += Capture_ImageGrabbed;
                        capture.Start();
                        countdownValue = 5;
                        timer.Start();
                    }
                }
                else
                {
                    image.Show();
                    image.pictureBoxTaken.Image = pictureFace.Image;
                    //capture.ImageGrabbed -= Capture_ImageGrabbed;
                    capture.Stop();
                    image.btnTakeAgain.Click += btnTakeAgain_Click;
                    image.btnOK.Click += btnOK_Click;
                }
                timer.Stop();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // capture.ImageGrabbed -= Capture_ImageGrabbed;
            capture.Stop();
            Image img = image.pictureBoxTaken.Image;
            info = new ConfirmInfo();
            // Convert the image to a byte array
            byte[] imageBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Use the appropriate image format
                imageBytes = ms.ToArray();
            }
            UserInfo.Picture = Convert.ToBase64String(imageBytes);
            info.fullNameTxt.Text = UserInfo.FullName;
            info.phoneTxt.Text = UserInfo.PhoneNumber;
            info.birthdayTxt.Text = UserInfo.BirthDay;
            info.genderTxt.Text = UserInfo.Gender;
            info.pictureTaken.Image = img;
            info.Show();
            info.btnBack.Click += btnBack_Click;
            info.btnConfirm.Click += btnConfirm_Click;
            image.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            splashRegisterUser.ShowWaitForm();
            onCreateUser();
            splashRegisterUser.CloseWaitForm();
        }

        private async void onCreateUser()
        {
            RequestInfo request = new RequestInfo();
            var uInfo = new object[5];
            uInfo[0] = UserInfo.FullName == null ? "Nguyen Ngoc Thien" : UserInfo.FullName;
            uInfo[1] = UserInfo.PhoneNumber == null ? "0365858975" : UserInfo.PhoneNumber;
            uInfo[2] = UserInfo.Gender == null ? "Male" : UserInfo.Gender;
            uInfo[3] = UserInfo.Picture == null ? "124" : UserInfo.Picture;
            uInfo[4] = UserInfo.BirthDay == null ? "2023-06-23" : UserInfo.BirthDay;

            tblUserInfo user = new tblUserInfo()
            {
                UserID = String.Empty,
                UserType = "USR001",
                Password = "",
                UserName = UserInfo.FullName,
                PhoneNumber = UserInfo.PhoneNumber,
                Birthday = Convert.ToDateTime(UserInfo.BirthDay),
                Email = String.Empty,
                Gender = UserInfo.Gender == "Male" ? true : false,
                ApproveReject = true,
                UserStatus = String.Empty,
                RegistDate = DateTime.Now,
                Desc = String.Empty,
                UseYN = false,
                LoginIP = String.Empty,
                AuthMethod = "Phone Number"
            };
            request.Data = user;
            DataRequest userMgtData = new DataRequest()
            {
                Signature = 104,
                FrameID = 0,
                FunctionCode = 4098,
                DataLength = 10000,
                Data = request
            };

            var repose = await ApiMethod.PostCall(userMgtData);
            if (repose.StatusCode == System.Net.HttpStatusCode.OK)
            {

            }
            else
            {
                XtraMessageBox.Show("Register user failed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Settings.countDown = 120;
            info.Close();
            Utils.Back(ParentForm, "pictureBoxOTP", "pictureBoxPhone", "PhoneNumber");
        }

        private void btnTakeAgain_Click(object sender, EventArgs e)
        {
            countdownValue = 5;
            capture.ImageGrabbed += Capture_ImageGrabbed;
            timer.Start();
            image.Close();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            Utils.Back(ParentForm, "pictureBoxFace", "pictureBoxName", "FullName");

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

        private void Capture_ImageGrabbedSSD(object send, EventArgs e)
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


                detectionResults = ssd.DetectObjects(frame);

                DrawBoundingBoxesSSD(frame, detectionResults);
                pictureFace.Image = frame.ToBitmap();
                fpsCounter.Update();
                Console.WriteLine("FPS: " + fpsCounter.CurrentFPS.ToString("F2"));
            }

        }
        private void Capture_ImageGrabbed(object sender, EventArgs e)
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

                // Preprocess the frame to match the model input requirements
                var inputMeta = session.InputMetadata;
                uint[] inputData = Preprocess(frame, inputMeta);

                // Create an input tensor from the preprocessed data
                var inputName = inputMeta.Keys.FirstOrDefault();
                var tensor = new DenseTensor<uint>(inputData, new int[] { 1, 3, 300, 300 });
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
                Console.WriteLine("FPS: " + fpsCounter.CurrentFPS.ToString("F2"));
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
            countObject = detections.Count;
            return detections;
        }
        private void DrawBoundingBoxes(Mat frame, List<DetectionResult> detectionResults)
        {
            foreach (var detection in detectionResults)
            {
                int x = (int)(detection.Left * frame.Width);
                int y = (int)(detection.Top * frame.Height);
                int width = (int)((detection.Right - detection.Left) * frame.Width);
                int height = (int)((detection.Bottom - detection.Top) * frame.Height);

                if (detection.Score > 0.5)
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
        private void DrawBoundingBoxesSSD(Mat frame, List<DetectionResult> detectionResults)
        {
            foreach (var detection in detectionResults)
            {
                int x = (int)detection.Top;
                int y = (int)detection.Right;
                int width = (int)(detection.Bottom - detection.Top);
                int height = (int)(detection.Left - detection.Right);

                if (detection.Score > 0.5)
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