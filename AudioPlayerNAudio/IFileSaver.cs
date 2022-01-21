using System;
using System.Collections.Generic;
using System.Text;

namespace AudioPlayerNAudio
{
    public interface IFileSaver
    {
        void ImportFromFile();
        void ExportToFile();
    }
}
