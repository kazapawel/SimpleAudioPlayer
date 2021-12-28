using AudioPlayerNAudio;
using System.Windows.Threading;
using System;
using System.Collections.Generic;

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
        /// Returns true if audio is playing.
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


        #region PAPAPSDFPASPDFPASDPFAPSDFASDF

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

        #endregion

        #region EVENTS

        /// <summary>
        /// Occurs when user stops audio
        /// </summary>
        public event EventHandler StopAudioBeforeEndEvent;

        /// <summary>
        /// Occurs when next song is pressed on ui
        /// </summary>
        public event EventHandler NextTrackRequestEvent;

        /// <summary>
        /// Occurs when prevvious song is pressed on ui
        /// </summary>
        public event EventHandler PreviousTrackRequestEvent;

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
        public AudioPlayerVM()
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

                //If audio is not paused: to prevent from playing multiple files at the same time
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

                    //Here should be an event informing all about new audio plyaback start
                    //
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

                //Changes state of player
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

                //Clears audio player. Setting to null is questionable but how to make it better?
                audioFilePlayer = null;

                //On property changed
                UpdateTime();

                //Changes state of player
                IsPlaying = false;

                //Raises stop audio before end event
                StopAudioBeforeEndEvent?.Invoke(this, new EventArgs());

                OnPropertyChanged(nameof(TimeTotal));

                UpdatePosition();
            }
        }

        /// <summary>
        /// 
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
        /// Raises an next track request event.
        /// </summary>
        /// <param name="o"></param>
        private void NextTrackRequest(object o)
        {
            NextTrackRequestEvent?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Raises an previous track request event.
        /// </summary>
        /// <param name="o"></param>
        private void PreviousTrackRequest(object o)
        {
            PreviousTrackRequestEvent?.Invoke(this, new EventArgs());
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

            //Loads track path which was sent by playlist
            Path = e.AudioFileVM.Path;

            //Plays new track
            PlayAudio(null);
        }

        /// <summary>
        /// If audio file has ended, requests antoher track to load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnAudioHasEnded(object sender, EventArgs e)
        {
            //Clears audio player
            audioFilePlayer = null;

            //Changes state of the player
            IsPlaying = false;

            //Request
            NextTrackRequest(null);
        }

        /// <summary>
        /// Clears audio file player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnPlaylistEnded(object sender, EventArgs e)
        {
            audioFilePlayer = null;
            //StopAudio(null);
        }

        /// <summary>
        /// If there is a audio player, sets it's volume.
        /// </summary>
        private void SetAudioPlayerVolume()
        {
            if (audioFilePlayer != null)
            {
                var volume = (float)(DbToPercent(storedVolume) / 100f);
                audioFilePlayer.Volume = Muted ? 0 : volume;
            }
        }

        /// <summary>
        /// Calculates volume percentage based on dB.
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        private static double DbToPercent(double dB)
        {
            var vol = Math.Pow(10, (double)dB / 10) * 100;
            return vol;
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
    }
}
