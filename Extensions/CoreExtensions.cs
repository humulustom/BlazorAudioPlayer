using BlazorAudioPlayer.Wrappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAudioPlayer.Extensions
{
    public static class CoreExtensions
    {
        public static void AddBlazorAudioPlayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAudioWrapper, HowlerAudioWrapper>();
            serviceCollection.AddTransient<IAudioPlayer, AudioPlayer>();
        }
    }
}
