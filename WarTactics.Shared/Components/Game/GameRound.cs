namespace WarTactics.Shared.Components.Game
{
    using System.Collections.Generic;

    using Nez;

    public class GameRound : Component
    {
        private Board board;

        private int currentPlayerIndex;

        public List<Player> Players { get; } = new List<Player>();

        public Player CurrentPlayer => currentPlayerIndex >= 0 ? this.Players[this.currentPlayerIndex] : null;

        public bool HasStarted { get; private set; }

        public Board Board => this.board;

        public void Start()
        {
            if (this.HasStarted)
            {
                throw new System.Exception("The game is already running!");
            }

            if (this.Players.Count < 2)
            {
                throw new System.Exception("Can't start a game with less than two players");
            }

            this.HasStarted = true;
            this.currentPlayerIndex = 0;
            foreach (var field in this.board.BoardFields())
            {
                field.TurnStarted();
                field.Unit?.TurnStarted();
            }
        }

        public override void onAddedToEntity()
        {
            this.board = this.entity.scene.findComponentOfType<Board>();
            base.onAddedToEntity();
        }

        public void EndTurn()
        {
            foreach (var unit in this.board.Units)
            {
                unit.TurnEnded();
            }

            foreach (var field in this.board.Fields)
            {
                field.TurnEnded();
            }

            this.currentPlayerIndex = (this.currentPlayerIndex + 1) % this.Players.Count;

            foreach (var unit in this.board.Units)
            {
                unit.TurnStarted();
            }

            foreach (var field in this.board.Fields)
            {
                field.TurnStarted();
            }
        }
    }
}
