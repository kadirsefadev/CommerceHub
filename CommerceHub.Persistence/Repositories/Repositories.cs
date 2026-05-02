using CommerceHub.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Persistence.Repositories
{
	public class Repositories<T> : IRepository<T> where T : class
	{

		protected readonly AppDbContext _context;
		protected readonly DbSet<T> _dbSet;
		public Repositories(AppDbContext context)
		{
			_context = context;
			_dbSet = context.Set<T>();
		}

		//await: ilgili task tamamlanana kadar operasyonun sonucunu bekler.ancak bu bekleme sırasında thread gereksiz yere bloke edilmez. 

		//async bir metodun icinde senkrron bekleme yapabilecegimizi belirtir
		// metodun içerisinde await kullanmasına izin veren ve metodun asenkron calısma akısına dahil oldugunu ifade eden kelimeld.r 

		public Task AddAsync(T item)
		{
			throw new NotImplementedException();
		}

		public Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public Task<List<T?>> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public Task<List<T>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<T?> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public IQueryable<T> Query()
		{
			throw new NotImplementedException();
		}

		public void Remove(T item)
		{
			throw new NotImplementedException();
		}

		public void Update(T item)
		{
			throw new NotImplementedException();
		}
	}
}
