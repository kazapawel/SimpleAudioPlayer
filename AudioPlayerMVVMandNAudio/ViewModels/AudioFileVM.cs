using AudioPlayerNAudio;

namespace AudioPlayerMVVMandNAudio
{
    /// <summary>
    /// View model class for audio file 
    /// </summary>
    public class AudioFileVM : BaseViewModel
    {
        #region PRIVATE MEMBERS

        /// <summary>
        /// Audio file model
        /// </summary>
        private AudioFile model;

        private bool isAudioFilePlaying;

        #endregion

        #region PUBLIC PROPERTIES

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
        /// 
        /// </summary>
        public string Path => model.Path;

        /// <summary>
        /// Total time in string format
        /// </summary>
        public string TimeTotal => model.Duration.ToString(@"hh\:mm\:ss");

        public string FileProperties => $"{model.Description} | {(model.AudioChannels>1 ? "stereo" : "mono")} | {model.AudioSampleRate} | {model.BitsPerSample}";

        /// <summary>
        /// 
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
        /// Default constructor with audio file model injected.
        /// </summary>
        /// <param name="audioFile"></param>
        public AudioFileVM(AudioFile audioFile)
        {
            model = audioFile;
        }

        /// <summary>
        /// Constructor which creates new AudioFile model based on path.
        /// </summary>
        public AudioFileVM(string path)
        {
            model = new AudioFile(path);
        }

        #endregion

        public AudioFile GetModel() => model;
        }
}
