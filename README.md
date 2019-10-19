# BlazorAudioPlayer

###Description
Simple audio player for Blazor Client Side (not tested with Server Side as of yet). From technical point of view it is a simple wrapper for howler.js (see https://howlerjs.com/). This library is in the earliest stages of development, and as of yet offers only very limited functionality. 

###License
Project is available with MIT licencse. Basically you can use it in whatever way you want as long as you don't claim it is yours and include the license text in your distribution. 

###How to use 
Download the project and add reference to it (nuget package may be available in the future). In you client register the Audio Player in Startup.cs:

```
public void ConfigureServices(IServiceCollection services)
{
	services.AddBlazorAudioPlayer();
}
```
In your component inject the IAudioPlayer and use PlayAudio function to play audio from given url
```
@inject IAudioPlayer AudioPlayer;
...
void PlayAudio(string audioUrl)
{
	AudioPlayer.PlayAudio(audioUrl);
}

```
AudioPlayer is designed to play one audio stream at a time. You can freely call PlayAudio on another source, it will stop the current audio and immediately start playing the next one. 

AudioPlayer is designed to work as transient, disposable service. The audio will currently stop when it is disposed. To allow for automatic disposal it is advised to use OwningComponentBase<IAudioPlayer> instead of inject:

```
@inherits OwningComponentBase<IAudioPlayer>
...
void PlayAudio(string audioUrl)
{
	Service.PlayAudio(audioUrl);
}
```

###Future and requesting features
This project is currently at extreamly early phase of development. I am creating it mostly for my own big project which needs rich audio playing functionality, so it will most likely receive many new features in the comming weeks/days. Still if you notice any bugs and/or have a need for a particular feature feel free to create an issue for it, I will try to respond as quickly as possible.
