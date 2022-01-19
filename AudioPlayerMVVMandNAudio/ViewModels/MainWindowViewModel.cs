using System.Windows;

namespace AudioPlayerMVVMandNAudio
{
    public class MainWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// Model of the view ;)
        /// </summary>
        private Window window;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="win"></param>
        public MainWindowViewModel(Window win)
        {
            window = win;
        }
    }
}
