using AudioPlayerNAudio;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System.Collections.Specialized;
using System.IO;

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
                if (bufferTrack != value)
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
                if (selectedTrack != value)
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

            if (!model.IsEmpty)
                BufferTrack = SongsListObservable[0];

            //commands
            LoadTrackCommand = new RelayCommand(LoadTrack);
            RemoveTracksFromPlaylistCommand = new RelayCommand(RemoveTracksFromPlaylist);
            ClearPlaylistCommand = new RelayCommand(ClearPlaylist);

            //SongsListObservable.CollectionChanged += RefreshModel;
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
            var index = SongsListObservable.IndexOf(BufferTrack) + 1;

            //Checks if can select next track on playlist
            if (index < SongsListObservable.Count)
            {
                //Loads next track
                BufferTrack = SongsListObservable[index];

                //Raises an event and plays buffer track
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

            //Sets new index of buffer track, if you press previous track on first trakc it will start over.
            index += index > 0 ? -1 : 0;

            //If there are songs in playlist
            if (index > -1)
            {
                BufferTrack = SongsListObservable[index];

                //Raises an event and plays buffer track
                InformAllAboutFileLoading();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trackPath"></param>
        public void AddTracksToPlaylist(IEnumerable<string> tracksPaths)
        {
            //Adds tracks to model and observable collection
            foreach (var trackPath in tracksPaths)
            {
                if(PathIsValid(trackPath))
                {
                    var track = new AudioFile(trackPath);
                    model.AddTrack(track);
                    SongsListObservable.Add(new AudioFileVM(track));
                }            
            }

            //Raises property changed on read only property
            OnPropertyChanged(nameof(Items));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        private void RemoveTracksFromPlaylist(object o)
        {
            //This wierd casting...
            System.Collections.IList items = (System.Collections.IList)o;
            var songs = items.Cast<AudioFileVM>();

            //Removes songs
            foreach (var song in songs)
                model.RemoveTrack(song.GetModel());

            //Collection without removed tracks
            var clean = SongsListObservable.Where(x => !songs.Contains(x));

            //Removes tracks from model collection -> method 1 => iterate in reverse
            //for(var i=SongsListObservable.Count-1;i>=0; i--)
            //{
            //    model.RemoveTrack()
            //}

            //Removes tracks from observable collection -> method 2 => new collection.
            SongsListObservable = new ObservableCollection<AudioFileVM>(clean);

            //CollectionChanged does not work for new-ing collection...?
            OnPropertyChanged(nameof(SongsListObservable));

            //Raise property changed on read only property
            OnPropertyChanged(nameof(Items));

        }

        /// <summary>
        /// Removes all trakcs from playlist.
        /// </summary>
        /// <param name="o"></param>
        private void ClearPlaylist(object o)
        {
            //Makes new observable collection
            SongsListObservable = new ObservableCollection<AudioFileVM>();

            //Makes new model collection 
            model.ClearPlaylist();

            //CollectionChanged does not work for new-ing collection...?
            OnPropertyChanged(nameof(SongsListObservable));

            //Raise property changed on read only property
            OnPropertyChanged(nameof(Items));
        }

        private bool PathIsValid(string path)
        {
            var file = new FileInfo(path);

            return !file.Attributes.HasFlag(FileAttributes.Directory);
        }

        private bool IsFileDirectory(string path)
        {
            return true;
        }

        //private void RefreshModel(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    switch (e.Action)
        //    {
        //        //Adding to observable collection
        //        case NotifyCollectionChangedAction.Add:

        //            //New, last added VM item
        //            var newToDoVm = (ToDoViewModel)e.NewItems[0];

        //            //Creates new model object from new VM
        //            if (newToDoVm != null)
        //                //model.AddTodo(new ToDo(newToDoVm.Title, newToDoVm.FinishDate, newToDoVm.Priority));
        //                model.AddTodo(newToDoVm.GetModel());
        //            OnPropertyChanged(nameof(ItemsCount));
        //            break;

        //        //Removing from observable collection
        //        case NotifyCollectionChangedAction.Remove:

        //            //
        //            var removedToDoVm = (ToDoViewModel)e.OldItems[0];
        //            //
        //            if (removedToDoVm != null)
        //                model.RemoveTodo(removedToDoVm.GetModel());
        //            OnPropertyChanged(nameof(ItemsCount));

        //            break;
        //    }
        //}
    }

        #endregion
    
}

