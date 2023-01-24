using System.Runtime.Serialization;

namespace OpenWiiManager.Language.Exceptions
{
    [Serializable]
    internal class SystemSoundException : Exception
    {
        public SystemSoundException()
        {
        }

        public SystemSoundException(string? message) : base(message)
        {
        }

        public SystemSoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SystemSoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}