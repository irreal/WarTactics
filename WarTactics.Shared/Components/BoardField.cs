﻿namespace WarTactics.Shared.Components
{
    using WarTactics.Shared.Components.Units;
    using WarTactics.Shared.Helpers;

    public class BoardField
    {
        public BoardField(int col, int row, BoardFieldType boardFieldType = BoardFieldType.Water)
        {
            this.Col = col;
            this.Row = row;
            this.Coords = new IntPoint(col, row);
            this.BoardFieldType = boardFieldType;
            this.Hex = OffsetCoord.QoffsetToCube(OffsetCoord.EVEN, new OffsetCoord(col, row));
        }

        public BoardFieldType BoardFieldType { get; set; }

        public Unit Unit { get; private set; }

        public Hex Hex { get; }

        public int Col { get; }

        public int Row { get; }

        public IntPoint Coords { get; }

        public void SetUnit(Unit unit)
        {
            if (this.Unit != null)
            {
                this.Unit.UnitUpdated -= this.UnitUpdated;
            }

            this.Unit = unit;

            this.Unit.UnitUpdated += this.UnitUpdated;
        }

        public virtual bool CanBeAttackedByUnit(Unit unit)
        {
            return this.Unit != null;
        }

        public virtual bool CanTakeUnit(Unit unit)
        {
            return this.Unit == null;
        }

        public virtual Unit RemoveUnit()
        {
            var unit = this.Unit;
            this.Unit.UnitUpdated -= this.UnitUpdated;
            this.Unit = null;
            return unit;
        }

        public virtual void TakeUnit(Unit unit)
        {
            this.SetUnit(unit);
            unit.Moved();
        }

        public virtual void TurnEnded()
        {
        }

        public virtual void TurnStarted()
        {
        }

        private void UnitUpdated(object sender, System.EventArgs e)
        {
            if (this.Unit.Health <= 0)
            {
                this.RemoveUnit();
            }
        }
    }
}