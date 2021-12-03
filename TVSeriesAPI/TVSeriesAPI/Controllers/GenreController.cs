using Microsoft.AspNetCore.Mvc;
using TVSeriesAPI.Models.Entities;
using TVSeriesAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using TVSeriesAPI.DAL.Repositories.Interfaces;
using AutoMapper;
using TVSeriesAPI.DAL.Extensions;
using TVSeriesAPI.IIncludableEntensions;

namespace TVSeriesAPI.Controllers
{
    [Route("genres")]
    [ApiController]
    [Authorize]
    public class GenreController : ControllerBase
    {
        private readonly ILogger<GenreController> _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryJoin<Serie> _serieRepository;
        private readonly IRepositoryJoin<Genre> _genreRepository;

        public GenreController(
            ILogger<GenreController> logger,
            IMapper mapper,
            IRepositoryJoin<Serie> serieRepository,
            IRepositoryJoin<Genre> genreRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _serieRepository = serieRepository;
            _genreRepository = genreRepository;
        }

        /// <summary>
        /// Gets all genres
        /// </summary>
        /// <returns>Status code and all genres on success</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /genres
        ///     
        /// </remarks>
        /// <response code="200">If genres are returned</response>
        /// <response code="404">If no genres exist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ICollection<GenreReadDto>>> GetGenres()
        {
            throw new NotImplementedException();
            var genres = await _genreRepository.GetAllAsync();
            var genresList = genres.ToList();
            if (genres is null || genresList.Count == 0)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ICollection<GenreReadDto>>(genresList));
        }

        /// <summary>
        /// Gets genre
        /// </summary>
        /// <returns>Status code and specified genre with series on success</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /genres/1
        ///     
        /// </remarks>
        /// <response code="200">If genre is returned</response>
        /// <response code="404">If genre not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetGenre")]
        public async Task<ActionResult<GenreReadDto>> GetGenre(int id)
        {
            var genres = await _genreRepository.GetAllAsync();
            var genre = genres.FirstOrDefaultAsyncCustom(g => g.Id == id);
            if (genre is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GenreReadDto>(genre));
        }

        /// <summary>
        /// Gets series of genre
        /// </summary>
        /// <returns>Status code and series of specified genre on success</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /genres/1/series
        ///     
        /// </remarks>
        /// <response code="200">If series are returned</response>
        /// <response code="404">If genre not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}/series")]
        public async Task<ActionResult<ICollection<SerieReadDto>>> GetSeriesOfGenre(int id)
        {
            var genres = await _genreRepository.GetAllAsync();
            var genre = genres.FirstOrDefaultAsyncCustom(g => g.Id == id);
            if (genre is null)
            {
                return NotFound();
            }
            var seriesQuery = await _serieRepository.GetAllAsync();
            var seriesList = seriesQuery.Where(s => s.GenreId == id).Join(series => series.Genre).ToListAsyncCustom();
            return Ok(_mapper.Map<ICollection<SerieReadDto>>(seriesList));
        }

        /// <summary>
        /// Creates a new series
        /// </summary>
        /// <param name="genreCreateDto">New genre object</param>
        /// <returns>Status code, and created series object on success</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /genres
        ///     {
        ///       "Name": "genre name"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201">If genre was created successfully</response>
        /// <response code="400">If failed to save genre to db</response>
        /// <response code="401">If client is unauthorized</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<ActionResult<GenreReadDto>> CreateGenre(GenreCreateDto genreCreateDto)
        {
            var genreModel = _mapper.Map<Genre>(genreCreateDto);
            await _genreRepository.AddAsync(genreModel);
            bool savedToDb = await _genreRepository.SaveChanges();
            if (!savedToDb)
            {
                return BadRequest();
            }
            var genreReadDto = _mapper.Map<GenreReadDto>(genreModel);
            return CreatedAtRoute(nameof(GetGenre), new { Id = genreModel.Id }, genreReadDto);
        }

        /// <summary>
        /// Updates Genre
        /// </summary>
        /// <returns>Status code on success</returns>
        /// <remarks>
        /// Sample request:
        ///     PUT /genres/1
        /// Sample body: 
        /// {
        ///     "Name": "new name"
        /// }
        /// </remarks>
        /// <response code="204">If genre is updated</response>
        /// <response code="404">If genre not found</response>
        /// <response code="401">If client is unauthorized</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGenre(int id, GenreUpdateDto genreUpdateDto)
        {
            var genresQuery = await _genreRepository.GetAllAsync();
            var genreModel = await genresQuery.FirstOrDefaultAsyncCustom(g => g.Id == id);
            if (genreModel is null)
            {
                return NotFound();
            }
            _mapper.Map(genreUpdateDto, genreModel); // << update
            await _genreRepository.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deletes Genre
        /// </summary>
        /// <returns>Status code on success</returns>
        /// <remarks>
        /// Sample request:
        ///     DELETE /genres/1
        /// </remarks>
        /// <response code="204">If genre is deleted</response>
        /// <response code="400">If failed to delete genre</response>
        /// <response code="401">If client is unauthorized</response>
        /// <response code="404">If genre not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            var genres = await _genreRepository.GetAllAsync();
            var genreModel = await genres.Join(genre=>genre.Series).FirstOrDefaultAsyncCustom(g => g.Id == id);
            if (genreModel == null)
            {
                return NotFound();
            }
            await _genreRepository.DeleteAsync(genreModel);
            bool savedToDb = await _genreRepository.SaveChanges();
            if (!savedToDb)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}

