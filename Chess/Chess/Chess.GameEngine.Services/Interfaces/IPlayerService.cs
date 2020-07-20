using Chess.Core.Services.Interfaces;
using Chess.GameEngine.DataAccess.Entities;
using Chess.GameEngine.Models;

namespace Chess.GameEngine.Services.Interfaces
{
    public interface IPlayerService : IBaseCrudService<Player, PlayerModel>
    {
    }
}
