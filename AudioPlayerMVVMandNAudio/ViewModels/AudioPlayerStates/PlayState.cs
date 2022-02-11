
namespace AudioPlayerMVVMandNAudio
{
    public class PlayState : IState
    {
        private AudioPlayerVM vM;

        public PlayState(AudioPlayerVM vm)
        {
            vM = vm;
        }

        public void EnterState()
        {
            if (vM.BufferTrack != null && vM.BufferTrack.Path != null)
            {
                //Plays audio
                vM.AudioEnginePlay();

                //Timer
                vM.StartTimer();

                //Changes state of buffer track
                vM.BufferTrack.IsAudioFilePlaying = true;

                //Changes player's state flag
                vM.IsPlaying = true;
            }
        }
        public void Play()
        {
            vM.State = new PauseState(vM);
            vM.State.EnterState();
        }

        public void Stop()
        {
            vM.State = new StopState(vM);
            vM.State.EnterState();
        }

        public void Pause()
        {
            //DoNothing
        }
    }
}
