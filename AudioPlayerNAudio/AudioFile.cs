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
        /// Private instance of TagLib. Contains info about audio file.
        /// </summary>
        private readonly File tags;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Gets audio file's path.
        /// </summary>
        public string PathOfFile { get; }

        /// <summary>
        /// Gets audio file's name ex: audiofile.wav
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets audio file's title. If title is not set gets file's name.
        /// </summary>
        public string Title => tags is null ? Name : tags.Tag.Title ?? Name;

        /// <summary>
        /// Gets first artist from performers list or description it there are no performers.
        /// </summary>
        public string Artist => tags is null ? string.Empty : tags.Tag.Performers.Length > 0 ? tags.Tag.Performers[0] : string.Empty;

        /// <summary>
        /// Gets audio's file album title.
        /// </summary>
        public string Album => tags is null ? string.Empty : tags.Tag.Album;

        /// <summary>
        /// Gets audio file's duration time.
        /// </summary>
        public TimeSpan Duration => tags is null ? new TimeSpan(0) : tags.Properties.Duration;

        public string Description => tags is null ? string.Empty : tags.Properties.Description;
        public int AudioChannels => tags is null ? 0 : tags.Properties.AudioChannels;
        public int AudioSampleRate => tags is null ? 0 : tags.Properties.AudioSampleRate;
        public int BitsPerSample => tags is null ? 0 : tags.Properties.BitsPerSample == 0 ? tags.Properties.AudioBitrate : tags.Properties.BitsPerSample;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="path"></param>
        public AudioFile(string path)
        {
            //File's path
            PathOfFile = path;

            //File's name
            Name = System.IO.Path.GetFileName(path);

            //Creates TagLib instance
            try
            {
                tags = File.Create(path);
            }

            //TagLib.CorruptFileException: 'MPEG audio header not found.
            catch (CorruptFileException e)
            {
                //So user can see info about file corruption
                Name = e.Message;
            }

            /*
             * TagLib.UnsupportedFormatException: 
             * 'E:\Muza\breakbit\Booty Luv - Shine (Destroyers & Aggresivnes Rmx).mp3.sfk (taglib/sfk)'
             */
        }

        #endregion
    }
}
