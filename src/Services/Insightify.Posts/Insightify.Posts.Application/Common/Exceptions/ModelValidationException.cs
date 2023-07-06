using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Posts.Application.Common.Exceptions
{
    public class ModelValidationException : Exception
    {
        public ModelValidationException()
            : base("One or more validation errors have occurred.")
            => this.Errors = new Dictionary<string, string[]>();

        public ModelValidationException(IEnumerable<ValidationFailure> errors)
            : this()
        {
            var failureGroups = errors
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();

                this.Errors.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
