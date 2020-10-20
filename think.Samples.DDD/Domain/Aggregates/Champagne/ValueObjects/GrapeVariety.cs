using System;
using System.Linq;

namespace Domain.Aggregates.Champagne.ValueObjects
{
    public class GrapeVariety: SingleValueObject<string>
    {
        private string[] _allowedGrapeTypes = 
        {
            "Chardonnay",
            "PinotNoir",
            "PinotMeunier",
            "PinotBlanc",
            "PinotGris",
            "Arbane",
            "PetitMeslier"
        };

        public GrapeVariety(string value) : base(value)
        {
            if(string.IsNullOrEmpty(value))
                throw new ArgumentException(nameof(value));
            
            if(!_allowedGrapeTypes.Contains(value))
                throw new ArgumentException("Grape is not allowed in a champagne", nameof(value));
        }
    }
}