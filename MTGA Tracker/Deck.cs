using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MTGA_Tracker
{
    class Deck
    {
        #region DllImport

        [DllImport("shell32.dll")]
        static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr pszPath);

        #endregion

        #region constructors

        #endregion

        #region properties

        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string format { get; set; }
        public string resourceId { get; set; }
        public string deckTileId { get; set; }

        #endregion

        #region methods

        //Get the whole deck list using RegEx, excluding precog decks
        public static MatchCollection GetDeckLists()
        {
            //Get the log file
            string s = GetLog();

            //Create the RegEx string and normalize new line characters
            Regex regex = new Regex(@"(?:Deck.GetDeckLists\([\d]+\)(?:\n|\r|\r\n))(?:.*?)(?:}(?:\n\n|\r\r|\r\n\r\n)])", RegexOptions.Singleline);
            s = Regex.Replace(s, @"\r\n|\r|\n", "\r\n");

            if (regex.IsMatch(s))
            {
                Console.WriteLine("yes\n\n###");

                //Create a collection containing the results
                MatchCollection matchCollection = regex.Matches(s);

                Console.WriteLine("matchCollection count: " + matchCollection.Count);
                Console.WriteLine("matchCollection[0].Groups count: " + matchCollection[0].Groups.Count);
                //Console.WriteLine("###\n" + matchCollection[0].Groups[0].Value);

                //Return the results
                return matchCollection;
            }
            else
            {
                Console.WriteLine("no");

                return null;
            }

            //Console.WriteLine("matchCollection count: " + matchCollection.Count);
            //Console.WriteLine("matchCollection[0].Groups count: " + matchCollection[0].Groups.Count);
            //Console.WriteLine("###\n" + matchCollection[0].Groups[0].Value);

            //foreach (Group group in matchCollection[0].Groups)
            //{
            //    Console.WriteLine("###\n" + group.Value + "\n");
            //}
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

    #region JSON

    public class MainDeck
    {
        public string id { get; set; }
        public int quantity { get; set; }
    }

    public class RootObject
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

    #endregion
}
