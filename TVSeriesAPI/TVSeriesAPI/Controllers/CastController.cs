using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TVSeriesAPI.DAL.Extensions;
using TVSeriesAPI.DAL.Repositories;
using TVSeriesAPI.DAL.Repositories.Interfaces;
using TVSeriesAPI.Models.DTOs;
using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.Controllers
{
    [Route("cast")]
    [ApiController]
    [Authorize]
    public class CastController : ControllerBase
    {
        private readonly ILogger<CastController> _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryJoin<Serie> _serieRepository;
        private readonly IRepositoryJoin<CastMember> _castRepository;
        private readonly IRepositoryJoin<Season> _seasonRepository;
        private readonly IRepositoryJoin<Episode> _episodeRepository;

        public CastController(
            ILogger<CastController> logger,
            IMapper mapper,
            IRepositoryJoin<Serie> serieRepository,
            IRepositoryJoin<CastMember> castRepository,
            IRepositoryJoin<Episode> episodeRepository,
            IRepositoryJoin<Season> seasonRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _serieRepository = serieRepository;
            _castRepository = castRepository;
            _seasonRepository = seasonRepository;
            _episodeRepository = episodeRepository;
        }


        /// <summary>
        /// Gets all cast members
        /// </summary>
        /// <returns>Status code and all cast members on success</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /cast
        ///     
        /// </remarks>
        /// <response code="200">If cast members are returned</response>
        /// <response code="404">If no cast members exist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<CastMemberReadDto>>> GetCast()
        {
            var castMembers = await _castRepository.GetAllAsync();
            var castMembersList = castMembers.ToList();
            if (castMembersList is null || castMembersList.Count == 0)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ICollection<CastMemberReadDto>>(castMembersList));
        }

        /// <summary>
        /// Gets specified cast member
        /// </summary>
        /// <returns>Status code and specified cast member on success</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /cast/1
        ///     
        /// </remarks>
        /// <response code="200">If cast member is returned</response>
        /// <response code="404">If cast member doesnt exist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetCastMember")]
        public async Task<ActionResult<CastMemberReadDto>> GetCastMember(int id)
        {
            var castMembers = await _castRepository.GetAllAsync();
            var castMemberModel = castMembers.FirstOrDefault(cm => cm.Id == id);
            if (castMemberModel is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CastMemberReadDto>(castMemberModel));
        }

        /// <summary>
        /// Gets all series of speciefied cast memeber
        /// </summary>
        /// <returns>Status code and all series of speciefied cast memeber on success</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /cast/1/series
        ///     
        /// </remarks>
        /// <response code="200">If series are returned</response>
        /// <response code="404">If no cast members exist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{castMemberId}/series")]
        public async Task<ActionResult<ICollection<SerieReadDto>>> GetSeriesWhereCastMember(int castMemberId)
        {
            var castMembersQuery = await _castRepository.GetAllAsync();
            var castMember = castMembersQuery.Join(x => x.Episodes).FirstOrDefault(x => x.Id == castMemberId);
            if (castMember is null)
            {
                _logger.LogError("cast member not found");
                return NotFound();
            }
            var episodeIds = castMember.Episodes.Select(x => x.Id);

            var episodesQuery = await _episodeRepository.GetAllAsync();
            var seasonIds = episodesQuery.Where(x => episodeIds.Contains(x.Id)).Select(x => x.SeasonId).ToList().Distinct();

            var seasonsQuery = await _seasonRepository.GetAllAsync();
            var serieIds = seasonsQuery.Where(x => seasonIds.Contains(x.Id)).Select(x => x.SerieId).ToList().Distinct();

            var seriesQuery = await _serieRepository.GetAllAsync();
            var series = seriesQuery.Join(x => x.Genre).Where(x => serieIds.Contains(x.Id)).ToList().Distinct().ToList();
            if (series is null || series.Count == 0)
            {
                _logger.LogError("series not found");
                return NotFound();
            }

            return Ok(_mapper.Map<ICollection<SerieReadDto>>(series));
        }

        /// <summary>
        /// Creates a new cast member
        /// </summary>
        /// <param name="castMemberCreateDto">New cast object</param>
        /// <returns>Status code, and created cast member object on success</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /cast
        ///     {
        ///       "Name": "cast member name",
        ///       "Position": 0
        ///     }
        ///     
        /// </remarks>
        /// <response code="201">If cast member was created successfully</response>
        /// <response code="400">If failed to save cast member to db</response>
        /// <response code="401">If client is unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<ActionResult<CastMemberReadDto>> CreateCastMember(CastMemberCreateDto castMemberCreateDto)
        {
            var castModel = _mapper.Map<CastMember>(castMemberCreateDto);
            await _castRepository.AddAsync(castModel);
            bool savedToDb = await _castRepository.SaveChanges();
            if (!savedToDb)
            {
                return BadRequest();
            }
            var castMemberReadDto = _mapper.Map<CastMemberReadDto>(castModel);
            return CreatedAtRoute(nameof(GetCastMember), new { Id = castModel.Id }, castMemberReadDto);
        }


        /// <summary>
        /// Updates cast member
        /// </summary>
        /// <returns>Status code on success</returns>
        /// <remarks>
        /// Sample request:
        ///     PUT /cast/1
        /// Sample body: 
        /// {
        ///     "Name": "new name",
        ///     "Position": 0
        /// }
        /// </remarks>
        /// <response code="204">If cast member is updated</response>
        /// <response code="404">If cast member not found</response>
        /// <response code="401">If client is unauthorized</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCastMember(int id, CastMemberUpdateDto castMemberUpdateDto)
        {
            var castQuery = await _castRepository.GetAllAsync();
            var castModel = castQuery.FirstOrDefault(c => c.Id == id);
            if (castModel is null)
            {
                return NotFound();
            }
            _mapper.Map(castMemberUpdateDto, castModel); // << update
            await _castRepository.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deletes cast member
        /// </summary>
        /// <returns>Status code on success</returns>
        /// <remarks>
        /// Sample request:
        ///     DELETE /cast/1
        /// </remarks>
        /// <response code="204">If cast member is deleted</response>
        /// <response code="400">If failed to delete cast member</response>
        /// <response code="401">If client is unauthorized</response>
        /// <response code="404">If cast member not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCastMember(int id)
        {
            var cast = await _castRepository.GetAllAsync();
            var castModel = cast.FirstOrDefault(c => c.Id == id);
            if (castModel == null)
            {
                return NotFound();
            }
            await _castRepository.DeleteAsync(castModel);
            bool savedToDb = await _castRepository.SaveChanges();
            if (!savedToDb)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
