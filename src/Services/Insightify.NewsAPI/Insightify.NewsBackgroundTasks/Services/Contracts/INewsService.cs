using Insightify.NewsBackgroundTasks.ResponceModels.LiveNews;

namespace Insightify.NewsBackgroundTasks.Services.Contracts
{
    /// <summary>
    /// Interface representing a news service for managing news articles.
    /// </summary>
    public interface INewsService
    {
        /// <summary>
        /// Adds a new news article to the service.
        /// </summary>
        /// <param name="article">The news article to be added.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Add(NewsArticleResponseModel article);

        /// <summary>
        /// Deletes a news article from the service.
        /// </summary>
        /// <param name="id">The unique identifier of the news article to be deleted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Delete(string id);
    }
}
