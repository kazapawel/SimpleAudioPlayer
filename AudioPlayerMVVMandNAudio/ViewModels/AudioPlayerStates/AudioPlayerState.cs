namespace AudioPlayerMVVMandNAudio
{
    /// <summary>
    /// Base abstract class for audio player states.
    /// </summary>
    public abstract class AudioPlayerState
    {
        /// <summary>
        /// Reference to AudioPlayerVM
        /// </summary>
        protected AudioPlayerVM ViewModel { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="viewModel"></param>
        public AudioPlayerState(AudioPlayerVM viewModel)
        {
            ViewModel = viewModel;
        }

        public abstract void EnterState();
        public abstract void PlayTrack();
        public abstract void StopTrack();
        public abstract void OnAudioHasEnded();
    }
}
