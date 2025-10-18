document.addEventListener('DOMContentLoaded', function () {
    const botBoard = document.getElementById('bot-board');

    if (botBoard) {
        botBoard.addEventListener('click', function (e) {
            const cell = e.target;
            if (cell.classList.contains('cell') && (cell.classList.contains('unknown'))) {
                const row = cell.dataset.row;
                const col = cell.dataset.col;

                // Gửi yêu cầu bắn đến server
                fetch('/Game/Shoot', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded',
                    },
                    body: `row=${row}&col=${col}`
                })
                .then(response => response.json())
                .then(gameState => {
                    // Cập nhật giao diện với trạng thái game mới
                    updateUI(gameState);
                });
            }
        });
    }
});

function updateUI(gameState) {
    // Cập nhật header
    const header = document.querySelector('.game-header');
    if (gameState.isGameOver) {
        header.querySelector('.alert').className = 'alert alert-success';
        header.querySelector('.alert').textContent = gameState.winnerMessage;
    } else {
        header.querySelector('.alert').className = 'alert alert-info';
        header.querySelector('.alert').textContent = `Lượt của: ${gameState.players[gameState.currentPlayerIndex].name}. Lượt bắn thứ: ${gameState.turnCount}`;
    }

    // Cập nhật bàn cờ người chơi
    const playerBoard = document.getElementById('player-board');
    updateBoard(playerBoard, gameState.players[0].board, false);

    // Cập nhật bàn cờ BOT
    const botBoard = document.getElementById('bot-board');
    updateBoard(botBoard, gameState.players[1].board, true);
}

function updateBoard(boardElement, boardData, isOpponent) {
    const cells = boardElement.querySelectorAll('.cell');
    let cellIndex = 0;
    for (let r = 0; r < 7; r++) {
        for (let c = 0; c < 7; c++) {
            const cellState = boardData.grid[r][c];
            let cssClass = '';

            // Ánh xạ enum value (0, 1, 2, 3) sang tên class
            switch(cellState) {
                case 0: cssClass = 'empty'; break;
                case 1: cssClass = 'ship'; break;
                case 2: cssClass = 'hit'; break;
                case 3: cssClass = 'miss'; break;
            }

            if (isOpponent && (cssClass === 'ship' || cssClass === 'empty')) {
                cssClass = 'unknown';
            }
            
            cells[cellIndex].className = `cell ${cssClass}`;
            cellIndex++;
        }
    }
}