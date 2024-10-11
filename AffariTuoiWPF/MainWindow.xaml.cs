using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AffariTuoiWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class BoxInfo
        {
            public enum BoxColor
            {
                RED, BLUE
            }
            public bool IsPresent { get; set; }
            public int Value { get; private set; }
            public BoxColor Color => Value < 5000 ? BoxColor.BLUE : BoxColor.RED;

            public BoxInfo(int value)
            {
                IsPresent = true;
                Value = value;
            }
        }

        private Dictionary<Button, BoxInfo> boxes;

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
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is not Button)
                return;

            Button b = (Button)sender;

            boxes[b].IsPresent = !boxes[b].IsPresent;
            UpdateAll();
        }

        private void OnOffer(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            DoMathStuff();
        }

        private void UpdateAll()
        {
            UpdateUI();
            DoMathStuff();
        }

        private void UpdateUI()
        {
            foreach (Button b in boxes.Keys)
            {
                if (boxes[b].Color == BoxInfo.BoxColor.BLUE)
                {
                    if (boxes[b].IsPresent)
                    {
                        b.Margin = new Thickness(0, 6, 0, 6);
                    }
                    else
                    {
                        b.Margin = new Thickness(-85, 6, 0, 6);
                    }
                }
                else
                {
                    if (boxes[b].IsPresent)
                    {
                        b.Margin = new Thickness(0, 6, 0, 6);
                    }
                    else
                    {
                        b.Margin = new Thickness(0, 6, -85, 6);
                    }
                }
            }
        }

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