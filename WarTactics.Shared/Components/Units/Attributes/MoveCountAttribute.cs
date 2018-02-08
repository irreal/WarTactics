namespace WarTactics.Shared.Components.Units.Attributes
{
    using System;
    
    [AttributeUsage(AttributeTargets.Class)]
    public class MoveCountAttribute : Attribute
    {
        public MoveCountAttribute(int amount)
        {
            this.Amount = amount;
        }

        public int Amount { get; }
    }
}
