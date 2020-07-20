using Chess.Core.Models;

namespace Chess.GameEngine.Models
{
    public class PlayerModel : BaseModel
    {
        public string Username { get; set; }
        public int Points { get; set; }
    }
}