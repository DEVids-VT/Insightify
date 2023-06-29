using FluentAssertions;
using Insightify.Posts.Domain.Posts.Models;
using Xunit;

namespace Insightify.Posts.Domain.Common.Models
{
    public class EntitySpecs
    {
        [Fact]
        public void EntitiesWithEqualIdsShouldBeEqual()
        {
            // Arrange
            var id = Guid.NewGuid();
            var first = new Like("First", DateTime.Now).SetId(id);
            var second = new Like("Second", DateTime.Now).SetId(id);

            // Act
            var result = first == second;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void EntitiesWithDifferentIdsShouldNotBeEqual()
        {
            // Arrange
            var first = new Like("First", DateTime.Now).SetId(Guid.NewGuid());
            var second = new Like("Second", DateTime.Now).SetId(Guid.NewGuid());

            // Act
            var result = first == second;

            // Assert
            result.Should().BeFalse();
        }
    }
    internal static class EntityExtensions
    {
        public static TEntity SetId<TEntity>(this TEntity entity, Guid id)
            where TEntity : Entity<Guid>
            => (entity.SetIdentifier(id) as TEntity)!;

        private static Entity<T> SetIdentifier<T>(this Entity<T> entity, Guid id)
            where T : struct
        {
            entity
                .GetType()
                .BaseType!
                .GetProperty(nameof(Entity<T>.Id))!
                .GetSetMethod(true)!
                .Invoke(entity, new object[] { id });

            return entity;
        }
    }
}
