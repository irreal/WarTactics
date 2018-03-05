namespace WarTactics.Server
{
    using System;
    using System.Collections.Generic;

    using WarTactics.Shared;
    using WarTactics.Shared.Components;
    using WarTactics.Shared.Components.Game;

    public static class GameManager
    {
        public static List<GameRound> Games { get; set; }

        public static GameRound NewGame(Guid id)
        {
            var map = GetFieldsFromMap(WtGame.GetMap());

            var board = new Board(map);
            return new GameRound(board) { Id = id };
        }

        private static BoardField[,] GetFieldsFromMap(BoardFieldType[,] mapInfo)
        {
            var fields = new BoardField[mapInfo.GetLength(0), mapInfo.GetLength(1)];
            for (int col = 0; col < mapInfo.GetLength(0); col++)
            {
                for (int row = 0; row < mapInfo.GetLength(1); row++)
                {
                    fields[col, row] = new BoardField(col, row, mapInfo[col, row]);
                }
            }

            return fields;
        }
    }
}
