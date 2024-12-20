﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipApp.Infrastructure.Repositories.Interfaces
{
	public interface IRepository<T>
	{
		T GetById(string id);
        T GetById(int id);
        Task<T> GetByIdAsync(int id);

        Task<T> GetByIdAsync(string id);

		IEnumerable<T> GetAll();
		Task<IEnumerable<T>> GetAllAsync();
		IQueryable<T> GetAllQueryable();

		void Add(T item);

		Task AddAsync(T item);

		bool DeleteById(string id);
		Task<bool> DeleteByIdAsync(string id);
		Task<bool> DeleteByIdAsync(int id);
        Task<bool> DeleteByConditionAsync(Expression<Func<T, bool>> predicate);

        Task<bool> DeleteAsync(T item);


		bool Update(T item);
		Task<bool> UpdateAsync(T item);

	}
}
