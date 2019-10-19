(function () {

    var audioPlayers = {};

    window.howlerAudioPlayers = {
        playAudio: function (playerGuid, audioUrl) {
            if (!(playerGuid in audioPlayers))
            {
                addPlayer(playerGuid, audioUrl)
            }
            
            try{
                var howl = audioPlayers[playerGuid];
                howl.unload(true);
                howl._src = audioUrl;
                howl.load();
                howl.play();
            } catch(error) {
                console.log(error)
            }
        },

        stopAudio: function (playerGuid, playerId) {
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
        }
    };
    
    function addPlayer (guid, audioUrl) {
        var howl = new Howl({
            src: [audioUrl],
            html5: true
        });
        audioPlayers[guid] = howl;
    }
})();