using System;
using NAudio.Wave;
using System.Linq;
using System.Text;
using System.IO;

namespace AudioPlayerNAudio
{
    /// <summary>
    /// Audio file player based on NAudio library.
    /// </summary>
    public class AudioFilePlayerNAudio : IAudioFilePlayer<float>
    {
        #region PRIVATE MEMBERS

        /// <summary>
        /// NAudio audio output device
        /// </summary>
        private WaveOutEvent outputDevice;

        /// <summary>
        /// NAudio audio file reader 
        /// </summary>
        private AudioFileReader audioFileReader;

        /// <summary>
        /// Flag that informs if playback was stopped before reaching its end(ex: by user pressing stop).
        /// </summary>
        private bool StoppedBeforeEnd;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Gets or sets volume of audio file.
        /// 
        /// </summary>
        public float Volume
        {
            get => audioFileReader.Volume;
            set => audioFileReader.Volume = value;
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
        public AudioFilePlayerNAudio(string path)
        {
            outputDevice = new WaveOutEvent();

            //Creates audio file reader
            try
            {
                audioFileReader = new AudioFileReader(path);
            }
            catch (System.IO.InvalidDataException e)
            {
                //Re-throws exception
                throw;
            }            

            /*
             * System.InvalidOperationException: 
             * 'Got a frame at sample rate 32000, in an MP3 with sample rate 44100.
             * Mp3FileReader does not support sample rate changes.'
             * System.IO.InvalidDataException: 'Invalid MP3 file - no MP3 Frames Detected'

             * 
             */
            outputDevice.PlaybackStopped += OnPlaybackStopped;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Plays audio file from the beginning.
        /// </summary>
        /// <param name="path">Audio file's path</param>
        public void PlayAudio()
        {
            //Inits audio device
            outputDevice.Init(audioFileReader);

            //Plays audio
            outputDevice.Play();
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
        /// If audio is paused, resumes it.
        /// </summary>
        public void ResumeAudio()
        {
            //NAudio.MM.Exception: 'NoDriver calling waveOutRestart'
            if (outputDevice?.PlaybackState == PlaybackState.Paused)
                outputDevice.Play();
        }

        /// <summary>
        /// Stops audio playback before audio file ends (ex. user pressed stop).
        /// </summary>
        public void StopAudio()
        {
            StoppedBeforeEnd = true;
            outputDevice?.Stop();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPlaybackStopped(object sender, EventArgs e)
        {
            //Disposes all
            DisposeDevices();

            //If audio has stopped by reaching it's end - raises an event
            if (!StoppedBeforeEnd)
                AudioHasEndedEvent?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Disposes all devices.
        /// </summary>
        private void DisposeDevices()
        {
            outputDevice.Dispose();
            outputDevice = null;
            audioFileReader.Dispose();
            audioFileReader = null;
        }
        #endregion

    }
}
