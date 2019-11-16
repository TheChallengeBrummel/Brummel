using Mobile.Services;
using Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage(bool hasCash)
        {
            InitializeComponent();

            var items = new []
            {
                new GridMenuItem("Süssigkeiten", "candy_cane.png", "candy_cane_disabled.png", ItemTypes.Candy),
                new GridMenuItem("Essen", "hamburger.png", "hamburger_disabled.png",ItemTypes.Food),
                new GridMenuItem("Kleidung", "tshirt.png", "tshirt_disabled.png",ItemTypes.Cloths),
                new GridMenuItem("Spielsachen", "cubes.png", "cubes_disabled.png",ItemTypes.Toys),
                new GridMenuItem("Bücher / \"Heftli\"", "book_open.png", "book_open_disabled.png",ItemTypes.PrintMedia),
                new GridMenuItem("Anderes", "question.png", "question_disabled.png",ItemTypes.Other)
            };

            for (var i = 0; i < (items.Length + 1) / 2; i++)
            {
                MenuGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(180)
                });
            }

            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                var view = new ItemView
                {
                    Text = item.Title,
                    Icon = hasCash ? item.Image : item.DisabledImage,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    IsEnabled = hasCash
                };
                view.Clicked += (sender, args) => Navigation.PushAsync(new ItemDetailPage(item));
                MenuGrid.Children.Add(view, i % 2, i / 2);
            }
        }
    }
}