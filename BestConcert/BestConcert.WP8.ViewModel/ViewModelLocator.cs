/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:BestConcert.WP8.ViewModel"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using BestConcert.WP8.ViewModel.ViewModel;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace BestConcert.WP8.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<SaveUserViewModel>();
            SimpleIoc.Default.Register<DetailViewModel>();
            SimpleIoc.Default.Register<ValidationViewModel>();
            SimpleIoc.Default.Register<OrdersViewModel>();
            SimpleIoc.Default.Register<BasketViewModel>();
            SimpleIoc.Default.Register<TokenValidationViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public LoginViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public SaveUserViewModel Save
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SaveUserViewModel>();
            }
        }

        public DetailViewModel Detail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DetailViewModel>();
            }
        }

        public ValidationViewModel Validation
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ValidationViewModel>();
            }
        }

        public OrdersViewModel Orders
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OrdersViewModel>();
            }
        }

        public BasketViewModel Basket
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BasketViewModel>();
            }
        }

        public TokenValidationViewModel TokenValidation
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TokenValidationViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}