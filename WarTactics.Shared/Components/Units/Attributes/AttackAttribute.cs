namespace WarTactics.Shared.Components.Units.Attributes
{
    using System;
    
    [AttributeUsage(AttributeTargets.Class)]
    public class AttackAttribute : Attribute
    {
        public AttackAttribute(double amount)
        {
            this.Amount = amount;
        }

        public double Amount { get; }
    }
}
