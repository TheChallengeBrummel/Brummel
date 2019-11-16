using System.Collections.Generic;
using System.Linq;
using TheChallenge.Backend.Core.Models;

namespace TheChallenge.Backend.API.Tagging
{
    public static class ItemTagMaps
    {
        private static List<TagMap> _mapping = new List<TagMap>{
                new TagMap() { Tag = ItemTypes.Candy.ToString(), Amount = 0, Description = "Süssigkeiten gekauft"  },
                new TagMap() { Tag = ItemTypes.Cloths.ToString(), Amount = 0, Description = "Kleidung gekauft" },
                new TagMap() { Tag = ItemTypes.Food.ToString(), Amount = 0, Description = "Nahrungsmittel gekauft" },
                new TagMap() { Tag = ItemTypes.Other.ToString(), Amount = 0, Description = "Anderes gekauft" },
                new TagMap() { Tag = ItemTypes.PrintMedia.ToString(), Amount = 0, Description = "Print Medium gekauft" },
                new TagMap() { Tag = ItemTypes.Toys.ToString(), Amount = 0, Description = "Spielzeug gekauft" }
        };

        public static TagMap GetMapByTag(string tag)
        {
            var map = _mapping.SingleOrDefault(m => m.Tag == tag);
            return map == null ? new TagMap() { Amount = 0, Description = "Unbekannt", Tag = "unknown" } : map;
        }
    }
}