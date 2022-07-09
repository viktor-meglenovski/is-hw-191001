using domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace repository.Interface
{
    public interface IProjectionRepository
    {
        List<Projection> GetAllProjectionsWithMovies();
        Projection GetProjectionWithMovie(Guid id);
    }
}
