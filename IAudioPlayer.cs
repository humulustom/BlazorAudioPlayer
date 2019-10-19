using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAudioPlayer
{
    public interface IAudioPlayer
    {
        void PlayAudio(string audioUrl);
    }
}
