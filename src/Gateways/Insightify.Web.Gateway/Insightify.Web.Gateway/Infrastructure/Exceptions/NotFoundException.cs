namespace Insightify.Web.Gateway.Infrastructure.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
            
        }

        public NotFoundException(string message) : base(message) { }
    }
}
