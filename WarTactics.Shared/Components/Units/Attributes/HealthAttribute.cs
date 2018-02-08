namespace WarTactics.Shared.Components.Units.Attributes
{
    using System;
    
    [AttributeUsage(AttributeTargets.Class)]
    public class HealthAttribute : Attribute
    {
        public HealthAttribute(double amount) : this(amount, amount)
        {
        }

        public HealthAttribute(double amount, double maximumAmount)
        {
            this.Amount = amount;
            this.MaximumAmount = maximumAmount;
        }

        public double Amount { get; }

        public double MaximumAmount { get; }
    }
}
