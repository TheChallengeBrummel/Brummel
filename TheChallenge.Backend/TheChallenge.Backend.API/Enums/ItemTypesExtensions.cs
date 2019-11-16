using TheChallenge.Backend.Core.Models;

namespace TheChallenge.Backend.API.Enums
{
    public static class ItemTypesExtension
    {
        public static ItemTypes GetItemTypeBySting(string itemTypeString)
        {
            switch (itemTypeString.ToLowerInvariant())
            {
                case "candy":
                    return ItemTypes.Candy;

                case "cloths":
                    return ItemTypes.Cloths;

                case "food":
                    return ItemTypes.Food;

                case "printmedia":
                    return ItemTypes.PrintMedia;

                case "toys":
                    return ItemTypes.Toys;

                default:
                    return ItemTypes.Other;
            }
        }
    }
}