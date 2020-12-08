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
    public partial class CustomMessage : PopupPage
    {
        public CustomMessage()
        {
            InitializeComponent();
        }

        private void OnClickOk(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PopPopupAsync();

        }
    }
}