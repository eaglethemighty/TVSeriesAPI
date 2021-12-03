using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        // GET: series/5/seasons/5/episodes
        [HttpGet("{seriesId}/seasons/{seasonId}/episodes")]
        public async Task<ActionResult<IList<EpisodeReadDto>>> GetSeriesSeasonsEpisodes(int seriesId, int seasonId)
        {
            var serie = await (await _serieRepository.GetAllAsync()).Join(s => s.Seasons).ThenJoin(seasons => seasons.Episodes).FirstOrDefaultAsync(serie => serie.Id == seriesId);
            if (serie is null)
            {
                return NotFound();
            }

            var season = serie.Seasons.ToList().FirstOrDefault(season => season.Id == seasonId);
            if (season is null)
            {
                return NotFound();
            }

            List<Episode> allEpisodes = season.Episodes.ToList();

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
        [AllowAnonymous]
        // GET: series/5/seasons/5/episodes/5
        [HttpGet("{seriesId}/seasons/{seasonId}/episodes/{episodeId}", Name = "GetSeriesSeasonsEpisodes")]
        public async Task<ActionResult<EpisodeReadDto>> GetSeriesSeasonsEpisodes(int seriesId, int seasonId, int episodeId)
        {
            var serie = await (await _serieRepository.GetAllAsync()).Join(s => s.Seasons).ThenJoin(seasons => seasons.Episodes).FirstOrDefaultAsync(serie => serie.Id == seriesId);
            if (serie is null)
            {
                return NotFound();
            }

            var season = serie.Seasons.ToList().FirstOrDefault(season => season.Id == seasonId);
            if (season is null)
            {
                return NotFound();
            }

            var episode = season.Episodes.ToList().FirstOrDefault(ep => ep.Id == episodeId);
            if (episode is null)
            {
                return NotFound();
            }

            EpisodeReadDto episodesMapped = _mapper.Map<EpisodeReadDto>(episode);
            return Ok(episodesMapped);
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
        /// <response code="401">If client is unauthorized</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // POST: series/5/seasons/5/episodes
        [HttpPost("{seriesId}/seasons/{seasonId}/episodes")]
        public async Task<ActionResult<EpisodeReadDto>> PostSeriesSeasonsEpisodes(int seriesId, int seasonId, EpisodeCreateDto episode)
        {
            var serie = await (await _serieRepository.GetAllAsync()).Join(s => s.Seasons).ThenJoin(seasons => seasons.Episodes).FirstOrDefaultAsync(serie => serie.Id == seriesId);
            if (serie is null)
            {
                return NotFound();
            }

            var season = serie.Seasons.ToList().FirstOrDefault(season => season.Id == seasonId);
            if (season is null)
            {
                return NotFound();
            }


            if (season.Episodes.ToList().Any(ep => ep.Number == episode.Number))
            {
                Dictionary<string, string> errors = new() { { "Number", "Duplicate episode number for the sesason." } };
                return CustomBadRequest(errors);
            }

            Episode episodeToAdd = _mapper.Map<Episode>(episode);


            return CreatedAtRoute(nameof(GetSeriesSeasonsEpisodes), new { seasonId = episodeToAdd.SeasonId }, _mapper.Map<EpisodeReadDto>(episodeToAdd));
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
        /// <response code="401">If client is unauthorized</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // PUT: series/5/seasons/5/episodes/5
        [HttpPut("{seriesId}/seasons/{seasonId}/episodes/{episodeId}")]
        public async Task<ActionResult> PutSeriesSeasonsEpisodes(int seriesId, int seasonId, int episodeId, EpisodeCreateDto episode)
        {
            var serie = await (await _serieRepository.GetAllAsync()).Join(s => s.Seasons).ThenJoin(seasons => seasons.Episodes).FirstOrDefaultAsync(serie => serie.Id == seriesId);
            if (serie is null)
            {
                return NotFound();
            }

            var season = serie.Seasons.ToList().FirstOrDefault(season => season.Id == seasonId);
            if (season is null)
            {
                return NotFound();
            }


            var episodeToUpdate = season.Episodes.ToList().FirstOrDefault(ep => ep.Id == episodeId);
            if (episodeToUpdate is null)
            {
                return NotFound();
            }

            Episode episodeToUpdateInDatabase = _mapper.Map(episode, episodeToUpdate);

            return CreatedAtRoute(nameof(GetSeriesSeasonsEpisodes), new { seasonId = episodeToUpdateInDatabase.SeasonId }, _mapper.Map<EpisodeReadDto>(episodeToUpdateInDatabase));
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
        /// <response code="401">If client is unauthorized</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // DELETE: series/5/seasons/5/episodes/5
        [HttpDelete("{seriesId}/seasons/{seasonId}/episodes/{episodeId}")]
        public async Task<ActionResult> DeleteSeriesSeasonsEpisodes(int seriesId, int seasonId, int episodeId)
        {
            var serie = await (await _serieRepository.GetAllAsync()).Join(s => s.Seasons).ThenJoin(seasons => seasons.Episodes).FirstOrDefaultAsync(serie => serie.Id == seriesId);
            if (serie is null)
            {
                return NotFound();
            }

            var season = serie.Seasons.ToList().FirstOrDefault(season => season.Id == seasonId);
            if (season is null)
            {
                return NotFound();
            }

            var episode = season.Episodes.ToList().FirstOrDefault(ep => ep.Id == episodeId);
            if (episode is null)
            {
                return NotFound();
            }

            await _episodeRepository.DeleteAsync(episode);
            bool isDatabaseChanged = await _episodeRepository.SaveChanges();
            if (!isDatabaseChanged)
            {
                Dictionary<string, string> errors = new() { { "Database Error", "Episode update failed." } };
                return CustomBadRequest(errors);
            }

            return NoContent();
        }
    }
}
