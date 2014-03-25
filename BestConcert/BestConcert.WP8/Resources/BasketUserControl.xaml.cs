using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using BestConcert.WP8.Model;

namespace BestConcert.WP8.Resources
{
    public partial class BasketUserControl : UserControl
    {
        public static readonly DependencyProperty ListSourceProperty =
            DependencyProperty.Register("ListSource", typeof(string), typeof(BasketUserControl), new PropertyMetadata(default(ObservableCollection<Order>)));

        public ObservableCollection<Order> ListSource
        {
            get { return (ObservableCollection<Order>)GetValue(ListSourceProperty); }
            set { SetValue(ListSourceProperty, value); }
        }

        public event EventHandler CloseClick;

        public BasketUserControl()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
            CloseButton.Click += CloseButtonOnClick;
        }

        private void CloseButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var eventHandler = this.CloseClick;

            if (eventHandler != null)
            {
                eventHandler(this, routedEventArgs);
            }   
        }
    }
}
