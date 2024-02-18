using EntityFrameworkPaginateCore;
using PatternRepository.Application.Dto.FilterDto;
using PatternRepository.Application.Extensions;
using PatternRepository.Application.Interface.Repository;
using PatternRepository.Application.Interface.Service;
using PatternRepository.Domain.Entities;
using PatternRepositroy.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepositroy.Infrastructure.Service
{
    public class MovieService : BaseService<Movie>, IMovieService
    {
        private readonly IRepository<Movie> _repo;
        private readonly ApplicationDbContext _dbContext;

        public MovieService(IRepository<Movie> repo, ApplicationDbContext dbContext) : base(repo)
        {
            _dbContext = dbContext;
        }

        public async Task<Page<Movie>> FilterListMovie(MovieFilterDto filter)
        {
            Filters<Movie> filters = new Filters<Movie>();
            filters.Add(!filter.IsAdvance && filter.Keyword.HasValue(), x => x.Title.ToLower().Contains(filter.Keyword!.ToLower()) ||
             x.Year.ToString().ToLower().Contains(filter.Keyword!.ToLower()));

            //filters.Add(filter.IsAdvance && filter.Title.HasValue(), x => x.Title.ToLower().Contains(filter.Title!.ToLower()));
            //filters.Add(filter.IsAdvance && filter.Year.HasValue, x => x.Year.Equals(filter.Year));
            //filters.Add(filter.IsAdvance && filter.GenreId.HasValue, x => x.GenreId.Equals(filter.GenreId));

            var result = await _dbContext.Movies.PaginateAsync(filter.PageNumber, filter.PageSize, new Sorts<Movie>(), filters);
            return result;
        }

        public async Task<Page<Movie>> FilterListMovieAdvance(MovieFilterDto filtor)
        {
            Filters<Movie> filters = new Filters<Movie>();
            filters.Add(filtor.IsAdvance && filtor.Title.HasValue(), x => x.Title.ToLower().Contains(filtor.Title!.ToLower()));
            filters.Add(filtor.IsAdvance && filtor.Year.HasValue, x => x.Year.Equals(filtor.Year));
            filters.Add(filtor.IsAdvance && filtor.GenreId.HasValue, x => x.GenreId.Equals(filtor.GenreId));

            var results = await _dbContext.Movies.PaginateAsync(filtor.PageNumber, filtor.PageSize, new Sorts<Movie>(), filters);
            return results;

        }
    }
}
