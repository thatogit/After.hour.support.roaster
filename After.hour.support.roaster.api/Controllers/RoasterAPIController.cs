using After.hour.support.roaster.api.Logging;
using After.hour.support.roaster.api.Model;
using After.hour.support.roaster.api.Model.Dto;
using After.hour.support.roaster.api.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace After.hour.support.roaster.api.Controllers
{
    [Route("api/RoasterAPI")]
    [ApiController]
    public class RoasterAPIController : ControllerBase
    {
        private readonly IRoasterRepository _roasterRepository;
        private readonly ILogging _logger;
        private readonly IMapper _mapper;
        protected APIResponse _response;


        public RoasterAPIController(IRoasterRepository roasterRepository,ILogging logger,IMapper mapper)
        {
            this._roasterRepository = roasterRepository;
            _logger = logger;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetRoasters()
        {
            try
            {
                IEnumerable<Roaster> roasterList = await _roasterRepository.GetAllAsync();
                _response.Result = _mapper.Map<List<RoasterCreateDto>>(roasterList);
                _response.statusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.Message };
            }

            _logger.Log("Get the roaster list.","");
            return _response;
        }

        [HttpGet("{Id:int}", Name ="GetRoaster")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetRoaster([FromRoute] int Id)
        {
            try
            {
                if (Id==0)
                {
                    _logger.Log("Get the roaster Error with Id:" + Id,"Error");
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                    
                }
                var roaster =await _roasterRepository.GetAsync(f => f.Id==Id);
                if (roaster ==null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);

                }
                _response.Result = _mapper.Map<RoasterCreateDto>(roaster);
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
        public async Task <ActionResult<APIResponse>> CreateRoaster([FromBody]RoasterCreateDto roasterDto)
        {

            try
            {
                if (await _roasterRepository.GetAsync(u => u.firstLine.ToLower() == roasterDto.firstLine.ToLower() && u.supportDueDate == roasterDto.supportDueDate) != null)
                {
                    ModelState.AddModelError("DuplicateError", "This record already exist!");
                    return BadRequest(ModelState);
                }
                if (roasterDto == null)
                {
                    return BadRequest(roasterDto);

                }

                Roaster roaster = _mapper.Map<Roaster>(roasterDto); 
                await _roasterRepository.CreateAsync(roaster);

                //_response.Result = _mapper.Map<List<RoasterDto>>(roaster);
                _response.Result = _mapper.Map<RoasterCreateDto>(roaster);
                _response.statusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;

                return CreatedAtRoute("GetRoaster",new {Id = roaster.Id},_response);

            }
            catch(Exception ex) 
            {

                _response.IsSuccess = false;
                _response.ErrorMessage=new List<string> { ex.Message };
            }

            return _response;

        }

        [HttpDelete("{Id:int}", Name ="DeleteRoaster")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async  Task<ActionResult<APIResponse>> DeleteRoaster(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);

                }

                var roaster = await _roasterRepository.GetAsync(u => u.Id == Id);
                if (roaster == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);

                }

                await _roasterRepository.RemoveAsync(roaster);

                _response.statusCode =HttpStatusCode.NoContent;
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

        [HttpPut("{Id:int}",Name ="UpdateRoaster")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> UpdateRoaster(int Id, [FromBody]RoasterUpdateDto roasterUpdateDto)
        {

            try
            {
                if (roasterUpdateDto == null || Id != roasterUpdateDto.Id)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                Roaster model = _mapper.Map<Roaster>(roasterUpdateDto);

                await _roasterRepository.UpdateAsync(model);
                _response.statusCode=HttpStatusCode.NoContent;
                _response.IsSuccess=true;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessage=new List<string> { ex.Message };
                _response.IsSuccess = false;

            }           
            return _response;

        }

        [HttpPatch("{Id:int}",Name ="UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdatePartialVilla(int Id,JsonPatchDocument<RoasterUpdateDto> jsonPatch)
        {

            try
            {
   
                if (jsonPatch == null || Id==0)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var roaster = await _roasterRepository.GetAsync(u=>u.Id==Id,tracked:false);

                RoasterUpdateDto roasterUpdateDto = _mapper.Map<RoasterUpdateDto>(roaster);

                if (roaster == null)
                {
                    _response.statusCode=HttpStatusCode.BadRequest;
                    return BadRequest(_response);

                }

                jsonPatch.ApplyTo(roasterUpdateDto,ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);

                }

                Roaster model = _mapper.Map<Roaster>(roasterUpdateDto);

                await _roasterRepository.UpdateAsync(model);
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
