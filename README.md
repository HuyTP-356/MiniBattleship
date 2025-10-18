**Mini Battleship - Game Tàu Chiến .NET MVC
**
Đây là một dự án game Tàu chiến (Battleship) đơn giản được xây dựng bằng ASP.NET Core MVC (.NET 8). Người chơi sẽ đối đầu với một máy tính (BOT) có trí tuệ nhân tạo (AI) cơ bản. Dự án này nhằm mục đích minh họa các khái niệm cốt lõi của .NET MVC, bao gồm Routing, Models, Views, Controllers, và cách quản lý trạng thái game bằng Singleton Service.
(Ghi chú: Hãy thay thế link ảnh trên bằng ảnh chụp màn hình hoặc ảnh GIF gameplay thực tế của bạn!)
Tính năng chính
Bàn cờ 7x7: Mỗi người chơi có một bàn cờ kích thước 7x7.
3 Tàu mỗi bên: Mỗi bên được trang bị 3 tàu với kích thước cố định (1 tàu 3 ô, 2 tàu 2 ô).
Tự động đặt tàu: Tàu được tự động đặt ngẫu nhiên và hợp lệ (không chồng chéo) khi bắt đầu mỗi ván mới.
Chế độ chơi vs. BOT: Người chơi sẽ đấu với một AI.
BOT thông minh: BOT sử dụng chiến lược "Săn lùng & Tiêu diệt" (Hunt & Target), giúp nó bắn hiệu quả hơn sau khi đã bắn trúng một mục tiêu.
Giao diện đồ họa: Sử dụng hình ảnh cho các bộ phận tàu, hiệu ứng bắn trúng (Hit) và bắn trượt (Miss) thay vì chỉ dùng màu sắc.
Thông báo trạng thái: Hiển thị rõ ràng lượt đi, số lượt đã bắn, và thông báo người chiến thắng.
Chơi lại: Dễ dàng bắt đầu một ván mới sau khi game kết thúc.
Công nghệ sử dụng
Backend: C# với ASP.NET Core MVC (.NET 8)
Frontend: HTML, CSS, và JavaScript (Vanilla JS)
Kiến trúc:
Model-View-Controller (MVC): Phân tách rõ ràng giữa logic nghiệp vụ, dữ liệu và giao diện người dùng.
Singleton Service: GameService được đăng ký dưới dạng Singleton để duy trì trạng thái của ván game trong bộ nhớ (in-memory) xuyên suốt các request.
