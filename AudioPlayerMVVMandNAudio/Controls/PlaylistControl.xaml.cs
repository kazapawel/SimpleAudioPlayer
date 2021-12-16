using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
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
                SendFilesToViewModel(GetFiles((IEnumerable<string>)e.Data.GetData(DataFormats.FileDrop)));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IEnumerable<string> GetFiles(IEnumerable<string> data)
        {
            //Collections which is going to be returned
            var files = new List<string>();

            //Checks if path is directory or file
            foreach(var path in data)
            {
                //If path is directory
                if (IsDirectory(path))
                {
                    //Gets all items from this directory
                    foreach ( var file in GetFiles(Directory.GetFileSystemEntries(path)))
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
        /// <param name="directory"></param>
        /// <returns></returns>
        private IEnumerable<string> GetFilesFromDirectory(string directory)
        {
            return null;
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

        /// <summary>
        /// Sends paths to view model.
        /// </summary>
        /// <param name="files"></param>
        private void SendFilesToViewModel(IEnumerable<string> files)
        {
            var playlist = this.DataContext as PlaylistVM;
            playlist.AddTracksToPlaylist(files);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnFilesDragOutsidePlaylist(object sender, DropFilesEventArgs e)
        {
            SendFilesToViewModel(GetFiles( (string[])e.Files ));
        }
    }
}
