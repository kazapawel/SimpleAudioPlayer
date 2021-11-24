using System;

namespace AudioPlayerMVVMandNAudio
{
    public class AudioFileInfoVM : BaseViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;


        public AudioFileInfoVM(AudioFileVM file)
        {
            Title = file.TrackTitle;
            Album = file.Album;
            Artist = file.Artist;
        }

        public AudioFileInfoVM()
        { }
    }
}
