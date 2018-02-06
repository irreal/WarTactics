namespace WarTactics.Shared.Components
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Xna.Framework;

    using Nez;

    using WarTactics.Shared.Components.Units;
    using WarTactics.Shared.Helpers;

    public class Board : Component
    {
        private readonly Layout hexLayout = new Layout(Layout.flat, new PointD(31, 31), new PointD(0, 0));
        private readonly BoardField[,] fields;

        public Board(BoardFieldType[,] mapInfo)
        {
            this.fields = new BoardField[mapInfo.GetLength(0), mapInfo.GetLength(1)];
            this.Size = new IntPoint(mapInfo.GetLength(0), mapInfo.GetLength(1));
            for (int i = 0; i < mapInfo.GetLength(0); i++)
            {
                for (int u = 0; u < mapInfo.GetLength(1); u++)
                {
                    this.fields[i, u] = new BoardField(i, u, mapInfo[i, u]);
                }
            }
        }

        public BoardField[,] Fields => this.fields;

        public IntPoint Size { get; }

        public BoardField FieldFromUnit(Unit unit)
        {
            return this.BoardFields().FirstOrDefault(bf => bf.Unit == unit);
        }

        public Vector2 HexPosition(int col, int row)
        {
            return Layout.HexToPixel(this.hexLayout, this.fields[col, row].Hex);
        }

        public IntPoint IntPointFromPosition(Vector2 position)
        {
            return OffsetCoord.QoffsetFromCube(OffsetCoord.EVEN, FractionalHex.HexRound(Layout.PixelToHex(this.hexLayout, position)));
        }

        private IEnumerable<BoardField> BoardFields()
        {
            for (int col = 0; col < this.fields.GetLength(0); col++)
            {
                for (int row = 0; row < this.fields.GetLength(1); row++)
                {
                    yield return this.fields[col, row];
                }
            }
        }
    }
}