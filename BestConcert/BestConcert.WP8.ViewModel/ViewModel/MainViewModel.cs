
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BestConcert.WP8.Model;
using BestConcert.WP8.ViewModel.Tools;
using Cimbalino.Phone.Toolkit.Services;
using System.Threading.Tasks;
using BestConcert.WP8.Core.Provider;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Maps.Services;

namespace BestConcert.WP8.ViewModel.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region properties


        private ConcertModel _selectedConcert;

        public ConcertModel SelectedConcert
        {
            get { return _selectedConcert; }
            set
            {
                _selectedConcert = value;
                if (_selectedConcert != null)
                {
                    Singleton.ConcertDataSingleton.Instance.Concert = _selectedConcert;
                    nav.NavigateTo(new Uri("/View/DetailPage.xaml", UriKind.RelativeOrAbsolute));
                }
                RaisePropertyChanged(() => SelectedConcert);
            }
        }

        private ObservableCollection<ConcertModel> _concertsListFromWeb;

        public ObservableCollection<ConcertModel> ConcertsListFromWeb
        {
            get { return _concertsListFromWeb; }
            set
            {
                _concertsListFromWeb = value;
                RaisePropertyChanged(() => ConcertsListFromWeb);
            }
        }

        private ObservableCollection<ConcertModel> _concertsListByDate;

        public ObservableCollection<ConcertModel> ConcertsListByDate
        {
            get { return _concertsListByDate; }
            set
            {
                _concertsListByDate = value;
                RaisePropertyChanged(() => ConcertsListByDate);
            }
        }

        private List<KingOfConcertGroup<ConcertModel>> _concertsListByKind;

        public List<KingOfConcertGroup<ConcertModel>> ConcertsListByKind
        {
            get { return _concertsListByKind; }
            set
            {
                _concertsListByKind = value;
                RaisePropertyChanged(() => ConcertsListByKind);
            }
        }


        private List<KingOfConcertGroup<ConcertModel>> _concertsListByArtist;

        public List<KingOfConcertGroup<ConcertModel>> ConcertsListByArtist
        {
            get { return _concertsListByArtist; }
            set
            {
                _concertsListByArtist = value;
                RaisePropertyChanged(() => ConcertsListByArtist);
            }
        }

        private ObservableCollection<string> _concertArtist;

        public ObservableCollection<string> ConcertArtist
        {
            get { return _concertArtist; }
            set
            {
                _concertArtist = value;
                RaisePropertyChanged(() => ConcertArtist);
            }
        }

        private string _selectedSearch;

        public string SelectedSearch
        {
            get { return _selectedSearch; }
            set
            {
                _selectedSearch = value;
                RaisePropertyChanged(() => SelectedSearch);
                setSearchConcert(SearchValue);
            }
        }

        private string _searchValue;

        public string SearchValue
        {
            get { return _searchValue; }
            set
            {
                _searchValue = value;
                RaisePropertyChanged(() => SelectedSearch);
            }
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                setsearchList(value);
                RaisePropertyChanged(() => SelectedIndex);
            }
        }


        #endregion

        #region Command

        public RelayCommand LoadPage { get; set; }
        public RelayCommand NavToBasket { get; set; }
        public RelayCommand NavToOrders { get; set; }
        public RelayCommand Logout { get; set; }

        #endregion

        private INavigationService nav;

        public MainViewModel(INavigationService n)
        {
            nav = n;

            ConcertsListFromWeb = new ObservableCollection<ConcertModel>();
            ConcertsListByDate = new ObservableCollection<ConcertModel>();
            ConcertsListByKind = new List<KingOfConcertGroup<ConcertModel>>();
            ConcertsListByArtist = new List<KingOfConcertGroup<ConcertModel>>();


            LoadPage = new RelayCommand(loadPage);
            NavToBasket = new RelayCommand(navToBasket);
            NavToOrders = new RelayCommand(navToOrders);
            Logout = new RelayCommand(logout);
        }

        private void navToOrders()
        {
            nav.NavigateTo(new Uri("/View/OrdersPage.xaml",UriKind.RelativeOrAbsolute));
        }

        private void navToBasket()
        {
            nav.NavigateTo(new Uri("/View/BasketPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void logout()
        {
           nav.NavigateTo(new Uri("/View/LoginPage.xaml",UriKind.RelativeOrAbsolute));
        }

        private void navtoSL()
        {
            //nav.NavigateTo();
        }

        private async void InitSecouriste()
        {
            ////var secouristeWeb = await ManagementProvider.GetCurrentSecouriste();
            //secouriste = secouristeWeb;
        }

        public async void initList()
        {
            ConcertsListFromWeb = new ObservableCollection<ConcertModel>();
            var concertList = await ManagementProvider.GetAllConcertAsync();
            if (concertList != null)
            {
                ConcertsListFromWeb = new ObservableCollection<ConcertModel>(concertList);
                setPostesListe(false);
                setsearchList(0);
            }
            else
            {
                ConcertsListFromWeb = new ObservableCollection<ConcertModel>();
                setPostesListe(true);
            }
        }

        private void setPostesListe(bool isNull)
        {
            if (isNull)
            {
                ConcertsListByDate = new ObservableCollection<ConcertModel>();
                ConcertsListByKind = new List<KingOfConcertGroup<ConcertModel>>();
                ConcertsListByArtist = new List<KingOfConcertGroup<ConcertModel>>();
            }
            else
            {
                ConcertsListByDate = new ObservableCollection<ConcertModel>(ConcertsListFromWeb.OrderBy(concert => concert.Date).ToList());
                var byKind = ConcertsListFromWeb.OrderBy(concert => concert.Genre).ToList();
                var byArtist = ConcertsListFromWeb.OrderBy(concert => concert.Artist).ToList();

                ConcertsListByKind = KingOfConcertGroup<ConcertModel>.CreateGroups(byKind,
               System.Threading.Thread.CurrentThread.CurrentUICulture,
               (ConcertModel s) => { return s.Genre; }, true);

                ConcertsListByArtist = KingOfConcertGroup<ConcertModel>.CreateGroups(byArtist,
                System.Threading.Thread.CurrentThread.CurrentUICulture,
                (ConcertModel s) => { return s.Artist; }, true);
            }

        }

        private void loadPage()
        {
            InitSecouriste();
            initList();
        }

        private void setSearchConcert(string name)
        {
            switch (SelectedIndex)
            {
                case 0:
                    ConcertsListByDate =
                        new ObservableCollection<ConcertModel>(
                            ConcertsListByDate.Where(poste => poste.Artist == name).ToList());
                    break;
                case 1:
                    var byKind = ConcertsListFromWeb.OrderBy(concert => concert.Genre).Where(poste => poste.Artist == name).ToList();
                
                    ConcertsListByKind = KingOfConcertGroup<ConcertModel>.CreateGroups(byKind,
               System.Threading.Thread.CurrentThread.CurrentUICulture,
               (ConcertModel s) => { return s.Genre; }, true);
                    break;
                case 2:
                    var byArtist = ConcertsListFromWeb.OrderBy(concert => concert.Artist).Where(poste => poste.Artist == name).ToList();
                    ConcertsListByKind = KingOfConcertGroup<ConcertModel>.CreateGroups(byArtist,
               System.Threading.Thread.CurrentThread.CurrentUICulture,
               (ConcertModel s) => { return s.Genre; }, true);
                    break;
            }
        }

        private void setsearchList(int value)
        {
            switch (SelectedIndex)
            {
                case 0:
                    ConcertArtist =
                        new ObservableCollection<string>(ConcertsListByDate.Select(concert => concert.Artist).ToList());
                    break;
                case 1:
                    ConcertArtist = new ObservableCollection<string>();
                    foreach (var list in ConcertsListByKind)
                    {
                        foreach (var concert in list)
                        {
                            ConcertArtist.Add(concert.Artist);
                        }
                    } break;
                case 2:
                    ConcertArtist = new ObservableCollection<string>();
                    foreach (var list in ConcertsListByArtist)
                    {
                        foreach (var concert in list)
                        {
                            ConcertArtist.Add(concert.Artist);
                        }
                    } break;
                    break;
            }
        }
    }
}