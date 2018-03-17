using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace HertfordshireMercury.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
            OpenSourceCommand = new Command(() => Device.OpenUri(new Uri("https://github.com/RobertEves92/HertfordshireMercury3")));
        }

        public ICommand OpenWebCommand { get; }
        public ICommand OpenSourceCommand { get; }
    }
}