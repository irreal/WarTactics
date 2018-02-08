namespace WarTactics.Shared.Components.Units.Events
{
    public class UnitEvent
    {
        public UnitEvent(UnitEventType eventType, double amount = 0)
        {
            this.EventType = eventType;
            this.Amount = amount;
        }

        public UnitEventType EventType { get; }

        public double Amount { get; }
    }
}
