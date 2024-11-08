using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace SMBMediaHub
{
    public partial class SettingsWindow : Window
    {
        // ObservableCollection to store the media sources
        private ObservableCollection<string> MediaSources;

        public SettingsWindow(ObservableCollection<string> mediaSources)
        {
            InitializeComponent();
            MediaSources = mediaSources;
            MediaSourceListView.ItemsSource = MediaSources;

            // Load media sources from file on window load
            LoadMediaSources();
        }

        // Save the list of media sources to a file
        private void SaveMediaSources()
        {
            string filePath = "mediaSources.json"; // Define file path

            // Serialize the ObservableCollection to JSON
            string json = JsonConvert.SerializeObject(MediaSources);

            // Write the JSON string to a file
            File.WriteAllText(filePath, json);
        }

        // Remove a media source from storage
        private void RemoveFromStorage(string item)
        {
            string filePath = "mediaSources.json"; // Define file path

            // Remove the item from the ObservableCollection
            MediaSources.Remove(item);

            // Serialize the updated list to JSON
            string json = JsonConvert.SerializeObject(MediaSources);

            // Write the updated JSON back to the file
            File.WriteAllText(filePath, json);
        }

        // Load the list of media sources from the file
        private void LoadMediaSources()
        {
            string filePath = "mediaSources.json"; // Define file path

            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Read the JSON string from the file
                string json = File.ReadAllText(filePath);

                // Deserialize the JSON string to an ObservableCollection
                ObservableCollection<string> loadedSources = JsonConvert.DeserializeObject<ObservableCollection<string>>(json);

                // If the list is not null, assign it to MediaSources
                if (loadedSources != null)
                {
                    MediaSources = loadedSources;
                    MediaSourceListView.ItemsSource = MediaSources; // Update the ListView
                }
            }
            else
            {
                // If the file doesn't exist, initialize with an empty list
                MediaSources = [];
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new instance of the AddSMBConnectionWindow
            AddSMBConnectionWindow addWindow = new AddSMBConnectionWindow();

            // Show it as a modal dialog and check if the dialog result is true (i.e., a new source was added)
            if (addWindow.ShowDialog() == true)
            {
                // Get the new media source (assuming the AddSMBConnectionWindow has a property or method to get the new source)
                string newMediaSource = addWindow.NewMediaSource;

                // Add the new media source to the ObservableCollection
                MediaSources.Add(newMediaSource);

                // Save the updated list to the file
                SaveMediaSources();

                // Optionally, refresh the ListView (ObservableCollection automatically updates the ListView)
                MediaSourceListView.ItemsSource = null; // Reset the ListView's item source
                MediaSourceListView.ItemsSource = MediaSources; // Set the updated source list
            }
        }

        // Event handler for the Remove button
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (MediaSourceListView.SelectedItem != null)
            {
                var selectedItem = MediaSourceListView.SelectedItem.ToString();
                MediaSources.Remove(selectedItem);
                RemoveFromStorage(selectedItem); // Remove the source from storage

                // Refresh the list view (optional, as ObservableCollection does this automatically)
                MediaSourceListView.ItemsSource = null;
                MediaSourceListView.ItemsSource = MediaSources;
            }
            else
            {
                MessageBox.Show("Please select a media source to remove.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Event handler for the Save button
        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Save the media source list to persistent storage
            SaveMediaSources();

            // Close the settings window after saving
            this.DialogResult = true;
            this.Close();
        }
    }
}
