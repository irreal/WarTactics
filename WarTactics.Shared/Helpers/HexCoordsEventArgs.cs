using System;
using System.Collections.Generic;
using System.Text;

namespace WarTactics.Shared.Helpers
{
    public class HexCoordsEventArgs : EventArgs
    {
        public HexCoordsEventArgs(IntPoint coords)
        {
            this.Coords = coords;
        }

        public IntPoint Coords;
    }
}
