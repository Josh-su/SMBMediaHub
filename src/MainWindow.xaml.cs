using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        // Initialize Files as an ObservableCollection
        private ObservableCollection<string> Files = [];

        public MainWindow()
        {
            InitializeComponent();
            FileListView.ItemsSource = Files; // Bind the ListView to the Files ObservableCollection
            LoadSMBFiles(); // Load the SMB files when the window is initialized
        }
        private void LoadSMBFiles()
        {
            // Example method to load files from an SMB source, replace with your actual logic
            var files = SMBFileLoader.LoadFilesFromSMB("serverIp", "username", "password", "shareName");

            Files.Clear();
            foreach (var file in files)
            {
                Files.Add(file);
            }
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
            if (FileListView.SelectedItem != null)
            {
                string selectedFile = FileListView.SelectedItem.ToString();
                PlayVideo(selectedFile);
            }
        }

        private void PlayVideo(string fileName)
        {
            // Logic to play video using VLC or MediaElement
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new(Files);
            if (settingsWindow.ShowDialog() == true)
            {
                // Refresh the list of sources after saving settings
                // For example, update the UI to reflect changes made in the Settings window.
            }
        }
    }
}