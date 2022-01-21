using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayerMVVMandNAudio
{
    public class CloseWindowCommand : CommandBase
    {
        private readonly ICloseWindow viewModel;

        public CloseWindowCommand(ICloseWindow vm)
        {
            viewModel = vm;
        }

        public override void Execute(object parameter)
        {
            viewModel.OnClosed();
        }
    }
}
