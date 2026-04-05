using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RoldanWPFapp
{
    public partial class MainWindow : Window
    {
        // Collection for storing music
        ObservableCollection<Music> musicList = new ObservableCollection<Music>();

        // Track selected item
        Music selectedMusic = null;

        public MainWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = musicList;
        }

        // SAVE / UPDATE
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = txtFullname.Text;
            string song = txtAddress.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(song))
            {
                MessageBox.Show("Please fill all fields!");
                return;
            }

            if (selectedMusic == null)
            {
                // ADD NEW
                musicList.Add(new Music
                {
                    Firstname = name,
                    Address = song
                });

                MessageBox.Show("🎵 Music added!");
            }
            else
            {
                // UPDATE
                selectedMusic.Firstname = name;
                selectedMusic.Address = song;

                dataGrid.Items.Refresh();

                MessageBox.Show("✏️ Music updated!");
                selectedMusic = null;
            }

            ClearFields();
        }

        // SELECT ROW
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem is Music music)
            {
                selectedMusic = music;

                txtFullname.Text = music.Firstname;
                txtAddress.Text = music.Address;

                btnDeleteData.IsEnabled = true;
            }
            else
            {
                btnDeleteData.IsEnabled = false;
            }
        }

        // DELETE
        private void btnDeleteData_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMusic != null)
            {
                musicList.Remove(selectedMusic);
                selectedMusic = null;

                ClearFields();
                btnDeleteData.IsEnabled = false;

                MessageBox.Show("🗑 Music deleted!");
            }
            else
            {
                MessageBox.Show("Select a row first.");
            }
        }

        // CLEAR INPUT
        private void ClearFields()
        {
            txtFullname.Clear();
            txtAddress.Clear();
            dataGrid.SelectedItem = null;
        }

        // OPTIONAL (you can leave empty or add validation)
        private void txtFullname_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Optional: live validation
        }
    }

    // MODEL CLASS
    public class Music
    {
        public string Firstname { get; set; }
        public string Address { get; set; }
    }
}