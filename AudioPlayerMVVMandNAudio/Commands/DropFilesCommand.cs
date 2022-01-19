
namespace AudioPlayerMVVMandNAudio
{
    public class DropFilesCommand : CommandBase
    {
        private readonly PlaylistVM playlistVM;

        public DropFilesCommand(PlaylistVM vm)
        {
            playlistVM = vm;
        }
        public override void Execute(object parameter)
        {
            playlistVM.AddTracksToPlaylist(playlistVM.IncomingFiles);
        }
    }
}
