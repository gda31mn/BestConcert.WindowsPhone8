using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<OrderItem> _currentOrder;

        public ObservableCollection<OrderItem> CurrentOrder
        {
            get { return _currentOrder; }
            set
            {
                _currentOrder = value;
                RaisePropertyChanged("CurrentOrder");
            }
        }
        public UserModel CurrentUser { get; set; }

        public RelayCommand AddToBasket { get; set; }
        public RelayCommand Basket { get; set; }
        public RelayCommand Cancel { get; set; }
        public RelayCommand IncreaseQuantity { get; set; }
        public RelayCommand DecreaseQuantity { get; set; }
        public RelayCommand LoadPage { get; set; }
            

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

            CurrentOrder = new ObservableCollection<OrderItem>();

            AddToBasket = new RelayCommand(addToBasket);
            Basket = new RelayCommand(basket);
            Cancel = new RelayCommand(goBack);
            IncreaseQuantity = new RelayCommand(increaseQuantity);
            DecreaseQuantity = new RelayCommand(decreaseQuantity);
            LoadPage = new RelayCommand(loadPage);
            //GetCurrentOrder();
        }

        private void loadPage()
        {
            Quantity = 1;
        }

        private void decreaseQuantity()
        {
            if (Quantity > 1)
            {
                Quantity--;
            }
        }

        private void increaseQuantity()
        {
            Quantity++;
        }

        private async void GetCurrentOrder()
        {
            CurrentOrder = new ObservableCollection<OrderItem>((await ManagementProvider.GetCurrentOrderFromUserTokenAsync(CurrentUser.Token)).OrderItems);
        }

        private void goBack()
        {
            _nav.GoBack();
        }

        private void basket()
        {
            _nav.NavigateTo(new Uri("/View/BasketPage.xaml",UriKind.RelativeOrAbsolute));
        }

        private async void addToBasket()
        {
            await ManagementProvider.AddOrderItemAsync(CurrentUser.Token, CurrentConcert.Id.ToString(),
                _quantity.ToString());
        }
    }
}
