namespace System.Windows.Forms
{
    public class DragableBorderlessForm : Form
    {
        private int _x, _y;
        private bool _isDown;

        public int MoveableTop { get; set; } = 0;
        public int MoveableLeft { get; set; } = 0;
        public int MoveableRight { get; set; } = 0;
        public int MoveableBottom { get; set; } = 0;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            MouseDown += (sender, e) =>
            {
                if (FormBorderStyle != FormBorderStyle.None) return;

                if ((e.X > MoveableLeft && e.X < Width - MoveableRight) &&
                    (e.Y > MoveableTop && e.Y < Height - MoveableBottom)) return;

                _isDown = true;
                _x = e.X;
                _y = e.Y;
            };

            MouseMove += (sender, e) =>
            {
                if (!_isDown) return;
                Left = Cursor.Position.X - _x;
                Top = Cursor.Position.Y - _y;
            };

            MouseUp += (sender, e) =>
            {
                _isDown = false;
                _x = 0;
                _y = 0;
            };
        }
    }
}