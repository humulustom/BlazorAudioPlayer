using BlazorAudioPlayer.Wrappers;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlazorAudioPlayer
{
    public class AudioPlayer : IAudioPlayer, IDisposable
    {
        public event EventHandler<string> AudioLoadedEvent;
        public event EventHandler AudioPausedEvent;
        public event EventHandler AudioStartedEvent;

        private readonly IAudioWrapper _audioWrapper;
        private readonly ILogger<AudioPlayer> _logger;

        public AudioPlayer(IAudioWrapper audioWrapper, ILogger<AudioPlayer> logger)
        {
            _audioWrapper = audioWrapper;

            _audioWrapper.AudioLoadedEvent += _audioWrapper_AudioLoadedEvent;
            _audioWrapper.AudioPausedEvent += (s,e) => AudioPausedEvent?.Invoke(this, e);
            _audioWrapper.AudioStartedEvent += (s, e) => AudioStartedEvent?.Invoke(this, e);

            _logger = logger;
        }

        private void _audioWrapper_AudioLoadedEvent(object sender, string e)
        {
            AudioLoadedEvent?.Invoke(this, e);
        }

        public async Task PlayAudio(string audioUrl)
        {
            _logger.LogDebug($"Playing audio {audioUrl}");
            try
            {
                await _audioWrapper.StopAudio();
                await _audioWrapper.PlayAudio(audioUrl);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Exception caught when playing audio {e.Message}");
                //TODO: Add proper exceptions
                throw e; //Rethrow
            }
        }

        public async Task PauseCurrentAudio()
        {
            _logger.LogDebug($"Pausing audio");
            try
            {
                await _audioWrapper.PauseCurrentAudio();
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Exception caught when pausing audio {e.Message}");
                //TODO: Add proper exceptions
                throw e; //Rethrow
            }
        }

        public async Task ResumeCurrentAudio()
        {
            _logger.LogDebug($"Resuming audio");
            try
            {
                await _audioWrapper.ResumeCurrentAudio();
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Exception caught when resuming audio {e.Message}");
                //TODO: Add proper exceptions
                throw e; //Rethrow
            }
        }

        public Task<TimeSpan> GetCurrentAudioDuration()
        {
            return _audioWrapper.GetCurrentAudioDuration();
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
