using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AudioPlayerNAudio
{
    /// <summary>
    /// Represents collection of songs. 
    /// </summary>
    public class Playlist
    {
        /// <summary>
        /// Collection of audio files.
        /// </summary>
        public List<AudioFile> SongsList { get; set; }

        /// <summary>
        /// Returns true if there is no tracks in playlist
        /// </summary>
        public bool IsEmpty => SongsList.Count == 0;

        public string PlaylistPath { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Playlist()
        {
            PlaylistPath = Path.Combine(Environment.CurrentDirectory, "defaultplaylist.txt");

            SongsList = new List<AudioFile>();

            LoadPlaylistFromFile();

            //Dummy data
            LoadDummyData();
        }

        private void LoadPlaylistFromFile()
        {

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

        

    }
}
