namespace Insightify.Posts.Domain.Posts.Exceptions
{
    using Insightify.Posts.Domain.Common;

    internal class InvalidTagException : BaseDomainException
    {
        public InvalidTagException()
        {
        }

        public InvalidTagException(string error) => this.Error = error;
    }
}
