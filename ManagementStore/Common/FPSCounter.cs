
using System.Diagnostics;

namespace ManagementStore
{
    public class FPSCounter
    {
        private Stopwatch stopwatch;
        private int frameCount;

        public float CurrentFPS { get; private set; }

        public FPSCounter()
        {
            stopwatch = new Stopwatch();
            frameCount = 0;
            CurrentFPS = 0f;
        }

        public void Start()
        {
            stopwatch.Start();
        }

        public void Update()
        {
            frameCount++;

            if (stopwatch.Elapsed.TotalSeconds >= 1.0)
            {
                CurrentFPS = frameCount / (float)stopwatch.Elapsed.TotalSeconds;
                frameCount = 0;
                stopwatch.Restart();
            }
        }
    }


}