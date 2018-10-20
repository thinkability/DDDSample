using System;

namespace Domain
{
    public class AggregateId : SingleValueObject<Guid>
    {
        public AggregateId(Guid value) : base(value)
        {
            if(value == Guid.Empty)
                throw new ArgumentException(nameof(value));
        }
    }
}