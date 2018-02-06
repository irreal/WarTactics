namespace WarTactics.Shared.Components
{
    using WarTactics.Shared.Components.Game;
    using WarTactics.Shared.Components.Units;
    using WarTactics.Shared.Helpers;

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

        public Unit Unit { get; private set; }

        public Hex Hex { get; }

        public int Col { get; }

        public int Row { get; }

        public void SetUnit(Unit unit)
        {
            this.Unit = unit;
        }

        public virtual void TurnEnded(Player currentPlayer)
        {
        }

        public virtual void TurnStarted(Player currentPlayer)
        {
        }
    }
}