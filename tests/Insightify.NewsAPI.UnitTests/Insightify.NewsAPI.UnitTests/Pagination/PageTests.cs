using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.NewsAPI.Pagination;

namespace Insightify.NewsAPI.UnitTests.Pagination
{
    public class PageTests
    {
        [Fact]
        public void Page_Constructor_AssignsValuesCorrectly()
        {
            // Arrange
            var values = new List<string> { "Item1", "Item2", "Item3" };
            int currentPage = 1;
            int pageSize = 3;
            int totalCount = 3;

            // Act
            var page = new Page<string>(values, currentPage, pageSize, totalCount);

            // Assert
            Assert.Equal(currentPage, page.CurrentPage);
            Assert.Equal(pageSize, page.PageSize);
            Assert.Equal(totalCount, page.TotalCount);
        }

        [Fact]
        public void Page_GetEnumerator_ReturnsCorrectItems()
        {
            // Arrange
            var values = new List<string> { "Item1", "Item2", "Item3" };
            var page = new Page<string>(values, 1, 3, 3);

            // Act
            var enumeratedValues = page.ToList(); // Using LINQ ToList to enumerate

            // Assert
            Assert.Equal(values.Count, enumeratedValues.Count);
            for (int i = 0; i < values.Count; i++)
            {
                Assert.Equal(values[i], enumeratedValues[i]);
            }
        }
    }
}
