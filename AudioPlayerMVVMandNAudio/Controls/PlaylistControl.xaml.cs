using System.Windows;
using Microsoft.Win32;

namespace AudioPlayerMVVMandNAudio
{
    /// <summary>
    /// Interaction logic for PlaylistControl.xaml
    /// </summary>
    public partial class PlaylistControl : System.Windows.Controls.UserControl
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PlaylistControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens new file dialog for files selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            //Enables selection of multiple files
            dialog.Multiselect = true;

            //Sets filter for files that can be chosen
            dialog.Filter = "Audio files |*.mp3;*.wav";

            //If user selects files and presses OK
            if (dialog.ShowDialog() == true)
            {
                //Gets names of all selected files
                SendFilesToViewModel(dialog.FileNames);
            }

        }

        /// <summary>
        /// Adds trakcs to playlist by draging file from computer and dropping them on playlist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaylistListbox_Drop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //Gets string paths of dropped files
                SendFilesToViewModel((string[])e.Data.GetData(DataFormats.FileDrop));
            }
        }

        /// <summary>
        /// Sends paths to view model.
        /// </summary>
        /// <param name="files"></param>
        private void SendFilesToViewModel(string[] files)
        {
            var playlist = this.DataContext as PlaylistVM;
            playlist.AddTracksToPlaylist(files);
        }
    }
}
