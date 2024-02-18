
using JobFinder.Helper;
using JobFinder.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatternRepository.Application.Dto;
using PatternRepository.Application.Dto.FilterDto;
using PatternRepository.Application.Interface.Service;
using PatternRepository.Domain.Entities;
using PatternRepository.Extensions;
using PatternRepositroy.Infrastructure.Service;

namespace PatternRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieSevice;
        private readonly ICurrentUser _currentUser;

        public MoviesController(IMovieService movieSevice, ICurrentUser current)
        {
            _movieSevice = movieSevice;
            _currentUser = current;
        }

        [HttpGet("{GenreId}/GenresMovies")]
        public async Task<IActionResult> GetAllMovies(Guid GenreId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movies = await _movieSevice.GetListAsync(x => x.GenreId == GenreId, false);
            if (movies == null)
                return NotFound();
            return Ok(movies);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movies = await _movieSevice.GetListAsync(x=>x.Genre!);
            if (movies == null)
                return NotFound();
            return Ok(movies.AsRespons<IEnumerable<MovieListDto>>());
        }
        [HttpGet("{movieId}/Movies")]
        public async Task<IActionResult> GetMovie(Guid movieId)
        {
            var movie = await _movieSevice.GetAsync(movieId);
            if (movie == null)
                return NotFound();
            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var movie = new Movie()
            {
                Title = movieDto.Title,
                StoreLine = movieDto.StoreLine,
                Year = movieDto.Year,
                GenreId = movieDto.GenreId,
                Rate = movieDto.Rate,
            };
            await _movieSevice.CreateAsync(movie);
            return Ok(movie);
        }
        [HttpDelete("{movieId}")]
        public async Task<IActionResult> DeleteMovie(Guid movieId)
        {
            if (movieId == Guid.Empty)
            {
                return NotFound();
            }
            var movie = await _movieSevice.GetAsync(movieId);
            if (movie == null)
                return NotFound();
            await _movieSevice.DeleteAsync(movieId);
            return Ok(movie);
        }
        [HttpPut("{movieId}")]
        public async Task<IActionResult> UpdateMovie(Guid movieId, [FromBody] MovieDto movie)
        {
            if (movieId == Guid.Empty)
                return BadRequest(ModelState);
            var movies = await _movieSevice.GetAsync(movieId);
            if (movies == null)
                return NotFound();
            movies.Title = movie.Title;
            movies.StoreLine = movie.StoreLine;
            movies.Year = movie.Year;
            movies.Rate = movie.Rate;
            movies.GenreId = movie.GenreId;
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

           await _movieSevice.UpdateAsync(movies);
            return Ok($"Updated Successfully");
        }
        [HttpPost("filter")]
        public async Task<IActionResult> FilterMovie(MovieFilterDto filterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (filterDto.IsAdvance == true && filterDto.Title != null && filterDto.Year != 0 && filterDto.GenreId != null)
            {
                var movieResult = await _movieSevice.FilterListMovie(filterDto);
                return Ok(movieResult);
            }
            else
            {
                var movieResults = await _movieSevice.FilterListMovieAdvance(filterDto);
                return Ok(movieResults);
            }
        }

    }
}
