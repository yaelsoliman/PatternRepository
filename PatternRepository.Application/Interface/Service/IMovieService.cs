using EntityFrameworkPaginateCore;
using PatternRepository.Application.Dto.FilterDto;
using PatternRepository.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepository.Application.Interface.Service
{
    public interface IMovieService:IBaseService<Movie>
    {
        Task<Page<Movie>> FilterListMovie(MovieFilterDto filter);
        Task<Page<Movie>> FilterListMovieAdvance(MovieFilterDto filter);
    }
}
