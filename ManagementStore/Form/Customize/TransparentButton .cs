using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementStore.Form.Customize
{
    public class TransparentButton : Button
    {
        public TransparentButton()
        {
            // Set the control style to enable the transparent background
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Create a new GraphicsPath to define the button's shape
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(ClientRectangle); // You can customize the shape here, such as using RoundedRectangles, etc.

                // Set the button's Region to the GraphicsPath to make it non-rectangular
                Region = new Region(path);

                // Draw the button's text with a transparent background
                TextRenderer.DrawText(e.Graphics, "1", Font, ClientRectangle, ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }
    }
}
