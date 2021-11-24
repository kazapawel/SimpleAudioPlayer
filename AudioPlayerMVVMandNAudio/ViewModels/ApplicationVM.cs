using AudioPlayerNAudio;
using System;
using System.Collections.ObjectModel;

namespace AudioPlayerMVVMandNAudio
{
    /*----
    TO DO:

        *Everything smaller
        *Better comments
        *Resizing window and fixed control size.
        *Files/projects structure
        *Not to fire stopped by user event all the time
            +DONE (Title bar - minimize/close window etc)
            +DONE (slider customization)
            +DONE (VolumE slider customization)
        *ID Numbers of tracks
            +DONE (*Selected/playing file info display)
            +DONE (*Playlist item display and customization)
            +DONE Playlist slider customization
        *Slider drag and move in XAML if possbile
            +DONE Playlist functionality: add files, 
            +DONE Playlist functionality: remove file,
            +DONE Playlist functionality: clear playlist,
            +DONE Playlist functionality: remove multiple files
        **Playlist functionality: save default playlist
        **Playlist functionality: save playlist,
        **Playlist functionality: laod playlist,
        **Playlist functionality: drag and drop tracks in playlist
        *Filter OpenFileDialog files
        *Drag and drop files into playlist
        *Volume in decibels
        *Random image
        *Loop image
        *Playmode random
        *Playmode loop
        *Style min/max/close window buttons
        *Style sliders and whole player

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
            AudioPlayerVM.StopAudioByUserEvent += OnAudioStoppedByUser;
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
