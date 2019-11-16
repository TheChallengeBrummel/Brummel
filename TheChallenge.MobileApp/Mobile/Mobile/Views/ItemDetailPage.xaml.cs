using System;
using Mobile.Services;
using Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        private readonly GridMenuItem item;

        public ItemDetailPage(GridMenuItem item)
        {
            this.item = item;
            InitializeComponent();

            ItemView.Icon = item.Image;
            ItemView.Text = item.Title;

            BtnConfirm.Clicked += BtnConfirmOnClicked;
        }

        private async void BtnConfirmOnClicked(object sender, EventArgs e)
        {
            if (double.TryParse(Amount.Text, out var amount))
            {
                BtnConfirm.IsEnabled = false;
                await ServiceLocator.Client.BuyAsync(new BuyItemRequest
                {
                    UserId = ServiceLocator.UserId.ToString(),
                    Amount = amount,
                    ItemType = item.ItemType.ToString()
                });
                ServiceLocator.LastBoughtType = item.ItemType;

                await Navigation.PopToRootAsync();
            }
        }
    }
}
