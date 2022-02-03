using AudioPlayerNAudio;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AudioPlayerMVVMandNAudio
{
    /*
     * 
    TO DO:
        *Allow drop on playlistitem
        *Volume slider - make it expotential, not linear
        *Position slider - update value after drag ends
        *Time popup - display value of dragged slider
        *Options buttons images
        *Application logic: do not raise nexttrackrequest and stoppedbeforend sim.
        *NEW PLAYER LOGIC - test it.
        *Keyboard bindings - space/pause, delete/delete, enter/play etc.
        *Resizing/minimizing controls with button clicks - view model for manipulating 
        *Remeber player settings after restart - file and viewmodel
        *Right click on playlsit item - options
        
    PLAYER logic
        *Catch exception in playlist when creating audiofile (wrong file format)
        *MP3 loads to slow - change this in audio library implementation
        *Exceptions error logger
        *Validation rules for files selected
        *Playmode random
        *Playmode loop
        *Filter OpenFileDialog files
        *Make red font when file is corrupted and icon when u cant play file
        **Playlist functionality: save playlist,
        **Playlist functionality: load playlist,
     
    PROJECT:
        *Better comments     
        *Files/projects structure      
    
    ----*/

    public class ApplicationVM : BaseViewModel
    {
        #region PUBLIC PROPERTIES

        /// <summary>
        /// View model for audio player functionality
        /// </summary>
        public TransportPanelVM TransportPanelVM { get; set; }

        /// <summary>
        /// View model for playlist
        /// </summary>
        public PlaylistVM PlaylistVM { get; set; }

        /// <summary>
        /// Info about current track
        /// </summary>
        public AudioInfoVM AudioInfoVM { get; set; }

        #endregion

        public ICommand CloseWindowCommand { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ApplicationVM()
        {
            PlaylistVM = new PlaylistVM();
            TransportPanelVM = new TransportPanelVM();
            AudioInfoVM = new AudioInfoVM();

            //Subscribes PLAYLIST to TransportPanelVM events:
            TransportPanelVM.AudioStartEvent += PlaylistVM.OnAudioStart;
            TransportPanelVM.NextTrackRequestEvent += PlaylistVM.OnNextTrackRequest;
            TransportPanelVM.PreviousTrackRequestEvent += PlaylistVM.OnPreviousTrackRequest;
            TransportPanelVM.StopAudioBeforeEndEvent += PlaylistVM.OnAudioStoppedBeforeEnd;

            //Subscribes TRANSPORT to PlaylistVM events:
            PlaylistVM.LoadAudioFileEvent += TransportPanelVM.OnAudioFileLoaded;
            PlaylistVM.PlaylistEndedEvent += TransportPanelVM.OnPlaylistEnded;
            PlaylistVM.PlaylistClearedEvent += TransportPanelVM.OnPlaylistCleared;

            //Subscribes AUDIO INFO to TransportPanelVM events:
            TransportPanelVM.StopAudioBeforeEndEvent += AudioInfoVM.OnAudioStoppedBeforeEnd;
            TransportPanelVM.AudioStartEvent += AudioInfoVM.OnAudioFileStart;

            //Subscribes AUDIO INFO to PlaylistVM events:
            PlaylistVM.PlaylistEndedEvent += AudioInfoVM.OnPlaylistEnded;
        }
    }
}