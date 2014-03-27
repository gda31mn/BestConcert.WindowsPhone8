using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

        public ICommand ValidationCommand { get; set; }

        public ValidationViewModel()
        {
            ValidationCommand = new RelayCommand(OnClickValidation);
        }

        private void OnClickValidation()
        {
            if (String.IsNullOrEmpty(CreditCardNumber))
            {
                
            }
        }
    }
}
