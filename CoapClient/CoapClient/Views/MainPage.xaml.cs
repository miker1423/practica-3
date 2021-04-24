using System;

using CoapClient.ViewModels;

using Windows.UI.Xaml.Controls;

namespace CoapClient.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel
        {
            get { return ViewModelLocator.Current.MainViewModel; }
        }

        public MainPage()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox box && !byte.TryParse(box.Text, out _))
                box.Text = string.Empty;
            else return;
        }
    }
}
