using System;
using System.Runtime.Serialization;

namespace FileStorage.BLL.Infrastructure
{
    /// <summary>
    /// ValidationException
    /// </summary>
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException() : base()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}