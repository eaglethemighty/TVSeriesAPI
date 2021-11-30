using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TVSeriesAPI.DAL.Repositories;

namespace TVSeriesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SeriesController
    {
        private readonly ILogger<SeriesController> _logger;
        private readonly IRepository<Serie> _serieRepository;
        private readonly IRepository<Season> _seasonRepository;
        private readonly IRepository<Episode> _episodeRepository;

        public SeriesController(
            ILogger<SeriesController> logger, 
            IRepository<Serie> serieRepository, 
            IRepository<Season> seasonRepository, 
            IRepository<Episode> episodeRepository)
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
        public async Task<ActionResult<ICollection<SerieReadDTO>>> GetSeries()
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
        public async Task<ActionResult<SerieReadDTO>> GetSeries(int seriesId)
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
        public async Task<ActionResult<SerieReadDTO>> PostSeries(SerieCreateDTO serie)
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
        public async Task<IActionResult> PutSeries(int seriesId, SerieCreateDTO serie)
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

        /// <summary>
        /// Gets all seasons of a series
        /// </summary>
        /// <param name="seriesId">Series ID</param>
        /// <returns>Status code, and all seasons of a series on success</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /series/5/seasons
        ///     
        /// </remarks>
        /// <response code="200">If seasons are returned</response>
        /// <response code="404">If series with given ID was not found, or no seasons of that series exist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: series/5/seasons
        [HttpGet("{seriesId}/seasons")]
        public async Task<ActionResult<IList<SeasonReadDTO>>> GetSeriesSeasons(int seriesId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a season of a series by ID
        /// </summary>
        /// <param name="seriesId">Series ID</param>
        /// <param name="seasonId">Season ID</param>
        /// <returns>Status code, and season of a series on success</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /series/5/seasons/5
        ///     
        /// </remarks>
        /// <response code="200">If season is returned</response>
        /// <response code="404">If series or season with given ID was not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: series/5/seasons/5
        [HttpGet("{seriesId}/seasons/{seasonId}")]
        public async Task<ActionResult<SeasonReadDTO>> GetSeriesSeasons(int seriesId, int seasonId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new season of a given series
        /// </summary>
        /// <param name="seriesId">Series ID</param>
        /// <param name="season">New season object</param>
        /// <returns>Status code, and created season object on success</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /series/5/seasons
        ///     {
        ///       "number": 5
        ///     }
        ///     
        /// </remarks>
        /// <response code="201">If season was created successfully</response>
        /// <response code="400">If season object wasn't valid</response>
        /// <response code="404">If series with given ID was not found</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // POST: series/5/seasons
        [HttpPost("{seriesId}/seasons")]
        public async Task<ActionResult<SeasonReadDTO>> GetSeriesSeasons(int seriesId, SeasonCreateDTO season)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a season of a given series
        /// </summary>
        /// <param name="seriesId">Series ID</param>
        /// <param name="seasonId">Season ID</param>
        /// <param name="season">Updated season object</param>
        /// <returns>Status code</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /series/5/seasons/5
        ///     {
        ///       "number": 5
        ///     }
        ///     
        /// </remarks>
        /// <response code="204">If season was updated successfully</response>
        /// <response code="400">If season object wasn't valid</response>
        /// <response code="404">If series or season with given ID was not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // PUT: series/5/seasons/5
        [HttpPut("{seriesId}/seasons/{seasonId}")]
        public async Task<ActionResult> PutSeriesSeasons(int seriesId, int seasonId, SeasonCreateDTO season)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a season of a given series and all associated episodes
        /// </summary>
        /// <param name="seriesId">Series ID</param>
        /// <param name="seasonId">Season ID</param>
        /// <returns>Status code</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /series/5/seasons/5
        ///     
        /// </remarks>
        /// <response code="204">If season was deleted successfully</response>
        /// <response code="404">If series or season with given ID was not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // DELETE: series/5/seasons/5
        [HttpDelete("{seriesId}/seasons/{seasonId}")]
        public async Task<ActionResult> DeleteSeriesSeasons(int seriesId, int seasonId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all episodes of a season of a series
        /// </summary>
        /// <param name="seriesId">Series ID</param>
        /// <param name="seasonId">Season ID</param>
        /// <returns>Status code, and all episodes of a season on success</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /series/5/seasons/5/episodes
        ///     
        /// </remarks>
        /// <response code="200">If episodes are returned</response>
        /// <response code="404">If series or seasons with given ID was not found, or no episodes of that season exist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: series/5/seasons/5/episodes
        [HttpGet("{seriesId}/seasons/{seasonId}/episodes")]
        public async Task<ActionResult<IList<EpisodeReadDTO>>> GetSeriesSeasonsEpisodes(int seriesId, int seasonId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets an episode of a season of a series by ID
        /// </summary>
        /// <param name="seriesId">Series ID</param>
        /// <param name="seasonId">Season ID</param>
        /// <param name="episodeId">Episode ID</param>
        /// <returns>Status code, and episode of a season on success</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /series/5/seasons/5/episodes/5
        ///     
        /// </remarks>
        /// <response code="200">If episode is returned</response>
        /// <response code="404">If series, season, or episode with given ID was not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: series/5/seasons/5/episodes/5
        [HttpGet("{seriesId}/seasons/{seasonId}/episodes/{episodeId}")]
        public async Task<ActionResult<EpisodeReadDTO>> GetSeriesSeasonsEpisodes(int seriesId, int seasonId, int episodeId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new episode of a given season of a series
        /// </summary>
        /// <param name="seriesId">Series ID</param>
        /// <param name="seasonId">Season ID</param>
        /// <param name="episode">New episode object</param>
        /// <returns>Status code, and created episode object on success</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /series/5/seasons/5/episodes
        ///     {
        ///       "number": 5,
        ///       "title": "Title of new episode"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201">If episode was created successfully</response>
        /// <response code="400">If episode object wasn't valid</response>
        /// <response code="404">If series or season with given ID was not found</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // POST: series/5/seasons/5/episodes
        [HttpPost("{seriesId}/seasons/{seasonId}/episodes")]
        public async Task<ActionResult<EpisodeReadDTO>> PostSeriesSeasonsEpisodes(int seriesId, int seasonId, EpisodeCreateDTO episode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an episode of a given season of a series
        /// </summary>
        /// <param name="seriesId">Series ID</param>
        /// <param name="seasonId">Season ID</param>
        /// <param name="episodeId">Episode ID</param>
        /// <param name="episode">Updated episode object</param>
        /// <returns>Status code</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /series/5/seasons/5/episodes/5
        ///     {
        ///       "number": 5,
        ///       "title": "Title of updated episode"
        ///     }
        ///     
        /// </remarks>
        /// <response code="204">If episode was updated successfully</response>
        /// <response code="400">If episode object wasn't valid</response>
        /// <response code="404">If series, season, or episode with given ID was not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // PUT: series/5/seasons/5/episodes/5
        [HttpPut("{seriesId}/seasons/{seasonId}/episodes/{episodeId}")]
        public async Task<ActionResult> PutSeriesSeasonsEpisodes(int seriesId, int seasonId, int episodeId, EpisodeCreateDTO episode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes an episode of a given season of a series
        /// </summary>
        /// <param name="seriesId">Series ID</param>
        /// <param name="seasonId">Season ID</param>
        /// <param name="episodeId">Episode ID</param>
        /// <returns>Status code</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /series/5/seasons/5/episodes/5
        ///     
        /// </remarks>
        /// <response code="204">If episode was deleted successfully</response>
        /// <response code="404">If series, season, or episode with given ID was not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // DELETE: series/5/seasons/5/episodes/5
        [HttpDelete("{seriesId}/seasons/{seasonId}/episodes/{episodeId}")]
        public async Task<ActionResult> DeleteSeriesSeasonsEpisodes(int seriesId, int seasonId, int episodeId)
        {
            throw new NotImplementedException();
        }
    }
}
