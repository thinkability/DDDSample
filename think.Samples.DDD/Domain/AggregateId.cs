using System;

namespace Domain
{
    public class AggregateId : SingleValueObject<Guid>
    {
        public AggregateId(Guid value) : base(value)
        {
        }
    }
}