
using System;
using System.Collections.ObjectModel;
using System.Linq;
using BestConcert.WP8.Model;
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

        public UserModel secouriste { get; set; }

        private ObservableCollection<ConcertModel> _concertsListFromWeb;

        public ObservableCollection<ConcertModel> ConcertsListFromWeb
        {
            get { return _concertsListFromWeb; }
            set { _concertsListFromWeb = value; RaisePropertyChanged(() => ConcertsListFromWeb); }
        }

        private ObservableCollection<ConcertModel> _concertsListByDate;

        public ObservableCollection<ConcertModel> ConcertsListByDate
        {
            get { return _concertsListByDate; }
            set { _concertsListByDate = value; RaisePropertyChanged(() => ConcertsListByDate); }
        }

        private ObservableCollection<ConcertModel> _concertsListByKind;

        public ObservableCollection<ConcertModel> ConcertsListByKind
        {
            get { return _concertsListByKind; }
            set { _concertsListByKind = value; RaisePropertyChanged(() => ConcertsListByKind); }
        }


        private ObservableCollection<ConcertModel> _concertsListByArtist;

        public ObservableCollection<ConcertModel> ConcertsListByArtist
        {
            get { return _concertsListByArtist; }
            set { _concertsListByArtist = value; RaisePropertyChanged(() => ConcertsListByArtist); }
        }

        private ObservableCollection<string> _postesName;

        public ObservableCollection<string> PostesName
        {
            get { return _postesName; }
            set { _postesName = value; RaisePropertyChanged(() => PostesName); }
        }

        private string _selectedSearch;

        public string SelectedSearch
        {
            get { return _selectedSearch; }
            set
            {
                _selectedSearch = value;
                RaisePropertyChanged(() => SelectedSearch);
                setSearchPoste(SearchValue);
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

        private void setsearchList(int value)
        {
            switch (SelectedIndex)
            {
                case 0:
                    PostesName = new ObservableCollection<string>(ConcertsListByDate.Select(concert => concert.Artist).ToList());
                    break;
                case 1:
                    PostesName = new ObservableCollection<string>(ConcertsListByKind.Select(concert => concert.Artist).ToList());
                    break;
                case 2:
                    PostesName = new ObservableCollection<string>(ConcertsListByArtist.Select(concert => concert.Artist).ToList());
                    break;
            }
        }

        #endregion

        #region Command
        public RelayCommand LoadPage { get; set; }
        public RelayCommand NavToSecoursList { get; set; }
        #endregion

        private INavigationService nav;
        public MainViewModel(INavigationService n)
        {
            nav = n;

            ConcertsListFromWeb = new ObservableCollection<ConcertModel>();
            ConcertsListByDate = new ObservableCollection<ConcertModel>();
            ConcertsListByKind = new ObservableCollection<ConcertModel>();
            ConcertsListByArtist = new ObservableCollection<ConcertModel>();


            LoadPage = new RelayCommand(loadPage);
            NavToSecoursList = new RelayCommand(navtoSL);
        }

        private void navtoSL()
        {
            nav.NavigateTo();
        }

        private async void InitSecouriste()
        {
            var secouristeWeb = LocalQuery.GetCurrentSecouriste();
            secouriste = secouristeWeb;
        }

        public async void initList()
        {
            ConcertsListFromWeb = new ObservableCollection<ConcertModel>();
            var posteList = await Query<>.GetPoste();
            if (posteList != null)
            {
                foreach (var item in posteList)
                {
                    ConcertsListFromWeb.Add(
                        new ConcertModel()
                        {
                            Date = Convert.ToDateTime(item.Date_poste),
                            Name = item.State,
                            ID = item.Id,
                            Intitule = item.Intitule,
                            Lieu = item.Lieu,
                            HeureDebut = Convert.ToDateTime(item.Heure_debut),
                            HeureFin = Convert.ToDateTime(item.Heure_fin),
                            SecouristeList = new List<Secouriste>()
                        });
                }
                PostesName = new ObservableCollection<string>(PosteDeSecoursWeb.Select(poste => poste.Intitule).ToList());
                setPostesListe();
            }
        }

        /*private void SelectionChanged(SelectionChangedEventArgs e)
        {
            /*int index = ((ListBox)(((ListBoxItem)e.AddedItems[0]).Parent)).Items.IndexOf((ListBoxItem)e.AddedItems[0]);
            setPostesListe(index);#1#
        }*/

        private void setPostesListe()
        {
            secouriste = new Secouriste("BURRELL", "Sarah", "05.55.51.22.56", "sarah@gmail.com");
            PosteDeSecours = new ObservableCollection<PosteDeSecours>();
            PosteDeSecours = PosteDeSecoursWeb;

            MyPostes = new ObservableCollection<PosteDeSecours>();
            foreach (var pos in from pos in PosteDeSecours from sec in pos.SecouristeList where sec.Prenom == secouriste.Prenom && sec.Nom == secouriste.Nom select pos)
            {
                MyPostes.Add(pos);
            }
            //MyPostes = new ObservableCollection<PosteDeSecours>(PosteDeSecours.Where(poste => poste.SecouristeList.Contains(secouriste)).ToList());


            PostesEnAttente = new ObservableCollection<PosteDeSecours>();
            PostesEnAttente = new ObservableCollection<PosteDeSecours>(PosteDeSecours.Where(poste => poste.Etat == "En attente").ToList());
        }

        private void loadPage()
        {
            InitSecouriste();
            initList();
        }

        private void setSearchPoste(string name)
        {
            switch (SelectedIndex)
            {
                case 0:
                    ConcertsListByDate = new ObservableCollection<ConcertModel>(ConcertsListByDate.Where(poste => poste.Artist == name).ToList());
                    break;
                case 1:
                    ConcertsListByKind = new ObservableCollection<ConcertModel>(ConcertsListByKind.Where(poste => poste.Artist == name).ToList());
                    break;
                case 2:
                    ConcertsListByArtist = new ObservableCollection<ConcertModel>(ConcertsListByArtist.Where(poste => poste.Artist == name).ToList());
                    break;
            }
    }
}