namespace AudioPlayerMVVMandNAudio
{
    public class PlayState : IAudioPlayerState
    {
        private readonly AudioPlayerVM vM;

        public PlayState(AudioPlayerVM vm)
        {
            vM = vm;
        }


        public void EnterState()
        {
            //Plays audio only when there is a buffer track
            if (vM.BufferTrack != null && vM.BufferTrack.Path != null)
            {
                //Sets path of audio file in audio file player
                vM.AudioFilePlayer.Path = vM.BufferTrack.Path;

                //Plays audio
                vM.AudioFilePlayer.PlayAudio();

                //Starts timer for readonly properties
                vM.StartTimer();

                //Changes state of buffer track
                vM.BufferTrack.IsAudioFilePlaying = true;

                //Refresh readonly property
                vM.OnPropertyChanged(nameof(vM.IsPlaying));
            }
            ////Else raises an event. This situation should happend only once - after application start, when buffer is empty.
            //else
            //    vM.RaiseOnAudioHasEndedEvent();
        }

        public void PlayTrack()
        {
            vM.State = new PauseState(vM);
            vM.State.EnterState();
        }

        public void StopTrack()
        {
            vM.State = new StopState(vM);
            vM.State.EnterState();
        }
    }
}
