using AudioPlayerNAudio;
using System;
using System.Collections.ObjectModel;

namespace AudioPlayerMVVMandNAudio
{
    /*---- commit 3
    TO DO:
    GUI:
        *Everything smaller
        *What font
        *Resizing window and fixed control size.
        *Resizing/minimizing controls with button clicks
        *Loop image
        *Random image
        *Style sliders and whole player
        *Style min/max/close window buttons
        *Slider drag and move in XAML if possbile
        *Popup time to go on mouse position
        *Logo
        
        *Better comments
        
        *Files/projects structure
        *Exceptions error logger
        *Fix whole player logic!
        *Not to fire stopped by user event all the time
        *Make play icon functionality
        *Keyboard keys functionality
        *File saving/loading WHEN?
        *Updating model - best way
        *Clear playlist event so info can clear
            +DONE (Title bar - minimize/close window etc)
            +DONE (slider customization)
            +DONE (VolumE slider customization)
        *ID Numbers of tracks
            +DONE (*Selected/playing file info display)
            +DONE (*Playlist item display and customization)
            +DONE Playlist slider customization      
            +DONE Playlist functionality: add files, 
            +DONE Playlist functionality: remove file,
            +DONE Playlist functionality: clear playlist,
            +DONE Playlist functionality: remove multiple files
            +DONE Playlist functionality: save/load default playlist
        **Playlist functionality: save playlist,
        **Playlist functionality: laod playlist,
        **Playlist functionality: drag and move tracks in playlist
            +DONE Playlist items on property changed
        *Filter OpenFileDialog files
            +DONE Wav file name 
            +DONE Drag and drop files into playlist
        *Drag and drop folders into playlist
        *MVVM Drag and drop files into playlist
        *Validation rules for files selected
        *Volume in decibels 
        *Playmode random
        *Playmode loop
        
       
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

            //Subscribes AudioPlayerVM to PlaylistVM events:
            PlaylistVM.LoadAudioFileEvent += AudioPlayerVM.OnAudioFileLoaded;
            PlaylistVM.PlaylistEndedEvent += AudioPlayerVM.OnPlaylistEnded;

            //Subscribes ApplicationVM to PlaylistVM event
            PlaylistVM.LoadAudioFileEvent += OnAudioFileLoaded;

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
    }
}
