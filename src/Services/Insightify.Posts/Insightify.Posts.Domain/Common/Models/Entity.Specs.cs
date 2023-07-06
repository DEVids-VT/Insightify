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
            
            var first = new Like("First", DateTime.Now).SetId(1);
            var second = new Like("Second", DateTime.Now).SetId(1);

            // Act
            var result = first == second;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void EntitiesWithDifferentIdsShouldNotBeEqual()
        {
            // Arrange
            var first = new Like("First", DateTime.Now).SetId(1);
            var second = new Like("Second", DateTime.Now).SetId(2);

            // Act
            var result = first == second;

            // Assert
            result.Should().BeFalse();
        }
    }
    internal static class EntityExtensions
    {
        public static TEntity SetId<TEntity>(this TEntity entity, int id)
            where TEntity : Entity<int>
            => (entity.SetIdentifier(id) as TEntity)!;

        private static Entity<T> SetIdentifier<T>(this Entity<T> entity, int id)
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
