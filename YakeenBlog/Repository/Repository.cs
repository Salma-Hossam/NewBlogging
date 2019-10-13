using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using YakeenBlog.Models;

namespace YakeenBlog.Repository
{
    public class Repository<TEntity, Context> : IRepository<TEntity>
        where TEntity : class
        where Context : ApplicationDbContext, new()
    {
        Context context;
        DbSet<TEntity> set;

        public Repository(Context context)
        {
            this.context = context;
            set = context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            set.Add(entity);
            return context.SaveChanges() > 0 ? entity : null;
        }

        public bool Delete(TEntity entity)
        {
            set.Remove(entity);
            return context.SaveChanges() > 0;
        }

        public IQueryable<TEntity> GetAll()
        {
            return set;
        }

        public List<TEntity> GetAllBind()
        {
            return GetAll().ToList();
        }

        public TEntity GetById(params object[] id)
        {
            return set.Find(id);
        }

        public bool Update(TEntity entity)
        {
            set.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChanges() > 0;
        }
    }
}