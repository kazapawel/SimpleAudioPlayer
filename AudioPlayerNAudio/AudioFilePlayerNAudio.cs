using System;
using NAudio.Wave;

namespace AudioPlayerNAudio
{
    /// <summary>
    /// Audio file player based on NAudio library.
    /// </summary>
    public class AudioFilePlayerNAudio : IAudioFilePlayer
    {
        #region PRIVATE MEMBERS

        /// <summary>
        /// NAudio audio output device
        /// </summary>
        private WaveOutEvent outputDevice;

        /// <summary>
        /// NAudio audio file reader 
        /// </summary>
        private MediaFoundationReader audioFileReader;

        /// <summary>
        /// Flag that informs if playback was stopped before reaching its end(ex: by user pressing stop).
        /// </summary>
        private bool StoppedBeforeEnd;

        private double storedVolume;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// 
        /// </summary>
        public bool IsReady => outputDevice != null && audioFileReader != null;

        /// <summary>
        /// Gets and sets output device volume
        /// </summary>
        public double Volume
        {
            get
            {
                return outputDevice != null ? (double)outputDevice.Volume * 100 : 0;
            }
            set
            {
                storedVolume = value;
                if (outputDevice != null)
                    outputDevice.Volume=(float)(value / 100);
            }
        }

        /// <summary>
        /// Current time (position) of audio file.
        /// </summary>
        public TimeSpan TimeCurrent => audioFileReader.CurrentTime;

        /// <summary>
        /// Total time of audio file.
        /// </summary>
        public TimeSpan TimeTotal => audioFileReader.TotalTime;

        /// <summary>
        /// Actual position of a audio stream
        /// </summary>
        public long StreamPosition
        {
            get => audioFileReader != null ? audioFileReader.Position : 0;
            set => audioFileReader.Position = audioFileReader != null ? value : 0;
        }

        /// <summary>
        /// Audio stream's total length.
        /// </summary>
        public long StreamLength => audioFileReader != null ? audioFileReader.Length : 0;
      
        /// <summary>
        /// Path of the file to play.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string[] ErrorLog { get; set; }

        #endregion

        #region EVENTS

        /// <summary>
        /// Occurs when audio file reaches it's end.
        /// </summary>
        public event EventHandler AudioHasEndedEvent;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="path"></param>
        public AudioFilePlayerNAudio()
        {
            Volume = 50;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Plays audio file from the beginning.
        /// </summary>
        /// <param name="path">Audio file's path</param>
        public void PlayAudio()
        {
            //If audio is paused resumes audio //NAudio.MM.Exception: 'NoDriver calling waveOutRestart'
            if (outputDevice?.PlaybackState == PlaybackState.Paused)
                outputDevice.Play();
            else
            {
                //Crates device
                outputDevice = new WaveOutEvent();
                outputDevice.PlaybackStopped += OnPlaybackStopped;

                //Creates file reader
                try
                {
                    audioFileReader = new MediaFoundationReader(Path);
                    //System.Runtime.InteropServices.COMException: 'Typ strumienia bajtów podanego adresu URL jest nieobsługiwany. (0xC00D36C4)'
                }
                catch (Exception e)
                {
                    ErrorLog = new string[] { e.ToString(), e.Message };
                    DisposeDevices();
                    return;
                }

                outputDevice.Init(audioFileReader);
                SetVolume();
                outputDevice.Play();
            }
        }

        /// <summary>
        /// If audio is playing - pauses.
        /// </summary>
        public void PauseAudio()
        {
            if (outputDevice?.PlaybackState == PlaybackState.Playing)
                outputDevice.Pause();
        }

        /// <summary>
        /// Stops audio playback before audio file ends (ex. user pressed stop).
        /// </summary>
        public void StopAudio()
        {
            if(outputDevice?.PlaybackState!=PlaybackState.Stopped)
            {
                //Marks flag as true
                StoppedBeforeEnd = true;

                //Stops audio
                outputDevice?.Stop();

                //
                DisposeDevices();
            }
        }

        /// <summary>
        /// This is used only when audio reaches it's end.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPlaybackStopped(object sender, EventArgs e)
        {
            //If audio has stopped by reaching it's end - raises an event
            if (!StoppedBeforeEnd)
            {
                //Disposes all
                DisposeDevices();

                AudioHasEndedEvent?.Invoke(this, null);
            }

            StoppedBeforeEnd = false;
        }

        /// <summary>
        /// Disposes all devices.
        /// </summary>
        private void DisposeDevices()
        {
            outputDevice?.Dispose();
            outputDevice = null;
            audioFileReader?.Dispose();
            audioFileReader = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetVolume()
        {
            if (audioFileReader != null)
            {
                outputDevice.Volume = (float)(storedVolume / 100);
            }
        }

        #endregion

    }
}
