using System;
using System.Runtime.Serialization;

namespace Rightpoint.UnitTesting.Demo.Mvc.Exceptions
{
    [Serializable]
    public class RemoteInvalidOperationException : RemoteException
    {
        public RemoteInvalidOperationException()
        {
        }

        public RemoteInvalidOperationException(string message)
            : base(message)
        {
        }

        public RemoteInvalidOperationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected RemoteInvalidOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
