using System.Windows;
using System.Windows.Controls;

namespace AffariTuoiWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Utility class, contains information about a box in play
        /// </summary>
        /// <remarks>
        /// Creates a new instance of this class
        /// </remarks>
        /// <param name="value">Value of the box</param>
        public sealed class BoxInfo(int value)
        {
            /// <summary>
            /// Enumerates which color a box can have
            /// </summary>
            public enum BoxColor
            {
                /// <summary>
                /// Red box, prizes &gte; 5000€
                /// </summary>
                RED,
                /// <summary>
                /// Blue box, prizes &lte; 5000€
                /// </summary>
                BLUE
            }

            /// <summary>
            /// Indicates whether or not this box is still in play
            /// </summary>
            public bool IsPresent { get; set; } = true;
            /// <summary>
            /// The value of this box
            /// </summary>
            public int Value { get; private set; } = value;
            /// <summary>
            /// The color of the box. <see cref="BoxColor"/>
            /// </summary>
            public BoxColor Color => Value < 5000 ? BoxColor.BLUE : BoxColor.RED;
        }

        /// <summary>
        /// The boxes
        /// </summary>
        private readonly Dictionary<Button, BoxInfo> boxes;

        /// <summary>
        /// Creates a new instance of this class
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            boxes = new()
            {
                { box0, new BoxInfo(0) },
                { box1, new BoxInfo(1) },
                { box5, new BoxInfo(5) },
                { box10, new BoxInfo(10) },
                { box20, new BoxInfo(20) },
                { box50, new BoxInfo(50) },
                { box75, new BoxInfo(75) },
                { box100, new BoxInfo(100) },
                { box200, new BoxInfo(200) },
                { box500, new BoxInfo(500) },
                { box5000, new BoxInfo(5000) },
                { box10000, new BoxInfo(10000) },
                { box15000, new BoxInfo(15000) },
                { box20000, new BoxInfo(20000) },
                { box30000, new BoxInfo(30000) },
                { box50000, new BoxInfo(50000) },
                { box75000, new BoxInfo(75000) },
                { box100000, new BoxInfo(100000) },
                { box200000, new BoxInfo(200000) },
                { box300000, new BoxInfo(300000) }
            };

            UpdateAll();
        }

        /// <summary>
        /// When the user clicks one of the button linked to a box
        /// </summary>
        /// <param name="sender">Object which raised the event</param>
        /// <param name="e">Event information</param>
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is not Button)
                return;

            Button b = (Button)sender;

            boxes[b].IsPresent = !boxes[b].IsPresent;
            UpdateAll();
        }

        /// <summary>
        /// When the value of the offer changes
        /// </summary>
        /// <param name="sender">Object which raised the event</param>
        /// <param name="e">Event information</param>
        private void OnOffer(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            DoMathStuff();
        }

        /// <summary>
        /// Updates both UI graphics and underlying math values
        /// </summary>
        private void UpdateAll()
        {
            UpdateUI();
            DoMathStuff();
        }

        /// <summary>
        /// Updates UI graphics
        /// </summary>
        private void UpdateUI()
        {
            foreach (Button b in boxes.Keys)
            {
                if (boxes[b].Color == BoxInfo.BoxColor.BLUE)
                {
                    if (boxes[b].IsPresent)
                    {
                        b.Margin = new Thickness(0, 6, 0, 6);
                        b.Content = boxes[b].Value.ToString("0 €");
                    }
                    else
                    {
                        b.Margin = new Thickness(-85, 6, 0, 6);
                        b.Content = "";
                    }
                }
                else
                {
                    if (boxes[b].IsPresent)
                    {
                        b.Margin = new Thickness(0, 6, 0, 6);
                        b.Content = boxes[b].Value.ToString("0 €");
                    }
                    else
                    {
                        b.Margin = new Thickness(0, 6, -85, 6);
                        b.Content = "";
                    }
                }
            }
        }

        /// <summary>
        /// Updates underlying math values
        /// </summary>
        private void DoMathStuff()
        {
            if (boxes == null)
                return; // on init
            IEnumerable<BoxInfo> boxInPlay = boxes.Values.Where(b => b.IsPresent);

            double avg = boxInPlay.Select(b => b.Value).Average();
            double sigma = Math.Sqrt(boxInPlay.Select(b => Math.Pow(b.Value - avg, 2)).Sum() / boxInPlay.Count());
            double ratioToAvg = (double)offerNumericUpDown.Value! / avg;
            double sigmaCoeff = sigma / avg;
            double goodOffer = sigmaCoeff < 1d ? avg : avg / sigmaCoeff;

            string fmt = "0";
            string fmtDec = "0.00";
            string fmtEur = fmt + " €";
            string fmtPerc = fmtDec + " %";

            boxInPlayTextBox.Text = boxInPlay.Count().ToString(fmt);
            avgTextBox.Text = avg.ToString(fmtEur);
            sigmaTextBox.Text = sigma.ToString(fmtEur);
            sigmaCoeffTextBox.Text = sigmaCoeff.ToString(fmtDec);
            goodOfferTextBox.Text = goodOffer.ToString(fmtEur);
            offerGoodnessTextBox.Text = ratioToAvg.ToString(fmtPerc);
        }
    }
}