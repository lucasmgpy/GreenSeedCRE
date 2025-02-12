﻿using GreenSeed.Data;
using Microsoft.EntityFrameworkCore;

namespace GreenSeed.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext _context { get; set; }
        private DbSet<T> _dbSet { get; set; }

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            T entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await GetByIdAsync(id, new QueryOptions<T>());
        }

        public async Task<T> GetByIdAsync(int id, QueryOptions<T> options)
        {
            IQueryable<T> query = _dbSet;
            if (options.HasWhere)
            {
                query = query.Where(options.Where);
            }
            if (options.HasOrderBy)
            {
                query = query.OrderBy(options.OrderBy);
            }
            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }

            var key = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.FirstOrDefault();
            string primaryKeyName = key?.Name;
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, primaryKeyName) == id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllByIdAsync<TKey>(TKey id, string propertyName, QueryOptions<T> options)
        {
            IQueryable<T> query = _dbSet;

            if (options.HasWhere)
            {
                query = query.Where(options.Where);
            }


            if (options.HasOrderBy)
            {
                query = query.OrderBy(options.OrderBy);
            }

            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }
            //Filtra pelo nome da propriedade e id especificado
            query = query.Where(e => EF.Property<TKey>(e, propertyName).Equals(id));

            return await query.ToListAsync();

        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await GetAllAsync(new QueryOptions<T>());
        }


        public async Task<IEnumerable<T>> GetAllAsync(QueryOptions<T> options)
        {
            IQueryable<T> query = _dbSet;

            if (options.HasWhere)
            {
                query = query.Where(options.Where);
            }

            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }

            if (options.HasOrderBy)
            {
                query = options.OrderByDirection == "DESC" ?
                    query.OrderByDescending(options.OrderBy) :
                    query.OrderBy(options.OrderBy);
            }

            return await query.ToListAsync();
        }

    }
}
