using System;

namespace Chess.GameEngine.Models
{
    public class StartGameModel
    {
        /// <summary>
        /// Represents the user which requested to play a game
        /// </summary>
        public Guid UserId { get; set; }

        public PlayerModel PlayerInfo { get; set; }
    }
}