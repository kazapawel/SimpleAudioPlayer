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
    public class PlaylistVM : BaseViewModel, ICloseWindow
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

        /// <summary>
        /// Gets number of items in playlist.
        /// </summary>
        public string Items => model.SongsList.Count.ToString();

        #region DRAG AND DROP PROPERTIES

        private IEnumerable<string> incomingFiles;
        public IEnumerable<string> IncomingFiles
        {
            get => incomingFiles;
            set
            {
                if(incomingFiles!=value)
                {
                    incomingFiles = value;
                    OnPropertyChanged(nameof(IncomingFiles));
                }
            }
        }

        public AudioFileVM TargetItem { get; set; }
        public AudioFileVM MovedItem { get; set; }

        
        #endregion

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

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> PlaylistClearedEvent;

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

        /// <summary>
        /// 
        /// </summary>
        public ICommand AddFilesCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand MoveItemCommand { get; set; }

        public ICommand CloseWindowCommand { get; set; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PlaylistVM()
        {
            //Model
            model = new Playlist();

            //Playlist for binding
            SongsListObservable = new ObservableCollection<AudioFileVM>();

            //Load data from model to observable collection
            foreach (var song in model.SongsList)
                SongsListObservable.Add(new AudioFileVM(song));

            //Commands
            LoadTrackCommand = new RelayCommand(LoadTrack);
            RemoveTracksFromPlaylistCommand = new RelayCommand(RemoveTracksFromPlaylist);
            ClearPlaylistCommand = new RelayCommand(ClearPlaylist);
            AddFilesCommand = new AddFilesCommand(this);
            MoveItemCommand = new MoveItemCommand(this);
            CloseWindowCommand = new CloseWindowCommand(this);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Raises an LoadAudioFileEvent and sends selected track as argumentjksdhfkjasdhfjksdhfjksdfkljsdljkfh
        /// </summary>
        /// <param name="o"></param>
        private void LoadTrack(object o)
        {
            //Changes state of current audiofile bool flag
            if (BufferTrack !=null)
                BufferTrack.IsAudioFilePlaying = false;

            //Selected track becomes buffer track
            BufferTrack = SelectedTrack;

            //Raises en event about new file in buffer
            LoadAudioFileEvent?.Invoke(this, new AudioFileVMEventArgs(BufferTrack));
        }

        #region SUBSCRIPTIONS METHODS

        /// <summary>
        /// Changes buffer track state flag when audio starts playing. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnAudioStart(object sender, EventArgs e)
        {
            //Sets buffer track state - this has to chagne when playback starts
            if(BufferTrack != null)
                BufferTrack.IsAudioFilePlaying = true;
        }

        /// <summary>
        /// Sets next track in playlist as selected track.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnNextTrackRequest(object sender, EventArgs e)
        {
            //Changes state of current audiofile bool flag
            if (BufferTrack != null)
                BufferTrack.IsAudioFilePlaying = false;

            //Gets index of next track
            var newIndex = SongsListObservable.IndexOf(BufferTrack) + 1;

            //Checks if index is in range
            if (newIndex < SongsListObservable.Count)
            {
                //Changes track selection
                BufferTrack = SongsListObservable[newIndex];

                //Loads track
                //LoadTrack(null);
                LoadAudioFileEvent?.Invoke(this, new AudioFileVMEventArgs(BufferTrack));
            }

            //If playlist reaches an end - raises an event
            else
            {
                if (BufferTrack != null)
                    BufferTrack.IsAudioFilePlaying = false;
                PlaylistEndedEvent?.Invoke(this, new AudioFileVMEventArgs(bufferTrack));
            }
        }

        /// <summary>
        /// Sets previous track in playlist as selected track.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnPreviousTrackRequest(object sender, EventArgs e)
        {
            ////Change state of current audio file playing bool flag
            //if(BufferTrack != null)
            //    BufferTrack.IsAudioFilePlaying = false;

            //Gets index of currently playing track
            var index = SongsListObservable.IndexOf(BufferTrack);

            //Sets new index of buffer track, if you press previous track on first track it will start over.
            index += index > 0 ? -1 : 0;

            //If there are songs in playlist
            if (index > -1)
            {
                //Changes track selection
                BufferTrack = SongsListObservable[index];

                //Loads track
                //LoadTrack(null);
                LoadAudioFileEvent?.Invoke(this, new AudioFileVMEventArgs(BufferTrack));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnAudioStoppedBeforeEnd(object sender, EventArgs e)
        {
            if(BufferTrack !=null)
                BufferTrack.IsAudioFilePlaying = false;

            //If playlist was cleared during audio play
            if (SongsListObservable.Count == 0)
            {             
                //Clears buffer track
                BufferTrack = null;

                //Raises event about playlist cleared
                PlaylistClearedEvent?.Invoke(this, new AudioFileVMEventArgs(BufferTrack));
            }
        }

        #endregion

        #region PLAYLIST CRUD METHODS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trackPath"></param>
        public void AddTracksToPlaylist(IEnumerable<string> tracksPaths)
        {
            //Adds tracks to model and observable collection
            foreach (var trackPath in tracksPaths)
            {
                var track = new AudioFile(trackPath);
                model.AddTrack(track);
                SongsListObservable.Add(new AudioFileVM(track));

                //Raises property changed on read only property
                OnPropertyChanged(nameof(Items));
            }
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

            ClearObservableCollection(clean);
        }

        /// <summary>
        /// Removes all trakcs from playlist.
        /// </summary>
        /// <param name="o"></param>
        private void ClearPlaylist(object o)
        {
            //Makes new model collection 
            model.ClearPlaylist();

            ClearObservableCollection(null);

            PlaylistClearedEvent?.Invoke(this, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        private void ClearObservableCollection(IEnumerable<AudioFileVM> collection)
        {
            //Makes new observable collection
            SongsListObservable = collection != null ? new ObservableCollection<AudioFileVM>(collection) 
                                                     : new ObservableCollection<AudioFileVM>();

            //CollectionChanged does not work for new-ing collection...?
            OnPropertyChanged(nameof(SongsListObservable));

            //Raise property changed on read only property
            OnPropertyChanged(nameof(Items));
        }

        /// <summary>
        /// Switches two items.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="moved"></param>
        public void MoveItem(AudioFileVM target, AudioFileVM moved)
        {
            //Without checking this there is no highlight in listbox
            if (moved != target)
                SongsListObservable.Move(SongsListObservable.IndexOf(moved), SongsListObservable.IndexOf(target));
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void OnClosed() => model.SavePlaylist();
    }

    #endregion

}

