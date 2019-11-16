using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mobile.Services;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnimationPage : ContentPage
    {
        private bool isAddingCash;
        private bool hasAddedCash;
        private TransactionsProvider transactionsProvider;

        public AnimationPage()
        {
            InitializeComponent();
            btnTakePhoto.Clicked += BtnTakePhotoOnClicked;
        }

        public async void Load()
        {
            SetLoading(true);
            await Task.WhenAll(LoadImage(true), LoadAmount());

            LblAmount.IsVisible = true;
            SetLoading(false);
            await LoadImage(false);
        }

        private void SetLoading(bool loading)
        {
            LblAmount.IsVisible = !loading;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!isAddingCash)
            {
                Load();
            }
        }

        private async void BtnTakePhotoOnClicked(object sender, EventArgs e)
        {
            isAddingCash = true;
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                DisplayAlert("No camera", "No camera available", "OK");
                isAddingCash = false;
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Small,
                Directory = "photos",
                Name = "money.jpg"
            });

            if (file == null)
            {
                isAddingCash = false;
                return;
            }

            var fileStream = file.GetStream();
            byte[] data;

            using (var memoryStream = new MemoryStream())
            {
                await fileStream.CopyToAsync(memoryStream);
                fileStream.Seek(0, SeekOrigin.Begin);
                memoryStream.Seek(0, SeekOrigin.Begin);

                data = memoryStream.ToArray();
            }

            var json = JsonConvert.SerializeObject(new
            {
                UserId = ServiceLocator.UserId.ToString(),
                ImageData = Convert.ToBase64String(data)
            });

            SetLoading(true);
            _ = LoadImage(true);
            await ServiceLocator.HttpClient.PostAsync(ServiceLocator.Client.BaseUrl + "Money/add", new StringContent(json, Encoding.UTF8, "application/json"));
            hasAddedCash = true;
            isAddingCash = false;
            Load();
        }

        private async Task LoadAmount()
        {
            var response = await ServiceLocator.Client.UserAsync(ServiceLocator.UserId.ToString());
            transactionsProvider = new TransactionsProvider(response);
            LblAmount.Text = $"{response.Balance:0.00} CHF";
            if (ServiceLocator.LastBalanceValue == response.Balance)
            {
                hasAddedCash = false;
            }
            ServiceLocator.LastBalanceValue = response.Balance;
        }

        private async Task LoadImage(bool loading)
        {
            var image = "bear.gif";
            if (loading)
            {
                image = "bear_idle_new.gif";
            }
            else
            {
                if (ServiceLocator.LastBalanceValue > 0)
                {
                    image = GetAnimationImage();
                }
                else
                {
                    image = "bear_sad.gif";
                }

                if (hasAddedCash)
                {
                    image = "bear_cash.gif";
                    SoundPlayer.PlaySound("Love.wav");
                }

                hasAddedCash = false;
                ServiceLocator.LastBoughtType = null;
            }

            using (var bgContent = typeof(AnimationPage).Assembly.GetManifestResourceStream("Mobile.Animations.background.jpg"))
            using (var bgMemoryStream = new MemoryStream())
            using (var content = typeof(AnimationPage).Assembly.GetManifestResourceStream("Mobile.Animations." + image))
            using (var memoryStream = new MemoryStream())
            {
                await bgContent.CopyToAsync(bgMemoryStream);
                var background = Convert.ToBase64String(bgMemoryStream.ToArray());

                await content.CopyToAsync(memoryStream);
                var data = Convert.ToBase64String(memoryStream.ToArray());

                WebView.Source = new HtmlWebViewSource
                {
                    Html = $"<html><body style=\"margin:0px; padding:0px; background-size: cover; background-image: url('data:image/jpeg;base64,{background}')\"><div style=\"position: fixed; bottom: 100px\"><img src=\"data:image/gif;base64,{data}\" style=\"max-width: 60%; max-height: 80%; margin: 0 auto; display: block;\" /></div></body></html>"
                };
            }
        }

        private string GetAnimationImage()
        {
            switch (ServiceLocator.LastBoughtType)
            {
                case ItemTypes.Candy:
                    SoundPlayer.PlaySound("yummy.mp3");
                    return "bear_candy.gif";
                case ItemTypes.Cloths:
                    return "bear_clothes.gif";
                case ItemTypes.PrintMedia:
                    return "bear_book.gif";
                case ItemTypes.Toys:
                    return "bear_toys.gif";
                case ItemTypes.Food:
                    SoundPlayer.PlaySound("Food.wav", true);
                    return "bear_food.gif";
            }
            return "bear.gif";
        }

        private void OnBearTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuPage(ServiceLocator.LastBalanceValue > 0));
        }

        private void OnBalanceTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BIView(transactionsProvider), true);
        }
    }
}