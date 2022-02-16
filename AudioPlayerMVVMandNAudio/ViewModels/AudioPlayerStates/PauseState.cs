namespace AudioPlayerMVVMandNAudio
{
    public class PauseState : AudioPlayerState
    {
        public PauseState(AudioPlayerVM viewModel) : base(viewModel) { }
        public override void EnterState()
        {
            //Pauses audio
            ViewModel.AudioFilePlayer.PauseAudio();

            //Stops timer
            ViewModel.StopTimer();

            //Change pause state property 
            ViewModel.OnPropertyChanged(nameof(ViewModel.IsPlaying));
        }
        public override void PlayTrack()
        {
            ViewModel.State = new PlayState(ViewModel);
            ViewModel.State.EnterState();
        }
        public override void StopTrack()
        {
            ViewModel.State = new StopState(ViewModel);
            ViewModel.State.EnterState();
        }
        public override void OnAudioHasEnded()
        {
            //to do
            throw new System.NotImplementedException();
        }
    }
}