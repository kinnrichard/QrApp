
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QrAppMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Transparent;
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
