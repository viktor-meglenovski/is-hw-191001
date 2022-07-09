using domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace repository.Implementation
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Movie> entities;
        public MovieRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Movie>();
        }
        public Movie GetMovieWithProjections(Guid id)
        {
            return entities
                .Include(x => x.Projections)
                .SingleOrDefaultAsync(x => x.Id == id).Result;
        }
    }
}
