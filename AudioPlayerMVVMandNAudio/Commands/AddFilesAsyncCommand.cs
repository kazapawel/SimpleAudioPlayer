using System.Threading.Tasks;

namespace AudioPlayerMVVMandNAudio
{
    public class AddFilesAsyncCommand : AsyncCommandBase
    {
        private readonly PlaylistVM playlistVM;

        public AddFilesAsyncCommand(PlaylistVM playlistVM)
        {
            this.playlistVM = playlistVM;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            await playlistVM.AddTracksToPlaylistAsync(playlistVM.IncomingFiles);
        }
    }
}
