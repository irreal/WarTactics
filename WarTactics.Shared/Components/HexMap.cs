using Microsoft.Xna.Framework;
using Nez;
using WarTactics.Shared.Helpers;

namespace WarTactics.Shared.Components
{
    public class HexMap : Component
    {
        Layout hexLayout = new Layout(Layout.flat, new PointD(31, 31), new PointD(0, 0));
        BoardField[,] fields;
        public HexMap(int[,] mapInfo)
        {
            fields = new BoardField[mapInfo.GetLength(0), mapInfo.GetLength(1)];
            for (int i = 0; i < mapInfo.GetLength(0); i++)
            {
                for (int u = 0; u < mapInfo.GetLength(1); u++)
                {
                    fields[i, u] = new BoardField(i,u, (BoardFieldType)mapInfo[i,u]);
                }
            }
        }

        public Vector2 HexPosition(int col, int row)
        {
            return Layout.HexToPixel(hexLayout, fields[col, row].Hex);
        }
        public IntPoint IntPointFromPosition(Vector2 position)
        {
            return OffsetCoord.QoffsetFromCube(OffsetCoord.EVEN, FractionalHex.HexRound(Layout.PixelToHex(this.hexLayout, position)));
        }

        public BoardField[,] Fields => this.fields;
    }
}
