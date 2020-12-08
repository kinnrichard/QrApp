using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QrAppMobile.Customs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomSignout : PopupPage
    {
        public CustomSignout()
        {
            InitializeComponent();
        }
        
        private void OnEXit(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PopPopupAsync();
            App.Current.MainPage.Navigation.PopAsync();

        }
    }
}