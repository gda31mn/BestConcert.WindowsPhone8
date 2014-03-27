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
using GalaSoft.MvvmLight.Command;

namespace BestConcert.WP8.ViewModel.ViewModel
{
    public class TokenValidationViewModel : BestConcertViewModelBase
    {
        private string _tokenValidation;

        public string TokenValidation
        {
            get { return _tokenValidation; }
            set { _tokenValidation = value; RaisePropertyChanged();}
        }

        public ICommand ValidateCommand { get; set; }

        private INavigationService _nav;
        private UserModel _userConnect;

        public TokenValidationViewModel(INavigationService n)
        {
            _nav = n;

            ValidateCommand = new RelayCommand(OnClickValidate);

            _userConnect = Singleton.UserDataSingleton.Instance.User;
        }

        private async void OnClickValidate()
        {
            if (String.IsNullOrEmpty(TokenValidation) || _userConnect == null)
                return;

            try
            {
                var result = await ManagementProvider.ValidateSecureTokenAsync(_userConnect.Token, TokenValidation);

                if (result.Validated && !result.HasError)
                {
                    MessageBox.Show(result.Status);
                    _nav.NavigateTo(new Uri("/View/MainPage.xaml", UriKind.RelativeOrAbsolute));
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
