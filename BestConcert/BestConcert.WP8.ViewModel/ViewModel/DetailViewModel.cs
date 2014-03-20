using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BestConcert.WP8.Core.Provider;
using BestConcert.WP8.Model;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace BestConcert.WP8.ViewModel.ViewModel
{
    public class DetailViewModel : ViewModelBase
    {

        public ConcertModel CurrentConcert { get; set; }

        public UserModel CurrentUser { get; set; }

        public RelayCommand AddToBasket { get; set; }
        public RelayCommand Basket { get; set; }
        public RelayCommand Cancel { get; set; }

        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                RaisePropertyChanged(() => Quantity);
            }
        }

        private INavigationService _nav;
        public DetailViewModel(INavigationService nav)
        {
            _nav = nav;
            CurrentConcert = Singleton.ConcertDataSingleton.Instance.Concert;
            CurrentUser = Singleton.UserDataSingleton.Instance.User;
            AddToBasket = new RelayCommand(addToBasket);
            Basket = new RelayCommand(basket);
            Cancel = new RelayCommand(goBack);

            //Test
            _quantity = 5;
        }

        private void goBack()
        {
            _nav.GoBack();
        }

        private async void basket()
        {
            foreach (var orderItem in
                (await ManagementProvider.GetCurrentOrderFromUserIdAsync(Singleton.UserDataSingleton.Instance.User.Token)).OrderItems)
            {
                Debug.WriteLine(orderItem.Concert);
            }
        }

        private async void addToBasket()
        {
            await ManagementProvider.AddOrderItemAsync(CurrentUser.Token, CurrentConcert.Id.ToString(),
                _quantity.ToString());
        }
    }
}
