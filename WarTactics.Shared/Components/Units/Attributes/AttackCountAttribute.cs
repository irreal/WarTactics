namespace WarTactics.Shared.Components.Units.Attributes
{
    using System;
    
    [AttributeUsage(AttributeTargets.Class)]
    public class AttackCountAttribute : Attribute
    {
        public AttackCountAttribute(int amount)
        {
            this.Amount = amount;
        }

        public int Amount { get; }
    }
}
