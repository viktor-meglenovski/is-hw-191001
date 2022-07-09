using domain.Identity;
using Microsoft.EntityFrameworkCore;
using repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<AppUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<AppUser>();
        }
        public IEnumerable<AppUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public AppUser Get(string id)
        {
            return entities
               .Include(z => z.ShoppingCart)
               .Include("ShoppingCart.ProjectionsInShoppingCart")
               .Include("ShoppingCart.ProjectionsInShoppingCart.Projection")
               .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(AppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(AppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(AppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
