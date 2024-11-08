using Newtonsoft.Json;
using SMBMediaHub.JsonClass;
using System.IO;
using System.Windows;

namespace SMBMediaHub
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the SettingsWindow
            AddSourceWindow addSourceWindow = new()
            {
                // Set the owner of the SettingsWindow to the current MainWindow, so it opens centered
                Owner = this,

                // Set the window to open in the center of the MainWindow
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            // Open the settings window as a modal dialog
            addSourceWindow.ShowDialog();
        }

        // Event handler for the Remove button
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MediaSourceListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void SettingsWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Save the media source list to json

            // Close the settings window after saving
            this.DialogResult = true;
            this.Close();
        }
    }
}
