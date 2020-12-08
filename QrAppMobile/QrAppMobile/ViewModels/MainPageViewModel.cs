using Acr.UserDialogs;
using Newtonsoft.Json;
using QrAppMobile.Customs;
using QrAppMobile.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace QrAppMobile.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private Employee _employee = new Employee();
        private readonly HttpClient _client = new HttpClient();
        private const string _employeeApi = "https://qrappwebapi.azurewebsites.net/api/employees/username/";

        public ICommand ScanIdCommand { get; private set; }
        public ICommand ScreenshotCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        bool _entryVisible;
        public bool EntryVisible
        {
            get { return _entryVisible; }
            set
            {
                _entryVisible = value;
                OnPropertyChanged();
            }
        }

        string _result;
        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        string _postition;
        public string Position
        {
            get { return _postition; }
            set
            {
                _postition = value;
                OnPropertyChanged();
            }
        }

        string _department;
        public string Department
        {
            get { return _department; }
            set
            {
                _department = value;
                OnPropertyChanged();
            }
        }

        string _time;
        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged();
            }
        }

        string _latitude;
        string _longitude;

        string _location;
        public string CurrentLocation
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        string _success;
        public string Success
        {
            get { return _success; }
            set
            {
                _success = value;
                OnPropertyChanged();
            }
        }

        string _noMatch;
        public string NoMatch
        {
            get { return _noMatch; }
            set
            {
                _noMatch = value;
                OnPropertyChanged();
            }
        }

        bool _noMatchVisible;
        public bool NoMatchVisible
        {
            get { return _noMatchVisible; }
            set
            {
                _noMatchVisible = value;
                OnPropertyChanged();
            }
        }

        string _messageName;
        public string MessageName
        {
            get { return _messageName; }
            set
            {
                _messageName = value;
                OnPropertyChanged();
            }
        }

        string _messageBody;
        public string MessageBody
        {
            get { return _messageBody; }
            set
            {
                _messageBody = value;
                OnPropertyChanged();
            }
        }

        public MainPageViewModel()
        {         
            EntryVisible = false;
            NoMatchVisible = false;

            ScanIdCommand = new Command
                (async () => await QrScan());
            ScreenshotCommand = new Command
               (async () => await CaptureScreenshot());
            ExitCommand = new Command(ExitPopup);
        }

        //ScreenCapturing
        public async Task CaptureScreenshot()
        {
            var screenshot = await Screenshot.CaptureAsync();
            var stream = await screenshot.OpenReadAsync();          
        }

        //Check internet Connection
        public void CheckInternet()
        {           
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                UserDialogs.Instance.HideLoading();
                NoInternetPopup();
                MessageName = "No Internet";
                MessageBody = "You have no internet connection. Please make sure to have a secure internet access.";
               
                
            }
        }

        //Using Custom popup message box
        public void NoInternetPopup()
        {
            var popup = new CustomMessage();
            App.Current.MainPage.Navigation.PushPopupAsync(popup, true);
        }

        //Using Custom popup message box
        public void ExitPopup()
        {
            var popup = new CustomSignout();
            App.Current.MainPage.Navigation.PushPopupAsync(popup, true);
        }


        //Opening QR Scanner 
        public async Task QrScan()
        {
            await GetLocation();
            EntryVisible = false;
            NoMatchVisible = false;

            var scan = new ZXingScannerPage();
            await App.Current.MainPage.Navigation.PushAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.Navigation.PopAsync();
                    UserDialogs.Instance.ShowLoading("Getting data from server...");
                    await Task.Delay(2000);
                    UserDialogs.Instance.HideLoading();
                    
                    Result = result.Text;
                    await CheckUsername(Result);                   
                });
            };
        }

        //Getting username if exist in Web API
        public async Task CheckUsername(string scanResult)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                NoInternetPopup();             
            }
            else
            {
                string empId = scanResult;
                string employeeUrl = _employeeApi + empId;
                var result = await _client.GetAsync(employeeUrl);
                string data = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    string urlContent = await _client.GetStringAsync(employeeUrl);
                    _employee = JsonConvert.DeserializeObject<Employee>(urlContent);

                    Name = _employee.EmployeeLastName + ", " + _employee.EmployeeFirstName + _employee.EmployeeMiddleName;
                    Position = _employee.EmployeePosition;
                    Department = _employee.EmployeeDepartment;
                    Time = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
                    Success = "Your attendance successfully saved.";
                    EntryVisible = true;
                    NoMatchVisible = false;
                }
                else
                {
                    NoMatch = "No Record.";
                    NoMatchVisible = true;
                    EntryVisible = false;
                }
            }           
        }

        //Getting location address using Geocoding
        public async Task GetLocation()
        {
            Location location = await Geolocation.GetLastKnownLocationAsync();
            if(location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }

            _latitude = location.Latitude.ToString();
            _longitude = location.Longitude.ToString();

            try
            {
                double.TryParse(_latitude, out var lt);
                double.TryParse(_longitude, out var ln);

                var placemarks = await Geocoding.GetPlacemarksAsync(lt, ln);
                Placemark placemark = placemarks.FirstOrDefault();
                if (placemark == null)
                {
                    CurrentLocation = "Unable to detect Location";
                }
                else
                {
                    CurrentLocation = placemark.SubThoroughfare + "," + placemark.Thoroughfare + "," + placemark.Locality + "," + placemark.PostalCode + "," + placemark.AdminArea + "," + placemark.CountryName;
                }
            }
            catch (FeatureNotSupportedException notsuppex)
            {
            }
            catch (Exception ex)
            {                
            }
        }
    }
}
