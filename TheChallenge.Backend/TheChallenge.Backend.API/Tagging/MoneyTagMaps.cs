using System.Collections.Generic;
using System.Linq;
using TheChallenge.Backend.WebApi;

namespace TheChallenge.Backend.API.Tagging
{
    public static class MoneyTagMaps
    {
        private static List<TagMap> _mapping = new List<TagMap>{
                new TagMap() { Tag = Const.ImageRecognitionTag10BackChf, Amount = 10, Description = "10.- hinzugefügt"  },
                new TagMap() { Tag = Const.ImageRecognitionTag10FrontChf, Amount = 10, Description = "10.- hinzugefügt" },
                new TagMap() { Tag = Const.ImageRecognitionTag20BackChf, Amount = 20, Description = "20.- hinzugefügt" },
                new TagMap() { Tag = Const.ImageRecognitionTag20FrontChf, Amount = 20, Description = "20.- hinzugefügt" },
                new TagMap() { Tag = Const.ImageRecognitionTag2BackChf, Amount = 2, Description = "2.- hinzugefügt" },
                new TagMap() { Tag = Const.ImageRecognitionTag2FrontChf, Amount = 2, Description = "2.- hinzugefügt" },
                new TagMap() { Tag = Const.ImageRecognitionTag50BackChf, Amount = 50, Description = "50.- hinzugefügt" },
                new TagMap() { Tag = Const.ImageRecognitionTag50FrontChf, Amount = 50, Description = "50.- hinzugefügt" },
                new TagMap() { Tag = Const.ImageRecognitionTag5BackChf, Amount = 5, Description = "5.- hinzugefügt" },
                new TagMap() { Tag = Const.ImageRecognitionTag5FrontChf, Amount = 5, Description = "5.- hinzugefügt" }
        };

        public static TagMap GetMapByTag(string tag)
        {
            var map = _mapping.SingleOrDefault(m => m.Tag == tag);
            return map == null ? new TagMap() { Amount = 0, Description = "Unbekannt", Tag = "unknown" } : map;
        }
    }
}
