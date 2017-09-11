using System;
using System.Runtime.Serialization;

namespace Rightpoint.UnitTesting.Demo.Common.Exceptions
{
    [Serializable]
    public class DemoInvalidOperationException : DemoException
    {
        public DemoInvalidOperationException()
        {
        }

        public DemoInvalidOperationException(string message)
            : base(message)
        {
        }

        public DemoInvalidOperationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected DemoInvalidOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
