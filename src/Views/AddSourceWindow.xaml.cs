using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace SMBMediaHub
{
    public partial class AddSourceWindow : Window
    {
        public AddSourceWindow()
        {
            InitializeComponent();
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SourceType_Changed(object sender, RoutedEventArgs e)
        {
            // Check if the Local Source radio button is selected
            if (LocalSourceRadioButton.IsChecked == true)
            {
                // Show the Local Source input panel and hide the SMB Source panel
                LocalSourcePanel.Visibility = Visibility.Visible;
                SMBSourcePanel.Visibility = Visibility.Collapsed;
            }
            else if (SMBSourceRadioButton.IsChecked == true)
            {
                // Show the SMB Source input panel and hide the Local Source panel
                LocalSourcePanel.Visibility = Visibility.Collapsed;
                SMBSourcePanel.Visibility = Visibility.Visible;
            }
        }
    }
}
