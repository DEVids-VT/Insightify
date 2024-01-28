namespace Insightify.Posts.Domain.Posts.Exceptions
{
    using Insightify.Posts.Domain.Common;

    internal class InvalidSaveException : BaseDomainException
    {
        public InvalidSaveException()
        {
        }

        public InvalidSaveException(string error) => this.Error = error;
    }
}
