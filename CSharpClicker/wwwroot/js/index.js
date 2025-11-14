document.addEventListener('DOMContentLoaded', function () {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl('/clickerHub')
        .withAutomaticReconnect()
        .build();

    connection.start()
        .then(function () {
            console.log('Connection started');
        })
        .catch(function (err) {
            return console.error(err.toString());
        });

    connection.on('ScoreUpdated', function (current, record) {
        const currentScoreElement = document.getElementById('currentScore');
        const recordScoreElement = document.getElementById('recordScore');

        currentScoreElement.textContent = current;
        recordScoreElement.textContent = record;
    });

    const clickButton = document.getElementById('click-item');
    clickButton.addEventListener('click', async function () {
        const clickCount = 1; // You can change this to the actual click count
        await connection.invoke('RegisterClicks', clickCount);
    });

});