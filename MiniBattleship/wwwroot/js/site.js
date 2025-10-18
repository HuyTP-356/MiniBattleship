document.addEventListener('DOMContentLoaded', function () {
    const botBoard = document.getElementById('bot-board');
    // Lấy token từ thẻ input ẩn mà Razor đã tạo
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    if (botBoard) {
        botBoard.addEventListener('click', function (e) {
            const cell = e.target;
            const gameState = document.getElementById('game-status').textContent;

            // Chỉ cho phép bắn khi game chưa kết thúc và đó là lượt của người chơi
            if (cell.classList.contains('cell') && cell.classList.contains('unknown') && !gameState.includes("thắng")) {
                const row = cell.dataset.row;
                const col = cell.dataset.col;

                // Gửi yêu cầu bắn đến server
                fetch('/Game/Shoot', {
                    method: 'POST',
                    headers: {
                        // Thêm token vào header của request
                        'RequestVerificationToken': token,
                        'Content-Type': 'application/x-www-form-urlencoded',
                    },
                    body: `row=${row}&col=${col}`
                })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Network response was not ok');
                        }
                        return response.json();
                    })
                    .then(newGameState => {
                        // Cập nhật toàn bộ giao diện với trạng thái game mới
                        updateUI(newGameState);
                    })
                    .catch(error => {
                        console.error('There has been a problem with your fetch operation:', error);
                    });
            }
        });
    }
});

function updateUI(gameState) {
    // Cập nhật thông báo trạng thái
    const statusDiv = document.getElementById('game-status');
    if (gameState.isGameOver) {
        statusDiv.className = 'alert alert-success';
        statusDiv.textContent = gameState.winnerMessage;
    } else {
        statusDiv.className = 'alert alert-info';
        statusDiv.textContent = `Lượt của: ${gameState.players[gameState.currentPlayerIndex].name}. Lượt bắn thứ: ${gameState.turnCount}`;
    }

    // Cập nhật bàn cờ người chơi (để hiển thị đòn tấn công của bot)
    const playerBoard = document.getElementById('player-board');
    updateBoard(playerBoard, gameState.players[0].board, false);

    // Cập nhật bàn cờ BOT (để hiển thị kết quả bắn của người chơi)
    const botBoard = document.getElementById('bot-board');
    updateBoard(botBoard, gameState.players[1].board, true);
}

function updateBoard(boardElement, boardData, isOpponent) {
    const cells = boardElement.querySelectorAll('.cell');
    let cellIndex = 0;
    for (let r = 0; r < 7; r++) {
        for (let c = 0; c < 7; c++) {
            const cellStateValue = boardData.grid[r][c];
            let cssClass = '';

            // Ánh xạ enum value (0:Empty, 1:Ship, 2:Hit, 3:Miss) sang tên class CSS
            switch (cellStateValue) {
                case 0: cssClass = 'empty'; break;
                case 1: cssClass = 'ship'; break;
                case 2: cssClass = 'hit'; break;
                case 3: cssClass = 'miss'; break;
            }

            // Nếu là bàn cờ đối thủ, ẩn vị trí tàu chưa bị bắn
            if (isOpponent && (cssClass === 'ship' || cssClass === 'empty')) {
                cssClass = 'unknown';
            }

            cells[cellIndex].className = `cell ${cssClass}`;
            cellIndex++;
        }
    }
}