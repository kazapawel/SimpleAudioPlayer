using System;

namespace AudioPlayerNAudio
{
    /// <summary>
    /// Audio player is responsible for all audio operations. 
    /// </summary>
    public interface IAudioFilePlayer
    {
        #region PROPERTIES

        /// <summary>
        /// 
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Loaded audio file volume
        /// </summary>
        double Volume { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsReady { get; }

        TimeSpan TimeCurrent { get; }

        TimeSpan TimeTotal { get; }

        long StreamPosition { get; set; }

        long StreamLength { get;}

        #endregion

        #region EVENTS

        event EventHandler AudioHasEndedEvent;

        #endregion

        #region METHODS

        /// <summary>
        /// Plays loaded audio file.
        /// </summary>
        /// <param name="path"></param>
        void PlayAudio();

        /// <summary>
        /// Stops playing loaded audio file.
        /// </summary>
        void StopAudio();

        /// <summary>
        /// 
        /// </summary>
        void PauseAudio();

        /// <summary>
        /// 
        /// </summary>
        //void ResumeAudio();

        #endregion
    }
}
