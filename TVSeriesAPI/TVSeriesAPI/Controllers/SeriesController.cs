using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TVSeriesAPI.DAL.Repositories;
using TVSeriesAPI.Models.DTOs;
using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public partial class SeriesController
    {
        private readonly ILogger<SeriesController> _logger;
        private readonly SerieRepository _serieRepository;
        private readonly SeasonRepository _seasonRepository;
        private readonly EpisodeRepository _episodeRepository;

        public SeriesController(
            ILogger<SeriesController> logger,
            SerieRepository serieRepository, 
            SeasonRepository seasonRepository, 
            EpisodeRepository episodeRepository)
        {
            this._logger = logger;
            this._serieRepository = serieRepository;
            this._seasonRepository = seasonRepository;
            this._episodeRepository = episodeRepository;
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
        [HttpGet]
        public async Task<ActionResult<ICollection<SerieReadDto>>> GetSeries()
        {
            throw new NotImplementedException();
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
        [HttpGet("{seriesId}")]
        public async Task<ActionResult<SerieReadDto>> GetSeries(int seriesId)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
