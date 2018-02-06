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
        private readonly BoardField[,] fields;

        public Board(BoardField[,] boardFields)
        {
            this.fields = boardFields;
            this.Size = new IntPoint(boardFields.GetLength(0), boardFields.GetLength(1));
        }

        public BoardField[,] Fields => this.fields;

        public IEnumerable<Unit> Units => this.BoardFields().Where(bf => bf.Unit != null).Select(bf => bf.Unit);

        public IntPoint Size { get; }

        public Layout HexLayout { get; } = new Layout(Layout.flat, new PointD(31, 31), new PointD(0, 0));

        public BoardField FieldFromUnit(Unit unit)
        {
            return this.BoardFields().FirstOrDefault(bf => bf.Unit == unit);
        }

        public bool CoordInMap(IntPoint coord)
        {
            return coord.X >= 0 && coord.X < this.Size.X && coord.Y >= 0 && coord.Y < this.Size.Y;
        }

        public Vector2 HexPosition(int col, int row)
        {
            return Layout.HexToPixel(this.HexLayout, this.fields[col, row].Hex);
        }

        public IntPoint IntPointFromPosition(Vector2 position)
        {
            return OffsetCoord.QoffsetFromCube(OffsetCoord.EVEN, FractionalHex.HexRound(Layout.PixelToHex(this.HexLayout, position)));
        }

        public IEnumerable<BoardField> BoardFields()
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