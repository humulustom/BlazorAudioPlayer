using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAudioPlayer.Wrappers
{
    public interface IAudioWrapper
    {
        void StopAudio();
        void PlayAudio(string audioUrl);
    }
}
