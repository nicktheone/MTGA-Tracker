using System;
using System.Collections.Generic;
using System.Drawing;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;

            ////Create the FlowLayoutPanel and set its properties and events
            //TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            //tableLayoutPanel.AutoScroll = true;
            //tableLayoutPanel.Dock = DockStyle.Fill;
            //this.Controls.Add(tableLayoutPanel);

            //Create the FlowLayoutPanel and set its properties and events
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.DoubleClick += flowLayoutPanel_DoubleClick;
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.Dock = DockStyle.Fill;
            this.Controls.Add(flowLayoutPanel);
        }

        private async void flowLayoutPanel_DoubleClick(object sender, EventArgs e)
        {
            List<Decks.Deck> decks = Scryfall.AddDataFromScryfall();
            foreach (var card in decks[8].mainDeck)
            {
                //Create a new Picture Box for the card image
                PictureBox pictureBox = new PictureBox();
                pictureBox.MouseHover += PictureBox_MouseHover;
                pictureBox.Tag = card;

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

                //Set autosizing for the Picture Box
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;

                //using (Graphics G = Graphics.FromImage(pictureBox.Image))
                //{
                //    using (var sf = new StringFormat()
                //    {
                //        Alignment = StringAlignment.Center,
                //        LineAlignment = StringAlignment.Center,
                //    })
                //    {
                //        G.DrawString(card.name, new Font("Tahoma", 20), Brushes.White, new Rectangle(0, 0, pictureBox.Width, pictureBox.Height), sf);
                //    }
                //} 
                //pictureBox.Invalidate();

                //Add the control to the Flow Layout Panel
                FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)sender;
                flowLayoutPanel.Controls.Add(pictureBox);
            }
        }

        private void PictureBox_MouseHover(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            Decks.Card card = (Decks.Card)pictureBox.Tag;

            PictureBox pictureBox2 = new PictureBox();
            pictureBox2.Load(card.image_uris.small);
            pictureBox2.Location = new Point(50, 50);
            Controls.Add(pictureBox2);
            pictureBox2.BringToFront();
        }
    }
}
