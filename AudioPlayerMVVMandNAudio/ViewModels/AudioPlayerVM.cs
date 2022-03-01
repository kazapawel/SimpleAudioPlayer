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

        internal AudioPlayerState State;

        private AudioFileVM bufferTrack;

        public IAudioFilePlayer AudioFilePlayer { get; set; }

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
                if(AudioFilePlayer !=null)
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
                if(AudioFilePlayer != null)
                    SetAudioPlayerVolume();
            }
        }

        /// <summary>
        /// Returns true if audio is playing. This is a flag to swap between play and pause image in gui.
        /// </summary>
        public bool IsPlaying => State is PlayState;

        /// <summary>
        /// Audio time position
        /// </summary>
        public double Position
        {
            get => AudioFilePlayer.IsReady ? (double) (AudioFilePlayer.StreamPosition)/(AudioFilePlayer.StreamLength/100) : 0;
            set
            {
                if(AudioFilePlayer.IsReady)
                {
                    AudioFilePlayer.StreamPosition = (long) value * (AudioFilePlayer.StreamLength / 100);
                    OnPropertyChanged(nameof(TimeCurrent));
                }
            }
        }

        /// <summary>
        /// String representation of current time of audio which is playing.
        /// </summary>
        public string TimeCurrent => AudioFilePlayer.IsReady? AudioFilePlayer.TimeCurrent.ToString(@"h\:mm\:ss") : " --:-- ";

        /// <summary>
        /// String representation of total time of audio which is playing
        /// </summary>
        public string TimeTotal => AudioFilePlayer.IsReady? AudioFilePlayer.TimeTotal.ToString(@"h\:mm\:ss") : " --:-- ";

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

            //Audio player model
            AudioFilePlayer = new AudioFilePlayerNAudio();
            AudioFilePlayer.AudioHasEndedEvent += OnAudioHasEnded;

            //Timer
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);

            //State
            State = new StopState(this);
        }

        #endregion

        #region METHODS

        private void PlayPauseAudio(object o) => State.PlayTrack();
        private void StopAudio(object o) => State.StopTrack();
        private void SetAudioPlayerVolume()
        {
            AudioFilePlayer.Volume = Muted ? 0 : storedVolume;

            //if (AudioFilePlayer != null)
            //{
            //    var volume = (float)(storedVolume / 100);
            //    AudioFilePlayer.Volume = Muted ? 0 : volume;
            //}
        }
        private void OnAudioHasEnded(object sender, EventArgs e)
        {
            State.OnAudioHasEnded();

            //Notify subscribers about playback end
            AudioHasEndedEvent?.Invoke(this, null);
        }
        internal void RequestTrack() => AudioHasEndedEvent?.Invoke(this, null);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnPlaylistEnded(object sender, EventArgs e)
        {
            StopAudio(null);
        }

        #endregion

        #endregion
    }
}
