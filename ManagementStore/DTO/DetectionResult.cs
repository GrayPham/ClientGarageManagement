namespace ManagementStore.DTO
{
    public class DetectionResult
    {
        public float Score { get; set; }
        public float Top { get; set; }
        public float Right { get; set; }
        public float Bottom { get; set; }
        public float Left { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }

        public DetectionResult(float score, float top, float right, float bottom, float left, int classId, string className)
        {
            Score = score;
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
            ClassId = classId;
            ClassName = className;
        }
    }
}
