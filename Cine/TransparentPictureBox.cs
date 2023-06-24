using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cine
{
    public class TransparentPictureBox : PictureBox
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parameters = base.CreateParams;
                parameters.ExStyle |= 0x20; // WS_EX_TRANSPARENT
                return parameters;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Do not paint the background.
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Set the transparent background color.
            e.Graphics.Clear(Color.Transparent);
        }
    }
}
