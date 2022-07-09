using domain.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace repository.Interface
{
    public interface IProjectionShoppingCartRepository
    {
        List<ProjectionShoppingCart> GetAllProjectionsWithMovies();
    }
}
