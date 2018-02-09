using System;
using System.Collections.Generic;
using System.Text;

namespace War1.WarBasics.Map
{
    public static class Map
    {
        public static int CalculateDistance(MapPosition position1, MapPosition position2)
        {
            int result = 0;

            int minX = Math.Min(position1.X, position2.X);
            int maxX = Math.Max(position1.X, position2.X);

            int minY = Math.Min(position1.Y, position2.Y);
            int maxY = Math.Max(position1.Y, position2.Y);

            while (minX<maxX && minY<maxY)
            {
                result++;
                minX++;
                minY++;
            }

            result += (maxX - minX);
            result += (maxY - minY);

            return result;
        }
    }
}
