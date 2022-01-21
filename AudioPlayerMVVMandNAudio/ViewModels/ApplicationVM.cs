using AudioPlayerNAudio;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AudioPlayerMVVMandNAudio
{
    /*
     * 
    TO DO:
            +DONE Drag and drop to reorder items in playlist
            +DONE Volume slider and position slider drag and move
        *Volume slider not linear but expotential
        *Playlist import to file implementation
        *Options buttons images
        *New transport buttons
        +DONE Fix selected/buffer track problem
        *Fix code duplication in views code behind
        *Application logic: dop not raise nexttrackrequest and stoppedbeforeend sim.
        *Buffer track as audioinfoVM

    GUI look:
            +DONE Not to resize playlist - fixed values, min values etc.
        *!Current playing icon remake
        *Resizing/minimizing controls with button clicks - view model for manipulating 
        *Loop playback image
        *Random playback image
        *Style min/max/close window buttons       
        *Popup time to go on mouse position
        *Logo
    
    GUI logic:
        *Display time time with correct format: 0m:0s ex:  04:03
            +DONE Time display - not to display hours but total minutes
        *Keyboard keys functionality (ex: space - play/pause etc)
        *Remeber player settings after restart - file and viewmodel
        *Track options right click
        
    PLAYER logic
            +DONE Implement AudioInfo again
            +DONE Fix problem -> playslit logic - request track -> change selected track -> load track (buffer=selected) -> raise an event
            +DONE Fix problem -> audio plays, clear playlist, audio stop by user, clear buffer
            +DONE Fix order of adding files when drop - first files than directories
            +DONE Fix problem => change play button when audio ends and playlist ends
            +DONE Seperate viewmodel for audioFileinfo - applicationVM only for coordinate others
        *Catch exception in playlist when creating audiofile (wrong file format)
        *MP3 loads to slow - change this in audio library implementation
            +DONE "Clear playlist" event so infoVM and player can clear
        *Exceptions error logger
        *Validation rules for files selected
        *Playlist loading/saving - when (On program close)
        *Playmode randome
        *Playmode loop
        *Filter OpenFileDialog files
            +DONE Drag and drop directories into playlist       
            +DONE Drag and drop files also on player, not only playlist
        **Playlist functionality: save playlist,
        **Playlist functionality: laod playlist,
        **Playlist functionality: drag and move tracks in playlist
     
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