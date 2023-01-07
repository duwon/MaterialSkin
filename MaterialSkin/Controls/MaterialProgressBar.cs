namespace MaterialSkin.Controls
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MaterialProgressBar : ProgressBar, IMaterialControl
    {
        public MaterialProgressBar()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public bool UseAccentColor { get; set; }
        
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, 5, specified);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var doneProgress = (int)(Width * ((double)Value / Maximum));
            brush b = null;
            if ( Enabled )
            {
                if ( UseAccentColor )
                {
                    b = SkinManager.ColorScheme.AccentBrush;
                }
                else
                {
                    b = SkinManager.ColorScheme.PrimaryBrush;
                }
            }
            else
            {
                b = new SolidBrush( DrawHelper.BlendColor( SkinManager.ColorScheme.PrimaryColor, SkinManager.SwitchOffDisabledThumbColor, 197 ) );
            }
            e.Graphics.FillRectangle(b, 0, 0, doneProgress, Height);
            e.Graphics.FillRectangle(SkinManager.BackgroundFocusBrush, doneProgress, 0, Width - doneProgress, Height);
        }
    }
}
