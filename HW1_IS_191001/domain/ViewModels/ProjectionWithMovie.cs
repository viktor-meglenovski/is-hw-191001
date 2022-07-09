using domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace domain.ViewModels
{
    public class ProjectionWithMovie
    {
        public Projection Projection { get; set; }
        public Movie Movie { get; set; }

        public ProjectionWithMovie(Projection projection, Movie movie)
        {
            Projection = projection;
            Movie = movie;
        }
        public ProjectionWithMovie() { }
    }
}
