using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAudioPlayer.Wrappers
{
    public interface IAudioWrapper
    {
        event EventHandler<string> AudioLoadedEvent;
        event EventHandler AudioPausedEvent;
        event EventHandler AudioStartedEvent;
        Task StopAudio();
        Task PlayAudio(string audioUrl);
        Task PauseCurrentAudio();
        Task ResumeCurrentAudio();
        Task<TimeSpan> GetCurrentAudioDuration();
        Task<TimeSpan> GetCurrentAudioPosition();
    }
}
