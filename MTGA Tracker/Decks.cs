using System.Collections.Generic;

namespace MTGA_Tracker
{
    class Decks
    {
        #region Deck

        public class Deck
        {
            public string name { get; set; }
            public int deckTileId { get; set; }
            public List<Card> mainDeck { get; set; }
        }

        public class Card
        {
            public string id { get; set; }
            public int quantity { get; set; }
            public string name { get; set; }
            public string layout { get; set; }
            public List<CardFace> card_faces { get; set; }
            public string manaCost { get; set; }
            public double cmc { get; set; }
            public string power { get; set; }
            public string toughness { get; set; }
            public List<string> colors { get; set; }
            public string setName { get; set; }
            public ImageUris image_uris { get; set; }
        }

        public class CardFace
        {
            public string name { get; set; }
            public string mana_cost { get; set; }
            public string type_line { get; set; }
            public string oracle_text { get; set; }
            public List<object> colors { get; set; }
            public string flavor_text { get; set; }
            public string artist { get; set; }
            public string illustration_id { get; set; }
            public ImageUris image_uris { get; set; }
        }

        public class ImageUris
        {
            public string small { get; set; }
            public string normal { get; set; }
            public string large { get; set; }
            public string png { get; set; }
            public string art_crop { get; set; }
            public string border_crop { get; set; }
        }

        #endregion

        #region Methods

        //Get decks from log and format them to a more useful structure
        public static List<Deck> FormatDecks()
        {
            //Get decks from parsed log file
            List<Log.Deck> logDecks = new List<Log.Deck>(Log.GetDecks());

            //Create a list containing every deck, pruning useless info
            List<Deck> decks = new List<Deck>();

            //Add to the new list every deck contained in the parsed log file's decks
            foreach (var deck in logDecks)
            {
                decks.Add(new Deck() { name = deck.name, deckTileId = deck.deckTileId, mainDeck = new List<Card>() });
            }

            //Add to each new deck its respective cards from the parsed log file's decks
            for (int i = 0; i < logDecks.Count; i++)
            {
                foreach (var card in logDecks[i].mainDeck)
                {
                    decks[i].mainDeck.Add(new Card() { id = card.id, quantity = card.quantity });
                }
            }
            
            return decks;
        }

        #endregion
    }
}
