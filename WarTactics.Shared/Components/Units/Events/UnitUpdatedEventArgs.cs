namespace WarTactics.Shared.Components.Units.Events
{
    using System;

    public class UnitUpdatedEventArgs : EventArgs
    {
        public UnitUpdatedEventArgs(UnitEvent unitEvent)
        {
            this.UnitEvent = unitEvent;
        }

        public UnitEvent UnitEvent { get; }
    }
}
