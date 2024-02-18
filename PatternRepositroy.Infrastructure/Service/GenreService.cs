using EntityFrameworkPaginateCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
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
    public class GenreService : BaseService<Genre>, IGenreService
    {
        private readonly ApplicationDbContext _dbContext;

        public GenreService(IRepository<Genre> repo,ApplicationDbContext dbContext): base(repo) 
        {
            _dbContext = dbContext;
        }

        public async Task<Page<Genre>> FilterListGenre(GenreFilterDto filterDto)
        {
            Filters<Genre> filters = new Filters<Genre>();
            filters.Add(!filterDto.IsAdvance && filterDto.Keyword.HasValue(),x=>x.Name.ToLower().Contains(filterDto.Keyword!.ToLower()));

            filters.Add(filterDto.IsAdvance && filterDto.Name.HasValue(), x => x.Name.ToLower().Contains(filterDto.Name));

            var result= await _dbContext.Genres.PaginateAsync(filterDto.PageNumber, filterDto.PageSize,new Sorts<Genre>(),filters);
             return result;
        }

        public async Task<ICollection<Genre>> PageGenresAsync(int page, int pageSize)
        {
            //if(page<1)
            //    page = 0;
            //int totalPage=pageSize*page;
            //var result = _dbContext.Genres.ToList().Skip(totalPage).Take(pageSize).ToList();
            //return new DataList<Genre>
            //{
            //    result = result,
            //    page = page
            //};
            return new List<Genre>();
        }

        public async Task<ICollection<Genre>> SearchGenresAsync(string genre)
        {
                return _dbContext.Genres.Where(a => a.Name.ToLower().Contains(genre.ToLower())).ToList();
        }
    }
}
