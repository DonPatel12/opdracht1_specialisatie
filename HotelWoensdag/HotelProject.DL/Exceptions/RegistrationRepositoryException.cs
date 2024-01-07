using System.Runtime.Serialization;

namespace HotelProject.DL.Exceptions
{
    [Serializable]
    public class RegistrationRepositoryException : Exception
    {
        public RegistrationRepositoryException()
        {
        }

        public RegistrationRepositoryException(string? message) : base(message)
        {
        }

        public RegistrationRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RegistrationRepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
