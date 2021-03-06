using AudioPlayerNAudio;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System.Threading.Tasks;

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

        private bool info;

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

        /// <summary>
        /// Flag that indicates loop state.
        /// </summary>
        public bool LoopOn { get; set; }

        /// <summary>
        /// Flag that indicates random play state.
        /// </summary>
        public bool ShuffleOn { get; set; }

        /// <summary>
        /// Gets number of items in playlist.
        /// </summary>
        public string Items => SongsListObservable.Count.ToString();

        /// <summary>
        /// Flag to inform about adding files.
        /// </summary>
        public bool Info
        {
            get => info;
            set
            {
                if (info != value)
                {
                    info = value;
                    OnPropertyChanged(nameof(Info));
                }
            }
        }      

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
        /// Occurs when selected file is loaded into "buffer".
        /// </summary>
        public event EventHandler<AudioFileVMEventArgs> LoadSelectedAudioFileEvent;

        /// <summary>
        /// Occurs when playlist reaches an end.
        /// </summary>
        public event EventHandler PlaylistHasEndedEvent;

        #endregion

        #region COMMANDS

        public ICommand LoadSelectedTrackCommand { get; set; }

        public ICommand RemoveTracksFromPlaylistCommand { get; set; }

        public ICommand ClearPlaylistCommand { get; set; }

        public ICommand AddFilesAsyncCommand { get; set; }

        public ICommand AddFilesCommand { get; set; }

        public ICommand MoveItemCommand { get; set; }

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
            AddTracksToPlaylist(model.SongsList);

            model = null;

            //Commands
            LoadSelectedTrackCommand = new RelayCommand(LoadSelectedTrackToBuffer);
            RemoveTracksFromPlaylistCommand = new RelayCommand(RemoveTracksFromPlaylist);
            ClearPlaylistCommand = new RelayCommand(ClearPlaylist);
            AddFilesCommand = new AddFilesCommand(this);
            AddFilesAsyncCommand = new AddFilesAsyncCommand(this);
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
            //No action if file is corrupted
            if (SelectedTrack.IsCorrupted)
                return;

            //Selected track becomes buffer track
            BufferTrack = SelectedTrack;

            //Raises en event about new file in buffer
            LoadSelectedAudioFileEvent?.Invoke(this, new AudioFileVMEventArgs(BufferTrack));
        }

        /// <summary>
        /// Sets next track in playlist.
        /// </summary>
        /// <param name="o"></param>
        private void NextTrack(object o)
        {
            if (ShuffleOn)
                RandomTrackSet();

            //If playlist has ended raises an event an returns
            else if (!LoopOn && SongsListObservable.IndexOf(BufferTrack) + 1 == SongsListObservable.Count)
            {
                PlaylistHasEndedEvent?.Invoke(this, null);
                return;
            }
            else
                NextTrackSet();

            LoadSelectedTrackToBuffer(null);
        }

        /// <summary>
        /// Sets previous track.
        /// </summary>
        /// <param name="o"></param>
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
        /// Sets random track in playlist as selected track.
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
        /// <param name="tracksPaths"></param>
        /// <returns></returns>
        public async Task AddTracksToPlaylistAsync(IEnumerable<string> tracksPaths)
        {
            Info = true;

            SongsListObservable = await Task.Run(() => AddItems(tracksPaths));

            //CollectionChanged does not work for new-ing collection
            OnPropertyChanged(nameof(SongsListObservable));

            //Raises property changed on read only property
            OnPropertyChanged(nameof(Items));

            Info = false;

            GC.Collect();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tracksPaths"></param>
        /// <returns></returns>
        private ObservableCollection<AudioFileVM> AddItems(IEnumerable<string> tracksPaths)
        {
            var tracksCollection = new ObservableCollection<AudioFileVM>();

            //Adds present items to new collection
            foreach (var item in SongsListObservable)
                tracksCollection.Add(item);

            //Creates and adds new items
            foreach (var path in tracksPaths)
            {
                var track = new AudioFileVM(path);
                tracksCollection.Add(track);          
            }
            
            return tracksCollection;
        }

        /// <summary>
        /// Adds tracks to observable collection.
        /// </summary>
        /// <param name="trackPath"></param>
        public void AddTracksToPlaylist(IEnumerable<string> tracksPaths)
        {
            foreach (var path in tracksPaths)
            {
                var track = new AudioFileVM(path);

                SongsListObservable.Add(track);
            };

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

            //Removes tracks
            foreach (var track in songs.ToList())
                SongsListObservable.Remove(track);

            //Raise property changed on read only property
            OnPropertyChanged(nameof(Items));

            GC.Collect();
        }

        /// <summary>
        /// Removes all tracks from observable collection.
        /// </summary>
        /// <param name="o"></param>
        private void ClearPlaylist(object o)
        {
            SongsListObservable = new ObservableCollection<AudioFileVM>();

            //Raise property changed on read only properties
            OnPropertyChanged(nameof(SongsListObservable));
            OnPropertyChanged(nameof(Items));

            GC.Collect();
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

