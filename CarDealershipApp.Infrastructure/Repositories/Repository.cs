using CarDealershipApp.Data;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipApp.Infrastructure.Repositories
{
	public class Repository<T> : IRepository<T> where T 
		: class
	{
		private readonly CarDealershipDbContext context;
		private readonly DbSet<T> dbSet;
        public Repository(CarDealershipDbContext context)
        {
            this.context = context;
			this.dbSet = this.context.Set<T>();
        }
        public void Add(T item)
		{
			context.Add(item);
			context.SaveChanges();
		}

		public async Task AddAsync(T item)
		{
			await context.AddAsync(item);
			await context.SaveChangesAsync();
		}

		public bool DeleteById(string id)
		{
			T item = dbSet.Find(id);
			if (item is null)
			{
				return false;
			}
			dbSet.Remove(item);
			context.SaveChanges();
			return true;
		}

		public async Task<bool> DeleteByIdAsync(string id)
		{
			T item = await dbSet.FindAsync(id);
			if (item is null)
			{
				return false;
			}
			dbSet.Remove(item);
			await context.SaveChangesAsync();
			return true;
		}

		public IEnumerable<T> GetAll()
		{
			return dbSet.AsQueryable();
		}

		public async IEnumerable<T> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<T> GetAllAttached()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<T> GetAllAttachedAsync()
		{
			throw new NotImplementedException();
		}

		public T GetById(string id)
		{
			throw new NotImplementedException();
		}

		public Task<T> GetByIdAsync(string id)
		{
			throw new NotImplementedException();
		}

		public bool Update(T item)
		{
			try
			{
				dbSet.Attach(item);
				context.Entry(item).State = EntityState.Modified;
				context.SaveChanges();
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
			
		}

		public async Task<bool> UpdateAsync(T item)
		{
			try
			{
				dbSet.Attach(item);
				context.Entry(item).State = EntityState.Modified;
				await context.SaveChangesAsync();
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}
	}
}
