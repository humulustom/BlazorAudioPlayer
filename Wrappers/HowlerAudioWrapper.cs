using Microsoft.JSInterop;
using System;

namespace BlazorAudioPlayer.Wrappers
{
    class HowlerAudioWrapper : IAudioWrapper, IDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly Guid _guid = Guid.NewGuid();

        public HowlerAudioWrapper(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
        }

        public void PlayAudio(string audioUrl)
        {
            _jsRuntime.InvokeVoidAsync("howlerAudioPlayers.playAudio", _guid.ToString(), audioUrl);
        }

        public void StopAudio()
        {
            _jsRuntime.InvokeVoidAsync("howlerAudioPlayers.stopAudio", _guid.ToString());
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    try
                    {
                        _jsRuntime.InvokeVoidAsync("howlerAudioPlayers.disposeAudioPlayer", _guid.ToString());
                    }
                    catch(Exception e)
                    {
                        //TODO: Should we log from wrapper?
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
