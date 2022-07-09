using domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace repository.Interface
{
    public interface IMovieRepository
    {
        Movie GetMovieWithProjections(Guid id);
    }
}
