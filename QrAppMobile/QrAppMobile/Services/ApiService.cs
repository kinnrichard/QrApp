using Newtonsoft.Json;
using QrAppMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;

namespace QrAppMobile.Services
{
    public class ApiService : INotifyPropertyChanged
    {
        public const string ApiUrl = "https://xamarinwebapiqr.azurewebsites.net/api/accounts";
        public List<Account> AccountList { get; set; }
        public ObservableCollection<Account> _account;
        public ObservableCollection<Account> Account
        {
            get
            {
                return _account;
            }
            set
            {
                _account = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ApiService()
        {
            GetAaccounts();
        }

        public async void GetAaccounts()
        {
            using (var client = new HttpClient())
            {
                var responseContent = await client.GetStringAsync(ApiUrl);
                AccountList = JsonConvert.DeserializeObject<List<Account>>(responseContent);
                Account = new ObservableCollection<Account>(AccountList);
            }
        }
    }
}
