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

        updateBoostsAvailability();
    });

    connection.on('ProfitUpdated', function (profitPerClick, profitPerSecond) {
        const profitPerClickElement = document.getElementById('profitPerClick');
        const profitPerSecondElement = document.getElementById('profitPerSecond');

        profitPerClickElement.textContent = profitPerClick;
        profitPerSecondElement.textContent = profitPerSecond;
    });

    connection.on('BoostUpdated', function (boostId, quantity, currentPrice) {
        // Quote the attribute value to ensure correct CSS selection (numbers can be problematic unquoted)
        const boostElement = document.querySelector(`[data-boost-id="${boostId}"]`);

        const priceElement = boostElement.querySelector('[data-boost-price]');
        const quantityElement = boostElement.querySelector('[data-boost-quantity]');

        priceElement.textContent = currentPrice;
        quantityElement.textContent = quantity;

        updateBoostsAvailability();
    });

    const clickButton = document.getElementById('click-item');
    clickButton.addEventListener('click', async function () {
        const clickCount = 1; // You can change this to the actual click count
        await connection.invoke('RegisterClicks', clickCount);
    });

    const boostElements = document.querySelectorAll('.boost-item');

    boostElements.forEach(function (boostElement) {
        const boostId = boostElement.getAttribute('data-boost-id');
        const buyButton = boostElement.querySelector('.buy-boost-button');

        buyButton.addEventListener('click', async function () {
            connection.invoke('BuyBoost', parseInt(boostId, 10));
        });
    });

    updateBoostsAvailability();

    function updateBoostsAvailability() {
        const currentScoreElement = document.getElementById('currentScore');

        const currentScore = parseInt(currentScoreElement.textContent, 10);

        boostElements.forEach(function (boostElement) {
            const priceElement = boostElement.querySelector('[data-boost-price]');
            const buyButton = boostElement.querySelector('.buy-boost-button');

            if (currentScore < parseInt(priceElement.textContent, 10)) {
                boostElement.classList.add('disabled');
                buyButton.disabled = true;
            } else {
                boostElement.classList.remove('disabled');
                buyButton.disabled = false;
            }
        });
    }
});