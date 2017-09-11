using System;
using System.Runtime.Serialization;
using Rightpoint.UnitTesting.Demo.Common.Exceptions;

namespace Rightpoint.UnitTesting.Demo.Mvc.Exceptions
{
    [Serializable]
    public class RemoteException : DemoException
    {
        public RemoteException()
        {
        }

        public RemoteException(string message)
            : base(message)
        {
        }

        public RemoteException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected RemoteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
