using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAudioPlayer
{
    public interface IAudioPlayer
    {
        event EventHandler<string> AudioLoadedEvent;
        event EventHandler AudioPausedEvent;
        event EventHandler AudioStartedEvent;

        Task PlayAudio(string audioUrl);
        Task PauseCurrentAudio();
        Task ResumeCurrentAudio();
        Task<TimeSpan> GetCurrentAudioDuration();
    }
}
