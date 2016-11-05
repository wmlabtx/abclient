namespace ABClient.AppControls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(CollapsibleSplitter))]
    public class CollapsibleSplitter : Splitter
    {
        private readonly Color _hotColor = CalculateColor(SystemColors.Highlight, SystemColors.Window, 70);
        private Border3DStyle _borderStyle = Border3DStyle.Flat;
        private Control _controlToHide;
        private SplitterState _currentState;
        private bool _expandParentForm;
        private bool _hot;
        private Form _parentForm;
        private Rectangle _rr;
        private SplitterVisualStyle _visualStyle;

        public CollapsibleSplitter()
        {
            Click += OnClick;
            Resize += OnResize;
            MouseLeave += OnMouseLeave;
            MouseMove += OnMouseMove;
        }

        [Bindable(true), Category("Collapsing Options"), DefaultValue("False"),
         Description("The initial state of the Splitter. Set to True if the control to hide is not visible by default")]
        public bool IsCollapsed
        {
            get { return _controlToHide == null || !_controlToHide.Visible; }
        }

        [Bindable(true), Category("Collapsing Options"), DefaultValue(""),
         Description("The System.Windows.Forms.Control that the splitter will collapse")]
        public Control ControlToHide
        {
            get { return _controlToHide; }
            set { _controlToHide = value; }
        }

        [Bindable(true), Category("Collapsing Options"), DefaultValue("False"),
         Description("When true the entire parent form will be expanded and collapsed, otherwise just the contol to expand will be changed")]
        public bool ExpandParentForm
        {
            get { return _expandParentForm; }
            set { _expandParentForm = value; }
        }

        [Bindable(true), Category("Collapsing Options"), DefaultValue("VisualStyles.XP"),
         Description("The visual style that will be painted on the control")]
        public SplitterVisualStyle VisualStyle
        {
            get
            {
                return _visualStyle;
            }

            set
            {
                _visualStyle = value;
                Invalidate();
            }
        }

        [Bindable(true), Category("Collapsing Options"), DefaultValue("System.Windows.Forms.Border3DStyle.Flat"),
         Description("An optional border style to paint on the control. Set to Flat for no border")]
        public Border3DStyle BorderStyle3D
        {
            get
            {
                return _borderStyle;
            }

            set
            {
                _borderStyle = value;
                Invalidate();
            }
        }

        public void ToggleState()
        {
            ToggleSplitter();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            _parentForm = FindForm();

            if (_controlToHide != null)
            {
                _currentState = _controlToHide.Visible ? SplitterState.Expanded : SplitterState.Collapsed;
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (_controlToHide == null)
            {
                return;
            }

            if (!_hot && _controlToHide.Visible)
            {
                base.OnMouseDown(e);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            var r = ClientRectangle;
            g.FillRectangle(new SolidBrush(BackColor), r);

            switch (Dock)
            {
                case DockStyle.Right:
                case DockStyle.Left:
                    OnPaintDockRightLeft(g, r, e);
                    break;
                case DockStyle.Bottom:
                case DockStyle.Top:
                    OnPaintDockTopBottom(g, r, e);
                    break;
            }

            g.Dispose();
        }

        private static Color CalculateColor(Color front, Color back, int alpha)
        {
            var frontColor = Color.FromArgb(255, front);
            var backColor = Color.FromArgb(255, back);

            float frontRed = frontColor.R;
            float frontGreen = frontColor.G;
            float frontBlue = frontColor.B;
            float backRed = backColor.R;
            float backGreen = backColor.G;
            float backBlue = backColor.B;

            var flatRed = ((frontRed * alpha) / 255) + (backRed * ((float)(255 - alpha) / 255));
            var newRed = (byte)flatRed;
            var flatGreen = ((frontGreen * alpha) / 255) + (backGreen * ((float)(255 - alpha) / 255));
            var newGreen = (byte)flatGreen;
            var flatBlue = ((frontBlue * alpha) / 255) + (backBlue * ((float)(255 - alpha) / 255));
            var newBlue = (byte)flatBlue;

            return Color.FromArgb(255, newRed, newGreen, newBlue);
        }

        private void OnResize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.X >= _rr.X && e.X <= _rr.X + _rr.Width && e.Y >= _rr.Y && e.Y <= _rr.Y + _rr.Height)
            {
                if (!_hot)
                {
                    _hot = true;
                    Cursor = Cursors.Hand;
                    Invalidate();
                }
            }
            else
            {
                if (_hot)
                {
                    _hot = false;
                    Invalidate();
                }

                Cursor = Cursors.Default;

                if (_controlToHide != null)
                {
                    if (!_controlToHide.Visible)
                    {
                        Cursor = Cursors.Default;
                    }
                    else
                    {
                        if (Dock == DockStyle.Left || Dock == DockStyle.Right)
                        {
                            Cursor = Cursors.VSplit;
                        }
                        else
                        {
                            Cursor = Cursors.HSplit;
                        }
                    }
                }
            }
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            _hot = false;
            Invalidate();
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (_controlToHide != null && _hot &&
                _currentState != SplitterState.Collapsing &&
                _currentState != SplitterState.Expanding)
            {
                ToggleSplitter();
            }
        }

        private void ToggleSplitter()
        {
            if (_currentState == SplitterState.Collapsing || _currentState == SplitterState.Expanding)
            {
                return;
            }

            if (_controlToHide.Visible)
            {
                _currentState = SplitterState.Collapsed;
                _controlToHide.Visible = false;
                if (_expandParentForm && _parentForm != null)
                {
                    if (Dock == DockStyle.Left || Dock == DockStyle.Right)
                    {
                        _parentForm.Width -= _controlToHide.Width;
                    }
                    else
                    {
                        _parentForm.Height -= _controlToHide.Height;
                    }
                }
            }
            else
            {
                _currentState = SplitterState.Expanded;
                _controlToHide.Visible = true;
                if (_expandParentForm && _parentForm != null)
                {
                    if (Dock == DockStyle.Left || Dock == DockStyle.Right)
                    {
                        _parentForm.Width += _controlToHide.Width;
                    }
                    else
                    {
                        _parentForm.Height += _controlToHide.Height;
                    }
                }
            }
        }

        private void OnPaintDockRightLeft(Graphics g, Rectangle r, PaintEventArgs e)
        {
            _rr = new Rectangle(r.X, r.Y + ((r.Height - 115) / 2), 8, 115);
            Width = 8;

            g.FillRectangle(_hot ? new SolidBrush(_hotColor) : new SolidBrush(BackColor),
                            new Rectangle(_rr.X + 1, _rr.Y, 6, 115));

            g.DrawLine(new Pen(SystemColors.ControlDark, 1), _rr.X + 1, _rr.Y, _rr.X + _rr.Width - 2, _rr.Y);
            g.DrawLine(
                new Pen(SystemColors.ControlDark, 1), 
                _rr.X + 1, 
                _rr.Y + _rr.Height, 
                _rr.X + _rr.Width - 2,
                _rr.Y + _rr.Height);

            if (Enabled)
            {
                g.FillPolygon(
                    new SolidBrush(SystemColors.ControlDarkDark),
                    ArrowPointArray(_rr.X + 2, _rr.Y + 3));
                g.FillPolygon(
                    new SolidBrush(SystemColors.ControlDarkDark),
                    ArrowPointArray(_rr.X + 2, _rr.Y + _rr.Height - 9));
            }

            var x = _rr.X + 3;
            var y = _rr.Y + 14;

            switch (_visualStyle)
            {
                case SplitterVisualStyle.NonMicrosoft:

                    for (var i = 0; i < 30; i++)
                    {
                        g.DrawLine(
                            new Pen(SystemColors.ControlLightLight), 
                            x, 
                            y + (i * 3), 
                            x + 1,
                            y + 1 + (i * 3));
                        g.DrawLine(
                            new Pen(SystemColors.ControlDarkDark), 
                            x + 1, 
                            y + 1 + (i * 3), 
                            x + 2,
                            y + 2 + (i * 3));
                        g.DrawLine(_hot ? new Pen(_hotColor) : new Pen(BackColor), x + 2, y + 1 + (i*3), x + 2,
                                   y + 2 + (i*3));
                    }

                    break;

                case SplitterVisualStyle.DoubleDots:
                    for (var i = 0; i < 30; i++)
                    {
                        g.DrawRectangle(new Pen(SystemColors.ControlLightLight), x, y + 1 + (i * 3), 1, 1);
                        g.DrawRectangle(new Pen(SystemColors.ControlDark), x - 1, y + (i * 3), 1, 1);
                        i++;
                        g.DrawRectangle(new Pen(SystemColors.ControlLightLight), x + 2, y + 1 + (i * 3), 1, 1);
                        g.DrawRectangle(new Pen(SystemColors.ControlDark), x + 1, y + (i * 3), 1, 1);
                    }

                    break;

                case SplitterVisualStyle.Win9X:

                    g.DrawLine(new Pen(SystemColors.ControlLightLight), x, y, x + 2, y);
                    g.DrawLine(new Pen(SystemColors.ControlLightLight), x, y, x, y + 90);
                    g.DrawLine(new Pen(SystemColors.ControlDark), x + 2, y, x + 2, y + 90);
                    g.DrawLine(new Pen(SystemColors.ControlDark), x, y + 90, x + 2, y + 90);
                    break;

                case SplitterVisualStyle.XP:

                    for (var i = 0; i < 18; i++)
                    {
                        g.DrawRectangle(new Pen(SystemColors.ControlLight), x, y + (i * 5), 2, 2);
                        g.DrawRectangle(new Pen(SystemColors.ControlLightLight), x + 1, y + 1 + (i * 5), 1, 1);
                        g.DrawRectangle(new Pen(SystemColors.ControlDarkDark), x, y + (i * 5), 1, 1);
                        g.DrawLine(new Pen(SystemColors.ControlDark), x, y + (i * 5), x, y + (i * 5) + 1);
                        g.DrawLine(new Pen(SystemColors.ControlDark), x, y + (i * 5), x + 1, y + (i * 5));
                    }

                    break;

                case SplitterVisualStyle.Lines:

                    for (var i = 0; i < 44; i++)
                    {
                        g.DrawLine(new Pen(SystemColors.ControlDark), x, y + (i * 2), x + 2, y + (i * 2));
                    }

                    break;
            }

            if (_borderStyle == Border3DStyle.Flat)
            {
                return;
            }

            ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, _borderStyle, Border3DSide.Left);
            ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, _borderStyle, Border3DSide.Right);
        }

        private void OnPaintDockTopBottom(Graphics g, Rectangle r, PaintEventArgs e)
        {
            _rr = new Rectangle(r.X + ((r.Width - 115) / 2), r.Y, 115, 8);
            Height = 8;

            g.FillRectangle(_hot ? new SolidBrush(_hotColor) : new SolidBrush(BackColor),
                            new Rectangle(_rr.X, _rr.Y + 1, 115, 6));

            g.DrawLine(new Pen(SystemColors.ControlDark, 1), _rr.X, _rr.Y + 1, _rr.X, _rr.Y + _rr.Height - 2);
            g.DrawLine(
                new Pen(SystemColors.ControlDark, 1), 
                _rr.X + _rr.Width, 
                _rr.Y + 1, 
                _rr.X + _rr.Width,
                _rr.Y + _rr.Height - 2);

            if (Enabled)
            {
                g.FillPolygon(
                    new SolidBrush(SystemColors.ControlDarkDark),
                    ArrowPointArray(_rr.X + 3, _rr.Y + 2));
                g.FillPolygon(
                    new SolidBrush(SystemColors.ControlDarkDark),
                    ArrowPointArray(_rr.X + _rr.Width - 9, _rr.Y + 2));
            }

            var x = _rr.X + 14;
            var y = _rr.Y + 3;

            switch (_visualStyle)
            {
                case SplitterVisualStyle.NonMicrosoft:

                    for (var i = 0; i < 30; i++)
                    {
                        g.DrawLine(
                            new Pen(SystemColors.ControlLightLight), 
                            x + (i * 3), 
                            y, 
                            x + 1 + (i * 3),
                            y + 1);
                        g.DrawLine(
                            new Pen(SystemColors.ControlDarkDark), 
                            x + 1 + (i * 3), 
                            y + 1,
                            x + 2 + (i * 3), 
                            y + 2);
                        g.DrawLine(_hot ? new Pen(_hotColor) : new Pen(BackColor), x + 1 + (i*3), y + 2, x + 2 + (i*3),
                                   y + 2);
                    }

                    break;

                case SplitterVisualStyle.DoubleDots:

                    for (var i = 0; i < 30; i++)
                    {
                        g.DrawRectangle(new Pen(SystemColors.ControlLightLight), x + 1 + (i * 3), y, 1, 1);
                        g.DrawRectangle(new Pen(SystemColors.ControlDark), x + (i * 3), y - 1, 1, 1);
                        i++;
                        g.DrawRectangle(new Pen(SystemColors.ControlLightLight), x + 1 + (i * 3), y + 2, 1, 1);
                        g.DrawRectangle(new Pen(SystemColors.ControlDark), x + (i * 3), y + 1, 1, 1);
                    }

                    break;

                case SplitterVisualStyle.Win9X:

                    g.DrawLine(new Pen(SystemColors.ControlLightLight), x, y, x, y + 2);
                    g.DrawLine(new Pen(SystemColors.ControlLightLight), x, y, x + 88, y);
                    g.DrawLine(new Pen(SystemColors.ControlDark), x, y + 2, x + 88, y + 2);
                    g.DrawLine(new Pen(SystemColors.ControlDark), x + 88, y, x + 88, y + 2);
                    break;

                case SplitterVisualStyle.XP:

                    for (var i = 0; i < 18; i++)
                    {
                        g.DrawRectangle(new Pen(SystemColors.ControlLight), x + (i * 5), y, 2, 2);
                        g.DrawRectangle(new Pen(SystemColors.ControlLightLight), x + 1 + (i * 5), y + 1, 1, 1);
                        g.DrawRectangle(new Pen(SystemColors.ControlDarkDark), x + (i * 5), y, 1, 1);
                        g.DrawLine(new Pen(SystemColors.ControlDark), x + (i * 5), y, x + (i * 5) + 1, y);
                        g.DrawLine(new Pen(SystemColors.ControlDark), x + (i * 5), y, x + (i * 5), y + 1);
                    }

                    break;

                case SplitterVisualStyle.Lines:

                    for (var i = 0; i < 44; i++)
                    {
                        g.DrawLine(new Pen(SystemColors.ControlDark), x + (i * 2), y, x + (i * 2), y + 2);
                    }

                    break;
            }

            if (_borderStyle == Border3DStyle.Flat)
            {
                return;
            }

            ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, _borderStyle, Border3DSide.Top);
            ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, _borderStyle, Border3DSide.Bottom);
        }

        private Point[] ArrowPointArray(int x, int y)
        {
            var point = new Point[3];

            if (_controlToHide != null)
            {
                if ((Dock == DockStyle.Right && _controlToHide.Visible) || (Dock == DockStyle.Left && !_controlToHide.Visible))
                {
                    point[0] = new Point(x, y);
                    point[1] = new Point(x + 3, y + 3);
                    point[2] = new Point(x, y + 6);
                }
                else if ((Dock == DockStyle.Right && !_controlToHide.Visible) || (Dock == DockStyle.Left && _controlToHide.Visible))
                {
                    point[0] = new Point(x + 3, y);
                    point[1] = new Point(x, y + 3);
                    point[2] = new Point(x + 3, y + 6);
                }
                else if ((Dock == DockStyle.Top && _controlToHide.Visible) || (Dock == DockStyle.Bottom && !_controlToHide.Visible))
                {
                    point[0] = new Point(x + 3, y);
                    point[1] = new Point(x + 6, y + 4);
                    point[2] = new Point(x, y + 4);
                }
                else if ((Dock == DockStyle.Top && !_controlToHide.Visible) || (Dock == DockStyle.Bottom && _controlToHide.Visible))
                {
                    point[0] = new Point(x, y);
                    point[1] = new Point(x + 6, y);
                    point[2] = new Point(x + 3, y + 3);
                }
            }

            return point;
        }
    }
}