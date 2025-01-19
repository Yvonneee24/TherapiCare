﻿using System.Linq.Expressions;

namespace TherapiCareTest.Repository.IRepository
{
        public interface IRepository<T> where T : class
        {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
            void Add(T item);

            void Remove(T item);
            void RemoveRange(List<T> item);
        }
}
