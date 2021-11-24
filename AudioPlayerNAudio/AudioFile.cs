using System;
using TagLib;

namespace AudioPlayerNAudio
{
    /// <summary>
    /// Information about single audio file.
    /// </summary>
    public class AudioFile
    {
        #region PRIVATE MEMBERS

        /// <summary>
        /// Private instance of TagLib. Contains info about  file.
        /// </summary>
        private File tags;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Gets file's path.
        /// </summary>
        public string Path { get; }

        public string Description => tags.Properties.Description;
        public int AudioChannels => tags.Properties.AudioChannels;
        public int AudioSampleRate => tags.Properties.AudioSampleRate;
        public int BitsPerSample => tags.Properties.BitsPerSample;

        /// <summary>
        /// Gets audio file's duration time.
        /// </summary>
        public TimeSpan Duration => tags.Properties.Duration;

        /// <summary>
        /// Gets audio file's title or path if title is not set.
        /// </summary>
        public string Title => tags.Tag.Title ?? Path;

        /// <summary>
        /// Gets first artist from performers list or description it there are no performers.
        /// </summary>
        public string Artist => tags.Tag.Performers.Length > 0 ? tags.Tag.Performers[0] : tags.Properties.MediaTypes.ToString();

        /// <summary>
        /// Gets audio's file album title.
        /// </summary>
        public string Album => tags.Tag.Album ?? "-";

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="path"></param>
        public AudioFile(string path)
        {
            //Creates TagLib instance
            tags = File.Create(path);

            /*
             * TagLib.UnsupportedFormatException: 
             * 'E:\Muza\breakbit\Booty Luv - Shine (Destroyers & Aggresivnes Rmx).mp3.sfk (taglib/sfk)'
             * 
             */
            //Sets file's path
            Path = path;
        }

        #endregion
    }
}
