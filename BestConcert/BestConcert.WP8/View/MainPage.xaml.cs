using System.ComponentModel;
using System.Windows;
using BestConcert.WP8.ViewModel.ViewModel;
using Microsoft.Phone.Controls;

namespace BestConcert.WP8.View
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.autoCompleteBox1.Text = "";
            (DataContext as MainViewModel).initList();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit?",
                                    MessageBoxButton.OKCancel) != MessageBoxResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}