namespace WarTactics.Server.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Xna.Framework;

    using WarTactics.Shared.Components.Game;
    using WarTactics.Shared.Networking;

    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private static readonly List<GameInfo> GameInfoList = new List<GameInfo>();

        [HttpGet]
        public IEnumerable<GameInfo> Get()
        {
            return GameInfoList;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]string gameName)
        {
            if (string.IsNullOrEmpty(gameName))
            {
                return this.BadRequest();
            }

            var gameInfo = new GameInfo(gameName);
            gameInfo.GameRound = GameManager.NewGame(gameInfo.Id);
            GameInfoList.Add(gameInfo);

            return this.Json(gameInfo);
        }

        [HttpPost("{id}/join")]
        public IActionResult Post(Guid id, [FromBody]string playerName)
        {
            var game = GameInfoList.FirstOrDefault(g => g.Id == id);
            if (game == null || string.IsNullOrWhiteSpace(playerName))
            {
                return this.NotFound();
            }

            if (game.PlayerCount == game.MaxPlayerCount)
            {
                return this.BadRequest("Max player count reached");
            }

            game.GameRound.Players.Add(new Player(playerName, Color.LightBlue));
            game.PlayerCount++;

            return this.Ok();
        }
    }
}
