using System;
using System.Collections.Generic;
using System.IO;

namespace AudioPlayerNAudio
{
    /// <summary>
    /// Represents collection of songs paths. 
    /// </summary>
    public class Playlist
    {
        #region PUBLIC PROPERTIES

        /// <summary>
        /// Collection of audio files paths.
        /// </summary>
        public IEnumerable<string> SongsList { get; set; }

        /// <summary>
        /// This playlist's path.
        /// </summary>
        public string PlaylistPath { get; set; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Default constructor
        /// </summary>
        public Playlist()
        {
            PlaylistPath = Path.Combine(Environment.CurrentDirectory, "default.txt");
            SongsList = LoadFromTextFile(PlaylistPath);
        }

        #endregion

        #region METHODS

        ///// <summary>
        ///// Adds a track to songs list.
        ///// </summary>
        ///// <param name="track"></param>
        //public void AddTrack(string track) => SongsList.Add(track);

        ///// <summary>
        ///// Removes track from songs list.
        ///// </summary>
        ///// <param name="track"></param>
        //public void RemoveTrack(AudioFile track) => SongsList.Remove(track);

        ///// <summary>
        ///// Removes all tracks from playlist.
        ///// </summary>
        //public void ClearPlaylist() => SongsList = new List<AudioFile>();

        /// <summary>
        /// 
        /// </summary>
        public void SavePlaylist() => SaveTextFile();//System.UnauthorizedAccessException: 'Access to the path 'D:\' is denied.'

        #endregion

        #region PRIVATE FILE SAVING/LOADING METHODS

        /// <summary>
        /// 
        /// </summary>
        private void SaveTextFile() => TextFileSaver.ExportToFile(PlaylistPath, SongsList);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private IEnumerable<string> LoadFromTextFile(string path) => TextFileSaver.ImportFromFile(path);

        #endregion



    }
}
