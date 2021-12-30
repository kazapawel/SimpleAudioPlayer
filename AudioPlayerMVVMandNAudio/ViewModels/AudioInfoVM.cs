
using System;

namespace AudioPlayerMVVMandNAudio
{
    public class AudioInfoVM : BaseViewModel
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
        public AudioInfoVM() { }

        #endregion

        #region METHODS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnAudioFileLoaded(object sender, AudioFileVMEventArgs e)
        {
            //AudioFileInfoVM = new AudioFileInfoVM(e.AudioFileVM);
            if (AudioFileVM is not null)
                AudioFileVM.IsAudioFilePlaying = false;

            AudioFileVM = e.AudioFileVM;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnAudioStoppedBeforeEnd(object sender, EventArgs e)
        {
            if (AudioFileVM != null)
                AudioFileVM.IsAudioFilePlaying = false;

            AudioFileVM = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnPlaylistEnded(object sender, EventArgs e)
        {
            AudioFileVM = null;
        }

        #endregion
    }
}
