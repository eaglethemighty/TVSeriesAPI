using Microsoft.AspNetCore.Mvc;
using TVSeriesAPI.DAL.Extensions;
using TVSeriesAPI.Models.DTOs;
using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.Controllers
{
    public partial class SeriesController
    {
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
        public async Task<ActionResult<IList<EpisodeReadDto>>> GetSeriesSeasonsEpisodes(int seriesId, int seasonId)
        {
            Serie? serie = (await _serieRepository.GetAllAsync()).Join(s => s.Seasons).ThenJoin(seasons => seasons.Episodes).FirstOrDefault(serie => serie.Id == seriesId);
            if (serie is null)
            {
                return NotFound();
            }

            List<Season>? seasons = serie.Seasons.ToList();
            if (seasons is null)
            {
                return NotFound();
            }

            List<Episode> allEpisodes = new();

            foreach (var season in seasons)
            {
                allEpisodes.AddRange(season.Episodes);
            }

            List<EpisodeReadDto> episodesMapped = _mapper.Map<List<EpisodeReadDto>>(allEpisodes);
            return Ok(episodesMapped);
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
        public async Task<ActionResult<EpisodeReadDto>> GetSeriesSeasonsEpisodes(int seriesId, int seasonId, int episodeId)
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
        public async Task<ActionResult<EpisodeReadDto>> PostSeriesSeasonsEpisodes(int seriesId, int seasonId, EpisodeCreateDto episode)
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
        public async Task<ActionResult> PutSeriesSeasonsEpisodes(int seriesId, int seasonId, int episodeId, EpisodeCreateDto episode)
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
