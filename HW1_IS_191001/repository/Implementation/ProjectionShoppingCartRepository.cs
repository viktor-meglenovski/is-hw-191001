using domain.Relations;
using Microsoft.EntityFrameworkCore;
using repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace repository.Implementation
{
    public class ProjectionShoppingCartRepository : IProjectionShoppingCartRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<ProjectionShoppingCart> entities;
        public ProjectionShoppingCartRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<ProjectionShoppingCart>();
        }
        public List<ProjectionShoppingCart> GetAllProjectionsWithMovies()
        {
            return entities
                .Include(x => x.Projection)
                .Include(x => x.Projection.Movie)
                .ToListAsync().Result;
        }
    }
}
