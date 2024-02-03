namespace Insightify.Posts.Domain.Posts.Exceptions
{
    using Insightify.Posts.Domain.Common;

    internal class InvalidCommentException : BaseDomainException
    {
        public InvalidCommentException()
        {
        }

        public InvalidCommentException(string error) => this.Error = error;
    }
}
