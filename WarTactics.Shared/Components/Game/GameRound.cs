﻿namespace WarTactics.Shared.Components.Game
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;

    using Nez;

    using WarTactics.Shared.Entities;

    public class GameRound : Component
    {
        private int currentPlayerIndex = -1;

        private int currentTurn;

        private Player firstPlayerToPlay;

        public GameRound(Board board)
        {
            this.Board = board;
        }

        public List<Player> Players { get; } = new List<Player>();

        public Player CurrentPlayer => this.currentPlayerIndex >= 0 ? this.Players[this.currentPlayerIndex] : null;

        public bool HasStarted { get; private set; }

        public Board Board { get; }

        public int CurrentTurn => this.currentTurn;

        public Guid Id { get; set; }

        public void Start()
        {
            if (this.HasStarted)
            {
                throw new Exception("The game is already running!");
            }

            if (this.Players.Count < 2)
            {
                throw new Exception("Can't start a game with less than two players");
            }

            this.HasStarted = true;
            this.currentPlayerIndex = 0;
            this.currentTurn++;
            this.firstPlayerToPlay = this.CurrentPlayer;
            foreach (var field in this.Board.BoardFields())
            {
                field.TurnStarted();
                field.Unit?.TurnStarted();
            }

            this.entity.scene.addEntity(
                new TextEventEntity(
                    $"Game starting. {Environment.NewLine} {this.CurrentPlayer.Name}'s turn!",
                    Color.White,
                    Screen.center,
                    true));
        }

        public void EndTurn()
        {
            foreach (var unit in this.Board.Units)
            {
                unit.TurnEnded();
            }

            foreach (var field in this.Board.Fields)
            {
                field.TurnEnded();
            }

            // this.entity.scene.TurnEnded();
            this.currentPlayerIndex = (this.currentPlayerIndex + 1) % this.Players.Count;
            if (this.CurrentPlayer == this.firstPlayerToPlay)
            {
                this.currentTurn++;
            }

            this.CurrentPlayer.StrategyPoints += 3;

            foreach (var unit in this.Board.Units)
            {
                unit.TurnStarted();
            }

            foreach (var field in this.Board.Fields)
            {
                field.TurnStarted();
            }

            // this.entity.scene.TurnStarted();
        }
    }
}
