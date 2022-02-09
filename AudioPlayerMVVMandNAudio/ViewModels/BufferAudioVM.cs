
using AudioPlayerNAudio;
using System;

namespace AudioPlayerMVVMandNAudio
{
    public class BufferAudioVM : BaseViewModel
    {
        #region PRIVATE MEMBERS

        /// <summary>
        /// 
        /// </summary>
        private AudioFileVM audioFileVM;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Audio file which is currently loaded in buffer
        /// </summary>
        public AudioFileVM AudioFileVM
        {
            get
            {
                return audioFileVM;
            }
            set
            {
                if (audioFileVM != value)
                {
                    audioFileVM = value;
                    OnPropertyChanged(nameof(AudioFileVM));
                }
            }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Default empty constructor.
        /// </summary>
        public BufferAudioVM() { }

        #endregion

        #region METHODS

        /// <summary>
        /// Creates new instance of AudioFileVM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnAudioFileStart(object sender, FilePathEventArgs e)
        {
            AudioFileVM = new AudioFileVM(new AudioFile(e.Path));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnAudioStoppedBeforeEnd(object sender, EventArgs e)
        {
            AudioFileVM = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnPlaylistEnded(object sender, EventArgs e)
        {
            //AudioFileVM = null;
        }

        #endregion
    }
}
