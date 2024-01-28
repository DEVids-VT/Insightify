namespace Insightify.Posts.Domain.Posts.Exceptions
{
    using Insightify.Posts.Domain.Common;

    internal class InvalidPostException : BaseDomainException
    {
        public InvalidPostException()
        {
        }

        public InvalidPostException(string error) => this.Error = error;
    }
}
