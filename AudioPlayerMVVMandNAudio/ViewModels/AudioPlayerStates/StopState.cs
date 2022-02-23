namespace AudioPlayerMVVMandNAudio
{
    public class StopState : AudioPlayerState
    {
        public StopState(AudioPlayerVM viewModel) : base(viewModel) { }
        public override void EnterState()
        {
            //Stops audio
            ViewModel.AudioFilePlayer.StopAudio();

            //Stops timer
            ViewModel.StopTimer();

            //Sets buffer state
            if (ViewModel.BufferTrack != null)
                ViewModel.BufferTrack.IsAudioFilePlaying = false;


            //Refresh readonly property
            ViewModel.OnPropertyChanged(nameof(ViewModel.IsPlaying));
        }
        public override void PlayTrack()
        {
            ViewModel.State = new PlayState(ViewModel);
            ViewModel.State.EnterState();
        }
        public override void StopTrack()
        {
            //Do nothing
        }
        public override void OnAudioHasEnded()
        {
            //do nothing
        }
    }
}
