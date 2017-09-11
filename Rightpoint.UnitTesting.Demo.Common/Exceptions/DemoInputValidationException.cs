using System;
using System.Runtime.Serialization;

namespace Rightpoint.UnitTesting.Demo.Common.Exceptions
{
    [Serializable]
    public class DemoInputValidationException : DemoException
    {
        public DemoInputValidationException()
        {
        }

        public DemoInputValidationException(string message)
            : base(message)
        {
        }

        public DemoInputValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected DemoInputValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
