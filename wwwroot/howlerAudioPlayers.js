(function () {

    var audioPlayers = {};

    window.howlerAudioPlayers = {
        playAudio (playerGuid, audioUrl, dotnetObject) {

            if (!(playerGuid in audioPlayers))
            {
                addPlayer(playerGuid, audioUrl)
            }
            
            try {
                var howl = audioPlayers[playerGuid];

                howl.unload(true);
                howl.off();
                howl._src = audioUrl;

                howl.on('load', function() {
                    dotnetObject.invokeMethodAsync('AudioLoaded', audioUrl);
                });
                howl.on('play', function () {
                    dotnetObject.invokeMethodAsync('AudioPlayed');
                });
                howl.on('pause', function () {
                    dotnetObject.invokeMethodAsync('AudioPaused');
                });
                howl.load();
                howl.play();
            } catch(error) {
                console.log(error)
            }
        },

        stopAudio (playerGuid) {
            if (!(playerGuid in audioPlayers))
            {
                return; //nothing to stop
            }
            
            try
            {
                var howl = audioPlayers[playerGuid];
                howl.stop();
            } catch(error) {
                console.log(error);
            }
        },

        pauseAudio (playerGuid) {
            if (!(playerGuid in audioPlayers))
            {
                return; //TODO: Add some error event
            }

            try {
                var howl = audioPlayers[playerGuid];
                howl.pause();
            } catch (error) {
                console.log(error); //TODO: Add some error event
            }
        },

        resumeAudio (playerGuid, seekPosition) {
            if (!(playerGuid in audioPlayers)) {
                return; //TODO: Add some error event
            }

            try {
                var howl = audioPlayers[playerGuid];
                howl.play();             
            } catch (error) {
                console.log(error); //TODO: Add some error event
            }
        },

        disposeAudioPlayer (playerGuid) {
            if (!(playerGuid in audioPlayers))
            {
                return; //nothing to stop
            }

            try
            {
                var howl = audioPlayers[playerGuid];
                howl.unload(true);
                delete audioPlayers[playerGuid]
            } catch(error) {
                console.log(error);
            }
        },

        getCurrentAudioDuration(playerGuid) {
            if (!(playerGuid in audioPlayers)) {
                return -1; //nothing to stop
            }

            try {
                var howl = audioPlayers[playerGuid];
                var duration = howl.duration();
                return duration;
            } catch (error) {
                console.log(error);
                return -1;
            }
        }
    };
    
    function addPlayer (guid, audioUrl) {
        try {
            var howl = new Howl({
                src: [audioUrl],
                html5: true
            });
            audioPlayers[guid] = howl;
        }
        catch (error) {
            console.log(error);
        }
    }
})();