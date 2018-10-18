using System;

namespace Domain.Aggregates.Champagne.ValueObjects
{
    public class ChampagneName : SingleValueObject<string>
    {
        public ChampagneName(string value) : base(value)
        {
            if(string.IsNullOrEmpty(value))
                throw new ArgumentException(nameof(value));
        }
    }
}