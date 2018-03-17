using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using HertfordshireMercury.Models;
using HertfordshireMercury.ViewModels;

using Plugin.Share;

namespace HertfordshireMercury.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailPage : ContentPage
	{
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Title = "Item 1",
                Description = "This is an item description.",
                PublishingDate = DateTime.Now,
                Author="A Person",
                Link="http://google.co.uk"
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        private void ReadMore_Clicked(object sender, EventArgs e)
        {
            if (CrossShare.IsSupported)
                CrossShare.Current.OpenBrowser(viewModel.Item.Link);
            else
                Device.OpenUri(new Uri(viewModel.Item.Link));
        }

        private void Share_Clicked(object sender,EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}