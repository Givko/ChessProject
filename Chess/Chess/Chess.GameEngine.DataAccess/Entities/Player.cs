using Chess.Core.DataAccess.Entities;

namespace Chess.GameEngine.DataAccess.Entities
{
    public class Player : BaseEntity
    {
        public string Username { get; set; }
        public int Points { get; set; }
    }
}