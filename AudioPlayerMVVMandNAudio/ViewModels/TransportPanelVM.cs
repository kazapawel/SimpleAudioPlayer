using AudioPlayerNAudio;
using System.Windows.Threading;
using System;
using System.Collections.Generic;

namespace AudioPlayerMVVMandNAudio
{
    /// <summary>
    /// View model class for audio player and transport control.
    /// </summary>
    public class TransportPanelVM : BaseViewModel
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

        /// <summary>
        /// 
        /// </summary>
        private bool isPlaying;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Path of audio file.
        /// </summary>
        public string Path { get; set; }

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
        public bool IsPlaying
        {
            get
            {
                return isPlaying;
            }
            set
            {
                if(isPlaying!=value)
                {
                    isPlaying = value;
                    OnPropertyChanged(nameof(IsPlaying));
                }
            }
        }

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
                    audioFilePlayer.StreamPosition = (long)value * (audioFilePlayer.StreamLength / 100);
                OnPropertyChanged(nameof(TimeCurrent));
            }
                
        }

        /// <summary>
        /// String representation of current time of audio which is playing.
        /// </summary>
        public string TimeCurrent => audioFilePlayer != null ? audioFilePlayer.TimeCurrent.ToString(@"mm\:ss") : "--:--";

        /// <summary>
        /// String representation of total time of audio which is playing
        /// </summary>
        public string TimeTotal => audioFilePlayer != null ? audioFilePlayer.TimeTotal.ToString(@"mm\:ss") : "--:--";

        ///// <summary>
        ///// String representation of remaining time of audio which is playing.
        ///// </summary>
        //public string TimeRemaining => audioFilePlayer != null ? (audioFilePlayer.TotalTime - audioFilePlayer.CurrentTime).ToString(@"mm\:ss") : "--:--";//audioPlayer.IsAudioFileLoaded ? (audioPlayer.TotalTime - audioPlayer.CurrentTime).ToString(@"mm\:ss") : "--:--";

        #endregion

        #region EVENTS

        /// <summary>
        /// Occurs when user stops audio.
        /// </summary>
        public event EventHandler StopAudioBeforeEndEvent;

        /// <summary>
        /// Occurs when next track is requested.
        /// </summary>
        public event EventHandler NextTrackRequestEvent;

        /// <summary>
        /// Occurs when previous track is requested.
        /// </summary>
        public event EventHandler PreviousTrackRequestEvent;

        /// <summary>
        /// Occurs when audio player starts playback.
        /// </summary>
        public event EventHandler<FilePathEventArgs> AudioStartEvent;

        #endregion

        #region COMMANDS

        public RelayCommand PlayAudioCommand { get; set; }

        public RelayCommand StopAudioCommand { get; set; }

        public RelayCommand PauseAudioCommand { get; set; }

        public RelayCommand NextTrackRequestCommand { get; set; }

        public RelayCommand PreviousTrackRequestCommand { get; set; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TransportPanelVM()
        {
            //Commands:
            PlayAudioCommand = new RelayCommand(PlayAudio);
            StopAudioCommand = new RelayCommand(StopAudio);
            PauseAudioCommand = new RelayCommand(PauseAudio);
            NextTrackRequestCommand = new RelayCommand(NextTrackRequest);
            PreviousTrackRequestCommand = new RelayCommand(PreviousTrackRequest);

            //Sets start volume
            Volume = 50;

            //Inits errog log list
            ErrorLog = new List<string[]>();
        }

        #endregion

        #region METHODS

        #region TRANSPORT METHODS

        /// <summary>
        /// Creates new AudioFilePlayer and plays chosen audio file.
        /// </summary>
        /// <param name="o"></param>
        private void PlayAudio(object o)
        {
            //Works only if there is a path ready for loading into audio player
            if (Path != null)
            {
                //If audio is paused - resume audio
                if (audioFilePlayer != null)
                    audioFilePlayer.ResumeAudio();

                //If audio is not paused: creates new audioplayer model to prevent from playing multiple files at the same time
                else
                {
                    try
                    {
                        //Creates new instance of audio file player model
                        audioFilePlayer = new AudioFilePlayerNAudio(Path);
                    }
                    catch (Exception e)
                    {
                        //Logs exception info
                        ErrorLog.Add(new string[] { e.ToString(), e.Message });

                        //Resets audioFilePlayer
                        audioFilePlayer = null;

                        //Ends method = no audio playback
                        return;
                    }
                    
                    //Sets audioplayer model volume based on user volume settings(ex: volume fader)
                    SetAudioPlayerVolume();

                    //Subscribes to model event which informs about audio reaching it's end.
                    audioFilePlayer.AudioHasEndedEvent += OnAudioHasEnded;

                    //Plays audio
                    audioFilePlayer.PlayAudio();

                    //Event informing all about new audio plyaback start
                    AudioStartEvent?.Invoke(this, new FilePathEventArgs(Path));
                }

                //Creates new dispatcher timer for updating time values from model
                timer = new DispatcherTimer();
                timer.Tick += new EventHandler(Timer_Tick);
                timer.Interval = new TimeSpan(0, 0, 1);

                //Starts timer
                timer.Start();

                //Updates total time on startup
                OnPropertyChanged(nameof(TimeTotal));

                //Updates acutal time on startup - shows 00:00;
                OnPropertyChanged(nameof(TimeCurrent));

                //Changes player's state flag
                IsPlaying = true;
            }
        }

        /// <summary>
        /// Stops current audio playback before audio reaching it's end.
        /// </summary>
        /// <param name="o"></param>
        private void StopAudio(object o)
        {
            //If audio is playing
            if(audioFilePlayer != null)
            {
                //Stops audio file player
                audioFilePlayer?.StopAudio();

                //Stops timer
                timer?.Stop();

                //Clears model
                audioFilePlayer = null;

                //On property changed
                UpdateTime();

                //Changes state of player
                IsPlaying = false;

                //Raises stop audio before end event
                StopAudioBeforeEndEvent?.Invoke(this, new EventArgs());

                //
                OnPropertyChanged(nameof(TimeTotal));

                //
                UpdatePosition();
            }
        }

        /// <summary>
        /// Pauses current playback and stops time timer.
        /// </summary>
        /// <param name="o"></param>
        private void PauseAudio(object o)
        {
            //Stops timer
            timer?.Stop();

            //Pause audio
            audioFilePlayer?.PauseAudio();

            //Change pause state property 
            IsPlaying = false;
        }

        /// <summary>
        /// Stops current audio playback and requests next track.
        /// </summary>
        /// <param name="o"></param>
        private void NextTrackRequest(object o)
        {
            //Stops audio first
            StopAudio(null);

            //Raises an event
            NextTrackRequestEvent?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Stops current audio playback and requests previous track.
        /// </summary>
        /// <param name="o"></param>
        private void PreviousTrackRequest(object o)
        {
            //Stops audio
            StopAudio(null);

            //
            PreviousTrackRequestEvent?.Invoke(this, new EventArgs());
        }

        #endregion

        #region SUBSCRIPTIONS METHODS

        /// <summary>
        /// If audio file has ended, clears model and requests another track to load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnAudioHasEnded(object sender, EventArgs e)
        {
            //Clears audio player
            audioFilePlayer = null;

            //Changes state of the player
            IsPlaying = false;

            //Requests next track
            NextTrackRequestEvent?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Loads new audio file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnAudioFileLoaded(object sender, AudioFileVMEventArgs e)
        {
            //If audio is playing stops it
            if(audioFilePlayer != null)
            {
                StopAudio(null);
            }

            //EXCEPTION - where to check if file exists - model?

            //Loads track path which was sent by playlist - exception sometimes
            Path = e.AudioFileVM.Path;

            //Plays new track
            PlayAudio(null);
        }

        /// <summary>
        /// Clears audio file player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnPlaylistEnded(object sender, EventArgs e)
        {
                //audioFilePlayer = null;
                Path = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnPlaylistCleared(object sender, EventArgs e)
        {
            Path = null;
        }

        #endregion

        #region PRIVATE INNER METHODS

        /// <summary>
        /// If there is a audio player, sets it's volume.
        /// </summary>
        private void SetAudioPlayerVolume()
        {
            //var a = DbToPercent(Volume);
            //var b = DbToP(Volume);
            if (audioFilePlayer != null)
            {
                var volume = (float)(storedVolume/100);
                audioFilePlayer.Volume = Muted ? 0 : volume;
            }
        }

        /// <summary>
        /// Calculates volume percentage based on dB.
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        private double DbToPercent(double dB)
        {
            var vol = Math.Pow(10, (double)dB / 10) * 100;
            return vol;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dB"></param>
        /// <returns></returns>
        private double DbToP(double dB)
        {
            return 20 * Math.Log(10, Volume);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTime();
            UpdatePosition();
        }

        /// <summary>
        /// Raises OnPropertyChanged event to inform about audio time changes.
        /// </summary>
        private void UpdateTime()
        {
            OnPropertyChanged(nameof(TimeCurrent));
        }

        /// <summary>
        /// Raises OnPropertyChanged event to inform about audio stream position changes.
        /// </summary>
        private void UpdatePosition()
        {
            OnPropertyChanged(nameof(Position));
        }

        #endregion

        #endregion
    }
}
