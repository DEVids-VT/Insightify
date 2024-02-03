namespace Insightify.Posts.Domain.Posts.Exceptions
{
    using Insightify.Posts.Domain.Common;

    internal class InvalidLikeException : BaseDomainException
    {
        public InvalidLikeException()
        {
        }

        public InvalidLikeException(string error) => this.Error = error;
    }
}
