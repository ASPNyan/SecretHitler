window.audioFunctions = {
    playTypewriter: async function () {
        let number = Math.floor(Math.random() * 5 + 1);
        let audio = new Audio(`mp3/typewriter-0${number}.mp3`);
        await audio.play();
    },
    playTypewriterEnd: async function () {
        let audio = new Audio("mp3/typewriter-end.mp3");
        await audio.play();
    }
}