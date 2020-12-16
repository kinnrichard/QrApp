using QrAppMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QrAppMobile.ViewModels
{
        public class OnboardingViewModel : MvvmHelpers.BaseViewModel
    {
        private ObservableCollection<Onboarding> items;
        private int position;
        private string nextButtonText;
        private string skipButtonText;

        public OnboardingViewModel()
        {
            SetNextButtonText("NEXT");
            SetSkipButtonText("SKIP");
            OnBoarding();
            LaunchNextCommand();
            LaunchSkipCommand();
        }

        private void SetNextButtonText(string nextButtonText) => NextButtonText = nextButtonText;
        private void SetSkipButtonText(string skipButtonText) => SkipButtonText = skipButtonText;

        private void OnBoarding()
        {
            Items = new ObservableCollection<Onboarding>
            {
                new Onboarding
                {
                    Title = "Welcome to Net QR App",
                    Content = "Portable attendance monitoring \n available in your pocket.",
                    ImageUrl = "qrcode.svg"
                },
                new Onboarding
                {
                    Title = "Cloud-based Attendance",
                    Content = "Save attendance data realtime \n in the cloud.",
                    ImageUrl = "cloud.svg"
                },
                new Onboarding
                {
                    Title = "Cross-Platform",
                    Content = "Available in most Operating system \n in mobile.",
                    ImageUrl = "cross.svg"
                }
            };
        }

        private void LaunchNextCommand()
        {

            NextCommand = new Command(() =>
            {
                if (LastPositionReached())
                {
                    ExitOnBoarding();
                }
                else
                {
                    MoveToNextPosition();
                }
            });
        }        
        private void LaunchSkipCommand()
        {
            SkipCommand = new Command(() =>
            {
                ExitOnBoarding();

            });
        }

        private static void ExitOnBoarding()
            => Application.Current.MainPage.Navigation.PopModalAsync();

        private void MoveToNextPosition()
        {
            var nextPosition = ++Position;
            Position = nextPosition;
        }

        private bool LastPositionReached()
            => Position == Items.Count - 1;

        public ObservableCollection<Onboarding> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }

        public string NextButtonText
        {
            get => nextButtonText;
            set => SetProperty(ref nextButtonText, value);
        }  
        public string SkipButtonText
        {
            get => skipButtonText;
            set => SetProperty(ref skipButtonText, value);
        }

        public int Position
        {
            get => position;
            set
            {
                if (SetProperty(ref position, value))
                {
                    UpdateNextButtonText();
                }
            }
        }

        private void UpdateNextButtonText()
        {
            if (LastPositionReached())
            {
                SetNextButtonText("GOT IT");
            }
            else
            {
                SetNextButtonText("NEXT");
            }
        }

        public ICommand NextCommand { get; private set; }
        public ICommand SkipCommand { get; private set; }
    }
}
