using System;
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
        }

        #endregion

        #region Methods

        public static void Aaa(List<Log.Deck> logDecks)
        {
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

            Console.WriteLine("#####\n");
            foreach (var deck in decks)
            {
                Console.WriteLine(deck.name + "\n");

                foreach (var card in deck.mainDeck)
                {
                    Console.WriteLine("Card Id = {0}", card.id);
                    Console.WriteLine("Card Quantity = {0}", card.quantity);
                }

                Console.WriteLine("\n#####\n");
            }
        }

        #endregion
    }
}
