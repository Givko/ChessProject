using Chess.Core.Api.Controllers;
using Chess.GameEngine.DataAccess.Entities;
using Chess.GameEngine.Models;
using Chess.GameEngine.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Chess.GameEngine.Api.Controllers
{
    public class PlayersController : BaseCrudController<IPlayerService, PlayerModel, Player>
    {
        public PlayersController(IPlayerService service, ILogger logger) 
            : base(service, logger)
        {
        }
    }
}
