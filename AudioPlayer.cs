using BlazorAudioPlayer.Wrappers;
using Microsoft.Extensions.Logging;
using System;

namespace BlazorAudioPlayer
{
    public class AudioPlayer : IAudioPlayer, IDisposable
    {
        private readonly IAudioWrapper _audioWrapper;
        private readonly ILogger<AudioPlayer> _logger;

        public AudioPlayer(IAudioWrapper audioWrapper, ILogger<AudioPlayer> logger)
        {
            _audioWrapper = audioWrapper;
            _logger = logger;
        }

        public void PlayAudio(string audioUrl)
        {
            _logger.LogDebug($"Playing audio {audioUrl}");
            try
            {
                _audioWrapper.StopAudio();
                _audioWrapper.PlayAudio(audioUrl);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Exception caught when playing audio {e.Message}");
                //TODO: Add proper exceptions
                throw e; //Rethrow
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    var audioWrapperDisposable = _audioWrapper as IDisposable;
                    audioWrapperDisposable.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
