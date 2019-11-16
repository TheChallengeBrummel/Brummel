namespace Mobile.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Animation,
        Camera
    }

    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
