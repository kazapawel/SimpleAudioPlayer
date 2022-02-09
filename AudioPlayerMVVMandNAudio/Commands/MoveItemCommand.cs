
namespace AudioPlayerMVVMandNAudio
{
    public class MoveItemCommand : CommandBase
    {
        private readonly PlaylistVM playlistVM;

        public MoveItemCommand(PlaylistVM vm)
        {
            playlistVM = vm;
        }
        public override void Execute(object parameter)
        {
            playlistVM.MoveItem(playlistVM.TargetItem, playlistVM.MovedItem);
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
