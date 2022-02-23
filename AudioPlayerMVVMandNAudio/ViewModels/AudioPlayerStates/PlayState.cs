namespace AudioPlayerMVVMandNAudio
{
    public class PlayState : AudioPlayerState
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        public PlayState(AudioPlayerVM viewModel) : base(viewModel) { }

        /// <summary>
        /// 
        /// </summary>
        public override void EnterState()
        {
            //Plays audio only when there is a buffer track
            if (ViewModel.BufferTrack != null && ViewModel.BufferTrack.Path != null)
            {
                //Sets path of audio file in audio file player
                ViewModel.AudioFilePlayer.Path = ViewModel.BufferTrack.Path;

                //Plays audio
                ViewModel.AudioFilePlayer.PlayAudio();

                //Starts timer for readonly properties
                ViewModel.StartTimer();

                //Changes state of buffer track
                ViewModel.BufferTrack.IsAudioFilePlaying = true;

                //Refresh readonly property
                ViewModel.OnPropertyChanged(nameof(ViewModel.IsPlaying));
            }
            //Else rreturns to stop state without invoking enter state method
            else
            {
                ViewModel.State = new StopState(ViewModel);
                ViewModel.RequestTrack();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void PlayTrack()
        {
            ViewModel.State = new PauseState(ViewModel);
            ViewModel.State.EnterState();
        }
        public override void StopTrack()
        {
            ViewModel.State = new StopState(ViewModel);
            ViewModel.State.EnterState();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnAudioHasEnded()
        {
            //Updates flag
            ViewModel.BufferTrack.IsAudioFilePlaying = false;

            //Stops playback without invoking enter state method
            ViewModel.State = new StopState(ViewModel);

            //Refresh readonly property
            ViewModel.OnPropertyChanged(nameof(ViewModel.IsPlaying));
        }
    }
}
