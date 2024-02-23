using Insightify.Framework.MongoDb.Abstractions.Interfaces;
using Insightify.NewsAPI.Pagination;
using Moq;

namespace Insightify.NewsAPI.UnitTests.Pagination;
public class PagedListExtensionsTests
{
    [Fact]
    public void ToPage_ConvertsIPagedListToPageCorrectly()
    {
        // Arrange
        var mockPagedList = new Mock<IPagedList<string>>();
        mockPagedList.SetupGet(x => x.Items).Returns(new List<string> { "Item1", "Item2" });
        mockPagedList.SetupGet(x => x.PageIndex).Returns(1);
        mockPagedList.SetupGet(x => x.PageSize).Returns(2);
        mockPagedList.SetupGet(x => x.TotalCount).Returns(5L); // Assuming TotalCount is a long

        // Act
        var page = mockPagedList.Object.ToPage();

        // Assert
        Assert.Equal(mockPagedList.Object.Items, page);
        Assert.Equal(mockPagedList.Object.PageIndex, page.CurrentPage);
        Assert.Equal(mockPagedList.Object.PageSize, page.PageSize);
        Assert.Equal((int)mockPagedList.Object.TotalCount, page.TotalCount);
    }
}