using System;
using System.Collections.Generic;

namespace Domain.Aggregates.Champagne.ValueObjects
{
    public class GrapeBlend : ValueObject
    {
        public GrapeBlendPercentage Percentage { get; private set; }
        public GrapeVariety GrapeVariety { get; private set; }
        
        public GrapeBlend(GrapeBlendPercentage percentage, GrapeVariety grape)
        {
            if (percentage == null)
                throw new ArgumentException(nameof(percentage));
            
            if(grape == null)
                throw new ArgumentException(nameof(grape));

            Percentage = percentage;
            GrapeVariety = grape;
        }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Percentage;
            yield return GrapeVariety;
        }
    }
}