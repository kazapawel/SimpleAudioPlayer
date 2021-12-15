using AudioPlayerNAudio;
using System;
using System.Collections.ObjectModel;

namespace AudioPlayerMVVMandNAudio
{
    /*---- commit x
     * 
    TO DO:

    GUI look:
        *Not to resize playlist - fixed values, min values etc.
        *Transport buttons remake
        *Current playing icon remake
        *Options buttons remake - again
        *Resizing/minimizing controls with button clicks - view model for manipulating 
        *Loop playback image
        *Random playback image
        *Style min/max/close window buttons       
        *Popup time to go on mouse position
        *Logo
    
    GUI logic:
        *Slider drag and move in XAML if possbile
        *Drag and drop to reorder items in playlist
        *Time display - not to display hours but total minutes
        *Keyboard keys functionality (ex: space - play/pause etc)
        *Remeber player settings after restart - file and viewmodel
        *Track options right click
        
    PLAYER logic
        *Oddzielny viewmodel for audioFileifo - applicationVM only for coordinate others
        *Catch exception in playlist when creating audiofile (wrong file format)
        *MP3 loads to slow - change this in audio library implementation
            +DONE "Clear playlist" event so info can clear
        *Exceptions error logger
        *Validation rules for files selected
        *Playlist loading/saving - when (On program close)
        *Playmode random
        *Playmode loop
        *Volume in decibels
        *Filter OpenFileDialog files
            +DONE Drag and drop directories into playlist
        *Drag and drop files also on player, not only playlist
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
        /// 
        /// </summary>
        private AudioFileVM info;

        /// <summary>
        /// Audio file which is currently loaded in buffer
        /// </summary>
        public AudioFileVM AudioFileInfoVM
        {
            get
            {
                return info;
            }
            set
            {
                if(info!=value)
                {
                    info = value;
                    OnPropertyChanged(nameof(AudioFileInfoVM));
                }
            }
        }

        #endregion

        #region COMMANDS



        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ApplicationVM()
        {
            PlaylistVM = new PlaylistVM();
            AudioPlayerVM = new AudioPlayerVM();

            //Subscribes PlaylistVM to AudioPlayerVM events:
            AudioPlayerVM.NextTrackRequestEvent += PlaylistVM.OnNextTrackRequest;
            AudioPlayerVM.PreviousTrackRequestEvent += PlaylistVM.OnPreviousTrackRequest;
            AudioPlayerVM.StopAudioBeforeEndEvent += PlaylistVM.OnAudioStoppedBeforeEnd;

            //Subscribes AudioPlayerVM to PlaylistVM events:
            PlaylistVM.LoadAudioFileEvent += AudioPlayerVM.OnAudioFileLoaded;
            PlaylistVM.PlaylistEndedEvent += AudioPlayerVM.OnPlaylistEnded;

            //Subscribes ApplicationVM to PlaylistVM event
            PlaylistVM.LoadAudioFileEvent += OnAudioFileLoaded;
            PlaylistVM.PlaylistEndedEvent += OnPlaylistEnded;

            //Subscribes ApplicationVM to AudioPlayerVM event
            AudioPlayerVM.StopAudioBeforeEndEvent += OnAudioStoppedByUser;
            
        }

        /// <summary>
        /// Creates new instance of AudioFileVM, which represent info about current, active audio file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAudioFileLoaded(object sender, AudioFileVMEventArgs e)
        {
            //AudioFileInfoVM = new AudioFileInfoVM(e.AudioFileVM);
            if(AudioFileInfoVM is not null)
                AudioFileInfoVM.IsAudioFilePlaying = false;

            AudioFileInfoVM = e.AudioFileVM;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAudioStoppedByUser(object sender, EventArgs e)
        {
            if(AudioFileInfoVM !=null)
                AudioFileInfoVM.IsAudioFilePlaying = false;
        }

        private void OnPlaylistEnded(object sender, EventArgs e)
        {
            AudioFileInfoVM = null;
        }
    }
}
