namespace WarTactics.Shared.Components.Units.Attributes
{
    using System;
    
    [AttributeUsage(AttributeTargets.Class)]
    public class ArmorAttribute : Attribute
    {
        public ArmorAttribute(double amount)
        {
            this.Amount = amount;
        }

        public double Amount { get; }
    }
}
