using System;

namespace Domain
{
    public class DomainError : Exception
    {
        public DomainError(string message) : base(message)
        {
            
        }

        public static DomainError WithMessage(string message)
        {
            return new DomainError(message);
        }
    }
}