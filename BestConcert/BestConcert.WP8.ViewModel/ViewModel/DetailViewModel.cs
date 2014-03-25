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
        private List<OrderItem> _currentOrder;

        public List<OrderItem> CurrentOrder
        {
            get { return _currentOrder; }
            set
            {
                _currentOrder = value;
                RaisePropertyChanged(() => CurrentOrder);
            }
        }
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
        private Visibility _basketVisibilite;

        public Visibility BasketVisibilite
        {
            get { return _basketVisibilite; }
            set
            {
                _basketVisibilite = value;
                RaisePropertyChanged(() => BasketVisibilite);
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
            GetCurrentOrder();
            BasketVisibilite = Visibility.Collapsed;
            //Test
            _quantity = 5;
        }

        private async void GetCurrentOrder()
        {
            CurrentOrder = (await ManagementProvider.GetCurrentOrderFromUserTokenAsync(CurrentUser.Token)).OrderItems;
        }

        private void goBack()
        {
            _nav.GoBack();
        }

        private void basket()
        {
            BasketVisibilite = Visibility.Visible;
        }

        private async void addToBasket()
        {
            await ManagementProvider.AddOrderItemAsync(CurrentUser.Token, CurrentConcert.Id.ToString(),
                _quantity.ToString());
        }
    }
}
