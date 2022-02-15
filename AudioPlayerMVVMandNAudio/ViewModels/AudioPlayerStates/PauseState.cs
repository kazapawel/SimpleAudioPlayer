namespace AudioPlayerMVVMandNAudio
{
    public class PauseState : IAudioPlayerState
    {
        private readonly AudioPlayerVM vM;

        public PauseState(AudioPlayerVM vm)
        {
            vM = vm;
        }

        public void EnterState()
        {
            //Pauses audio
            vM.AudioFilePlayer.PauseAudio();

            //Stops timer
            vM.StopTimer();

            //Change pause state property 
            vM.OnPropertyChanged(nameof(vM.IsPlaying));
        }
        public void PlayTrack()
        {
            vM.State = new PlayState(vM);
            vM.State.EnterState();
        }
        public void StopTrack()
        {
            vM.State = new StopState(vM);
            vM.State.EnterState();
        }
    }
}