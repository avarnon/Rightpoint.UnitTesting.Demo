using System;
using System.Runtime.Serialization;

namespace Rightpoint.UnitTesting.Demo.Mvc.Exceptions
{
    [Serializable]
    public class DemoException : ApplicationException
    {
        public DemoException()
        {
        }

        public DemoException(string message)
            : base(message)
        {
        }

        public DemoException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected DemoException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
