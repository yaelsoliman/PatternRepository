using EntityFrameworkPaginateCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PatternRepository.Application.Dto.FilterDto;
using PatternRepository.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepository.Application.Interface.Service
{
    public interface IGenreService:IBaseService<Genre>
    {
        Task<ICollection<Genre>> SearchGenresAsync(string genre);
        Task<ICollection<Genre>> PageGenresAsync(int page, int pageSize);
        Task<Page<Genre>> FilterListGenre(GenreFilterDto filterDto);
    }
}
