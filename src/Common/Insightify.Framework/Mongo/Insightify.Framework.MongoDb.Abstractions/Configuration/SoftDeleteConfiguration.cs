namespace Insightify.Framework.MongoDb.Abstractions.Configuration
{
    /// <summary>
    /// Soft Delete Configuration
    /// </summary>
    public class SoftDeleteConfiguration
    {
        public bool IsEnabled { get; private set; }
        public TimeSpan DeleteAfter { get; private set; } = TimeSpan.FromDays(31);

        /// <summary>
        /// Enables Soft Deletions
        /// </summary>
        /// <param name="value">True to enable</param>
        /// <returns><see cref="MongoConfiguration"/></returns>
        public SoftDeleteConfiguration Enabled(bool value = true)
        {
            this.IsEnabled = value;
            return this;
        }

        /// <summary>
        /// Sets the time period that Mongo will determine a record as permanently deleted
        /// </summary>
        /// <param name="value">Time Delay</param>
        /// <returns><see cref="MongoConfiguration"/></returns>
        public SoftDeleteConfiguration HardDeleteAfter(TimeSpan value)
        {
            this.DeleteAfter = value;
            return this;
        }
    }
}
