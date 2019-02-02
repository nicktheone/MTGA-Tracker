using System;

namespace MTGA_Tracker
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("###\n\n" + Deck.GetDecks().Count + "\n\n###\n");

            //foreach (var deck in Deck.GetDecks())
            //{
            //    Console.WriteLine(deck.name);
            //}

            //foreach (var card in Log.GetDecks()[0].mainDeck)
            //{
            //    Console.WriteLine(card.id);
            //    Console.WriteLine(card.quantity);
            //}

            //foreach (var deck in Decks.GetDecks())
            //{
            //    foreach (var prop in deck.GetType().GetProperties())
            //    {
            //        Console.WriteLine(prop.Name + " = " + prop.GetValue(deck, null));
            //    }
            //}

            Decks.Aaa(Log.GetDecks());

            Console.ReadLine();
        }
    }
}
