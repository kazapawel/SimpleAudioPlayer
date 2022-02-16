namespace AudioPlayerMVVMandNAudio
{
    /// <summary>
    /// Base abstract class for playlist states.
    /// </summary>
    public abstract class PlaylistState
    {
        /// <summary>
        /// Reference to PlaylistVM
        /// </summary>
        protected PlaylistVM ViewModel { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="viewModel"></param>
        public PlaylistState(PlaylistVM viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
