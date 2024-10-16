using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AffariTuoiWPF
{
    /// <summary>
    /// Standard button with extensions
    /// </summary>
    public partial class ButtonEx : Button
    {
        /// <summary>
        /// Default on-mouse-hover brush
        /// </summary>
        readonly static Brush DefaultHoverBackgroundValue = (new BrushConverter().ConvertFromString("#FFBEE6FD") as Brush)!;
        /// <summary>
        /// Default pressed brush
        /// </summary>
        readonly static Brush DefaultPressedBackgroundValue = (new BrushConverter().ConvertFromString("#FFC4E5F6") as Brush)!;

        /// <summary>
        /// Creates a new instance of this class
        /// </summary>
        public ButtonEx()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The on mouse hover color brush
        /// </summary>
        public Brush HoverBackground
        {
            get { return (Brush)GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }
        /// <summary>
        /// The dependency property associated to <see cref="HoverBackground"/>
        /// </summary>
        public static readonly DependencyProperty HoverBackgroundProperty = DependencyProperty.Register("HoverBackground", typeof(Brush), typeof(ButtonEx), new PropertyMetadata(DefaultHoverBackgroundValue));

        /// <summary>
        /// The on pressed color brush
        /// </summary>
        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }
        /// <summary>
        /// The dependency property associated to <see cref="HoverBackground"/>
        /// </summary>
        public static readonly DependencyProperty PressedBackgroundProperty = DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(ButtonEx), new PropertyMetadata(DefaultPressedBackgroundValue));
    }
}
