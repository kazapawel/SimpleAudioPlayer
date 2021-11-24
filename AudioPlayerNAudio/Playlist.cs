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
        /// Returns true if there is no tracks in playlist
        /// </summary>
        public bool IsEmpty => SongsList.Count == 0;

        /// <summary>
        /// 
        /// </summary>
        public string PlaylistPath { get; set; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Default constructor
        /// </summary>
        public Playlist()
        {
            PlaylistPath = Path.Combine(Environment.CurrentDirectory,"defaultplaylist.txt");

            SongsList = new List<AudioFile>();

            LoadPlaylistFromFile();

            //Dummy data
            //LoadDummyData();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Adds a track to songs list.
        /// </summary>
        /// <param name="track"></param>
        public void AddTrack(AudioFile track)
        {
            SongsList.Add(track);
            SavePlaylistToFile();
        }

        /// <summary>
        /// Removes track from songs list.
        /// </summary>
        /// <param name="track"></param>
        public void RemoveTrack(AudioFile track)
        {
            SongsList.Remove(track);
            SavePlaylistToFile();
        }

        public void ClearPlaylist()
        {
            SongsList = new List<AudioFile>();
            SavePlaylistToFile();
        }

        private void LoadPlaylistFromFile()
        {
            if(File.Exists(PlaylistPath))
            {
                var paths = File.ReadAllLines(PlaylistPath);
                foreach(var p in paths)
                {
                    SongsList.Add(new AudioFile(p));
                }
            }
        }

        private void SavePlaylistToFile()
        {
            File.WriteAllLines(PlaylistPath, SongsList.Select(x => x.Path));
        }

        private void LoadDummyData()
        {
            SongsList.Add(new AudioFile(@"D:\sample po procesach.wav"));
            SongsList.Add(new AudioFile(@"D:\gitarasolo2.wav"));
            SongsList.Add(new AudioFile(@"D:\VOX czysty.wav"));
            SongsList.Add(new AudioFile(@"D:\Cry.mp3"));
            SongsList.Add(new AudioFile(@"D:\Cry.mp3"));
            SongsList.Add(new AudioFile(@"D:\Cry.mp3"));
            SongsList.Add(new AudioFile(@"D:\Cry.mp3"));
            SongsList.Add(new AudioFile(@"D:\Cry.mp3"));
        }
        private void LoadMoreDummyData()
        {
            SongsList.Add(new AudioFile(@"C:\monster.mp3"));
            SongsList.Add(new AudioFile(@"C:\breath of life.mp3"));
            SongsList.Add(new AudioFile(@"C:\feed me.mp3"));
            SongsList.Add(new AudioFile(@"C:\breath of life.mp3"));


        }

        #endregion

        

    }
}
