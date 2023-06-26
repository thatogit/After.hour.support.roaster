using After.hour.support.roaster.api.Logging;
using After.hour.support.roaster.api.Model;
using After.hour.support.roaster.api.Model.Dto;
using After.hour.support.roaster.api.Repository;
using After.hour.support.roaster.api.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace After.hour.support.roaster.api.Controllers
{
    [Route("api/TeamAPI")]
    [ApiController]
    public class TeamAPIController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ILogging _logger;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public TeamAPIController(ITeamRepository teamRepository, ILogging logger, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _logger = logger;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetTeams()
        {
            try
            {
                IEnumerable<Team> teamList = await _teamRepository.GetAllAsync();
                _response.Result = _mapper.Map<List<TeamDto>>(teamList);
                _response.statusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.Message };
            }

            _logger.Log("Get all the team details.", "");
            return _response;
        }


        [HttpGet("{Id:int}", Name = "GetTeam")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetTeam(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    _logger.Log("Get the team Error with Id:" + Id, "Error");
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);

                }
                var team = await _teamRepository.GetAsync(f => f.Id == Id);
                if (team == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);

                }
                _response.Result = _mapper.Map<TeamCreateDto>(team);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateTeam([FromBody] TeamCreateDto teamDto)
        {

            try
            {
                if (await _teamRepository.GetAsync(u => u.TeamLeader.ToLower() == teamDto.TeamLeader.ToLower() && u.TeamName == teamDto.TeamName) != null)
                {
                    ModelState.AddModelError("DuplicateError", "This record already exist!");
                    return BadRequest(ModelState);
                }
                if (teamDto == null)
                {
                    return BadRequest(teamDto);

                }

                Team team = _mapper.Map<Team>(teamDto);
                await _teamRepository.CreateAsync(team);

                //_response.Result = _mapper.Map<List<RoasterDto>>(roaster);
                _response.Result = _mapper.Map<TeamCreateDto>(team);
                _response.statusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;

                return CreatedAtRoute("GetTeam", new { Id = team.Id }, _response);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.Message };
            }

            return _response;

        }

        [HttpDelete("{Id:int}", Name = "DeleteTeam")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteTeam(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var team = await _teamRepository.GetAsync(u => u.Id == Id);
                if (team == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _teamRepository.RemoveAsync(team);

                _response.statusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.Message };
            }

            return _response;
        }

        [HttpPut("{Id:int}", Name = "UpdateTeam")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> UpdateTeam(int Id, [FromBody] TeamUpdateDto teamUpdateDto)
        {

            try
            {
                if (teamUpdateDto == null || Id != teamUpdateDto.Id)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                Team model = _mapper.Map<Team>(teamUpdateDto);

                await _teamRepository.UpdateAsync(model);
                _response.statusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessage = new List<string> { ex.Message };
                _response.IsSuccess = false;

            }
            return _response;

        }

        [HttpPatch("{Id:int}", Name = "UpdatePartialTeam")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdatePartialTeam(int Id, JsonPatchDocument<TeamUpdateDto> jsonPatch)
        {

            try
            {

                if (jsonPatch == null || Id == 0)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var team = await _teamRepository.GetAsync(u => u.Id == Id, tracked: false);

                TeamUpdateDto teamUpdateDto = _mapper.Map<TeamUpdateDto>(team);

                if (team == null)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);

                }

                jsonPatch.ApplyTo(teamUpdateDto, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);

                }

                Team model = _mapper.Map<Team>(teamUpdateDto);

                await _teamRepository.UpdateAsync(model);
                _response.statusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
            }
            catch (Exception)
            {

                throw;
            }

            return _response;
        }

    }
}
