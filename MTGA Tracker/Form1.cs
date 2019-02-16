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
            foreach (var card in decks[8].mainDeck)
            {
                PictureBox pictureBox = new PictureBox();

                //Check if card is multi-faced
                if (card.layout == "transform")
                {
                    //MessageBox.Show("TRANSFORM: " + card.name);
                    await Task.Run(() => pictureBox.Load(card.card_faces[0].image_uris.small));
                }
                else
                {
                    //MessageBox.Show(card.name);
                    await Task.Run(() => pictureBox.Load(card.image_uris.small));
                }
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                flowLayoutPanel1.Controls.Add(pictureBox);
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
