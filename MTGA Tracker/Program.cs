using System;

namespace MTGA_Tracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();

            Console.ReadLine();
        }

        private static void Test()
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

            //Console.WriteLine("#####\n");
            //foreach (var deck in Decks.FormatDeck())
            //{
            //    Console.WriteLine(deck.name + "\n");

            //    foreach (var card in deck.mainDeck)
            //    {
            //        Console.WriteLine("Card Id = {0}", card.id);
            //        Console.WriteLine("Card Quantity = {0}", card.quantity);
            //    }

            //    Console.WriteLine("\n#####\n");
            //}

            //Scryfall.Aaa(Decks.FormatDeck());
            //Console.WriteLine("Done!");

            //Console.WriteLine("#####\n");
            //foreach (var deck in Scryfall.Aaa(Decks.FormatDeck()))
            //{
            //    Console.WriteLine(deck.name + "\n");

            //    foreach (var card in deck.mainDeck)
            //    {
            //        Console.WriteLine("Card Id = {0}", card.id);
            //        Console.WriteLine("Card Quantity = {0}", card.quantity);
            //        Console.WriteLine("Card Name = {0}", card.name);
            //        Console.WriteLine("Card PNG URI = {0}", card.image_uris.png);
            //    }

            //    Console.WriteLine("\n#####\n");
            //}

            //Console.WriteLine("#####\n");
            //var deck = Scryfall.AddDataFromScryfall();
            //Console.WriteLine(deck[0].name + "\n");

            //foreach (var card in deck[0].mainDeck)
            //{
            //    Console.WriteLine("Card Id = {0}", card.id);
            //    Console.WriteLine("Card Quantity = {0}", card.quantity);
            //    Console.WriteLine("Card Name = {0}", card.name);
            //    Console.WriteLine("Card PNG URI = {0}", card.image_uris.png);
            //}
            //Console.WriteLine("\n#####\n");

            //Scryfall.DownloadBulkData();
            //Console.WriteLine("Done!");
        }
    }
}
