using domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace domain.ViewModels
{
    public class MovieAndProjectionsViewModel
    {
        public Movie Movie { get; set; }
        public List<Projection> Projections { get; set; }
        public MovieAndProjectionsViewModel()
        {
            Projections = new List<Projection>();
        }
        public MovieAndProjectionsViewModel(Movie movie, List<Projection> projections)
        {
            Movie = movie;
            Projections = projections;
        }
    }
}
