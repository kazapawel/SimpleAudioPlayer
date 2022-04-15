using AudioPlayerNAudio;

namespace AudioPlayerMVVMandNAudio
{
    /// <summary>
    /// View model class for audio file 
    /// </summary>
    public class AudioFileVM : BaseViewModel
    {
        #region PRIVATE MEMBERS

        private AudioFile model;
        private bool isAudioFilePlaying;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Get's track's corruption state.
        /// </summary>
        public bool IsCorrupted => model.IsCorrupted;

        /// <summary>
        /// Get's track path.
        /// </summary>
        public string Path => model.PathOfFile;

        /// <summary>
        /// Gets track's title.
        /// </summary>
        public string TrackTitle => model.Title;

        /// <summary>
        /// Gets track's artist.
        /// </summary>
        public string Artist => model.Artist;

        /// <summary>
        /// Gets album that track is from.
        /// </summary>
        public string Album => model.Album;

        /// <summary>
        /// Total time in string format.
        /// </summary>
        public string TimeTotal => $"{model.Duration.Hours * 60 + model.Duration.Minutes}:{model.Duration.Seconds :D2}";

        public string FileProperties => $"{model.Description} | {(model.AudioChannels>1 ? "stereo" : "mono")} | {model.AudioSampleRate} Hz | {model.BitsPerSample}";

        /// <summary>
        /// Flag for icon display
        /// </summary>
        public bool IsAudioFilePlaying
        {
            get => isAudioFilePlaying;
            set
            {
                if(isAudioFilePlaying!=value)
                {
                    isAudioFilePlaying = value;
                    OnPropertyChanged(nameof(IsAudioFilePlaying));
                }
            }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructor which creates new AudioFile model based on path.
        /// </summary>
        public AudioFileVM(string path)
        {
            model = new AudioFile(path);
        }

        #endregion

    }
}
