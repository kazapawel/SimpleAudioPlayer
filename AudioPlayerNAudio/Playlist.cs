using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace AudioPlayerNAudio
{
    /// <summary>
    /// Represents collection of songs. 
    /// </summary>
    public class Playlist
    {
        #region PUBLIC PROPERTIES

        /// <summary>
        /// Collection of audio files.
        /// </summary>
        public List<AudioFile> SongsList { get; set; }

        /// <summary>
        /// Returns true if there is no tracks in playlist.
        /// </summary>
        public bool IsEmpty => SongsList.Count == 0;

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

        /// <summary>
        /// Adds a track to songs list.
        /// </summary>
        /// <param name="track"></param>
        public void AddTrack(AudioFile track) => SongsList.Add(track);

        /// <summary>
        /// Removes track from songs list.
        /// </summary>
        /// <param name="track"></param>
        public void RemoveTrack(AudioFile track) => SongsList.Remove(track);

        /// <summary>
        /// 
        /// </summary>
        public void ClearPlaylist() => SongsList = new List<AudioFile>();

        /// <summary>
        /// 
        /// </summary>
        public void SavePlaylist() => SaveTextFile();//System.UnauthorizedAccessException: 'Access to the path 'D:\' is denied.'

        #endregion

        #region PRIVATE FILE SAVING/LOADING METHODS

        /// <summary>
        /// 
        /// </summary>
        private void SaveTextFile() => TextFileSaver.ExportToFile(PlaylistPath, SongsList.Select(x => x.PathOfFile));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private List<AudioFile> LoadFromTextFile(string path) => TextFileSaver.ImportFromFile(path)?.Select(x => new AudioFile(x)).ToList();

        #endregion



    }
}
