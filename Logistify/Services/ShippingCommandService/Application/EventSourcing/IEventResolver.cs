﻿using Domain.Abstractions;

namespace Application.EventSourcing
{
    public interface IEventResolver
    {
        Task Apply<TEvent, TEntity>(TEvent @event, TEntity entity) 
            where TEvent : IEvent
            where TEntity : class;
    }
}
