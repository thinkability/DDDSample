using System;

namespace Domain.Aggregates.Champagne.ValueObjects
{
    public class GrapeBlendPercentage : SingleValueObject<double>
    {
        public GrapeBlendPercentage(double value) : base(value)
        {
            if(value <= 0 || value > 1) 
                throw new ArgumentException("Percentage must be between ]0..1]", nameof(value));
        }
    }
}