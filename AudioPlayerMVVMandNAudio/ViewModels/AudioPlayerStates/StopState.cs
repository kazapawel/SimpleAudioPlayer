namespace AudioPlayerMVVMandNAudio
{
    public class StopState : IAudioPlayerState
    {
        private readonly AudioPlayerVM vM;

        public StopState(AudioPlayerVM vm)
        {
            vM = vm;
        }

        public void EnterState()
        {
            //Stops audio
            vM.AudioFilePlayer.StopAudio();

            //Stops timer
            vM.StopTimer();

            //Sets buffer state
            if (vM.BufferTrack != null)
                vM.BufferTrack.IsAudioFilePlaying = false;

            //Refresh readonly property
            vM.OnPropertyChanged(nameof(vM.IsPlaying));
        }

        public void PlayTrack()
        {
                vM.State = new PlayState(vM);
                vM.State.EnterState();
        }

        public void StopTrack()
        {
            //Do nothing
        }
    }
}
