using AudioPlayerNAudio;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;

namespace AudioPlayerMVVMandNAudio
{
    public class PlaylistVM : BaseViewModel
    {
        #region PRIVATE MEMBERS

        /// <summary>
        /// Model
        /// </summary>
        private Playlist model;

        /// <summary>
        /// Audio track in buffer
        /// </summary>
        private AudioFileVM bufferTrack;

        /// <summary>
        /// Audio track selected in playlist
        /// </summary>
        private AudioFileVM selectedTrack;


        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Playlist view model
        /// </summary>
        public ObservableCollection<AudioFileVM> SongsListObservable { get; set; }

        /// <summary>
        /// Track which is load into buffer.
        /// </summary>
        public AudioFileVM BufferTrack
        {
            get
            {
                return bufferTrack;
            }
            set
            {
                if (bufferTrack!=value)
                {
                    bufferTrack = value;
                    OnPropertyChanged(nameof(BufferTrack));
                }
            }
        }

        /// <summary>
        /// Track that is highlighted
        /// </summary>
        public AudioFileVM SelectedTrack
        {
            get => selectedTrack;
            set
            {
                if(selectedTrack!=value)
                {
                    selectedTrack = value;
                    OnPropertyChanged(nameof(SelectedTrack));
                }
            }
        }

        //public List<AudioFileVM> SelectedMultipleTracks
        //{
        //    set
        //    {
        //        if
        //    }
        //}

        /// <summary>
        /// Name of the playlist
        /// </summary>
        public string Name { get; set; } = "playlist";

        /// <summary>
        /// Gets number of items in playlist.
        /// </summary>
        public string Items => model.SongsList.Count.ToString();


        #endregion

        #region EVENTS

        /// <summary>
        /// Occurs when selected file is loaded to "buffer"
        /// </summary>
        public event EventHandler<AudioFileVMEventArgs> LoadAudioFileEvent;

        /// <summary>
        /// Occurs when playlist reaches an end and there is no track to play.
        /// </summary>
        public event EventHandler<AudioFileVMEventArgs> PlaylistEndedEvent;

        #endregion

        #region COMMANDS

        /// <summary>
        /// Loads selected track, in a responsone for double mouse click.
        /// </summary>
        public ICommand LoadTrackCommand { get; set; }

        /// <summary>
        /// Removes track from playlist.
        /// </summary>
        public ICommand RemoveTracksFromPlaylistCommand { get; set; }

        /// <summary>
        /// Removes all tracks from playlist.
        /// </summary>
        public ICommand ClearPlaylistCommand { get; set; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PlaylistVM()
        {
            //model
            model = new Playlist();

            //playlist for binding
            SongsListObservable = new ObservableCollection<AudioFileVM>();

            //load data from model to observable collection
            foreach (var song in model.SongsList)
                SongsListObservable.Add(new AudioFileVM(song));

            BufferTrack = SongsListObservable[0];

            //commands
            LoadTrackCommand = new RelayCommand(LoadTrack);
            RemoveTracksFromPlaylistCommand = new RelayCommand(RemoveTrackFromPlaylist);
            ClearPlaylistCommand = new RelayCommand(ClearPlaylist);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Raises an LoadAudioFileEvent and sends selected track as argument
        /// </summary>
        /// <param name="o"></param>
        private void LoadTrack(object o)
        {
            //Selected track becomes buffer track
            BufferTrack = SelectedTrack;

            InformAllAboutFileLoading();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InformAllAboutFileLoading()
        {
            //Raise event
            LoadAudioFileEvent?.Invoke(this, new AudioFileVMEventArgs(bufferTrack));

            //Sets buffer track state 
            BufferTrack.IsAudioFilePlaying = true;
        }

        /// <summary>
        /// Sets next track in playlist as selected track.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnNextTrackRequest(object sender, EventArgs e)
        {
            //Gets index of currently selected track
            var index = SongsListObservable.IndexOf(BufferTrack) + 2;

            //Checks if can select next track on playlist
            if (index < SongsListObservable.Count )
            {
                //Loads next track
                BufferTrack = SongsListObservable[index];

                //Raises an event
                InformAllAboutFileLoading();
            }

            //If playlist reaches an end - rasies an event
            else
                PlaylistEndedEvent?.Invoke(this, new AudioFileVMEventArgs(bufferTrack));

        }

        /// <summary>
        /// Sets previous track in playlist as selected track.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnPreviousTrackRequest(object sender, EventArgs e)
        {
            //Gets index of currently selected track
            var index = SongsListObservable.IndexOf(BufferTrack);

            //Checks if can select previous track on playlist
            if (index > 0)
                BufferTrack = SongsListObservable[index - 1];

            //Raises an event
            InformAllAboutFileLoading();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trackPath"></param>
        public void AddTrackToPlaylist(string trackPath)
        {
            SongsListObservable.Add(new AudioFileVM(trackPath));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        private void RemoveTrackFromPlaylist(object o)
        {
            //This wierd casting...
            System.Collections.IList items = (System.Collections.IList)o;
            var songs = items.Cast<AudioFileVM>();

            //List of songs without removed
            //var clean = SongsListObservable.Except(songs);
            var clean = SongsListObservable.Where(x => !songs.Contains(x));

            //Refresh observable collection
            SongsListObservable = new ObservableCollection<AudioFileVM>(clean);

            //CollectionChanged does not work for new-ing collection...?
            OnPropertyChanged(nameof(SongsListObservable));

            //Saves playlist in file
        }

        private void ClearPlaylist(object o)
        {
            SongsListObservable.Clear();
        }

        #endregion
    }
}

