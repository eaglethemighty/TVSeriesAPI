using Microsoft.AspNetCore.Mvc;
using TVSeriesAPI.DAL.Extensions;
using TVSeriesAPI.Models.DTOs;
using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.Controllers
{
    public partial class SeriesController
    {
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
        public async Task<ActionResult<IList<SeasonReadDto>>> GetSeriesSeasons(int seriesId)
        {
            Serie Serie = (await _serieRepository.GetAllAsync()).Join(s => s.Seasons).FirstOrDefault(s => s.Id == seriesId);
            if (Serie is null)
            {
                return NotFound();
            }

            List<Season> seasons = Serie.Seasons.OrderBy(season => season.Number).ToList();

            List<SeasonReadDto> seasonsMapped = _mapper.Map<List<SeasonReadDto>>(seasons);
            return Ok(seasonsMapped);
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
        [HttpGet("{seriesId}/seasons/{seasonId}", Name = "GetSeriesSeasons")]
        public async Task<ActionResult<SeasonReadDto>> GetSeriesSeasons(int seriesId, int seasonId)
        {
            Serie serie = (await _serieRepository.GetAllAsync()).Join(s => s.Seasons).FirstOrDefault(s => s.Id == seriesId);
            if (serie is null)
            {
                return NotFound();
            }

            Season season = serie.Seasons.FirstOrDefault(season => season.Id == seasonId);
            if (season is null)
            {
                return NotFound();
            }

            SeasonReadDto seasonMapped = _mapper.Map<SeasonReadDto>(season);
            return Ok(seasonMapped);
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
        public async Task<ActionResult<SeasonReadDto>> CreateSeriesSeasons(int seriesId, SeasonCreateDTO season)
        {
            Serie serie = (await _serieRepository.GetAllAsync()).Join(s => s.Seasons).FirstOrDefault(s => s.Id == seriesId);
            if (serie is null)
            {
                return NotFound();
            }
            List<Season> seasons = serie.Seasons.OrderBy(season => season.Number).ToList();

            if (seasons.Any(s => s.Number == season.Number))
            {
                return BadRequest("This series already has a season with given number");
            }
            
            Season seasonToAdd = _mapper.Map<Season>(season);
            seasonToAdd.SerieId = seriesId;
            await _seasonRepository.AddAsync(seasonToAdd);
            bool isDatabaseChanged = await _seasonRepository.SaveChanges();
            if (!isDatabaseChanged)
            {
                return BadRequest();
            }

            return CreatedAtRoute(nameof(GetSeriesSeasons), new { seriesId = seriesId, seasonId = seasonToAdd.Id}, _mapper.Map<SeasonReadDto>(seasonToAdd));
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
            Serie serie = (await _serieRepository.GetAllAsync()).Join(s => s.Seasons).FirstOrDefault(s => s.Id == seriesId);
            if (serie is null)
            {
                return NotFound();
            }
            List<Season> seasons = serie.Seasons.OrderBy(season => season.Number).ToList();

            if (seasons.Any(s => s.Number == season.Number))
            {
                return BadRequest("This series already has a season with given number");
            }

            Season seasonToAdd = _mapper.Map<Season>(season);
            await _seasonRepository.UpdateAsync(seasonToAdd);
            bool isDatabaseChanged = await _seasonRepository.SaveChanges();
            if (!isDatabaseChanged)
            {
                return BadRequest("Error updating seasons.");
            }

            return NoContent();
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
            Serie serie = (await _serieRepository.GetAllAsync()).Join(s => s.Seasons).FirstOrDefault(s => s.Id == seriesId);
            if (serie is null)
            {
                return NotFound();
            }

            Season season = serie.Seasons.FirstOrDefault(season => season.Id == seasonId);

            if (season is null)
            {
                return BadRequest("There is no series with given season ID.");
            }

            await _seasonRepository.DeleteAsync(season);
            bool isDatabaseChanged = await _seasonRepository.SaveChanges();
            if (!isDatabaseChanged)
            {
                return BadRequest("Error deleting season.");
            }

            return NoContent();
        }
    }
}
