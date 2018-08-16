using System.Drawing;

namespace System.Windows.Forms
{
    public class ColorButton : Control
    {
        private readonly RectangleF[] _dRect = new RectangleF[10];
        private readonly RectangleF[] _sRect = new RectangleF[10];
        private Rectangle _textBounds;
        private Image _img;
        private bool _btnDown;
        private bool _btnHover;

        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;

                var bmp = Properties.Resources.Skin_Button;
                Color color; byte r, g, b;
                for (var x = 0; x <= bmp.Width - 1; x++)
                {
                    for (var y = 0; y <= bmp.Height - 1; y++)
                    {
                        color = bmp.GetPixel(x, y);
                        r = (byte)(color.R * 0.5 + BackColor.R * 0.5);
                        g = (byte)(color.G * 0.5 + BackColor.G * 0.5);
                        b = (byte)(color.B * 0.5 + BackColor.B * 0.5);
                        bmp.SetPixel(x, y, Color.FromArgb(255, r, g, b));
                    }
                }

                _img = bmp;
            }
        }

        private void SetArea()
        {
            float d = 0;
            if (_btnDown)
                d = 9;
            else if (_btnHover)
                d = 18;

            float w, h;
            if (Width - 6 >= 0) w = Width - 6.0F;
            else w = Width;
            if (Height - 6 >= 0) h = Height - 6.0F;
            else h = Height;

            _dRect[0] = new RectangleF(0.0F, 0.0F, 3.0F, 3.0F);
            _dRect[1] = new RectangleF(3.0F, 0.0F, w, 3.0F);
            _dRect[2] = new RectangleF(Width - 3.0F, 0.0F, 3.0F, 3.0F);
            _dRect[3] = new RectangleF(0.0F, 3.0F, 3.0F, h);
            _dRect[4] = new RectangleF(3.0F, 3.0F, w, h);
            _dRect[5] = new RectangleF(Width - 3.0F, 3.0F, 3.0F, h);
            _dRect[6] = new RectangleF(0.0F, Height - 3.0F, 3.0F, 3.0F);
            _dRect[7] = new RectangleF(3.0F, Height - 3.0F, w, 3.0F);
            _dRect[8] = new RectangleF(Width - 3.0F, Height - 3.0F, 3.0F, 3.0F);

            _sRect[0] = new RectangleF(d, 0.0F, 3.0F, 3.0F);
            _sRect[1] = new RectangleF(d + 3.0F, 0.0F, 3.0F, 3.0F);
            _sRect[2] = new RectangleF(d + 6.0F, 0.0F, 3.0F, 3.0F);
            _sRect[3] = new RectangleF(d, 3.0F, 3.0F, 3.0F);
            _sRect[4] = new RectangleF(d + 3.0F, 3.0F, 3.0F, 3.0F);
            _sRect[5] = new RectangleF(d + 6.0F, 3.0F, 3.0F, 3.0F);
            _sRect[6] = new RectangleF(d, 6.0F, 3.0F, 3.0F);
            _sRect[7] = new RectangleF(d + 3.0F, 6.0F, 3.0F, 3.0F);
            _sRect[8] = new RectangleF(d + 6.0F, 6.0F, 3.0F, 3.0F);

            _textBounds = new Rectangle(4, 4, Width - 8, Height - 8);

            Refresh();
        }

        protected override void OnCreateControl()
        {
            BackColor = BackColor;
            SetArea();

            MouseDown += (sender, e) =>
            {
                _btnDown = true;
                SetArea();
            };

            MouseUp += (sender, e) =>
            {
                _btnDown = false;
                SetArea();
            };

            MouseEnter += (sender, e) =>
            {
                _btnHover = true;
                SetArea();
            };

            MouseLeave += (sender, e) =>
            {
                _btnDown = false;
                _btnHover = false;
                SetArea();
            };

            Resize += (sender, e) => { SetArea(); };
            TextChanged += (sender, e) => { SetArea(); };

            base.OnCreateControl();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(_img, _dRect[0], _sRect[0], GraphicsUnit.Pixel);
            e.Graphics.DrawImage(_img, _dRect[1], _sRect[1], GraphicsUnit.Pixel);
            e.Graphics.DrawImage(_img, _dRect[2], _sRect[2], GraphicsUnit.Pixel);
            e.Graphics.DrawImage(_img, _dRect[3], _sRect[3], GraphicsUnit.Pixel);
            e.Graphics.DrawImage(_img, _dRect[4], _sRect[4], GraphicsUnit.Pixel);
            e.Graphics.DrawImage(_img, _dRect[5], _sRect[5], GraphicsUnit.Pixel);
            e.Graphics.DrawImage(_img, _dRect[6], _sRect[6], GraphicsUnit.Pixel);
            e.Graphics.DrawImage(_img, _dRect[7], _sRect[7], GraphicsUnit.Pixel);
            e.Graphics.DrawImage(_img, _dRect[8], _sRect[8], GraphicsUnit.Pixel);

            using (var format = new StringFormat())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                if (Enabled) e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), _textBounds, format);
                else e.Graphics.DrawString(Text, Font, new SolidBrush(Color.Gray), _textBounds, format);
            }
        }
    }
}