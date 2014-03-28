using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestConcert.WP8.Core.Provider;
using BestConcert.WP8.Model;
using BestConcert.WP8.ViewModel.Singleton;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace BestConcert.WP8.ViewModel.ViewModel
{
    public class BasketViewModel : ViewModelBase
    {

        private float _basketPrice;

        public float BasketPrice
        {
            get { return _basketPrice; }
            set { _basketPrice = value; RaisePropertyChanged(() => BasketPrice); }
        }

        private UserModel _user;

        public UserModel User
        {
            get { return _user; }
            set { _user = value; RaisePropertyChanged(() => User); }
        }

        private ObservableCollection<OrderItem> _orderItemList;

        public ObservableCollection<OrderItem> OrderItemList
        {
            get { return _orderItemList; }
            set { _orderItemList = value; RaisePropertyChanged(() => OrderItemList); }
        }

        private OrderItem _selectedItem;

        public OrderItem SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; RaisePropertyChanged(() => SelectedItem); }
        }

        public RelayCommand LoadPage { get; set; }
        public RelayCommand NavToPayement { get; set; }
        public RelayCommand DeleteOrderItem { get; set; }

        private INavigationService _nav;
        public BasketViewModel(INavigationService nav)
        {
            _nav = nav;
            LoadPage = new RelayCommand(loadPage);
            NavToPayement = new RelayCommand(navtoPayement);
            DeleteOrderItem = new RelayCommand(deleteOrderItem);
            _orderItemList = new ObservableCollection<OrderItem>();
            User = UserDataSingleton.Instance.User;
        }

        private async  void deleteOrderItem()
        {
            if (SelectedItem != null)
            {
                await ManagementProvider.DeleteOrderItemAsync(SelectedItem.Id.ToString());
                OrderItemList.Remove(SelectedItem);
                calculPrice();
            }
        }

        private void navtoPayement()
        {
            if(OrderItemList.Count > 0)
            _nav.NavigateTo(new Uri("/View/ValidationPage.xaml",UriKind.RelativeOrAbsolute));
        }

        private async void loadPage()
        {
            OrderItemList = new ObservableCollection<OrderItem>((await ManagementProvider.GetCurrentOrderFromUserTokenAsync(User.Token)).OrderItems);
            calculPrice();
        }

        private void calculPrice()
        {
            BasketPrice = 0;
            if (OrderItemList != null)
            {
                foreach (var orderItem in OrderItemList)
                {
                    BasketPrice = BasketPrice + orderItem.Concert.Price*orderItem.Quantity;
                }
            }
        }

    }
}
