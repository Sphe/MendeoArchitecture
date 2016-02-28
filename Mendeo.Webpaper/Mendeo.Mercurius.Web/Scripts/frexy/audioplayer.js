$(document).ready(function(){

    new jPlayerPlaylist({
        jPlayer: "#jquery_jplayer_1",
        cssSelectorAncestor: "#jp_container_1",
        cssSelector: {
            stop: ''
        }
    }, [
        {
            title:"Lorem ipsum",
            mp3:"",
            oga:""
        }
    ], {
        swfPath: "js",
        supplied: "oga, mp3",
        wmode: "window",
        smoothPlayBar: true,
        keyEnabled: true
    });

    new jPlayerPlaylist({
        jPlayer: "#jquery_jplayer_2",
        cssSelectorAncestor: "#jp_container_2",
        cssSelector: {
            stop: ''
        }
    }, [
        {
            title:"Lorem ipsum",
            mp3:"",
            oga:""
        }
    ], {
        swfPath: "js",
        supplied: "oga, mp3",
        wmode: "window",
        smoothPlayBar: true,
        keyEnabled: true
    });

    new jPlayerPlaylist({
        jPlayer: "#jquery_jplayer_3",
        cssSelectorAncestor: "#jp_container_3",
        cssSelector: {
            stop: ''
        }
    }, [
        {
            title:"Lorem ipsum",
            mp3:"",
            oga:""
        }
    ], {
        swfPath: "js",
        supplied: "oga, mp3",
        wmode: "window",
        smoothPlayBar: true,
        keyEnabled: true
    });

});