using System.Drawing;
using System.Windows.Forms;

namespace ManagementStore.Form.Customize
{
    public class CountdownPictureBox : PictureBox
    {
        public int CountdownValue { get; set; }
        public CountdownPictureBox()
        {

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (var brush = new SolidBrush(ForeColor))
            {
                var format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                e.Graphics.DrawString(CountdownValue.ToString(), Font, brush, ClientRectangle, format);
            }
        }
    }
}
