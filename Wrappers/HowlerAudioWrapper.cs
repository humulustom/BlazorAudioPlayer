﻿using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorAudioPlayer.Wrappers
{
    public class HowlerAudioWrapper : IAudioWrapper, IDisposable
    {
        public event EventHandler<string> AudioLoadedEvent;
        public event EventHandler AudioPausedEvent;
        public event EventHandler AudioStartedEvent;

        private readonly IJSRuntime _jsRuntime;
        private readonly Guid _guid = Guid.NewGuid();

        public HowlerAudioWrapper(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
        }

        public Task PlayAudio(string audioUrl)
        {
            return _jsRuntime.InvokeVoidAsync(
                "howlerAudioPlayers.playAudio",
                _guid.ToString(),
                audioUrl,
                DotNetObjectReference.Create(this)).AsTask();
        }

        public Task StopAudio()
        {
            return _jsRuntime.InvokeVoidAsync("howlerAudioPlayers.stopAudio", _guid.ToString()).AsTask();
        }

        public Task PauseCurrentAudio()
        {
            return _jsRuntime.InvokeVoidAsync("howlerAudioPlayers.pauseAudio", _guid.ToString()).AsTask();
        }

        public Task ResumeCurrentAudio()
        {
            return _jsRuntime.InvokeVoidAsync("howlerAudioPlayers.resumeAudio", _guid.ToString()).AsTask();
        }

        public async Task<TimeSpan> GetCurrentAudioDuration()
        {
            var durationInSeconds = await _jsRuntime.InvokeAsync<double>("howlerAudioPlayers.getCurrentAudioDuration", _guid.ToString());
            return TimeSpan.FromSeconds(durationInSeconds);
        }

        public async Task<TimeSpan> GetCurrentAudioPosition()
        {
            var positionInSeconds = await _jsRuntime.InvokeAsync<double>("howlerAudioPlayers.getCurrentAudioPosition", _guid.ToString());
            return TimeSpan.FromSeconds(positionInSeconds);
        }

        [JSInvokable]
        public void AudioLoaded(string audioUrl)
        {
            AudioLoadedEvent?.Invoke(this, audioUrl);
        }

        [JSInvokable]
        public void AudioPaused()
        {
            AudioPausedEvent?.Invoke(this, EventArgs.Empty);
        }

        [JSInvokable]
        public void AudioPlayed()
        {
            AudioStartedEvent?.Invoke(this, EventArgs.Empty);
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
