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
    public class OrdersViewModel : ViewModelBase
    {

        public RelayCommand LoadPage { get; set; }

        private INavigationService _nav;

        public OrdersViewModel(INavigationService nav)
        {
            LoadPage = new RelayCommand(loadPage);
            User = UserDataSingleton.Instance.User;
            _nav = nav;
        }

        private ObservableCollection<Order> _orderList;

        public ObservableCollection<Order> OrderList
        {
            get { return _orderList; }
            set { _orderList = value; RaisePropertyChanged(() => OrderList); }
        }

        private UserModel _user;

        public UserModel User
        {
            get { return _user; }
            set { _user = value; RaisePropertyChanged(() => User); }
        }

        private async void loadPage()
        {
            OrderList = new ObservableCollection<Order>((await ManagementProvider.GetHistoryOrdersFromUserIdAsync(User.Token)));
        }
    }
}
