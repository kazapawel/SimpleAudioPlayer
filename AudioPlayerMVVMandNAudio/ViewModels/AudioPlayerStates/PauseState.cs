
namespace AudioPlayerMVVMandNAudio
{
    public class PauseState : IState
    {
        private AudioPlayerVM vM;

        public PauseState(AudioPlayerVM vm)
        {
            vM = vm;
        }

        public void EnterState()
        {
            //Stops timer
            //timer?.Stop();
            vM.StopTimer();
            //Pauses audio
            vM.AudioEnginePause();

            //Change pause state property 
            vM.OnPropertyChanged(nameof(vM.IsPlaying));
        }
        public void Play()
        {
            vM.State = new PlayState(vM);
            vM.State.EnterState();
        }
        public void Stop()
        {
            vM.State = new StopState(vM);
            vM.State.EnterState();
        }
        public void Pause()
        {
            //Do nothing
        }
    }
}