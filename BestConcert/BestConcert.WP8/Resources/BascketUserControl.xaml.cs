using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using BestConcert.WP8.Model;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace BestConcert.WP8
{
    public partial class BascketUserControl : UserControl
    {
        public static readonly DependencyProperty ListSourceProperty =
            DependencyProperty.Register("ListSource", typeof(string), typeof(BascketUserControl), new PropertyMetadata(default(ObservableCollection<Order>)));

        public ObservableCollection<Order> ListSource
        {
            get { return (ObservableCollection<Order>)GetValue(ListSourceProperty); }
            set { SetValue(ListSourceProperty, value); }
        }

        public event EventHandler CloseClick;

        public BascketUserControl()
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
