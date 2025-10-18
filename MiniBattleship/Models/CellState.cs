namespace MiniBattleship.Models
{
    public enum CellState
    {
        Empty, // Ô trống
        Ship, // Có tàu nhưng chưa bị bắn
        Hit, // Bắn trúng tàu
        Miss // Bắn trượt
    }
}