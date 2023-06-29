﻿namespace Insightify.Posts.Domain.Common
{
    public interface IFactory<out TEntity> where TEntity : IAggregateRoot
    {
        TEntity Build();
    }
}
