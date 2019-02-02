using Newtonsoft.Json;
using System.Collections.Generic;
using RestSharp;
using System.Threading;
using System;

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

        //URL for Scryfall
        private const string url = "https://api.scryfall.com/";

        public static List<Decks.Deck> Aaa(List<Decks.Deck> decks)
        {
            foreach (var card in decks[0].mainDeck)
            {
                var cardFromScryfall = GetCardFromScryfall(card.id);
                card.name = cardFromScryfall.name;
                card.manaCost = cardFromScryfall.mana_cost;
                card.cmc = cardFromScryfall.cmc;
                card.power = cardFromScryfall.power;
                card.toughness = cardFromScryfall.toughness;
                card.colors = cardFromScryfall.colors;
                card.setName = cardFromScryfall.setName;
                card.image_uris = (Decks.ImageUris)cardFromScryfall.image_uris;

                Thread.Sleep(50);
            }

            return decks;
        }

        private static RootObject GetCardFromScryfall(string id)
        {
            var client = new RestClient(url);

            var request = new RestRequest(string.Format("cards/arena/{0}", id), Method.GET);
            request.AddHeader("Accept", "application/json");

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            RootObject card = JsonConvert.DeserializeObject<RootObject>(content);

            return card;
        }
    }
}
