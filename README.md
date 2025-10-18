**1. Mini Battleship - Game Tàu Chiến .NET MVC**

Đây là một dự án game Tàu chiến (Battleship) đơn giản được xây dựng bằng ASP.NET Core MVC (.NET 8). Người chơi sẽ đối đầu với một máy tính (BOT) có trí tuệ nhân tạo (AI) cơ bản. Dự án này nhằm mục đích minh họa các khái niệm cốt lõi của .NET MVC, bao gồm Routing, Models, Views, Controllers, và cách quản lý trạng thái game bằng Singleton Service.

**2. Tính năng chính**

- Bàn cờ 7x7: Mỗi người chơi có một bàn cờ kích thước 7x7.

- 3 Tàu mỗi bên: Mỗi bên được trang bị 3 tàu với kích thước cố định (1 tàu 3 ô, 2 tàu 2 ô).

- Tự động đặt tàu: Tàu được tự động đặt ngẫu nhiên và hợp lệ (không chồng chéo) khi bắt đầu mỗi ván mới.

- Chế độ chơi vs. BOT: Người chơi sẽ đấu với một AI.

- BOT thông minh: BOT sử dụng chiến lược "Săn lùng & Tiêu diệt" (Hunt & Target), giúp nó bắn hiệu quả hơn sau khi đã bắn trúng một mục tiêu.

- Thông báo trạng thái: Hiển thị rõ ràng lượt đi, số lượt đã bắn, và thông báo người chiến thắng.

- Chơi lại: Dễ dàng bắt đầu một ván mới sau khi game kết thúc.

**3. Công nghệ sử dụng**

- Backend: C# với ASP.NET Core MVC (.NET 8)

- Frontend: HTML, CSS, và JavaScript 

**3. Kiến trúc:**

- Model-View-Controller (MVC): Phân tách rõ ràng giữa logic nghiệp vụ, dữ liệu và giao diện người dùng.

- Singleton Service: GameService được đăng ký dưới dạng Singleton để duy trì trạng thái của ván game trong bộ nhớ (in-memory) xuyên suốt các request.

**4. Cài đặt và Chạy dự án
Yêu cầu**

- .NET 8 SDK hoặc phiên bản mới hơn.

- Một trình soạn thảo code như Visual Studio 2022 hoặc Visual Studio Code.

**5. Hướng dẫn chơi**

- Khi vào game, bàn cờ của bạn (bên trái) và các tàu chiến đã được đặt sẵn.

- Click vào một ô bất kỳ trên Bàn cờ của BOT (bên phải) để thực hiện một lượt bắn.

- Kết quả bắn (Trúng hoặc Trượt) sẽ được hiển thị ngay lập tức.

- Sau lượt của bạn, BOT sẽ tự động thực hiện lượt bắn của nó vào bàn cờ của bạn.

- Mục tiêu là đánh chìm hết 3 tàu của BOT trước khi nó đánh chìm hết tàu của bạn.

- Khi một bên chiến thắng, một thông báo sẽ hiện ra. Nhấn nút "Chơi Lại" để bắt đầu một ván đấu mới.

**6. Nguồn hỗ trợ & Tạo mã (AI Code Assistance)**
- Google AI Studio / Gemini
- Github Copilot
