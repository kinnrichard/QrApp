using QrAppMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QrAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanDetailPage : ContentPage
    {
        public ScanDetailPage()
        {
            InitializeComponent();
            //BindingContext = new ScanDetailPageViewModel();
        }

       
    }
}