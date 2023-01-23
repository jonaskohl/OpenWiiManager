using System.Runtime.Serialization;

namespace OpenWiiManager.Language.Exceptions
{
    [Serializable]
    internal class RuntimeAssertionFailedException : Exception
    {
        public RuntimeAssertionFailedException()
        {
        }

        public RuntimeAssertionFailedException(string? message) : base(message)
        {
        }

        public RuntimeAssertionFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RuntimeAssertionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}