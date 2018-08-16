using System.Drawing;

namespace System.Windows.Forms
{
    public sealed class TitleBar : Panel
    {
        #region Block Spacing
        private int _blockSpacingH = 2;
        public int BlockHeightSpacing
        {
            get => _blockSpacingH;
            set
            {
                _blockSpacingH = value;
                AdjustTitleBar();
            }
        }

        private int _blockSpacingW = 2;
        public int BlockWidthSpacing
        {
            get => _blockSpacingW;
            set
            {
                _blockSpacingW = value;
                AdjustTitleBar();
            }
        }
        #endregion

        #region Buttons
        public enum ButtonLocations
        {
            Left,
            Right
        }

        private ButtonLocations _locButton = ButtonLocations.Right;
        public ButtonLocations ButtonLocation
        {
            get => _locButton;
            set
            {
                _locButton = value;
                AdjustTitleBar();
            }
        }

        private int _buttonSpacing = 2;
        public int ButtonSpacing
        {
            get => _buttonSpacing;
            set
            {
                _buttonSpacing = value;
                AdjustTitleBar();
            }
        }

        #region Close
        private ColorButton _btnClose = new ColorButton();

        public Color ButtonCloseColor
        {
            get => _btnClose.BackColor;
            set => _btnClose.BackColor = value;
        }

        public bool ButtonCloseVisible
        {
            get => _btnClose.Visible;
            set
            {
                _btnClose.Visible = value;
                AdjustTitleBar();
            }
        }
        #endregion

        #region Maximize
        private ColorButton _btnMaximize = new ColorButton();

        public Color ButtonMaximizeColor
        {
            get => _btnMaximize.BackColor;
            set => _btnMaximize.BackColor = value;
        }

        public bool ButtonMaximizeVisible
        {
            get => _btnMaximize.Visible;
            set
            {
                _btnMaximize.Visible = value;
                AdjustTitleBar();
            }
        }
        #endregion

        #region Minimize
        private ColorButton _btnMinimize = new ColorButton();

        public Color ButtonMinimizeColor
        {
            get => _btnMinimize.BackColor;
            set => _btnMinimize.BackColor = value;
        }

        public bool ButtonMinimizeVisible
        {
            get => _btnMinimize.Visible;
            set
            {
                _btnMinimize.Visible = value;
                AdjustTitleBar();
            }
        }
        #endregion
        #endregion

        #region Icon
        private PictureBox _picIcon = new PictureBox();
        public Bitmap Icon
        {
            get => (Bitmap)_picIcon.Image;
            set => _picIcon.Image = value;
        }

        public bool IconVisible
        {
            get => _picIcon.Visible;
            set
            {
                _picIcon.Visible = value;
                AdjustTitleBar();
            }
        }
        #endregion

        #region Title
        public enum TitleLocations
        {
            Left,
            Middle,
            Right
        }

        private TitleLocations _locTitle = 0;
        public TitleLocations TitleLocation
        {
            get => _locTitle;
            set
            {
                _locTitle = value;
                AdjustTitleBar();
            }
        }

        private Label _lblTitle = new Label(); // Set text on Adjust
        public bool TitleVisible
        {
            get => _lblTitle.Visible;
            set => _lblTitle.Visible = value;
        }
        #endregion

        #region Overrides
        public new MouseEventHandler MouseDown;
        public new MouseEventHandler MouseMove;
        public new MouseEventHandler MouseUp;
        protected override void OnCreateControl()
        {
            BackColor = Color.Transparent;

            Controls.Add(_btnClose);
            Controls.Add(_btnMaximize);
            Controls.Add(_btnMinimize);
            Controls.Add(_lblTitle);
            Controls.Add(_picIcon);

            _btnClose.Click += (sender, e) => { FindForm().Close(); };
            _btnMaximize.Click += (sender, e) =>
            {
                var frm = FindForm();

                if (frm.WindowState == FormWindowState.Normal)
                    frm.WindowState = FormWindowState.Maximized;
                else if (frm.WindowState == FormWindowState.Maximized)
                    frm.WindowState = FormWindowState.Normal;
            };
            _btnMinimize.Click += (sender, e) => { FindForm().WindowState = FormWindowState.Minimized; };

            _picIcon.MouseDown += (sender, e) => { MouseDown?.Invoke(sender, e); };
            _picIcon.MouseMove += (sender, e) => { MouseMove?.Invoke(sender, e); };
            _picIcon.MouseUp += (sender, e) => { MouseUp?.Invoke(sender, e); };

            _lblTitle.MouseDown += (sender, e) => { MouseDown?.Invoke(sender, e); };
            _lblTitle.MouseMove += (sender, e) => { MouseMove?.Invoke(sender, e); };
            _lblTitle.MouseUp += (sender, e) => { MouseUp?.Invoke(sender, e); };

            _picIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            _lblTitle.AutoSize = false;

            AdjustTitleBar();
            base.OnCreateControl();
        }

        protected override void OnParentChanged(EventArgs e)
        {
            if (Parent.GetType() != typeof(Form))
                Parent = FindForm();

            AdjustTitleBar();
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
            AdjustTitleBar();
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            AdjustTitleBar();
        }
        #endregion

        private void AdjustTitleBar()
        {
            Dock = DockStyle.Top;
            if (!(Parent.Controls[0] == this))
                Parent.Controls.SetChildIndex(this, 0);

            SuspendLayout();

            var size = Height - _blockSpacingH * 2;
            var offsetBtn = size + _buttonSpacing;
            var offsetAdd = size + _blockSpacingW;
            var offsetLeft = _blockSpacingW;
            var offsetRight = Width - offsetAdd;

            if (_blockSpacingH * 2 + 4 > Height)
                Height = _blockSpacingH * 2 + 4;

            if (_picIcon.Visible)
            {
                _picIcon.Location = new Point(offsetLeft, _blockSpacingH);
                offsetLeft += offsetAdd;
            }

            if (_locButton == ButtonLocations.Left)
            {
                if (_btnClose.Visible)
                {
                    _btnClose.Size = new Size(size, size);
                    _btnClose.Location = new Point(offsetLeft, _blockSpacingH);
                    offsetLeft += offsetBtn;
                }

                if (_btnMaximize.Visible)
                {
                    _btnMaximize.Size = new Size(size, size);
                    _btnMaximize.Location = new Point(offsetLeft, _blockSpacingH);
                    offsetLeft += offsetBtn;
                }

                if (_btnMinimize.Visible)
                {
                    _btnMinimize.Size = new Size(size, size);
                    _btnMinimize.Location = new Point(offsetLeft, _blockSpacingH);
                    offsetLeft += offsetBtn;
                }

                offsetLeft -= _buttonSpacing;
                offsetLeft += _blockSpacingW;
            }
            else if (_locButton == ButtonLocations.Right)
            {
                if (_btnClose.Visible)
                {
                    _btnClose.Size = new Size(size, size);
                    _btnClose.Location = new Point(offsetRight, _blockSpacingH);
                    offsetRight -= offsetBtn;
                }

                if (_btnMaximize.Visible)
                {
                    _btnMaximize.Size = new Size(size, size);
                    _btnMaximize.Location = new Point(offsetRight, _blockSpacingH);
                    offsetRight -= offsetBtn;
                }

                if (_btnMinimize.Visible)
                {
                    _btnMinimize.Size = new Size(size, size);
                    _btnMinimize.Location = new Point(offsetRight, _blockSpacingH);
                    offsetRight -= offsetBtn;
                }

                offsetRight += _buttonSpacing;
                offsetRight -= _blockSpacingW;
            }

            if (_lblTitle.Visible)
            {
                var titleSize = offsetRight - offsetLeft;
                if (titleSize < 0) titleSize = 0;

                _lblTitle.Text = FindForm().Text;
                _lblTitle.Location = new Point(offsetLeft, _blockSpacingH);
                _lblTitle.Size = new Size(titleSize, size);

                if (_locTitle == TitleLocations.Left)
                    _lblTitle.TextAlign = ContentAlignment.MiddleLeft;
                else if (_locTitle == TitleLocations.Middle)
                    _lblTitle.TextAlign = ContentAlignment.MiddleCenter;
                else if (_locTitle == TitleLocations.Right)
                    _lblTitle.TextAlign = ContentAlignment.MiddleRight;
            }

            ResumeLayout();
        }
    }
}