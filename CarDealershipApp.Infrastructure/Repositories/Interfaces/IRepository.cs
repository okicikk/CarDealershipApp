﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipApp.Infrastructure.Repositories.Interfaces
{
	public interface IRepository<T>
	{
		T GetById(string id);
		Task<T> GetByIdAsync(string id);

		IEnumerable<T> GetAll();
		IEnumerable<T> GetAllAsync();
		IEnumerable<T> GetAllAttached();
		IEnumerable<T> GetAllAttachedAsync();

		void Add(T item);

		Task AddAsync(T item);

		bool DeleteById(string id);
		Task<bool> DeleteByIdAsync(string id);

		void SoftDeleteById(string id);
		Task<bool> SoftDeleteByIdAsync(string id);

		bool Update(T item);
		Task<bool> UpdateAsync(T item);

	}
}