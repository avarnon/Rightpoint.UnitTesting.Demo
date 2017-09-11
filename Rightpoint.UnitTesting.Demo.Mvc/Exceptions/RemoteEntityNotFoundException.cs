using System;
using System.Runtime.Serialization;

namespace Rightpoint.UnitTesting.Demo.Mvc.Exceptions
{
    [Serializable]
    public class RemoteEntityNotFoundException : RemoteException
    {
        public RemoteEntityNotFoundException()
        {
        }

        public RemoteEntityNotFoundException(string message)
            : base(message)
        {
        }

        public RemoteEntityNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected RemoteEntityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
