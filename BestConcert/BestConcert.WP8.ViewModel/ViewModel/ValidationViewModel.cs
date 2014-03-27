using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BestConcert.WP8.Core.Provider;
using BestConcert.WP8.Model;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace BestConcert.WP8.ViewModel.ViewModel
{
    public class ValidationViewModel : BestConcertViewModelBase
    {
        private string _creditCardNumber;

        public string CreditCardNumber
        {
            get { return _creditCardNumber; }
            set
            {
                _creditCardNumber = value;
                RaisePropertyChanged();
            }
        }

        private DateTimeOffset _expirationDate;

        public DateTimeOffset ExpirationDate
        {
            get { return _expirationDate; }
            set
            {
                _expirationDate = value; 
                RaisePropertyChanged();
            }
        }

        public ICommand ValidateCommand { get; set; }


        private UserModel _userConnected;

        private INavigationService _nav;

        public ValidationViewModel(INavigationService n)
        {
            _nav = n;
            ValidateCommand = new RelayCommand(OnClickValidation);

            _userConnected = Singleton.UserDataSingleton.Instance.User;

        }

        private async void OnClickValidation()
        {
            if (_userConnected == null || !String.IsNullOrEmpty(CreditCardNumber)) return;

            try
            {
                var result = await ManagementProvider.CheckCreditCardAsync(_userConnected.Token, CreditCardNumber, ExpirationDate.DateTime.ToString("MM/yyyy"));

                if (result.IsValid && !result.HasError)
                {
                    _nav.NavigateTo(new Uri("/View/TokenValidationPage.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MessageBox.Show(result.Status);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }
    }
}
