using System;

namespace Messaging.Contracts
{
    public class IdResponse
    {
        public Guid Id { get; private set; }
        
        public IdResponse(Guid id)
        {
            Id = id;
        }
    }
}