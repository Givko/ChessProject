using Chess.Core.DataAccess.Entities;
using System;

namespace Chess.GameEngine.DataAccess.Entities
{
    public class Game : BaseEntity
    {
        public Guid PlayerOneId { get; set; }
        public virtual Player PlayerOne { get; set; }

        public Guid PlayerTwoId { get; set; }
        public virtual Player PlayerTwo { get; set; }

        public Guid WinnerId { get; set; }
        public virtual Player Winner { get; set; }
    }
}