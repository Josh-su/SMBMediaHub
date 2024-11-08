using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace SMBMediaHub
{
    public partial class AddSMBConnectionWindow : Window
    {
        public string NewMediaSource { get; set; }  // Property to hold the new media source

        public AddSMBConnectionWindow()
        {
            InitializeComponent();
        }

        // Example method that is called when the user clicks the "Add" button
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate and capture the new media source details (this will depend on the UI)
            NewMediaSource = "Some SMB Connection Info"; // Replace with actual input from the user

            // Close the window with a DialogResult of true
            this.DialogResult = true;
            this.Close();
        }
    }
}
