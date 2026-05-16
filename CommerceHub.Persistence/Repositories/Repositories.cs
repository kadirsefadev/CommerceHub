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

        //await: ilgili task tamamlanana kadar operasyonun sonucunu bekler. Ancak bu bekleme sırasında thread gereksiz yere bloke edilmez.
        //async: Bir metodun içinde senkron bekleme yapabileceğimizi belirtir. Metodun içinde await kullanılmasına izin veren ve asenkron çalışma akışına dahil olduguu ifade eden kelimedir.

        public async Task AddAsync(T item)=>await _dbSet.AddAsync(item);
      

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();



        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) => await _dbSet.FirstOrDefaultAsync(predicate);



        public async Task<List<T>> GetAllAsync()=> await _dbSet.ToListAsync();
       

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);


        public IQueryable<T> Query()=> _dbSet.AsQueryable();
      
        public void Remove(T item)=> _dbSet.Remove(item);
     

        public void Update(T item)=> _dbSet.Update(item);
       
    }
}
