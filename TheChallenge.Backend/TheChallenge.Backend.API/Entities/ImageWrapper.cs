using System;
namespace TheChallenge.Backend.Entities
{
    public class ImageWrapper
    {
        public Guid? correlationId;

        public ImageRootobject Image { get; set; }

        public DateTime Date { get; set;}
    }
}
