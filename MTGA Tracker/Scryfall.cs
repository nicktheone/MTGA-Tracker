using Newtonsoft.Json;
using System.Collections.Generic;
using RestSharp;
using System;
using System.IO;
using RestSharp.Extensions;
using System.Linq;
using System.Windows.Forms;

namespace MTGA_Tracker
{
    class Scryfall
    {
        #region Card

        public class ImageUris
        {
            public string small { get; set; }
            public string normal { get; set; }
            public string large { get; set; }
            public string png { get; set; }
            public string art_crop { get; set; }
            public string border_crop { get; set; }

            //Convert Scryfall.ImageUris to Decks.ImageUris
            public static explicit operator Decks.ImageUris(ImageUris v)
            {
                Decks.ImageUris a = new Decks.ImageUris()
                {
                    small = v.small,
                    normal = v.normal,
                    large = v.large,
                    png = v.png,
                    art_crop = v.art_crop,
                    border_crop = v.border_crop
                };

                return a;
            }
        }

        public class CardFace
        {
            public string @object { get; set; }
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

        public class Legalities
        {
            public string standard { get; set; }
            public string future { get; set; }
            public string frontier { get; set; }
            public string modern { get; set; }
            public string legacy { get; set; }
            public string pauper { get; set; }
            public string vintage { get; set; }
            public string penny { get; set; }
            public string commander { get; set; }
            public string __invalid_name__1v1 { get; set; }
            public string duel { get; set; }
            public string oldschool { get; set; }
            public string brawl { get; set; }
        }

        public class Prices
        {
            public string usd { get; set; }
            public string usd_foil { get; set; }
            public string eur { get; set; }
            public string tix { get; set; }
        }

        public class RelatedUris
        {
            public string gatherer { get; set; }
            public string tcgplayer_decks { get; set; }
            public string edhrec { get; set; }
            public string mtgtop8 { get; set; }
        }

        public class PurchaseUris
        {
            public string tcgplayer { get; set; }
            public string cardmarket { get; set; }
            public string cardhoarder { get; set; }
        }

        public class RootObject
        {
            public string @object { get; set; }
            public string id { get; set; }
            public string oracle_id { get; set; }
            public List<int> multiverse_ids { get; set; }
            public int mtgo_id { get; set; }
            public int mtgo_foil_id { get; set; }
            public int arena_id { get; set; }
            public int tcgplayer_id { get; set; }
            public string name { get; set; }
            public string lang { get; set; }
            public string released_at { get; set; }
            public string uri { get; set; }
            public string scryfall_uri { get; set; }
            public string layout { get; set; }
            public bool highres_image { get; set; }
            public ImageUris image_uris { get; set; }
            public string mana_cost { get; set; }
            public double cmc { get; set; }
            public string type_line { get; set; }
            public string oracle_text { get; set; }
            public string power { get; set; }
            public string toughness { get; set; }
            public List<string> colors { get; set; }
            public List<string> color_identity { get; set; }
            public List<CardFace> card_faces { get; set; }
            public Legalities legalities { get; set; }
            public List<string> games { get; set; }
            public bool reserved { get; set; }
            public bool foil { get; set; }
            public bool nonfoil { get; set; }
            public bool oversized { get; set; }
            public bool promo { get; set; }
            public bool reprint { get; set; }
            public string set { get; set; }
            [JsonProperty(PropertyName = "set_name")]
            public string setName { get; set; }
            [JsonProperty(PropertyName = "set_uri")]
            public string setUri { get; set; }
            public string set_search_uri { get; set; }
            public string scryfall_set_uri { get; set; }
            public string rulings_uri { get; set; }
            public string prints_search_uri { get; set; }
            public string collector_number { get; set; }
            public bool digital { get; set; }
            public string rarity { get; set; }
            public string flavor_text { get; set; }
            public string illustration_id { get; set; }
            public string artist { get; set; }
            public string border_color { get; set; }
            public string frame { get; set; }
            public string frame_effect { get; set; }
            public bool full_art { get; set; }
            public bool story_spotlight { get; set; }
            public int edhrec_rank { get; set; }
            public string usd { get; set; }
            public string eur { get; set; }
            public string tix { get; set; }
            public Prices prices { get; set; }
            public RelatedUris related_uris { get; set; }
            public PurchaseUris purchase_uris { get; set; }
        }

        #endregion

        //Add the data from the bulk of Scryfall to each card
        public static List<Decks.Deck> AddDataFromScryfall()
        {
            //Get a list of formatted decks
            List<Decks.Deck> decks = Decks.FormatDecks();
            //Load the whole library
            List<RootObject> bulkCard = GetCardFromBulkScryfall();

            foreach (var deck in decks)
            {
                //Add to each card it's correspective data from Scryfall
                foreach (var card in deck.mainDeck)
                {
                    try
                    {
                        //var cardFromScryfall = GetCardFromScryfall(card.id);
                        RootObject cardFromScryfall = bulkCard.Where(x => Convert.ToString(x.arena_id) == card.id).FirstOrDefault();
                        card.name = cardFromScryfall.name;
                        card.manaCost = cardFromScryfall.mana_cost;
                        card.cmc = cardFromScryfall.cmc;
                        card.power = cardFromScryfall.power;
                        card.toughness = cardFromScryfall.toughness;
                        card.colors = cardFromScryfall.colors;
                        card.color_identity = cardFromScryfall.color_identity;
                        card.setName = cardFromScryfall.setName;
                        card.layout = cardFromScryfall.layout;

                        //Check if card is multi-faced
                        if (card.layout == "transform")
                        {
                            //Take each card face and converts them (https://stackoverflow.com/questions/40148491/cast-class-a-to-class-b-without-generics/40148572#40148572)
                            card.card_faces = cardFromScryfall.card_faces.Select(a => new Decks.CardFace()
                            {
                                name = a.name,
                                mana_cost = a.mana_cost,
                                image_uris = (Decks.ImageUris)a.image_uris
                            }).ToList();
                        }
                        else
                        {
                            card.image_uris = (Decks.ImageUris)cardFromScryfall.image_uris;
                        }
                    }
                    //Output which card is giving issues
                    catch (NullReferenceException)
                    {
                        MessageBox.Show("ERROR: card missing {0}", card.id);
                    }
                }
            }

            //Return a list of sorted decks
            return SortDecks(decks);
        }

        private static List<Decks.Deck> SortDecks(List<Decks.Deck> decks)
        {
            foreach (var deck in decks)
            {
                List<Decks.Card> redCards = new List<Decks.Card>();
                List<Decks.Card> blueCards = new List<Decks.Card>();
                List<Decks.Card> blackCards = new List<Decks.Card>();
                List<Decks.Card> whiteCards = new List<Decks.Card>();
                List<Decks.Card> greenCards = new List<Decks.Card>();
                List<Decks.Card> noColorCards = new List<Decks.Card>();

                redCards = deck.mainDeck.Where(card => card.color_identity.Contains("R")).ToList();
                blueCards = deck.mainDeck.Where(card => card.color_identity.Contains("U")).ToList();
                blackCards = deck.mainDeck.Where(card => card.color_identity.Contains("B")).ToList();
                whiteCards = deck.mainDeck.Where(card => card.color_identity.Contains("W")).ToList();
                greenCards = deck.mainDeck.Where(card => card.color_identity.Contains("G")).ToList();
                var cards = redCards.Union(blueCards).Union(blackCards).Union(whiteCards).Union(greenCards).ToList();
                noColorCards = deck.mainDeck.Except(cards).ToList();
                deck.mainDeck = cards.Union(noColorCards).ToList();
            }

            return decks;
        }

        //Sorts cards in each deck based on a custom sorting priority
        private static List<Decks.Deck> SortDecks2(List<Decks.Deck> decks)
        {
            //https://stackoverflow.com/questions/54590688/sorting-a-listt-based-on-ts-liststring-property/

            // Higher-priority colours come first
            var coloursPriority = new List<string>() { "W", "U", "B", "R", "G", };

            foreach (var deck in decks)
            {
                // Turn the card's colour into an index. If the card has multiple colours,
                // pick the smallest of the corresponding indexes.
                try
                {
                    deck.mainDeck = deck.mainDeck.OrderBy(card => card.color_identity.Select(colour => coloursPriority.IndexOf(colour)).Min()).ToList();
                }
                catch (Exception)
                {
                    //throw;
                }
            }

            return decks;
        }

        //Get card data from Scryfall based on Arena Id
        private static RootObject GetCardFromScryfall(string id)
        {
            var client = new RestClient("https://api.scryfall.com/");

            var request = new RestRequest(string.Format("cards/arena/{0}", id), Method.GET);
            request.AddHeader("Accept", "application/json");

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            RootObject card = JsonConvert.DeserializeObject<RootObject>(content);

            return card;
        }

        //Deserialize the whole Scryfall library
        private static List<RootObject> GetCardFromBulkScryfall()
        {
            string s = null;

            using (StreamReader streamReader = new StreamReader(Path.Combine(GetAppDataPath(), @"scryfall-default-cards.json")))
            {
                s = streamReader.ReadToEnd();
            }

            List<RootObject> cards = JsonConvert.DeserializeObject<List<RootObject>>(s);

            return cards;
        }

        //Download the whole card library from Scryfall
        private static void DownloadBulkData()
        {
            var client = new RestClient("https://archive.scryfall.com/");
            var request = new RestRequest("json/scryfall-default-cards.json", Method.GET);

            //If the app directory doesn't exist create it
            if (!Directory.Exists(GetAppDataPath()))
            {
                Directory.CreateDirectory(GetAppDataPath());
            }

            client.DownloadData(request).SaveAs(GetAppDataPath() + @"\scryfall-default-cards.json");
        }

        //Get the app folder path
        private static string GetAppDataPath()
        {
            //Get the AppData path
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            //Don't add the "\" at the beginning of the path2 or else it'll return path2 (https://stackoverflow.com/questions/18008276/why-the-path-combine-is-not-combining-the-path-and-file)
            string appPath = Path.Combine(appDataPath, @"MTGA Tracker");

            return appPath;
        }
    }
}
