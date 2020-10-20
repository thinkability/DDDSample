using System;

namespace Domain
{
    public class DomainError : Exception
    {
        public DomainError(string message) : base(message)
        {
            
        }

        public static DomainError Because(string reason)
        {
            return new DomainError(reason);
        }
    }
}