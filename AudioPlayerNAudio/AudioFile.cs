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

        private string errorMessage;
        private string exceptionName;

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
        public string Title { get; private set; }

        /// <summary>
        /// Gets first artist from performers list or description it there are no performers.
        /// </summary>
        public string Artist { get; private set; }

        /// <summary>
        /// Gets audio's file album title.
        /// </summary>
        public string Album { get; private set; }

        /// <summary>
        /// Gets audio file's duration time.
        /// </summary>
        public TimeSpan Duration { get; private set; }

        public string Description { get; private set; }
        public int AudioChannels { get; private set; }
        public int AudioSampleRate { get; private set; }
        public int BitsPerSample { get; private set; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="path"></param>
        public AudioFile(string path)
        {
            PathOfFile = path;
            Name = System.IO.Path.GetFileName(path);

            //Creates TagLib instance
            File tags = null;
            try
            {
                tags = File.Create(PathOfFile);
            }
            catch (Exception e)
            {
                //For user to be able to see info about file corruption
                errorMessage = e.Message;
                exceptionName = e.GetType().FullName;
            }

            Title = tags is null ? Name : tags.Tag.Title ?? Name;
            Artist = tags is null ? exceptionName : tags.Tag.Performers.Length > 0 ? tags.Tag.Performers[0] : string.Empty;
            Album = tags is null ? string.Empty : tags.Tag.Album;
            Duration = tags is null ? new TimeSpan(0) : tags.Properties.Duration;
            Description = tags is null ? errorMessage : tags.Properties.Description;
            AudioChannels = tags is null ? 0 : tags.Properties.AudioChannels;
            AudioSampleRate = tags is null ? 0 : tags.Properties.AudioSampleRate;
            BitsPerSample = tags is null ? 0 : tags.Properties.BitsPerSample == 0 ? tags.Properties.AudioBitrate : tags.Properties.BitsPerSample;         
        }

        #endregion
    }
}
