using Mobile.Services;

namespace Mobile.ViewModels
{
    public class GridMenuItem
    {
        public GridMenuItem(string title, string image, string disabledImage, ItemTypes itemType)
        {
            Title = title;
            Image = image;
            DisabledImage = disabledImage;
            ItemType = itemType;
        }

        public string Image { get; }

        public string DisabledImage { get; }

        public ItemTypes ItemType { get; }

        public string Title { get; }
    }
}
