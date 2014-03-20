using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using BestConcert.WP8.Core.Provider;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace BestConcert.WP8.ViewModel.ViewModel
{
    public class SaveUserViewModel : ViewModelBase
    {
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => FirstName);
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged(() => LastName);
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                RaisePropertyChanged(() => Address);
            }
        }


        public ICommand Check { get; set; }
        public ICommand Cancel { get; set; }
        public INavigationService nav { get; set; }

        public SaveUserViewModel(INavigationService n)
        {
            nav = n;
            Check = new RelayCommand(checkAction);
            Cancel = new RelayCommand(cancelAction);
        }

        private void cancelAction()
        {
            nav.GoBack();
        }

        private async void checkAction()
        {
            if (IsValid(_email))
            {
                await ManagementProvider.AddUserAsync(FirstName, LastName, CalculateSha1(Password), Email, Address);
                nav.NavigateTo(new Uri("Mainpage.xaml",UriKind.RelativeOrAbsolute));
            }
            
        }

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
        public bool IsValid(string emailaddress)
        {
            try
            {
                System.Text.RegularExpressions.Regex myRegex = new Regex(@"^([\w]+)@([\w]+)\.([\w]+)$");
                //([\w]+) ==> caractère alphanumérique apparaissant une fois ou plus 
                return myRegex.IsMatch(emailaddress); // retourne true ou false selon la vérification
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
