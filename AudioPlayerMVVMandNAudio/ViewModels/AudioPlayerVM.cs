using AudioPlayerNAudio;
using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace AudioPlayerMVVMandNAudio
{
    /// <summary>
    /// View model class for audio player and transport control.
    /// </summary>
    public class AudioPlayerVM : BaseViewModel
    {
        #region PRIVATE MEMBERS

        /// <summary>
        /// Audio player model
        /// </summary>
        private IAudioFilePlayer<float> audioFilePlayer;

        /// <summary>
        /// Timer for updating data from model.
        /// </summary>
        private DispatcherTimer timer { get; set; }

        /// <summary>
        /// Returns true if audio is muted.
        /// </summary>
        private bool muted;

        /// <summary>
        /// Buffer volume used when audio is muted.
        /// </summary>
        private double storedVolume;

        internal IAudioPlayerState State;

        private AudioFileVM bufferTrack;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Audio player buffer - track which is currently loaded.
        /// </summary>
        public AudioFileVM BufferTrack
        {
            get => bufferTrack;
            set
            {
                if(bufferTrack!=value)
                {
                    bufferTrack = value;
                    OnPropertyChanged(nameof(bufferTrack));
                }
            }
        }

        /// <summary>
        /// Volume of bufferd/playing audio file.
        /// </summary>
        public double Volume
        {
            get => storedVolume;
            set
            {
                //Sets buffer volume
                storedVolume = value;

                //Sets audio file player volume if there is one, based on mute state
                SetAudioPlayerVolume();
            }

        }

        /// <summary>
        /// If true audio file volume is 0.
        /// </summary>
        public bool Muted
        {
            get
            {
                return muted;
            }
            set
            {
                muted = value;
                SetAudioPlayerVolume();
            }
        }

        /// <summary>
        /// Returns true if audio is playing. This is a flag to swap between play and pause image in gui.
        /// </summary>
        public bool IsPlaying => State is PlayState;

        /// <summary>
        /// 
        /// </summary>
        public List<string[]> ErrorLog { get; set; }

        /// <summary>
        /// Audio time position
        /// </summary>
        public double Position
        {
            get => audioFilePlayer != null ? (double)(audioFilePlayer.StreamPosition)/(audioFilePlayer.StreamLength/100) : 0;
            set
            {
                if(audioFilePlayer != null)
                {
                    audioFilePlayer.StreamPosition = (long)value * (audioFilePlayer.StreamLength / 100);
                    OnPropertyChanged(nameof(TimeCurrent));
                }
                    
            }
        }

        /// <summary>
        /// String representation of current time of audio which is playing.
        /// </summary>
        public string TimeCurrent => audioFilePlayer != null ? audioFilePlayer.TimeCurrent.ToString(@"h\:mm\:ss") : " --:-- ";

        /// <summary>
        /// String representation of total time of audio which is playing
        /// </summary>
        public string TimeTotal => audioFilePlayer != null ? audioFilePlayer.TimeTotal.ToString(@"h\:mm\:ss") : " --:-- ";

        #endregion

        #region EVENTS

        public event EventHandler AudioHasEndedEvent;

        #endregion

        #region COMMANDS

        public RelayCommand PlayPauseAudioCommand { get; set; }

        public RelayCommand StopAudioCommand { get; set; }


        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AudioPlayerVM()
        {
            //Commands:
            PlayPauseAudioCommand = new RelayCommand(PlayPauseAudio);
            StopAudioCommand = new RelayCommand(StopAudio);

            //Sets start volume
            Volume = 50;

            //Inits errog log list
            ErrorLog = new List<string[]>();

            //Timer
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);

            //State
            State = new StopState(this);
        }

        #endregion

        #region METHODS

        #region PLAYBACK METHODS  if/else approach
        /*
        /// <summary>
        /// Toggles between play and pause.
        /// </summary>
        /// <param name="o"></param>
        private void PlayPauseAudio(object o)
        {
            //Works only when there is a track in buffer
            if (BufferTrack != null && BufferTrack.Path != null)
            {
                //PAUSE
                if (IsPlaying)
                {
                    //Stops timer
                    timer?.Stop();

                    //Pauses audio
                    AudioEnginePause();

                    //Change pause state property 
                    IsPlaying = false;
                }
                //PLAY
                else
                {
                    //Plays audio
                    AudioEnginePlay();

                    //Timer
                    StartTimer();

                    //Sets buffer track state
                    BufferTrack.IsAudioFilePlaying = true;

                    //Sets this player state
                    IsPlaying = true;
                }
            }
        }

        /// <summary>
        /// Stops current audio playback before audio reaching it's end.
        /// </summary>
        /// <param name="o"></param>
        private void StopAudio(object o)
        {
            //Sets buffer track state
            if (BufferTrack != null)
                BufferTrack.IsAudioFilePlaying = false;

            AudioEngineStop();

            StopTimer();

            //Sets this player state
            IsPlaying = false;
        }
        */
        #endregion

        #region STATE METHODS state machine approach        

        private void PlayPauseAudio(object o)
        {
            State.PlayTrack();
        }

        private void StopAudio(object o)
        {
            State.StopTrack();
        }
        
        #endregion

        #region AUDIO ENGINE METHODS
        internal void AudioEnginePlay()
        {
            //If audio is paused - resumes audio
            if (audioFilePlayer != null)
                audioFilePlayer.ResumeAudio();

            //If audio is not paused: 
            else
            {
                //creates new audioplayer model to prevent from playing multiple files at the same time
                try
                {
                    //Creates new instance of audio file player model
                    audioFilePlayer = new AudioFilePlayerNAudio(BufferTrack.Path);
                }
                catch (Exception e)
                {
                    //Logs exception info
                    ErrorLog.Add(new string[] { e.ToString(), e.Message });

                    //Resets audioFilePlayer
                    audioFilePlayer = null;

                    return;
                }

                //Sets audioplayer model volume based on user volume settings(ex: volume fader)
                SetAudioPlayerVolume();

                //Subscribes to model event which informs about audio reaching it's end.
                audioFilePlayer.AudioHasEndedEvent += OnAudioHasEnded;

                //Plays audio
                audioFilePlayer.PlayAudio();
            }
        }
        internal void AudioEngineStop()
        {
            //If audio is playing or paused
            if (audioFilePlayer != null)
            {
                //Stops audio file player
                audioFilePlayer?.StopAudio();

                //Clears model
                audioFilePlayer = null;
            }
        }
        internal void AudioEnginePause()
        {
            //Pause audio
            audioFilePlayer?.PauseAudio();
        }

        /// <summary>
        /// If there is a audio player, sets it's volume.
        /// </summary>
        private void SetAudioPlayerVolume()
        {
            if (audioFilePlayer != null)
            {
                var volume = (float)(storedVolume / 100);
                audioFilePlayer.Volume = Muted ? 0 : volume;
            }
        }

        /// <summary>
        /// If audio file has ended, clears model.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAudioHasEnded(object sender, EventArgs e)
        {
            //Clears audio player
            audioFilePlayer = null;

            //Sets this player state
            OnPropertyChanged(nameof(IsPlaying));

            //Notify subscribers about playback end
            AudioHasEndedEvent?.Invoke(this, null);
        }

        public void RaiseOnAudioHasEndedEvent() => AudioHasEndedEvent?.Invoke(this, null);

        #endregion

        #region TIMER METHODS

        /// <summary>
        /// 
        /// </summary>
        internal void StartTimer()
        {
            //Creates new dispatcher timer for updating time values from model
            

            //Starts timer
            timer.Start();

            //Updates total time on startup
            OnPropertyChanged(nameof(TimeTotal));

            //Updates acutal time on startup - shows 00:00;
            OnPropertyChanged(nameof(TimeCurrent));
        }

        /// <summary>
        /// 
        /// </summary>
        internal void StopTimer()
        {
            //Stops timer
            timer?.Stop();
            //timer = null;

            //Updates current time
            OnPropertyChanged(nameof(TimeCurrent));

            //Updates total time
            OnPropertyChanged(nameof(TimeTotal));

            //Updates audio position
            OnPropertyChanged(nameof(Position));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Timer_Tick(object sender, EventArgs e)
        {
            //Updates current time
            OnPropertyChanged(nameof(TimeCurrent));

            //Updates position
            OnPropertyChanged(nameof(Position));
        }

        #endregion

        #region SUBSCRIPTIONS METHODS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnSelectedAudioFileLoaded(object sender, AudioFileVMEventArgs e)
        {
            //Stops audio before changing track
            StopAudio(null);

            //Sets new buffer track reference
            BufferTrack = e.AudioFileVM;

            //Plays new track
            PlayPauseAudio(null);
        }

        public void OnPlaylistEnded(object sender, EventArgs e)
        {
            StopAudio(null);
        }

        #endregion

        #endregion
    }
}
