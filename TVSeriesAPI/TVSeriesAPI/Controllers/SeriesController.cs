using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TVSeriesAPI.Controllers.Errors;
using TVSeriesAPI.DAL.Extensions;
using TVSeriesAPI.DAL.Repositories;
using TVSeriesAPI.DAL.Repositories.Interfaces;
using TVSeriesAPI.Models.DTOs;
using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public partial class SeriesController : Controller
    {
        private readonly ILogger<SeriesController> _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryJoin<Serie> _serieRepository;
        private readonly IRepositoryJoin<Season> _seasonRepository;
        private readonly IRepositoryJoin<Episode> _episodeRepository;
        private readonly IRepositoryJoin<Genre> _genreRepository;

        public SeriesController(
            ILogger<SeriesController> logger,
            IMapper mapper,
            IRepositoryJoin<Serie> serieRepository,
            IRepositoryJoin<Season> seasonRepository,
            IRepositoryJoin<Episode> episodeRepository,
            IRepositoryJoin<Genre> genreRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._serieRepository = serieRepository;
            this._seasonRepository = seasonRepository;
            this._episodeRepository = episodeRepository;
            this._genreRepository = genreRepository;
        }

        /// <summary>
        /// Gets all series
        /// </summary>
        /// <returns>Status code, and all series on success</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /series
        ///     
        /// </remarks>
        /// <response code="200">If series are returned</response>
        /// <response code="404">If no series exist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: series
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ICollection<SerieReadDto>>> GetSeries()
        {
            var seriesQuery = await _serieRepository.GetAllAsync();
            var series = seriesQuery.Join(x => x.Genre).ToList();
            if (series is null || series.Count == 0) return NotFound();

            return Ok(_mapper.Map<ICollection<SerieReadDto>>(series));
        }

        /// <summary>
        /// Gets a series by ID
        /// </summary>
        /// <param name="seriesId">Series ID</param>
        /// <returns>Status code, and series on success</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /series/5
        ///     
        /// </remarks>
        /// <response code="200">If series is returned</response>
        /// <response code="404">If series was not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: series/5
        [AllowAnonymous]
        [HttpGet("{seriesId}")]
        public async Task<ActionResult<SerieReadDto>> GetSeries(int seriesId)
        {
            var seriesQuery = await _serieRepository.GetAllAsync();
            var serie = seriesQuery.Join(x => x.Genre).FirstOrDefault(x => x.Id == seriesId);
            if (serie is null) return NotFound();

            return Ok(_mapper.Map<SerieReadDto>(serie));
        }

        /// <summary>
        /// Creates a new series
        /// </summary>
        /// <param name="serie">New series object</param>
        /// <returns>Status code, and created series object on success</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /series
        ///     {
        ///       "title": "Title of new Serie",
        ///       "releaseYear": 2000,
        ///       "genreId": 5
        ///     }
        ///     
        /// </remarks>
        /// <response code="201">If series was created successfully</response>
        /// <response code="400">If series object wasn't valid</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST: series
        [HttpPost]
        public async Task<ActionResult<SerieReadDto>> PostSeries(SerieCreateDto serie)
        {
            var serieEntity = _mapper.Map<Serie>(serie);
            var genreQuery = await _genreRepository.GetAllAsync();
            if (genreQuery.FirstOrDefault(x => x.Id == serie.GenreId) is null)
            {
                Dictionary<string, string> errors = new() { { "genreId", "Genre does not exist in database." } };
                return CustomBadRequest(errors);
            }
            await _serieRepository.AddAsync(serieEntity);
            bool result = await _serieRepository.SaveChanges();
            if (result is false) return BadRequest();

            return CreatedAtAction("GetSeries", new { seriesId = serieEntity.Id }, _mapper.Map<SerieReadDto>(serieEntity));
        }

        /// <summary>
        /// Updates a series
        /// </summary>
        /// <param name="seriesId">Series ID</param>
        /// <param name="serie">Updated series object</param>
        /// <returns>Status code</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /series/5
        ///     {
        ///       "title": "Title of updated Serie",
        ///       "releaseYear": 2000,
        ///       "genreId": 5
        ///     }
        ///     
        /// </remarks>
        /// <response code="204">If series was updated successfully</response>
        /// <response code="400">If series object wasn't valid</response>
        /// <response code="404">If series was not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // PUT: series
        [HttpPut("{seriesId}")]
        public async Task<IActionResult> PutSeries(int seriesId, SerieCreateDto serie)
        {
            var genreQuery = await _genreRepository.GetAllAsync();
            if (genreQuery.FirstOrDefault(x => x.Id == serie.GenreId) is null)
            {
                Dictionary<string, string> errors = new() { { "genreId", "Genre does not exist in database." } };
                return CustomBadRequest(errors);
            }
            var serieQuery = await _serieRepository.GetAllAsync();
            var serieEntity = serieQuery.FirstOrDefault(x => x.Id == seriesId);
            if (serieEntity is null) return NotFound();
            var updatedSerieEntity = _mapper.Map(serie, serieEntity);
            await _serieRepository.UpdateAsync(updatedSerieEntity);
            bool result = await _serieRepository.SaveChanges();
            if (result is false) return BadRequest();

            return NoContent();
        }

        /// <summary>
        /// Deletes series and all associated seasons and episodes
        /// </summary>
        /// <param name="seriesId">Series ID</param>
        /// <returns>Status code</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /series/5
        ///     
        /// </remarks>
        /// <response code="204">If series was deleted successfully</response>
        /// <response code="404">If series was not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // DELETE: series/5
        [HttpDelete("{seriesId}")]
        public async Task<IActionResult> DeleteSeries(int seriesId)
        {
            var seriesQuery = await _serieRepository.GetAllAsync();
            var serie = seriesQuery.FirstOrDefault(x => x.Id == seriesId);
            if (serie is null) return NotFound();
            await _serieRepository.DeleteAsync(serie);
            bool result = await _serieRepository.SaveChanges();
            if (result is false) return BadRequest();

            return NoContent();
        }

        private BadRequestObjectResult CustomBadRequest(Dictionary<string, string> errors)
        {
            var traceId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            return BadRequest(new BadRequestError(traceId!, errors));
        }
    }
}
