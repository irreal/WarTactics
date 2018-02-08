namespace WarTactics.Shared.Components.Units.Attributes
{
    using System;
    
    [AttributeUsage(AttributeTargets.Class)]
    public class SpeedAttribute : Attribute
    {
        public SpeedAttribute(int amount)
        {
            this.Amount = amount;
        }

        public int Amount { get; }
    }
}
