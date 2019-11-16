using System;
using System.Collections.Generic;
using System.Linq;

namespace TheChallenge.Backend.Entities
{
    public class ImageRootobject
    {
        public DateTime created { get; set; }

        public string id { get; set; }

        public string iteration { get; set; }

        public Prediction[] predictions { get; set; }

        public string project { get; set; }

        public IEnumerable<Prediction> GetSortedPredicationsHigherThan(double minimalProbability)
        {
            return predictions.Where(p => p.probability > minimalProbability).OrderByDescending(x => x.probability);
        }
    }

    public class Prediction
    {
        public float probability { get; set; }

        public string tagId { get; set; }

        public string tagName { get; set; }

        public string ToFriendlyString()
        {
            return $"{probability * 100}% {tagName}";
        }
    }
}
