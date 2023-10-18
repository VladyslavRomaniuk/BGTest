using BgLocal.DataAccess.Data;
using BgLocal.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BgLocal.DataAccess.Repository {
    public class Repository<T> : IRepository<T> where T : class {
        private readonly ApplicationDbContext _db;
        public DbSet<T> dbSet;

        public Repository(ApplicationDbContext db) {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() {
            IQueryable<T> query = dbSet;
            
            return await query.ToListAsync();
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter) {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> AddAsync(T entity) {
            dbSet.Add(entity);

            return entity;
        }

        public async Task<bool> UpdateAsync(int id, T entity) {
            var existingEntity = await dbSet.FindAsync(id);

            if (existingEntity == null) {
                throw new Exception("Cant find a user");
            }

            dbSet.Entry(existingEntity).CurrentValues.SetValues(entity);

            return true;
        }

        public async Task<bool> RemoveAsync(int id) {
            var existingEntity = await dbSet.FindAsync(id);

            if (existingEntity == null) {
                throw new Exception("Cant find a user");
            }

            dbSet.Remove(existingEntity);

            return true;
        }

        public async Task SaveChanges() {
            await _db.SaveChangesAsync();
        }
    }
}
