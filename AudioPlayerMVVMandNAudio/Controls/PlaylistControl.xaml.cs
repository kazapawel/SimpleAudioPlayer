using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace AudioPlayerMVVMandNAudio
{
    /// <summary>
    /// Interaction logic for PlaylistControl.xaml
    /// </summary>
    public partial class PlaylistControl : System.Windows.Controls.UserControl
    {
        public PlaylistControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens new file dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.Multiselect = true;

            dialog.Filter = "Audio files |*.mp3;*.wav";

            //If user selects file and presses OK
            if (dialog.ShowDialog() == true)
            {
                var names = dialog.FileNames;
                var playlistVM = this.DataContext as PlaylistVM;
                foreach(var name in names)
                playlistVM.AddTrackToPlaylist(name);
            }

        }
    }
}
