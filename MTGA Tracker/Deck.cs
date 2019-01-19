using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MTGA_Tracker
{
    class Deck
    {
        #region DllImport

        [DllImport("shell32.dll")]
        static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr pszPath);

        #endregion

        #region JSON

        public class CardList
        {
            public string id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string format { get; set; }
            public string resourceId { get; set; }
            public int deckTileId { get; set; }
            public List<MainDeck> mainDeck { get; set; }
            public List<object> sideboard { get; set; }
            public DateTime lastUpdated { get; set; }
            public bool lockedForUse { get; set; }
            public bool lockedForEdit { get; set; }
            public bool isValid { get; set; }
        }

        public class MainDeck
        {
            public string id { get; set; }
            public int quantity { get; set; }
        }

        #endregion

        #region methods

        //Get the whole deck list using RegEx, excluding precog decks
        public static List<CardList> GetDeckLists()
        {
            //Get the log file
            string s = GetLog();

            //Create the RegEx string and normalize new line characters
            Regex regex = new Regex(@"(?:Deck\.GetDeckLists\([\d]+\)(?:\n|\r|\r\n))(.*?)(}(?:\n\n|\r\r|\r\n\r\n)])", RegexOptions.Singleline);
            s = Regex.Replace(s, @"\r\n|\r|\n", "\r\n");

            if (regex.IsMatch(s))
            {
                Console.WriteLine("###\n\nYes\n\n###\n\n");

                //Create a collection containing the results
                MatchCollection matchCollection = regex.Matches(s);

                //Console.WriteLine("matchCollection count: " + matchCollection.Count);
                //Console.WriteLine("matchCollection[0].Groups count: " + matchCollection[0].Groups.Count);

                //Get the latest deck list and combines both capturing groups
                string ss = matchCollection[matchCollection.Count - 1].Groups[1].Value + matchCollection[matchCollection.Count - 1].Groups[2].Value;

                //Deserialize the deck list
                List<CardList> cardListWithPrecog = JsonConvert.DeserializeObject<List<CardList>>(ss);

                //Create a deck list for deck, excluding precogs
                List<CardList> cardList = new List<CardList>();

                //Add to cardList every non-precog deck in cardListWithPrecog
                foreach (var deck in cardListWithPrecog)
                {
                    if (!deck.name.Contains("?=?Loc/Decks/Precon/"))
                    {
                        cardList.Add(deck);
                    }
                }

                //Return a collection of decks using only the latest result, excludind precogs
                return cardList;
            }
            else
            {
                Console.WriteLine("no");

                return null;
            }
        }

        //Load the "output_log.txt" file ("%USERPROFILE%\AppData\LocalLow\Wizards Of The Coast\MTGA\output_log.txt")
        private static string GetLog()
        {
            using (StreamReader streamReader = new StreamReader(getLocalLowPath() + "\\Wizards Of The Coast\\MTGA\\output_log.txt"))
            {
                string s = streamReader.ReadToEnd();

                return s;
            }
        }

        //Get LocalLow folder path
        private static string getLocalLowPath()
        {
            //LocalLow GUID
            //https://docs.microsoft.com/en-us/windows/desktop/shell/knownfolderid#folderid_localappdatalow
            Guid localLowId = new Guid("A520A1A4-1780-4FF6-BD18-167343C5AF16");
            IntPtr intPtr;
            SHGetKnownFolderPath(localLowId, 0, IntPtr.Zero, out intPtr);  

            string path = Marshal.PtrToStringUni(intPtr);
            Marshal.FreeCoTaskMem(intPtr);
            return path;
        }

        #endregion
    }
}
