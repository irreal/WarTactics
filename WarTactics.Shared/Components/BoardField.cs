using WarTactics.Shared.Helpers;

namespace WarTactics.Shared.Components
{
    using WarTactics.Shared.Components.Units;

    public class BoardField
    {
        public BoardField(int col, int row, BoardFieldType boardFieldType = BoardFieldType.Water)
        {
            this.Col = col;
            this.Row = row;
            this.BoardFieldType = boardFieldType;
            this.Hex = OffsetCoord.QoffsetToCube(OffsetCoord.EVEN, new OffsetCoord(col, row));
        }

        public BoardFieldType BoardFieldType { get; set; }

        public Unit Unit { get; set; }

        public Hex Hex { get; }

        public int Col { get; }

        public int Row { get; }
    }
}