using System;
using System.Collections.Generic;
using System.Text;
using WarTactics.Shared.Helpers;

namespace WarTactics.Shared.Components
{
    public class BoardField
    {
        Hex hex;
        int col;
        int row;

        public BoardField(int col, int row, BoardFieldType boardFieldType = BoardFieldType.Water)
        {
            this.col = col;
            this.row = row;
            this.BoardFieldType = boardFieldType;
            this.hex = OffsetCoord.QoffsetToCube(OffsetCoord.EVEN, new OffsetCoord(col, row));
        }

        public BoardFieldType BoardFieldType { get; }
        public Hex Hex => this.hex;
        public int Col => this.col;
        public int Row => this.row;
    }
}
