using System;

namespace AudioPlayerNAudio
{
    public interface IAudioFilePlayer
    {
        string Path { get; set; }

        double Volume { get; set; }

        bool IsReady { get; }

        TimeSpan TimeCurrent { get; }

        TimeSpan TimeTotal { get; }

        long StreamPosition { get; set; }

        long StreamLength { get;}

        event EventHandler AudioHasEndedEvent;

        void PlayAudio();

        void StopAudio();

        void PauseAudio();

    }
}
