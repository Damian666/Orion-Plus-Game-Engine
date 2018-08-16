using System.Drawing;

namespace System.Windows.Forms
{
    public class ColorProgressBar : Control
    {
        private Brush _backBrush;
        private Pen _borderPen;
        private Color _borderColor = Color.DarkSlateGray;
        private Brush _foreBrush;
        private int _min = 0;
        private int _max = 100;
        private int _val = 0;
        public int Step { get; set; } = 10;

        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                _backBrush = new SolidBrush(value);
                Invalidate();
            }
        }

        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                _borderPen = new Pen(value);
                Invalidate();
            }
        }

        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                _foreBrush = new SolidBrush(value);
                Invalidate();
            }
        }

        public int Minimum
        {
            get => _min;
            set
            {
                if (_val < value) _val = value;
                _min = value;
                Invalidate();
            }
        }

        public int Maximum
        {
            get => _max;
            set
            {
                if (_val > value) _val = value;
                _max = value;
                Invalidate();
            }
        }

        public int Value
        {
            get => _val;
            set
            {
                if (value < _min) _val = _min;
                else if (value > _max) _val = _max;
                else _val = value;
                Invalidate();
            }
        }

        public void PerformStep()
        {
            Value += Step;
        }

        public void PerformStep(int amount)
        {
            Value += amount;
        }

        protected override void OnCreateControl()
        {
            if (_backBrush == null) _backBrush = new SolidBrush(base.BackColor);
            if (_borderPen == null) _borderPen = new Pen(_borderColor);
            if (_foreBrush == null) _foreBrush = new SolidBrush(base.ForeColor);
            base.OnCreateControl();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var rect = e.ClipRectangle;
            rect.Width -= 1; rect.Height -= 1;
            e.Graphics.FillRectangle(_backBrush, rect);
            e.Graphics.DrawRectangle(_borderPen, rect);
            rect = new Rectangle(1, 1, rect.Width - 1, rect.Height - 1);

            e.Graphics.FillRectangle(_foreBrush, 1, 1,
                System.Convert.ToInt32(rect.Width *
                (System.Convert.ToDouble(Value / (double)Maximum))),
                rect.Height);
        }
    }
}