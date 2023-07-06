using FluentValidation;
using Insightify.Posts.Application.Common.Exceptions;
using MediatR;

namespace Insightify.Posts.Application.Common.Behaviours
{
    public class RequestValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
            => this.validators = validators;

        public Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
            )
        {
            var context = new ValidationContext<TRequest>(request);

            var errors = this
                .validators
                .Select(v => v.Validate(context: context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (errors.Count != 0)
            {
                throw new ModelValidationException(errors);
            }

            return next();
        }
    }
}
