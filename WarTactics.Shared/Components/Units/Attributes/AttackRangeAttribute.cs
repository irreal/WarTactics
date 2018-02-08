namespace WarTactics.Shared.Components.Units.Attributes
{
    using System;
    
    [AttributeUsage(AttributeTargets.Class)]
    public class AttackRangeAttribute : Attribute
    {
        public AttackRangeAttribute(int amount)
        {
            this.Amount = amount;
        }

        public int Amount { get; }
    }
}
