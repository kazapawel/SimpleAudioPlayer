namespace AudioPlayerMVVMandNAudio
{
    public class PlayState : IState
    {
        /// <summary>
        /// Audio Player Vm reference
        /// </summary>
        private AudioPlayerVM vM;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="vm"></param>
        public PlayState(AudioPlayerVM vm)
        {
            vM = vm;
        }

        /// <summary>
        /// 
        /// </summary>
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
                vM.OnPropertyChanged(nameof(vM.IsPlaying));
            }
            else
                vM.RaiseAudioHasEndedEvent();
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
