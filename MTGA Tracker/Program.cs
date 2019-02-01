using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGA_Tracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("###\n\n" + Deck.GetDeckLists().Count + "\n\n###\n");

            foreach (var deck in Deck.GetDeckLists())
            {
                Console.WriteLine(deck.name);
            }

            Console.ReadLine();
        }
    }
}
