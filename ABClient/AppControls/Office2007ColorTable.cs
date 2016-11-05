namespace ABClient.AppControls
{
    using System.Drawing;
    using System.Windows.Forms;

    public class Office2007ColorTable : ProfessionalColorTable
    {
        private static readonly Color contextMenuBack = Color.FromArgb(250, 250, 250);
        private static readonly Color buttonPressedBegin = Color.FromArgb(248, 181, 106);
        private static readonly Color buttonPressedEnd = Color.FromArgb(255, 208, 134);
        private static readonly Color buttonPressedMiddle = Color.FromArgb(251, 140, 60);
        private static readonly Color buttonSelectedBegin = Color.FromArgb(255, 255, 222);
        private static readonly Color buttonSelectedEnd = Color.FromArgb(255, 203, 136);
        private static readonly Color buttonSelectedMiddle = Color.FromArgb(255, 225, 172);
        private static readonly Color menuItemSelectedBegin = Color.FromArgb(255, 213, 103);
        private static readonly Color menuItemSelectedEnd = Color.FromArgb(255, 228, 145);
        private static readonly Color checkBack = Color.FromArgb(255, 227, 149);
        private static readonly Color gripDark = Color.FromArgb(111, 157, 217);
        private static readonly Color gripLight = Color.FromArgb(255, 255, 255);
        private static readonly Color imageMargin = Color.FromArgb(233, 238, 238);
        private static readonly Color menuBorder = Color.FromArgb(134, 134, 134);
        private static readonly Color overflowBegin = Color.FromArgb(167, 204, 251);
        private static readonly Color overflowEnd = Color.FromArgb(101, 147, 207);
        private static readonly Color overflowMiddle = Color.FromArgb(167, 204, 251);
        private static readonly Color menuToolBack = Color.FromArgb(191, 219, 255);
        private static readonly Color separatorDark = Color.FromArgb(154, 198, 255);
        private static readonly Color separatorLight = Color.FromArgb(255, 255, 255);
        private static readonly Color statusStripLight = Color.FromArgb(215, 229, 247);
        private static readonly Color statusStripDark = Color.FromArgb(172, 201, 238);
        private static readonly Color toolStripBorder = Color.FromArgb(111, 157, 217);
        private static readonly Color toolStripContentEnd = Color.FromArgb(164, 195, 235);
        private static readonly Color toolStripBegin = Color.FromArgb(227, 239, 255);
        private static readonly Color toolStripEnd = Color.FromArgb(152, 186, 230);
        private static readonly Color toolStripMiddle = Color.FromArgb(222, 236, 255);
        private static readonly Color buttonBorder = Color.FromArgb(121, 153, 194);

        public override Color ButtonPressedGradientBegin
        {
            get { return buttonPressedBegin; }
        }

        public override Color ButtonPressedGradientEnd
        {
            get { return buttonPressedEnd; }
        }

        public override Color ButtonPressedGradientMiddle
        {
            get { return buttonPressedMiddle; }
        }

        public override Color ButtonSelectedGradientBegin
        {
            get { return buttonSelectedBegin; }
        }

        public override Color ButtonSelectedGradientEnd
        {
            get { return buttonSelectedEnd; }
        }

        public override Color ButtonSelectedGradientMiddle
        {
            get { return buttonSelectedMiddle; }
        }

        public override Color ButtonSelectedHighlightBorder
        {
            get { return buttonBorder; }
        }

        public override Color CheckBackground
        {
            get { return checkBack; }
        }

        public override Color GripDark
        {
            get { return gripDark; }
        }

        public override Color GripLight
        {
            get { return gripLight; }
        }

        public override Color ImageMarginGradientBegin
        {
            get { return imageMargin; }
        }

        public override Color MenuBorder
        {
            get { return menuBorder; }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get { return toolStripBegin; }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get { return toolStripEnd; }
        }

        public override Color MenuItemPressedGradientMiddle
        {
            get { return toolStripMiddle; }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get { return menuItemSelectedBegin; }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get { return menuItemSelectedEnd; }
        }

        public override Color MenuStripGradientBegin
        {
            get { return menuToolBack; }
        }

        public override Color MenuStripGradientEnd
        {
            get { return menuToolBack; }
        }

        public override Color OverflowButtonGradientBegin
        {
            get { return overflowBegin; }
        }

        public override Color OverflowButtonGradientEnd
        {
            get { return overflowEnd; }
        }

        public override Color OverflowButtonGradientMiddle
        {
            get { return overflowMiddle; }
        }

        public override Color RaftingContainerGradientBegin
        {
            get { return menuToolBack; }
        }

        public override Color RaftingContainerGradientEnd
        {
            get { return menuToolBack; }
        }

        public override Color SeparatorDark
        {
            get { return separatorDark; }
        }

        public override Color SeparatorLight
        {
            get { return separatorLight; }
        }

        public override Color StatusStripGradientBegin
        {
            get { return statusStripLight; }
        }

        public override Color StatusStripGradientEnd
        {
            get { return statusStripDark; }
        }

        public override Color ToolStripBorder
        {
            get { return toolStripBorder; }
        }

        public override Color ToolStripContentPanelGradientBegin
        {
            get { return toolStripContentEnd; }
        }

        public override Color ToolStripContentPanelGradientEnd
        {
            get { return menuToolBack; }
        }

        public override Color ToolStripDropDownBackground
        {
            get { return contextMenuBack; }
        }

        public override Color ToolStripGradientBegin
        {
            get { return toolStripBegin; }
        }

        public override Color ToolStripGradientEnd
        {
            get { return toolStripEnd; }
        }

        public override Color ToolStripGradientMiddle
        {
            get { return toolStripMiddle; }
        }

        public override Color ToolStripPanelGradientBegin
        {
            get { return menuToolBack; }
        }

        public override Color ToolStripPanelGradientEnd
        {
            get { return menuToolBack; }
        }
    }
}