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

        /// <summary>
        /// 
        /// </summary>
        public object TargetItem
        {
            get { return (object)GetValue(TargetItemProperty); }
            set { SetValue(TargetItemProperty, value); }
        }

        public static readonly DependencyProperty TargetItemProperty =
            DependencyProperty.Register("TargetItem", typeof(object), typeof(PlaylistControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ));

        /// <summary>
        /// 
        /// </summary>
        public object MovedItem
        {
            get { return (object)GetValue(MovedItemProperty); }
            set { SetValue(MovedItemProperty, value); }
        }

        public static readonly DependencyProperty MovedItemProperty =
            DependencyProperty.Register("MovedItem", typeof(object), typeof(PlaylistControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> IncomingFiles
        {
            get { return (IEnumerable<string>)GetValue(IncomingFilesProperty); }
            set { SetValue(IncomingFilesProperty, value); }
        }

        public static readonly DependencyProperty IncomingFilesProperty =
            DependencyProperty.Register("IncomingFiles", typeof(IEnumerable<string>), typeof(PlaylistControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 
        /// </summary>
        public ICommand AddFilesCommand
        {
            get { return (ICommand)GetValue(AddFilesCommandProperty); }
            set { SetValue(AddFilesCommandProperty, value); }
        }
       
        public static readonly DependencyProperty AddFilesCommandProperty =
            DependencyProperty.Register("AddFilesCommand", typeof(ICommand), typeof(PlaylistControl), new PropertyMetadata(null));


        /// <summary>
        /// 
        /// </summary>
        public ICommand MoveItemCommand
        {
            get { return (ICommand)GetValue(MoveItemCommandProperty); }
            set { SetValue(MoveItemCommandProperty, value); }
        }
        
        public static readonly DependencyProperty MoveItemCommandProperty =
            DependencyProperty.Register("MoveItemCommand", typeof(ICommand), typeof(PlaylistControl), new PropertyMetadata(null));

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
            var dialog = new OpenFileDialog
            {
                //Enables selection of multiple files
                Multiselect = true,

                //Sets filter for files that can be chosen
                Filter = "Audio files |*.mp3;*.wav"
            };

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
                if (AddFilesCommand?.CanExecute(null) ?? false)
                {
                    AddFiles(GetFiles((IEnumerable<string>)e.Data.GetData(DataFormats.FileDrop)));
                }
            }
        }

        /// <summary>
        /// Executes add files command. 
        /// </summary>
        /// <param name="files"></param>
        private void AddFiles(IEnumerable<string> files)
        {
            IncomingFiles = files;
            AddFilesCommand?.Execute(null);
        }

        #endregion

        #region PLAYLIST ITEM DRAG AND DROP

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is FrameworkElement frameworkElement)
            {
                DragDrop.DoDragDrop(frameworkElement, new DataObject(DataFormats.Serializable, frameworkElement.DataContext), DragDropEffects.Move);
            }
        }

        private void Item_DragOver(object sender, DragEventArgs e)
        {
            if (MoveItemCommand?.CanExecute(null) ?? false)
            {
                if (sender is FrameworkElement element)
                {
                    TargetItem = element.DataContext;
                    MovedItem = e.Data.GetData(DataFormats.Serializable);
                }

                MoveItemCommand?.Execute(null);
            }
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
