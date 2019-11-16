using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemView : ContentView
    {
        public event EventHandler Clicked;

        public string Text
        {
            set => LblTitle.Text = value;
        }

        public string Icon
        {
            set => Image.Source = value;
        }

        public ItemView()
        {
            InitializeComponent();
        }

        private void OnTapped(object sender, EventArgs e)
        {
            Clicked?.Invoke(sender, e);
        }
    }
}