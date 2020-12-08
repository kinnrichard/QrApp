using Newtonsoft.Json;
using QrAppMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QrAppMobile.ViewModels
{
    public class ScanDetailPageViewModel : INotifyPropertyChanged
    {
        private Employee _employee = new Employee();
        private readonly HttpClient _client = new HttpClient();
        private const string _apiUrl = "https://qrappwebapi.azurewebsites.net/api/employees/";

        public ICommand GetId { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string _employeeName;
        public string EmployeeName
        {
            get { return _employeeName; }
            set
            {
                _employeeName = value;
                OnPropertyChanged();
            }
        }

        public ScanDetailPageViewModel()
        {
           
        }

        public async Task GetUsername(string scanId)
        {
            string empId = scanId;
            string trimEmpId = empId.Remove(empId.Length - 1,1);
            string employeeUrl = _apiUrl + "username/" + trimEmpId;

            var result = await _client.GetAsync(employeeUrl);
            string data = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                string urlContent = await _client.GetStringAsync(employeeUrl);
                _employee = JsonConvert.DeserializeObject<Employee>(urlContent);

                var employee = _employee.EmployeeFirstName;
                EmployeeName = employee;

                
            }
           
        }
        
    }
}
