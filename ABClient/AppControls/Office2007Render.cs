namespace ABClient.AppControls
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public sealed class Office2007Render : ToolStripProfessionalRenderer
    {
        private const int GripOffset = 1;
        private const int GripSquare = 2;
        private const int GripSize = 3;
        private const int GripMove = 4;
        private const int GripLines = 3;
        private const int CheckInset = 1;
        private const int MarginInset = 2;
        private const int SeparatorInset = 31;
        private const float CutToolItemMenu = 1.0f;
        private const float CutContextMenu = 0f;
        private const float CutMenuItemBack = 1.2f;
        private const float ContextCheckTickThickness = 1.6f;

        private static readonly Blend StatusStripBlend = new Blend
                                                             {
                                                                 Positions = new[] { 0.0f, 0.25f, 0.25f, 0.57f, 0.86f, 1.0f },
                                                                 Factors = new[] { 0.1f, 0.6f, 1.0f, 0.4f, 0.0f, 0.95f }
                                                             };

        private static readonly Color C1 = Color.FromArgb(167, 167, 167);
        private static readonly Color C2 = Color.FromArgb(21, 66, 139);
        private static readonly Color C4 = Color.FromArgb(250, 250, 250);
        private static readonly Color C5 = Color.FromArgb(248, 248, 248);
        private static readonly Color C6 = Color.FromArgb(243, 243, 243);
        private static readonly Color R1 = Color.FromArgb(255, 255, 251);
        private static readonly Color R2 = Color.FromArgb(255, 249, 227);
        private static readonly Color R3 = Color.FromArgb(255, 242, 201);
        private static readonly Color R4 = Color.FromArgb(255, 248, 181);
        private static readonly Color R5 = Color.FromArgb(255, 252, 229);
        private static readonly Color R6 = Color.FromArgb(255, 235, 166);
        private static readonly Color R7 = Color.FromArgb(255, 213, 103);
        private static readonly Color R8 = Color.FromArgb(255, 228, 145);
        private static readonly Color R9 = Color.FromArgb(160, 188, 228);
        private static readonly Color Ra = Color.FromArgb(121, 153, 194);
        private static readonly Color Rd = Color.FromArgb(233, 168, 97);
        private static readonly Color Re = Color.FromArgb(247, 164, 39);
        private static readonly Color Rf = Color.FromArgb(246, 156, 24);
        private static readonly Color Rg = Color.FromArgb(253, 173, 17);
        private static readonly Color Rh = Color.FromArgb(254, 185, 108);
        private static readonly Color Ri = Color.FromArgb(253, 164, 97);
        private static readonly Color Rj = Color.FromArgb(252, 143, 61);
        private static readonly Color Rk = Color.FromArgb(255, 208, 134);
        private static readonly Color Rl = Color.FromArgb(249, 192, 103);
        private static readonly Color Rm = Color.FromArgb(250, 195, 93);
        private static readonly Color Rn = Color.FromArgb(248, 190, 81);
        private static readonly Color Ro = Color.FromArgb(255, 208, 49);
        private static readonly Color Rp = Color.FromArgb(254, 214, 168);
        private static readonly Color Rq = Color.FromArgb(252, 180, 100);
        private static readonly Color Rr = Color.FromArgb(252, 161, 54);
        private static readonly Color Rs = Color.FromArgb(254, 238, 170);
        private static readonly Color Rt = Color.FromArgb(249, 202, 113);
        private static readonly Color Ru = Color.FromArgb(250, 205, 103);
        private static readonly Color Rv = Color.FromArgb(248, 200, 91);
        private static readonly Color Rw = Color.FromArgb(255, 218, 59);
        private static readonly Color Rx = Color.FromArgb(254, 185, 108);
        private static readonly Color Ry = Color.FromArgb(252, 161, 54);
        private static readonly Color Rz = Color.FromArgb(254, 238, 170);

        private static readonly Color TextDisabled = C1;
        private static readonly Color TextMenuStripItem = C2;
        private static readonly Color TextStatusStripItem = C2;
        private static readonly Color TextContextMenuItem = C2;
        private static readonly Color ArrowDisabled = C1;
        private static readonly Color ArrowLight = Color.FromArgb(106, 126, 197);
        private static readonly Color ArrowDark = Color.FromArgb(64, 70, 90);
        private static readonly Color SeparatorMenuLight = Color.FromArgb(245, 245, 245);
        private static readonly Color SeparatorMenuDark = Color.FromArgb(197, 197, 197);
        private static readonly Color ContextMenuBack = C4;
        private static readonly Color ContextCheckBorder = Color.FromArgb(242, 149, 54);
        private static readonly Color ContextCheckTick = Color.FromArgb(66, 75, 138);
        private static readonly Color StatusStripBorderDark = Color.FromArgb(86, 125, 176);
        private static readonly Color StatusStripBorderLight = Color.White;
        private static readonly Color GripDark = Color.FromArgb(114, 152, 204);
        private static readonly Color GripLight = C5;
        private static readonly GradientItemColors ItemContextItemEnabledColors = new GradientItemColors(R1, R2, R3, R4, R5, R6, R7, R8, Color.FromArgb(217, 203, 150), Color.FromArgb(192, 167, 118));
        private static readonly GradientItemColors ItemDisabledColors = new GradientItemColors(C4, C6, Color.FromArgb(236, 236, 236), Color.FromArgb(230, 230, 230), C6, Color.FromArgb(224, 224, 224), Color.FromArgb(200, 200, 200), Color.FromArgb(210, 210, 210), Color.FromArgb(212, 212, 212), Color.FromArgb(195, 195, 195));
        private static readonly GradientItemColors ItemToolItemSelectedColors = new GradientItemColors(R1, R2, R3, R4, R5, R6, R7, R8, R9, Ra);
        private static readonly GradientItemColors ItemToolItemPressedColors = new GradientItemColors(Rd, Re, Rf, Rg, Rh, Ri, Rj, Rk, R9, Ra);
        private static readonly GradientItemColors ItemToolItemCheckedColors = new GradientItemColors(Rl, Rm, Rn, Ro, Rp, Rq, Rr, Rs, R9, Ra);
        private static readonly GradientItemColors ItemToolItemCheckPressColors = new GradientItemColors(Rt, Ru, Rv, Rw, Rx, Ri, Ry, Rz, R9, Ra);

        internal Office2007Render()
            : base(new Office2007ColorTable())
        {
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            if ((e.ArrowRectangle.Width <= 0) || (e.ArrowRectangle.Height <= 0))
            {
                return;
            }

            using (var arrowPath = CreateArrowPath(e.Item, e.ArrowRectangle, e.Direction))
            {
                var boundsF = arrowPath.GetBounds();
                boundsF.Inflate(1f, 1f);

                var color1 = (e.Item.Enabled ? ArrowLight : ArrowDisabled);
                var color2 = (e.Item.Enabled ? ArrowDark : ArrowDisabled);

                float angle = 0;

                switch (e.Direction)
                {
                    case ArrowDirection.Right:
                        angle = 0;
                        break;
                    case ArrowDirection.Left:
                        angle = 180f;
                        break;
                    case ArrowDirection.Down:
                        angle = 90f;
                        break;
                    case ArrowDirection.Up:
                        angle = 270f;
                        break;
                }

                using (var arrowBrush = new LinearGradientBrush(boundsF, color1, color2, angle))
                    e.Graphics.FillPath(arrowBrush, arrowPath);
            }
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            // Cast to correct type
            var button = (ToolStripButton)e.Item;

            if (button.Selected || button.Pressed || button.Checked)
            {
                RenderToolButtonBackground(e.Graphics, button, e.ToolStrip);
            }
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected || e.Item.Pressed)
            {
                RenderToolDropButtonBackground(e.Graphics, e.Item, e.ToolStrip);
            }
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            var checkBox = e.ImageRectangle;

            checkBox.Inflate(1, 1);

            if (checkBox.Top > CheckInset)
            {
                var diff = checkBox.Top - CheckInset;
                checkBox.Y -= diff;
                checkBox.Height += diff;
            }

            if (checkBox.Height <= (e.Item.Bounds.Height - (CheckInset * 2)))
            {
                var diff = e.Item.Bounds.Height - (CheckInset * 2) - checkBox.Height;
                checkBox.Height += diff;
            }

            using (new UseAntiAlias(e.Graphics))
            {
                using (var borderPath = CreateBorderPath(checkBox, CutMenuItemBack))
                {
                    using (var fillBrush = new SolidBrush(ColorTable.CheckBackground))
                        e.Graphics.FillPath(fillBrush, borderPath);

                    using (var borderPen = new Pen(ContextCheckBorder))
                        e.Graphics.DrawPath(borderPen, borderPath);

                    if (e.Image == null)
                    {
                        return;
                    }

                    var checkState = CheckState.Unchecked;
                    if (e.Item is ToolStripMenuItem)
                    {
                        var item = (ToolStripMenuItem)e.Item;
                        checkState = item.CheckState;
                    }

                    switch (checkState)
                    {
                        case CheckState.Checked:
                            using (var tickPath = CreateTickPath(checkBox))
                            {
                                using (var tickPen = new Pen(ContextCheckTick, ContextCheckTickThickness))
                                    e.Graphics.DrawPath(tickPen, tickPath);
                            }

                            break;
                        case CheckState.Indeterminate:
                            using (var tickPath = CreateIndeterminatePath(checkBox))
                            {
                                using (var tickBrush = new SolidBrush(ContextCheckTick))
                                    e.Graphics.FillPath(tickBrush, tickPath);
                            }

                            break;
                    }
                }
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (!e.Item.Enabled)
            {
                e.TextColor = TextDisabled;
            }
            else
            {
                if ((e.ToolStrip is MenuStrip) && !e.Item.Pressed && !e.Item.Selected)
                {
                    e.TextColor = TextMenuStripItem;
                }
                else if ((e.ToolStrip is StatusStrip) && !e.Item.Pressed && !e.Item.Selected)
                {
                    e.TextColor = TextStatusStripItem;
                }
                else
                {
                    e.TextColor = TextContextMenuItem;
                }
            }

            using (new UseClearTypeGridFit(e.Graphics)) base.OnRenderItemText(e);
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            if ((e.ToolStrip is ContextMenuStrip) ||
                (e.ToolStrip is ToolStripDropDownMenu))
            {
                if (e.Image != null)
                {
                    if (e.Item.Enabled)
                    {
                        e.Graphics.DrawImage(e.Image, e.ImageRectangle);
                    }
                    else
                    {
                        ControlPaint.DrawImageDisabled(
                            e.Graphics,
                            e.Image,
                            e.ImageRectangle.X,
                            e.ImageRectangle.Y,
                            Color.Transparent);
                    }
                }
            }
            else
            {
                base.OnRenderItemImage(e);
            }
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if ((e.ToolStrip is MenuStrip) ||
                (e.ToolStrip is ContextMenuStrip) ||
                (e.ToolStrip is ToolStripDropDownMenu))
            {
                if (e.Item.Pressed && e.ToolStrip is MenuStrip)
                {
                    DrawContextMenuHeader(e.Graphics, e.Item);
                }
                else
                {
                    if (e.Item.Selected)
                    {
                        if (e.Item.Enabled)
                        {
                            if (e.ToolStrip is MenuStrip)
                            {
                                DrawGradientToolItem(e.Graphics, e.Item, ItemToolItemSelectedColors);
                            }
                            else
                            {
                                DrawGradientContextMenuItem(e.Graphics, e.Item, ItemContextItemEnabledColors);
                            }
                        }
                        else
                        {
                            var mousePos = e.ToolStrip.PointToClient(Control.MousePosition);

                            if (!e.Item.Bounds.Contains(mousePos))
                            {
                                if (e.ToolStrip is MenuStrip)
                                {
                                    DrawGradientToolItem(e.Graphics, e.Item, ItemDisabledColors);
                                }
                                else
                                {
                                    DrawGradientContextMenuItem(e.Graphics, e.Item, ItemDisabledColors);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }

        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected || e.Item.Pressed)
            {
                var splitButton = (ToolStripSplitButton)e.Item;

                RenderToolSplitButtonBackground(e.Graphics, splitButton, e.ToolStrip);
                var arrowBounds = splitButton.DropDownButtonBounds;
                OnRenderArrow(new ToolStripArrowRenderEventArgs(
                                  e.Graphics,
                                  splitButton,
                                  arrowBounds,
                                  SystemColors.ControlText,
                                  ArrowDirection.Down));
            }
            else
            {
                base.OnRenderSplitButtonBackground(e);
            }
        }

        protected override void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs e)
        {
            using (SolidBrush darkBrush = new SolidBrush(GripDark), lightBrush = new SolidBrush(GripLight))
            {
                var rtl = (e.ToolStrip.RightToLeft == RightToLeft.Yes);
                var y = e.AffectedBounds.Bottom - (GripSize * 2) + 1;
                for (var i = GripLines; i >= 1; i--)
                {
                    var x = (rtl ? e.AffectedBounds.Left + 1 : e.AffectedBounds.Right - (GripSize * 2) + 1);
                    for (var j = 0; j < i; j++)
                    {
                        DrawGripGlyph(e.Graphics, x, y, darkBrush, lightBrush);
                        x -= (rtl ? -GripMove : GripMove);
                    }

                    y -= GripMove;
                }
            }
        }

        protected override void OnRenderToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs e)
        {
            base.OnRenderToolStripContentPanelBackground(e);
            if ((e.ToolStripContentPanel.Width <= 0) || (e.ToolStripContentPanel.Height <= 0))
            {
                return;
            }

            using (var backBrush = new LinearGradientBrush(
                e.ToolStripContentPanel.ClientRectangle,
                ColorTable.ToolStripContentPanelGradientEnd,
                ColorTable.ToolStripContentPanelGradientBegin,
                90f))
            {
                e.Graphics.FillRectangle(backBrush, e.ToolStripContentPanel.ClientRectangle);
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            if ((e.ToolStrip is ContextMenuStrip) ||
                (e.ToolStrip is ToolStripDropDownMenu))
            {
                using (Pen lightPen = new Pen(SeparatorMenuLight),
                           darkPen = new Pen(SeparatorMenuDark))
                {
                    DrawSeparator(
                        e.Graphics, 
                        e.Vertical, 
                        e.Item.Bounds,
                        lightPen, 
                        darkPen, 
                        SeparatorInset,
                        (e.ToolStrip.RightToLeft == RightToLeft.Yes));
                }
            }
            else if (e.ToolStrip is StatusStrip)
            {
                using (Pen lightPen = new Pen(ColorTable.SeparatorLight),
                           darkPen = new Pen(ColorTable.SeparatorDark))
                {
                    DrawSeparator(
                        e.Graphics, 
                        e.Vertical, 
                        e.Item.Bounds,
                        lightPen, 
                        darkPen, 
                        0, 
                        false);
                }
            }
            else
            {
                base.OnRenderSeparator(e);
            }
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if ((e.ToolStrip is ContextMenuStrip) ||
                (e.ToolStrip is ToolStripDropDownMenu))
            {
                using (GraphicsPath borderPath = CreateBorderPath(e.AffectedBounds, CutContextMenu),
                                    clipPath = CreateClipBorderPath(e.AffectedBounds, CutContextMenu))
                {
                    using (new UseClipping(e.Graphics, clipPath))
                    {
                        using (var backBrush = new SolidBrush(ContextMenuBack))
                            e.Graphics.FillPath(backBrush, borderPath);
                    }
                }
            }
            else if (e.ToolStrip is StatusStrip)
            {
                var backRect = new RectangleF(0, 1.5f, e.ToolStrip.Width, e.ToolStrip.Height - 2);
                if ((backRect.Width > 0) && (backRect.Height > 0))
                {
                    using (var backBrush = new LinearGradientBrush(
                        backRect,
                        ColorTable.StatusStripGradientBegin,
                        ColorTable.StatusStripGradientEnd,
                        90f))
                    {
                        backBrush.Blend = StatusStripBlend;
                        e.Graphics.FillRectangle(backBrush, backRect);
                    }
                }
            }
            else
            {
                base.OnRenderToolStripBackground(e);
            }
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            if ((e.ToolStrip is ContextMenuStrip) ||
                (e.ToolStrip is ToolStripDropDownMenu))
            {
                var marginRect = e.AffectedBounds;
                var rtl = (e.ToolStrip.RightToLeft == RightToLeft.Yes);
                marginRect.Y += MarginInset;
                marginRect.Height -= MarginInset * 2;

                if (!rtl)
                {
                    marginRect.X += MarginInset;
                }
                else
                {
                    marginRect.X += MarginInset / 2;
                }

                using (var backBrush = new SolidBrush(ColorTable.ImageMarginGradientBegin))
                    e.Graphics.FillRectangle(backBrush, marginRect);

                using (Pen lightPen = new Pen(SeparatorMenuLight),
                           darkPen = new Pen(SeparatorMenuDark))
                {
                    if (!rtl)
                    {
                        e.Graphics.DrawLine(lightPen, marginRect.Right, marginRect.Top, marginRect.Right, marginRect.Bottom);
                        e.Graphics.DrawLine(darkPen, marginRect.Right - 1, marginRect.Top, marginRect.Right - 1, marginRect.Bottom);
                    }
                    else
                    {
                        e.Graphics.DrawLine(lightPen, marginRect.Left - 1, marginRect.Top, marginRect.Left - 1, marginRect.Bottom);
                        e.Graphics.DrawLine(darkPen, marginRect.Left, marginRect.Top, marginRect.Left, marginRect.Bottom);
                    }
                }
            }
            else
            {
                base.OnRenderImageMargin(e);
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if ((e.ToolStrip is ContextMenuStrip) ||
                (e.ToolStrip is ToolStripDropDownMenu))
            {
                if (!e.ConnectedArea.IsEmpty)
                {
                    using (var excludeBrush = new SolidBrush(ContextMenuBack))
                        e.Graphics.FillRectangle(excludeBrush, e.ConnectedArea);
                }

                using (GraphicsPath borderPath = CreateBorderPath(e.AffectedBounds, e.ConnectedArea, CutContextMenu),
                                    insidePath = CreateInsideBorderPath(e.AffectedBounds, e.ConnectedArea, CutContextMenu),
                                    clipPath = CreateClipBorderPath(e.AffectedBounds, e.ConnectedArea, CutContextMenu))
                {
                    using (Pen borderPen = new Pen(ColorTable.MenuBorder),
                               insidePen = new Pen(SeparatorMenuLight))
                    {
                        using (new UseClipping(e.Graphics, clipPath))
                        {
                            using (new UseAntiAlias(e.Graphics))
                            {
                                e.Graphics.DrawPath(insidePen, insidePath);
                                e.Graphics.DrawPath(borderPen, borderPath);
                            }

                            e.Graphics.DrawLine(
                                borderPen, 
                                e.AffectedBounds.Right, 
                                e.AffectedBounds.Bottom,
                                e.AffectedBounds.Right - 1, 
                                e.AffectedBounds.Bottom - 1);
                        }
                    }
                }
            }
            else if (e.ToolStrip is StatusStrip)
            {
                using (Pen darkBorder = new Pen(StatusStripBorderDark),
                           lightBorder = new Pen(StatusStripBorderLight))
                {
                    e.Graphics.DrawLine(darkBorder, 0, 0, e.ToolStrip.Width, 0);
                    e.Graphics.DrawLine(lightBorder, 0, 1, e.ToolStrip.Width, 1);
                }
            }
            else
            {
                base.OnRenderToolStripBorder(e);
            }
        }

        private static void DrawGradientToolItem(
            Graphics g,
            ToolStripItem item,
            GradientItemColors colors)
        {
            DrawGradientItem(g, new Rectangle(Point.Empty, item.Bounds.Size), colors);
        }

        private static void DrawGradientToolSplitItem(
            Graphics g,
            ToolStripSplitButton splitButton,
            GradientItemColors colorsButton,
            GradientItemColors colorsDrop,
            GradientItemColors colorsSplit)
        {
            var backRect = new Rectangle(Point.Empty, splitButton.Bounds.Size);
            var backRectDrop = splitButton.DropDownButtonBounds;

            if ((backRect.Width <= 0) || (backRectDrop.Width <= 0) || (backRect.Height <= 0) || (backRectDrop.Height <= 0))
            {
                return;
            }

            var backRectButton = backRect;

            int splitOffset;
            if (backRectDrop.X > 0)
            {
                backRectButton.Width = backRectDrop.Left;
                backRectDrop.X -= 1;
                backRectDrop.Width++;
                splitOffset = backRectDrop.X;
            }
            else
            {
                backRectButton.Width -= backRectDrop.Width - 2;
                backRectButton.X = backRectDrop.Right - 1;
                backRectDrop.Width++;
                splitOffset = backRectDrop.Right - 1;
            }

            using (CreateBorderPath(backRect, CutMenuItemBack))
            {
                DrawGradientBack(g, backRectButton, colorsButton);
                DrawGradientBack(g, backRectDrop, colorsDrop);

                using (var splitBrush = new LinearGradientBrush(
                    new Rectangle(backRect.X + splitOffset, backRect.Top, 1, backRect.Height + 1),
                    colorsSplit.Border1, 
                    colorsSplit.Border2, 
                    90f))
                {
                    splitBrush.SetSigmaBellShape(0.5f);
                    using (var splitPen = new Pen(splitBrush))
                        g.DrawLine(splitPen, backRect.X + splitOffset, backRect.Top + 1, backRect.X + splitOffset, backRect.Bottom - 1);
                }

                DrawGradientBorder(g, backRect, colorsButton);
            }
        }

        private static void RenderToolButtonBackground(
            Graphics g,
            ToolStripButton button,
            Control toolstrip)
        {
            if (button.Enabled)
            {
                if (button.Checked)
                {
                    if (button.Pressed)
                    {
                        DrawGradientToolItem(g, button, ItemToolItemPressedColors);
                    }
                    else if (button.Selected)
                    {
                        DrawGradientToolItem(g, button, ItemToolItemCheckPressColors);
                    }
                    else
                    {
                        DrawGradientToolItem(g, button, ItemToolItemCheckedColors);
                    }
                }
                else
                {
                    if (button.Pressed)
                    {
                        DrawGradientToolItem(g, button, ItemToolItemPressedColors);
                    }
                    else if (button.Selected)
                    {
                        DrawGradientToolItem(g, button, ItemToolItemSelectedColors);
                    }
                }
            }
            else
            {
                if (button.Selected)
                {
                    var mousePos = toolstrip.PointToClient(Control.MousePosition);

                    if (!button.Bounds.Contains(mousePos))
                    {
                        DrawGradientToolItem(g, button, ItemDisabledColors);
                    }
                }
            }
        }

        private static void DrawGradientContextMenuItem(
            Graphics g,
            ToolStripItem item,
            GradientItemColors colors)
        {
            var backRect = new Rectangle(2, 0, item.Bounds.Width - 3, item.Bounds.Height);
            DrawGradientItem(g, backRect, colors);
        }

        private static void DrawGradientItem(
            Graphics g,
            Rectangle backRect,
            GradientItemColors colors)
        {
            if ((backRect.Width <= 0) || (backRect.Height <= 0))
            {
                return;
            }

            DrawGradientBack(g, backRect, colors);
            DrawGradientBorder(g, backRect, colors);
        }

        private static void DrawGradientBack(
            Graphics g,
            Rectangle backRect,
            GradientItemColors colors)
        {
            backRect.Inflate(-1, -1);

            var y2 = backRect.Height / 2;
            var backRect1 = new Rectangle(backRect.X, backRect.Y, backRect.Width, y2);
            var backRect2 = new Rectangle(backRect.X, backRect.Y + y2, backRect.Width, backRect.Height - y2);
            var backRect1I = backRect1;
            var backRect2I = backRect2;
            backRect1I.Inflate(1, 1);
            backRect2I.Inflate(1, 1);

            using (LinearGradientBrush insideBrush1 = new LinearGradientBrush(backRect1I, colors.InsideTop1, colors.InsideTop2, 90f),
                                       insideBrush2 = new LinearGradientBrush(backRect2I, colors.InsideBottom1, colors.InsideBottom2, 90f))
            {
                g.FillRectangle(insideBrush1, backRect1);
                g.FillRectangle(insideBrush2, backRect2);
            }

            y2 = backRect.Height / 2;
            backRect1 = new Rectangle(backRect.X, backRect.Y, backRect.Width, y2);
            backRect2 = new Rectangle(backRect.X, backRect.Y + y2, backRect.Width, backRect.Height - y2);
            backRect1I = backRect1;
            backRect2I = backRect2;
            backRect1I.Inflate(1, 1);
            backRect2I.Inflate(1, 1);

            using (LinearGradientBrush fillBrush1 = new LinearGradientBrush(backRect1I, colors.FillTop1, colors.FillTop2, 90f),
                                       fillBrush2 = new LinearGradientBrush(backRect2I, colors.FillBottom1, colors.FillBottom2, 90f))
            {
                backRect.Inflate(-1, -1);

                y2 = backRect.Height / 2;
                backRect1 = new Rectangle(backRect.X, backRect.Y, backRect.Width, y2);
                backRect2 = new Rectangle(backRect.X, backRect.Y + y2, backRect.Width, backRect.Height - y2);

                g.FillRectangle(fillBrush1, backRect1);
                g.FillRectangle(fillBrush2, backRect2);
            }
        }

        private static void DrawGradientBorder(
            Graphics g,
            Rectangle backRect,
            GradientItemColors colors)
        {
            using (new UseAntiAlias(g))
            {
                var backRectI = backRect;
                backRectI.Inflate(1, 1);

                using (var borderBrush = new LinearGradientBrush(backRectI, colors.Border1, colors.Border2, 90f))
                {
                    borderBrush.SetSigmaBellShape(0.5f);

                    using (var borderPen = new Pen(borderBrush))
                    {
                        using (var borderPath = CreateBorderPath(backRect, CutMenuItemBack))
                            g.DrawPath(borderPen, borderPath);
                    }
                }
            }
        }

        private static void DrawGripGlyph(
            Graphics g,
            int x,
            int y,
            Brush darkBrush,
            Brush lightBrush)
        {
            g.FillRectangle(lightBrush, x + GripOffset, y + GripOffset, GripSquare, GripSquare);
            g.FillRectangle(darkBrush, x, y, GripSquare, GripSquare);
        }

        private static void DrawSeparator(
            Graphics g,
            bool vertical,
            Rectangle rect,
            Pen lightPen,
            Pen darkPen,
            int horizontalInset,
            bool rtl)
        {
            if (vertical)
            {
                var l = rect.Width / 2;
                var t = rect.Y;
                var b = rect.Bottom;

                g.DrawLine(darkPen, l, t, l, b);
                g.DrawLine(lightPen, l + 1, t, l + 1, b);
            }
            else
            {
                var y = rect.Height / 2;
                var l = rect.X + (rtl ? 0 : horizontalInset);
                var r = rect.Right - (rtl ? horizontalInset : 0);

                g.DrawLine(darkPen, l, y, r, y);
                g.DrawLine(lightPen, l, y + 1, r, y + 1);
            }
        }

        private static GraphicsPath CreateBorderPath(
            Rectangle rect,
            Rectangle exclude,
            float cut)
        {
            if (exclude.IsEmpty)
            {
                return CreateBorderPath(rect, cut);
            }

            rect.Width--;
            rect.Height--;

            var pts = new List<PointF>();

            float l = rect.X;
            float t = rect.Y;
            float r = rect.Right;
            float b = rect.Bottom;
            var x0 = rect.X + cut;
            var x3 = rect.Right - cut;
            var y0 = rect.Y + cut;
            var y3 = rect.Bottom - cut;
            var cutBack = (cut == 0f ? 1 : cut);

            if ((rect.Y >= exclude.Top) && (rect.Y <= exclude.Bottom))
            {
                var x1 = exclude.X - 1 - cut;
                var x2 = exclude.Right + cut;

                if (x0 <= x1)
                {
                    pts.Add(new PointF(x0, t));
                    pts.Add(new PointF(x1, t));
                    pts.Add(new PointF(x1 + cut, t - cutBack));
                }
                else
                {
                    x1 = exclude.X - 1;
                    pts.Add(new PointF(x1, t));
                    pts.Add(new PointF(x1, t - cutBack));
                }

                if (x3 > x2)
                {
                    pts.Add(new PointF(x2 - cut, t - cutBack));
                    pts.Add(new PointF(x2, t));
                    pts.Add(new PointF(x3, t));
                }
                else
                {
                    x2 = exclude.Right;
                    pts.Add(new PointF(x2, t - cutBack));
                    pts.Add(new PointF(x2, t));
                }
            }
            else
            {
                pts.Add(new PointF(x0, t));
                pts.Add(new PointF(x3, t));
            }

            pts.Add(new PointF(r, y0));
            pts.Add(new PointF(r, y3));
            pts.Add(new PointF(x3, b));
            pts.Add(new PointF(x0, b));
            pts.Add(new PointF(l, y3));
            pts.Add(new PointF(l, y0));

            var path = new GraphicsPath();

            for (var i = 1; i < pts.Count; i++)
            {
                path.AddLine(pts[i - 1], pts[i]);
            }

            path.AddLine(pts[pts.Count - 1], pts[0]);

            return path;
        }

        private static GraphicsPath CreateBorderPath(Rectangle rect, float cut)
        {
            rect.Width--;
            rect.Height--;

            var path = new GraphicsPath();
            path.AddLine(rect.Left + cut, rect.Top, rect.Right - cut, rect.Top);
            path.AddLine(rect.Right - cut, rect.Top, rect.Right, rect.Top + cut);
            path.AddLine(rect.Right, rect.Top + cut, rect.Right, rect.Bottom - cut);
            path.AddLine(rect.Right, rect.Bottom - cut, rect.Right - cut, rect.Bottom);
            path.AddLine(rect.Right - cut, rect.Bottom, rect.Left + cut, rect.Bottom);
            path.AddLine(rect.Left + cut, rect.Bottom, rect.Left, rect.Bottom - cut);
            path.AddLine(rect.Left, rect.Bottom - cut, rect.Left, rect.Top + cut);
            path.AddLine(rect.Left, rect.Top + cut, rect.Left + cut, rect.Top);
            return path;
        }

        private static GraphicsPath CreateInsideBorderPath(Rectangle rect, float cut)
        {
            rect.Inflate(-1, -1);
            return CreateBorderPath(rect, cut);
        }

        private static GraphicsPath CreateInsideBorderPath(
            Rectangle rect,
            Rectangle exclude,
            float cut)
        {
            rect.Inflate(-1, -1);
            return CreateBorderPath(rect, exclude, cut);
        }

        private static GraphicsPath CreateClipBorderPath(Rectangle rect, float cut)
        {
            rect.Width++;
            rect.Height++;
            return CreateBorderPath(rect, cut);
        }

        private static GraphicsPath CreateClipBorderPath(
            Rectangle rect,
            Rectangle exclude,
            float cut)
        {
            rect.Width++;
            rect.Height++;
            return CreateBorderPath(rect, exclude, cut);
        }

        private static GraphicsPath CreateArrowPath(
            ToolStripItem item,
            Rectangle rect,
            ArrowDirection direction)
        {
            int x, y;

            if ((direction == ArrowDirection.Left) ||
                (direction == ArrowDirection.Right))
            {
                x = rect.Right - ((rect.Width - 4) / 2);
                y = rect.Y + (rect.Height / 2);
            }
            else
            {
                x = rect.X + (rect.Width / 2);
                y = rect.Bottom - ((rect.Height - 3) / 2);

                if ((item is ToolStripDropDownButton) &&
                    (item.RightToLeft == RightToLeft.Yes))
                {
                    x++;
                }
            }

            var path = new GraphicsPath();

            switch (direction)
            {
                case ArrowDirection.Right:
                    path.AddLine(x, y, x - 4, y - 4);
                    path.AddLine(x - 4, y - 4, x - 4, y + 4);
                    path.AddLine(x - 4, y + 4, x, y);
                    break;
                case ArrowDirection.Left:
                    path.AddLine(x - 4, y, x, y - 4);
                    path.AddLine(x, y - 4, x, y + 4);
                    path.AddLine(x, y + 4, x - 4, y);
                    break;
                case ArrowDirection.Down:
                    path.AddLine(x + 3f, y - 3f, x - 2f, y - 3f);
                    path.AddLine(x - 2f, y - 3f, x, y);
                    path.AddLine(x, y, x + 3f, y - 3f);
                    break;
                case ArrowDirection.Up:
                    path.AddLine(x + 3f, y, x - 3f, y);
                    path.AddLine(x - 3f, y, x, y - 4f);
                    path.AddLine(x, y - 4f, x + 3f, y);
                    break;
            }

            return path;
        }

        private static GraphicsPath CreateTickPath(Rectangle rect)
        {
            var x = rect.X + (rect.Width / 2);
            var y = rect.Y + (rect.Height / 2);

            var path = new GraphicsPath();
            path.AddLine(x - 4, y, x - 2, y + 4);
            path.AddLine(x - 2, y + 4, x + 3, y - 5);
            return path;
        }

        private static GraphicsPath CreateIndeterminatePath(Rectangle rect)
        {
            var x = rect.X + (rect.Width / 2);
            var y = rect.Y + (rect.Height / 2);

            var path = new GraphicsPath();
            path.AddLine(x - 3, y, x, y - 3);
            path.AddLine(x, y - 3, x + 3, y);
            path.AddLine(x + 3, y, x, y + 3);
            path.AddLine(x, y + 3, x - 3, y);
            return path;
        }

        private void RenderToolDropButtonBackground(
            Graphics g,
            ToolStripItem item, 
            Control toolstrip)
        {
            if (!item.Selected && !item.Pressed)
            {
                return;
            }

            if (item.Enabled)
            {
                if (item.Pressed)
                {
                    DrawContextMenuHeader(g, item);
                }
                else
                {
                    DrawGradientToolItem(g, item, ItemToolItemSelectedColors);
                }
            }
            else
            {
                var mousePos = toolstrip.PointToClient(Control.MousePosition);
                if (!item.Bounds.Contains(mousePos))
                {
                    DrawGradientToolItem(g, item, ItemDisabledColors);
                }
            }
        }

        private void RenderToolSplitButtonBackground(
            Graphics g,
            ToolStripSplitButton splitButton,
            Control toolstrip)
        {
            if (!splitButton.Selected && !splitButton.Pressed)
            {
                return;
            }

            if (splitButton.Enabled)
            {
                if (!splitButton.Pressed && splitButton.ButtonPressed)
                {
                    DrawGradientToolSplitItem(
                        g, 
                        splitButton,
                        ItemToolItemPressedColors,
                        ItemToolItemSelectedColors,
                        ItemContextItemEnabledColors);
                }
                else if (splitButton.Pressed && !splitButton.ButtonPressed)
                {
                    DrawContextMenuHeader(g, splitButton);
                }
                else
                {
                    DrawGradientToolSplitItem(
                        g, 
                        splitButton,
                        ItemToolItemSelectedColors,
                        ItemToolItemSelectedColors,
                        ItemContextItemEnabledColors);
                }
            }
            else
            {
                var mousePos = toolstrip.PointToClient(Control.MousePosition);
                if (!splitButton.Bounds.Contains(mousePos))
                {
                    DrawGradientToolItem(g, splitButton, ItemDisabledColors);
                }
            }
        }

        private void DrawContextMenuHeader(Graphics g, ToolStripItem item)
        {
            var itemRect = new Rectangle(Point.Empty, item.Bounds.Size);
            using (var borderPath = CreateBorderPath(itemRect, CutToolItemMenu))
            using (CreateInsideBorderPath(itemRect, CutToolItemMenu))
            using (var clipPath = CreateClipBorderPath(itemRect, CutToolItemMenu))
            {
                using (new UseClipping(g, clipPath))
                {
                    using (var backBrush = new SolidBrush(SeparatorMenuLight))
                        g.FillPath(backBrush, borderPath);

                    using (var borderPen = new Pen(ColorTable.MenuBorder))
                        g.DrawPath(borderPen, borderPath);
                }
            }
        }
        
        private sealed class GradientItemColors
        {
            internal readonly Color InsideTop1;
            internal readonly Color InsideTop2;
            internal readonly Color InsideBottom1;
            internal readonly Color InsideBottom2;
            internal readonly Color FillTop1;
            internal readonly Color FillTop2;
            internal readonly Color FillBottom1;
            internal readonly Color FillBottom2;
            internal readonly Color Border1;
            internal readonly Color Border2;

            internal GradientItemColors(
                Color insideTop1, 
                Color insideTop2,
                Color insideBottom1, 
                Color insideBottom2,
                Color fillTop1, 
                Color fillTop2,
                Color fillBottom1, 
                Color fillBottom2,
                Color border1, 
                Color border2)
            {
                InsideTop1 = insideTop1;
                InsideTop2 = insideTop2;
                InsideBottom1 = insideBottom1;
                InsideBottom2 = insideBottom2;
                FillTop1 = fillTop1;
                FillTop2 = fillTop2;
                FillBottom1 = fillBottom1;
                FillBottom2 = fillBottom2;
                Border1 = border1;
                Border2 = border2;
            }
        }
    }
}