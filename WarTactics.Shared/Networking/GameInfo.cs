namespace WarTactics.Shared.Networking
{
    using System;

    using Newtonsoft.Json;
    using WarTactics.Shared.Components.Game;

    public class GameInfo
    {
        public GameInfo(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public string Name { get; set; }

        public int PlayerCount { get; set; }

        public int MaxPlayerCount { get; set; } = 2;

        public Guid Id { get; }

        [JsonIgnore]
        public GameRound GameRound { get; set; }
    }
}
