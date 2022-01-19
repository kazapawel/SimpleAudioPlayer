using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using System.Linq;

namespace AudioPlayerMVVMandNAudio
{
    /// <summary>
    /// Interaction logic for PlaylistControl.xaml
    /// </summary>
    public partial class PlaylistControl : System.Windows.Controls.UserControl
    {
        #region DEPENDENCY PROPERTIES

        public IEnumerable<string> IncomingFiles
        {
            get { return (IEnumerable<string>)GetValue(IncomingFilesProperty); }
            set { SetValue(IncomingFilesProperty, value); }
        }

        public static readonly DependencyProperty IncomingFilesProperty =
            DependencyProperty.Register("IncomingFiles", typeof(IEnumerable<string>), typeof(PlaylistControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public ICommand DropFilesCommand
        {
            get { return (ICommand)GetValue(DropFilesCommandProperty); }
            set { SetValue(DropFilesCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DropFilesCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropFilesCommandProperty =
            DependencyProperty.Register("DropFilesCommand", typeof(ICommand), typeof(PlaylistControl), new PropertyMetadata(null));




        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PlaylistControl()
        {
            InitializeComponent();
        }

        #endregion

        #region ADD FILES TO PLAYLIST / DRAG AND DROP FILES INTO PLAYLIST

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
                AddFiles(dialog.FileNames);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PlaylistListbox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (DropFilesCommand?.CanExecute(null) ?? false)
                {
                    IncomingFiles = GetFiles((IEnumerable<string>)e.Data.GetData(DataFormats.FileDrop));
                    DropFilesCommand?.Execute(null);
                }
            }
        }

        /// <summary>
        /// Sends paths to view model.
        /// </summary>
        /// <param name="files"></param>
        private void AddFiles(IEnumerable<string> files)
        {
            //var playlist = this.DataContext as PlaylistVM;
            //playlist.AddTracksToPlaylist(files);
        }

        #endregion

        #region PRIVATE HELPER METHODS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IEnumerable<string> GetFiles(IEnumerable<string> data)
        {
            //Collections which is going to be returned
            var files = new List<string>();

            //Adds files to collection - first files in root directory then files in subdirectories
            foreach (var path in data.OrderBy(x => IsDirectory(x)))
            {
                //If path is directory
                if (IsDirectory(path))
                {
                    //Gets all items from this directory
                    foreach (var file in GetFiles(Directory.GetFileSystemEntries(path)))
                        files.Add(file);
                }

                //Adds file to collection
                else
                    files.Add(path);
            }

            return files;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool IsDirectory(string path)
        {
            var file = new FileInfo(path);

            return file.Attributes.HasFlag(FileAttributes.Directory);
        }

        #endregion
    }
}
