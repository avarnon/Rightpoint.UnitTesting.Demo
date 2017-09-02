using System;
using System.Runtime.Serialization;

namespace Rightpoint.UnitTesting.Demo.Mvc.Exceptions
{
    [Serializable]
    public class DemoEntityNotFoundException : DemoException
    {
        public DemoEntityNotFoundException()
        {
        }

        public DemoEntityNotFoundException(string message)
            : base(message)
        {
        }

        public DemoEntityNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected DemoEntityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
