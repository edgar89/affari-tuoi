namespace AffariTuoi
{
    public partial class MainForm : Form
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

        public MainForm()
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

        private void OnBoxClicked(object sender, EventArgs e)
        {
            if (sender is not Button)
                return;

            Button b = (Button)sender;

            boxes[b].IsPresent = !boxes[b].IsPresent;
            UpdateAll();
        }

        private void OnOffer(object sender, EventArgs e)
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
                        b.Location = new Point(0, b.Location.Y);
                    }
                    else
                    {
                        b.Location = new Point(-145, b.Location.Y);
                    }
                }
                else
                {
                    if (boxes[b].IsPresent)
                    {
                        b.Location = new Point(578, b.Location.Y);
                    }
                    else
                    {
                        b.Location = new Point(735, b.Location.Y);
                    }
                }
            }
        }

        private void DoMathStuff()
        {
            IEnumerable<BoxInfo> boxInPlay = boxes.Values.Where(b => b.IsPresent);

            double avg = boxInPlay.Select(b => b.Value).Average();
            double sigma = Math.Sqrt(boxInPlay.Select(b => Math.Pow(b.Value - avg, 2)).Sum() / boxInPlay.Count());
            double ratioToAvg = (double)offerNumericUpDown.Value / avg;
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
