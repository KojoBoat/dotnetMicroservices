﻿using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
                                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                           string includeString = null,
                                           bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
                                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                           List<Expression<Func<T, object>>> includes = null,
                                           bool disableTracking = true);
        Task<T> GetByIdAsync(int Id);
        Task<T> AddAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);
    }
}
