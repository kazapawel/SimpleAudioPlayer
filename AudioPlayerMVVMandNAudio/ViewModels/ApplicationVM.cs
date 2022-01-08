﻿using AudioPlayerNAudio;
using System;
using System.Collections.ObjectModel;

namespace AudioPlayerMVVMandNAudio
{
    /*
     * 
    TO DO:

    GUI look:
            +DONE Not to resize playlist - fixed values, min values etc.
        *!Transport buttons remake - again
        *!Current playing icon remake
        *!Options buttons remake - again
        *!Playlist item style
        *Resizing/minimizing controls with button clicks - view model for manipulating 
        *Loop playback image
        *Random playback image
        *Style min/max/close window buttons       
        *Popup time to go on mouse position
        *Logo
    
    GUI logic:
        *!Slider drag and move in XAML if possbile
        *!Drag and drop to reorder items in playlist
            +DONE Time display - not to display hours but total minutes
        *Main Window view viewmodel etc.
        *Keyboard keys functionality (ex: space - play/pause etc)
        *Remeber player settings after restart - file and viewmodel
        *Track options right click
        *Volume slider nopt linear but expotential
        
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
            +DONE Volume in decibels
        *Volume
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
        public AudioPlayerVM AudioPlayerVM { get; set; }

        /// <summary>
        /// View model for playlist
        /// </summary>
        public PlaylistVM PlaylistVM { get; set; }

        /// <summary>
        /// Info about current track
        /// </summary>
        public AudioInfoVM AudioInfoVM { get; set; }

        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ApplicationVM()
        {
            PlaylistVM = new PlaylistVM();
            AudioPlayerVM = new AudioPlayerVM();
            AudioInfoVM = new AudioInfoVM();

            //Subscribes PLAYLIST to AudioPlayerVM events:
            Audio‌PlayerVM.AudioStartEvent += PlaylistVM.OnAudioStart;
            AudioPlayerVM.NextTrackRequestEvent += PlaylistVM.OnNextTrackRequest;
            AudioPlayerVM.PreviousTrackRequestEvent += PlaylistVM.OnPreviousTrackRequest;
            AudioPlayerVM.StopAudioBeforeEndEvent += PlaylistVM.OnAudioStoppedBeforeEnd;

            //Subscribes AUDIO PLAYER to PlaylistVM events:
            PlaylistVM.LoadAudioFileEvent += AudioPlayerVM.OnAudioFileLoaded;
            PlaylistVM.PlaylistEndedEvent += AudioPlayerVM.OnPlaylistEnded;
            PlaylistVM.PlaylistClearedEvent += AudioPlayerVM.OnPlaylistCleared;

            //Subscribes AUDIO INFO to PlaylistVM event
            //PlaylistVM.LoadAudioFileEvent += AudioInfoVM.OnAudioFileLoaded;
            //PlaylistVM.PlaylistEndedEvent += AudioInfoVM.OnPlaylistEnded;

            //Subscribes AUDIO INFO to AudioPlayerVM event
            AudioPlayerVM.StopAudioBeforeEndEvent += AudioInfoVM.OnAudioStoppedBeforeEnd;
            AudioPlayerVM.AudioStartEvent += AudioInfoVM.OnAudioFileStart;
        }
    }
}
