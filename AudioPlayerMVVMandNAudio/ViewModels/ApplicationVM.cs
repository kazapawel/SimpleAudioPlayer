namespace AudioPlayerMVVMandNAudio
{
    public class ApplicationVM : BaseViewModel
    {
        #region PUBLIC PROPERTIES

        /// <summary>
        /// View model for audio player functionality
        /// </summary>
        public AudioPlayerVM AudioPlayerVM { get; set; }

        /// <summary>
        /// View model for playlist
        /// </summary>
        public PlaylistVM PlaylistVM { get; set; }

        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ApplicationVM()
        {
            PlaylistVM = new PlaylistVM();
            AudioPlayerVM = new AudioPlayerVM();

            //Subscribes AUDIOPLAYER to PlaylistVM events:
            PlaylistVM.LoadSelectedAudioFileEvent += AudioPlayerVM.OnSelectedAudioFileLoaded;
            PlaylistVM.PlaylistHasEndedEvent += AudioPlayerVM.OnPlaylistEnded;

            //Subscribes PLAYLIST to AudioPlayerVM events:
            AudioPlayerVM.AudioHasEndedEvent += PlaylistVM.OnAudioHasEnded;
        }
    }
}