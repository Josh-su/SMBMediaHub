using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Newtonsoft.Json;
using SMBLibrary;
using SMBLibrary.Client;

namespace SMBMediaHub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchBar_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBar.Text == "Search for videos...")
            {
                SearchBar.Text = "";
                SearchBar.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void SearchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBar.Text))
            {
                SearchBar.Text = "Search for videos...";
                SearchBar.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string searchText = SearchBar.Text.ToLower();
            //var filteredFiles = Files.Where(f => f.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)).ToList();
            //FileListView.ItemsSource = filteredFiles;
        }

        private void FileListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // You can add any additional logic to handle selection change if needed
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void GitHubButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the GitHub page in the default browser
            string url = "https://github.com/Josh-su/SMBMediaHub";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the SettingsWindow
            SettingsWindow settingsWindow = new()
            {
                // Set the owner of the SettingsWindow to the current MainWindow, so it opens centered
                Owner = this,

                // Set the window to open in the center of the MainWindow
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            // Open the settings window as a modal dialog
            settingsWindow.ShowDialog();
        }
    }
}