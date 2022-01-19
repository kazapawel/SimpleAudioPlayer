using System;
using System.Windows;

namespace AudioPlayerMVVMandNAudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Occurs when files are drop into control.
        /// </summary>
        public EventHandler<DragEventArgs> WindowFilesDropEvent;

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //this.DataContext = new MainWindowViewModel(this);

            //Redirects drop event to playlist control so user can drop files anywhere on the application window.
            WindowFilesDropEvent += playlistControl.PlaylistListbox_Drop;
        }

        /// <summary>
        /// Close window event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Maximize window event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        /// <summary>
        /// Minimize window event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Minimized ? WindowState.Normal : WindowState.Minimized;
        }

        /// <summary>
        /// When files are dropped from oputside the window, windows raises en event and sends dropped data to subscribers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                WindowFilesDropEvent?.Invoke(this, e);
            }
        }
    }
}
