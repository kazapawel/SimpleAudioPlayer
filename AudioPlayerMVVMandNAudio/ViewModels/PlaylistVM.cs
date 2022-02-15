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
        /// For making random index;
        /// </summary>
        private Random random;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Playlist view model
        /// </summary>
        public ObservableCollection<AudioFileVM> SongsListObservable { get; set; }

        /// <summary>
        /// Track loaded into buffer.
        /// </summary>
        public AudioFileVM BufferTrack { get; set; }

        /// <summary>
        /// Track highlighted in playlist.
        /// </summary>
        public AudioFileVM SelectedTrack { get; set; }

        public bool LoopOn { get; set; }
        public bool ShuffleOn { get; set; }

        /// <summary>
        /// Gets number of items in playlist.
        /// </summary>
        public string Items => SongsListObservable.Count.ToString();

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
        /// Occurs when selected file is loaded into "buffer"
        /// </summary>
        public event EventHandler<AudioFileVMEventArgs> LoadSelectedAudioFileEvent;

        public event EventHandler PlaylistHasEndedEvent;

        #endregion

        #region COMMANDS

        /// <summary>
        /// Loads selected track, in a responsone for double mouse click.
        /// </summary>
        public ICommand LoadSelectedTrackCommand { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public ICommand OnWindowClosingCommand { get; set; }

        public ICommand NextTrackCommand { get; set; }
        public ICommand PreviousTrackCommand { get; set; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PlaylistVM()
        {
            model = new Playlist();
            random = new Random();
            SongsListObservable = new ObservableCollection<AudioFileVM>();

            //Load data from model to observable collection
            foreach (var song in model.SongsList)
                SongsListObservable.Add(new AudioFileVM(song));

            model = null;

            //Commands
            LoadSelectedTrackCommand = new RelayCommand(LoadSelectedTrackToBuffer);
            RemoveTracksFromPlaylistCommand = new RelayCommand(RemoveTracksFromPlaylist);
            ClearPlaylistCommand = new RelayCommand(ClearPlaylist);
            AddFilesCommand = new AddFilesCommand(this);
            MoveItemCommand = new MoveItemCommand(this);
            OnWindowClosingCommand = new CloseWindowCommand(this);
            NextTrackCommand = new RelayCommand(NextTrack);
            PreviousTrackCommand = new RelayCommand(PreviousTrack);
        }

        #endregion

        #region METHODS

        #region FUNCTIONALITY METHODS

        /// <summary>
        /// Raises an LoadAudioFileEvent and sends selected track as argument
        /// </summary>
        /// <param name="o"></param>
        private void LoadSelectedTrackToBuffer(object o)
        {
            //Selected track becomes buffer track
            BufferTrack = SelectedTrack;

            //Raises en event about new file in buffer
            LoadSelectedAudioFileEvent?.Invoke(this, new AudioFileVMEventArgs(BufferTrack));
        }

        private void NextTrack(object o)
        {
            if (ShuffleOn)
                RandomTrackSet();
            else
                NextTrackSet();

            LoadSelectedTrackToBuffer(null);
        }

        private void PreviousTrack(object o)
        {
            if (ShuffleOn)
                RandomTrackSet();
            else
                PreviousTrackSet();

            LoadSelectedTrackToBuffer(null);
        }

        /// <summary>
        /// Sets next track in playlist as selected track.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextTrackSet()
        {
            //Gets index of next track
            var newIndex = SongsListObservable.IndexOf(BufferTrack) + 1;

            //if(LoopOn) //count - (count-index-1)

            //Checks if track is not last one
            if (newIndex < SongsListObservable.Count)
                //Changes track selection
                SelectedTrack = SongsListObservable[newIndex];

            //If playlist reaches an end
            else
            {
                //If loop is on then goes to first track in playlist
                if (LoopOn)
                    SelectedTrack = SongsListObservable[0];
            }
        }      

        /// <summary>
        /// Sets previous track in playlist as selected track.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousTrackSet()
        {
            //Gets index of current buffer track
            var index = SongsListObservable.IndexOf(BufferTrack);

            //Sets new index of selected track, if you press previous track on first track it will start over.
            index += index > 0 ? -1 : 0;

            //If there are songs in playlist
            if (index > -1)
                //Changes track selection
                SelectedTrack = SongsListObservable[index];
        }

        /// <summary>
        /// 
        /// </summary>
        private void RandomTrackSet()
        {
            SelectedTrack = SongsListObservable[random.Next(SongsListObservable.Count)];
        }

        #endregion

        #region PLAYLIST CRUD METHODS

        /// <summary>
        /// Adds tracks to observable collection.
        /// </summary>
        /// <param name="trackPath"></param>
        public void AddTracksToPlaylist(IEnumerable<string> tracksPaths)
        {
            foreach (var trackPath in tracksPaths)
            {
                var track = new AudioFileVM(trackPath);

                SongsListObservable.Add(track);
            }

            //Raises property changed on read only property
            OnPropertyChanged(nameof(Items));
        }

        /// <summary>
        /// Removes tracks from observable collection.
        /// </summary>
        /// <param name="o"></param>
        private void RemoveTracksFromPlaylist(object o)
        {
            //This wierd casting...
            System.Collections.IList items = (System.Collections.IList)o;
            var songs = items.Cast<AudioFileVM>();

            //Removes songs
            foreach (var song in songs)
                SongsListObservable.Remove(song);
        }

        /// <summary>
        /// Removes all tracks from playlist.
        /// </summary>
        /// <param name="o"></param>
        private void ClearPlaylist(object o)
        {
            ClearObservableCollection(null);
            GC.Collect();
        }

        /// <summary>
        /// Clears observable collection.
        /// </summary>
        /// <param name="collection"></param>
        private void ClearObservableCollection(IEnumerable<AudioFileVM> collection)
        {
            ////Makes new observable collection
            //SongsListObservable = collection != null ? new ObservableCollection<AudioFileVM>(collection)
            //                                         : new ObservableCollection<AudioFileVM>();

            SongsListObservable = new ObservableCollection<AudioFileVM>();

            //CollectionChanged does not work for new-ing collection...?
            OnPropertyChanged(nameof(SongsListObservable));

            //Raise property changed on read only property
            OnPropertyChanged(nameof(Items));
        }

        /// <summary>
        /// Switches two items in observable collection.
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

        #region SUBSCRIBTION METHODS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnAudioHasEnded(object sender, EventArgs e)
        {
            //Checks if playlist has ended 
            if (!LoopOn && SongsListObservable.IndexOf(BufferTrack) + 1 == SongsListObservable.Count)
                PlaylistHasEndedEvent?.Invoke(this, null);

            //If not, plays next track
            else
                NextTrack(null);
        }

        /// <summary>
        /// Saves playlist to file.
        /// </summary>
        public void OnWindowClosing()
        {
            model = new Playlist();
            model.SongsList = SongsListObservable.Select(x => x.Path);
            model.SavePlaylist();
        } 

        #endregion
    }

    #endregion

}

