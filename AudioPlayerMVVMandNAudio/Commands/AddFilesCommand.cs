
namespace AudioPlayerMVVMandNAudio
{
    public class AddFilesCommand : CommandBase
    {
        private readonly PlaylistVM playlistVM;

        public AddFilesCommand(PlaylistVM vm)
        {
            playlistVM = vm;
        }

        public override void Execute(object parameter)
        {
            playlistVM.AddTracksToPlaylist(playlistVM.IncomingFiles);
        }
    }
}
