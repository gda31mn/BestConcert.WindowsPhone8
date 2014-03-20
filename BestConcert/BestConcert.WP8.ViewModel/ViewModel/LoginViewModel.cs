using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BestConcert.WP8.Core.Provider;
using BestConcert.WP8.Model;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Shell;

namespace BestConcert.WP8.ViewModel.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        #region Properties

        private string _loginEmail;

        public string LoginEmail
        {
            get { return _loginEmail; }
            set
            {
                _loginEmail = value;
                RaisePropertyChanged(() => LoginEmail);
            }

        }

        private string _loginPassword;

        public string LoginPassword
        {
            get { return _loginPassword; }
            set { _loginPassword = value;
                RaisePropertyChanged(() => LoginPassword);
            }

        }
        private Visibility _verifVisibility;

        public Visibility VerifVisibility
        {
            get { return _verifVisibility; }
            set
            {
                _verifVisibility = value;
                RaisePropertyChanged(() => VerifVisibility);
            }

        }

        private Visibility _progressVisibility;

        public Visibility ProgressVisibility
        {
            get { return _progressVisibility; }
            set
            {
                _progressVisibility = value;
                RaisePropertyChanged(() => ProgressVisibility);
            }

        }

        private bool _progressActive;

        public bool ProgressActive
        {
            get { return _progressActive; }
            set
            {
                _progressActive = value;
                RaisePropertyChanged(() => ProgressActive);
            }

        }
        
        #endregion

        #region Command
        public ICommand LoginCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        #endregion 
        private INavigationService nav;
        public LoginViewModel(INavigationService n)
        {
            nav = n;
            LoginCommand = new RelayCommand(LoginAction);
            SignUpCommand = new RelayCommand(signUpAction);
            LoginEmail = "tata@tata.fr";
            LoginPassword = "tototata";
            VerifVisibility = Visibility.Collapsed;
            ProgressVisibility = Visibility.Collapsed;
            ProgressActive = false;
        }

        private void signUpAction()
        {
            nav.NavigateTo(new Uri("/View/SaveUserPage.xaml", UriKind.RelativeOrAbsolute));
        }


        
        public async void LoginAction()
        {
            if (!String.IsNullOrEmpty(LoginEmail) && !String.IsNullOrEmpty(LoginPassword))
            {
                ProgressVisibility = Visibility.Visible;
                ProgressActive = true;
                string passwordSha = "";
                passwordSha = CalculateSha1(LoginPassword);


                object[] connection = await ManagementProvider.SignInAsync(LoginEmail, passwordSha);
                if ((bool)connection[0])
                {
                    Singleton.UserDataSingleton.Instance.User = (UserModel)connection[1];
                    Singleton.UserDataSingleton.Instance.User.Orders = await getUserOrders();
                    VerifVisibility = Visibility.Collapsed;

                    nav.NavigateTo(new Uri("/View/MainPage.xaml", UriKind.RelativeOrAbsolute));
                    ProgressActive = false;
                    ProgressVisibility = Visibility.Collapsed;
                }
                else
                {
                    ProgressVisibility = Visibility.Collapsed;
                    ProgressActive = false;
                    VerifVisibility = Visibility.Visible;
                }
            }
            else
            {
                ProgressVisibility = Visibility.Collapsed;
                ProgressActive = false;
                VerifVisibility = Visibility.Visible;
            }


           
        }

        private Task<List<Order>> getUserOrders()
        {
            return ManagementProvider.GetAllOrdersFromUserIdAsync(Singleton.UserDataSingleton.Instance.User.Token);
        }

        #region Business methods

        private static string CalculateSha1(string text)
        {
            //Text is parsed into bytes array with UTF8 encoding
            var utf8Encoding = new UTF8Encoding();
            var byteTextArray = utf8Encoding.GetBytes(text.ToCharArray());

            //sha1Managed object encrypte bytes array
            var sha1Managed = new SHA1Managed();
            sha1Managed.ComputeHash(byteTextArray);

            //Retrieve hash from sha1Managed and parse it to a string
            string hashValue = BitConverter.ToString(sha1Managed.Hash).Replace("-", "").ToLower();
            System.Diagnostics.Debug.WriteLine("Original Text {0}, Access {1}", text, hashValue);

            return hashValue;
        }

        #endregion

    }
}
