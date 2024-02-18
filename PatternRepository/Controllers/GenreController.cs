using JobFinder.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using PatternRepository.Application.Dto;
using PatternRepository.Application.Interface.Service;
using PatternRepository.Domain.Entities;
using PatternRepository.Extensions;

namespace PatternRepository.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly ICurrentUser _currentUser;

        public GenreController(IGenreService genreService,ICurrentUser current)
        {
            _genreService = genreService;
            _currentUser = current;
        }
        [HttpGet("GetAllGenres")]
       // [Authorize(Roles ="Role Test")]
        public async Task<IActionResult> GetAllGenres()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genres=await _genreService.GetListAsync();
            if(genres == null)
                return NotFound();
            return Ok(genres.AsRespons<IEnumerable<GenreDto>>());
        }
        [HttpGet("{genreId}")]
        public async Task<IActionResult> GetGenre(Guid genreId)
        {
            if (genreId == null)
            {
                return NotFound();
            }
            var genre=await _genreService.GetAsync(genreId);
            if(genre==null)
                return NotFound();

            return Ok(genre.AsRespons<GenreDto>());
        }
        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] GenreDto genreDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (genreDto.Name.IsEmpty())
            {
                ModelState.AddModelError("", "The Field Name is Empty");
                return StatusCode(422, ModelState);
            }
            else
            {
                if(genreDto.Name.HasValue())
                {
                    var genre = new Genre
                    {
                        Name = genreDto.Name.UpperCaseFirstWord(),
                    };

                    await _genreService.CreateAsync(genre);
                    return Ok(genre);
                }

                return Content("Adding Success");
            }
            
        }
        [HttpDelete("{genreId}")]
        public async Task<IActionResult> DeleteGenre(Guid genreId)
        {
            var genre = await _genreService.GetAsync(genreId);

            if (genre == null)
            {
                return NotFound();
            }
            await _genreService.DeleteAsync(genre.Id);
            return Ok($"Deleted Successfully");
        }
        [HttpPut("{genreId}")]
        public async Task<IActionResult> UpdateGenre(Guid genreId, [FromBody] GenreDto genreDto)
        {
            if(genreId==null)
                return NotFound();

            var genre=await _genreService.GetAsync(genreId);
            if(genre==null)
                return NotFound();
            if (genreDto.Name.IsEmpty())
            {
                ModelState.AddModelError("", "The Field Name is Empty");
                return StatusCode(422, ModelState);
            }
            else
            {
                genre.Name = genreDto.Name;
            }
          
            await _genreService.UpdateAsync(genre);
            return Ok(genre);
        }
        [HttpGet("textSearch")]
        public async Task<IActionResult> SearchGenre(string genre)
        {
            var genres=await _genreService.SearchGenresAsync(genre);
            return Ok(genres);
        }
        [HttpGet("PageNumber")]
        public async Task<IActionResult> PageGenres(int page=1)
        {
            var genres = _genreService.PageGenresAsync(page, 2);
            return Ok( genres);
        }
    }
}
