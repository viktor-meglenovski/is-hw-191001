using domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace repository.Implementation
{
    public class ProjectionRepository : IProjectionRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Projection> entities;
        public ProjectionRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Projection>();
        }
        public List<Projection> GetAllProjectionsWithMovies()
        {
            return entities
                .Include(x => x.Movie)
                .ToListAsync().Result;
        }

        public Projection GetProjectionWithMovie(Guid id)
        {
            return entities
                .Include(x => x.Movie)
                .SingleOrDefaultAsync(z => z.Id==id).Result;
        }
    }
}
