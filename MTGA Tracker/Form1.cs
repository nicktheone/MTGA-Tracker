using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MTGA_Tracker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void flowLayoutPanel1_DoubleClick(object sender, System.EventArgs e)
        {
            List<Decks.Deck> decks = Scryfall.AddDataFromScryfall();
            foreach (var card in decks[0].mainDeck)
            {
                PictureBox pictureBox = new PictureBox();
                await Task.Run(() => pictureBox.Load(card.image_uris.small));
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                flowLayoutPanel1.Controls.Add(pictureBox);
            }
        }
    }
}
