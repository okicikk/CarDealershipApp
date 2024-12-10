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
			await dbSet.AddAsync(item);
			await context.SaveChangesAsync();
		}

		public async Task<bool> DeleteAsync(T item)
		{
			T? itemToDelete = await dbSet.FirstOrDefaultAsync(x=>x == item);
			if (item is null || itemToDelete is null)
			{
				return false;
			}
			dbSet.Remove(itemToDelete);
			await context.SaveChangesAsync();
			return true;
		}

		public bool DeleteById(string id)
		{
			T? item = dbSet.Find(id);
			if (item is null)
			{
				return false;
			}
			dbSet.Remove(item);
			context.SaveChanges();
			return true;
		}
		public bool DeleteById(int id)
		{
			T? item = dbSet.Find(id);
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
			T? item = await dbSet.FindAsync(id);
			if (item is null)
			{
				return false;
			}
			dbSet.Remove(item);
			await context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteByIdAsync(int id)
		{
			T? item = await dbSet.FindAsync(id);
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
			return dbSet.ToList();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await dbSet.ToListAsync();
		}

		public IQueryable<T> GetAllQueryable()
		{
			return dbSet.AsQueryable();
		}

		public T GetById(string id)
		{
			T? item = dbSet.Find(id);
			if (item is null)
			{
				return null;
			}
			return item;
		}
		public T GetById(int id)
		{
			T? item = dbSet.Find(id);
			if (item is null)
			{
				return null;
			}
			return item;
		}

		public async Task<T> GetByIdAsync(string id)
		{
			T? item = await dbSet.FindAsync(id);

			if (item is null)
			{
				return null;
			}
			return item;
		}
		public async Task<T> GetByIdAsync(int id)
		{
			T? item = await dbSet.FindAsync(id);

			if (item is null)
			{
				return null;
			}
			return item;
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
