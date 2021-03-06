using System;
using System.Windows;
using System.Windows.Input;

namespace AudioPlayerMVVMandNAudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region KEYBOARD KEYS EVENTS

        public EventHandler SpacebarPressedEvent;
        public EventHandler EnterPressedEvent;
        public EventHandler DownArrowPressedEvent;
        public EventHandler UpArrowPressedEvent;
        public EventHandler LeftArrowPressedEvent;
        public EventHandler RightArrowPressedEvent;
        public EventHandler DeletePressedEvent;

        #endregion

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

            // This windows takes all keys events
            this.PreviewKeyDown += MainWindow_KeyDown;

            // Redirects drop event to playlist control so user can drop files anywhere on the application window.
            WindowFilesDropEvent += playlistControl.PlaylistListbox_Drop;

            // When window is closed playlist is saved to file
            Closing += playlistControl.OnWindowClosing;

            // Subscribes other controls to keyboard keys events
            UpArrowPressedEvent += playlistControl.OnUpArrowPressed;
            DownArrowPressedEvent += playlistControl.OnDownArrowPressed;
            EnterPressedEvent += playlistControl.OnEnterPressed;
            LeftArrowPressedEvent += volumeSliderControl.OnLeftArrowPressed;
            RightArrowPressedEvent += volumeSliderControl.OnRightArrowPressed;
            DeletePressedEvent += playlistControl.OnDeletePressed;
        }

        #region WINDOW OPERATIONS 

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

        #endregion

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space: this.playButton.Command?.Execute(null);break;
                case Key.Enter: EnterPressedEvent?.Invoke(this, null); break;
                case Key.Up: UpArrowPressedEvent?.Invoke(this, null); break;
                case Key.Down: DownArrowPressedEvent?.Invoke(this, null); break;
                case Key.Left: LeftArrowPressedEvent?.Invoke(this, null);break;
                case Key.Right: RightArrowPressedEvent?.Invoke(this, null); break;
                case Key.Delete: DeletePressedEvent?.Invoke(this, null); break;
                default: return;
            }
        }
    }
}
