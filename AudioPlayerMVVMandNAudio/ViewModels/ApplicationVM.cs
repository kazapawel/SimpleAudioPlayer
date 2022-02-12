using AudioPlayerNAudio;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AudioPlayerMVVMandNAudio
{
    /*
     * 
    TO DO:
            +DONE Allow drop on playlistitem
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
        public AudioPlayerVM AudioPlayerVM { get; set; }

        /// <summary>
        /// View model for playlist
        /// </summary>
        public PlaylistVM PlaylistVM { get; set; }

        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ApplicationVM()
        {
            PlaylistVM = new PlaylistVM();
            AudioPlayerVM = new AudioPlayerVM();

            //Subscribes AUDIOPLAYER to PlaylistVM events:
            PlaylistVM.LoadSelectedAudioFileEvent += AudioPlayerVM.OnSelectedAudioFileLoaded;
            PlaylistVM.PlaylistHasEndedEvent += AudioPlayerVM.OnPlaylistEnded;

            //Subscribes PLAYLIST to AudioPlayerVM events:
            AudioPlayerVM.AudioHasEndedEvent += PlaylistVM.OnAudioHasEnded;
        }
    }
}