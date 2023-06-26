using After.hour.support.roaster.api.Logging;
using After.hour.support.roaster.api.Model.Dto;
using After.hour.support.roaster.api.Model;
using After.hour.support.roaster.api.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace After.hour.support.roaster.api.Controllers
{
    [Route("api/EmployeeAPI")]
    [ApiController]
    public class EmployeeAPIController : ControllerBase
    {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly ILogging _logger;
            private readonly IMapper _mapper;
            protected APIResponse _response;


            public EmployeeAPIController(IEmployeeRepository employeeRepository, ILogging logger, IMapper mapper)
            {
                _employeeRepository = employeeRepository;
                _logger = logger;
                _mapper = mapper;
                _response = new APIResponse();
            }

            [ProducesResponseType(StatusCodes.Status200OK)]
            [HttpGet]
            public async Task<ActionResult<APIResponse>> GetEmployees()
            {
                try
                {
                    IEnumerable<Employee> employeeList = await _employeeRepository.GetAllAsync();
                    _response.Result = _mapper.Map<List<EmployeeDto>>(employeeList);
                    _response.statusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;

                    return Ok(_response);
                }
                catch (Exception ex)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string> { ex.Message };
                }

                _logger.Log("Get all the employees details.", "");
                return _response;
            }

            [HttpGet("Id", Name = "GetEmployee")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ActionResult<APIResponse>> GetEmployee(int Id)
            {
                try
                {
                    if (Id == 0)
                    {
                        _logger.Log("Get the employee Error with Id:" + Id, "Error");
                        _response.statusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_response);

                    }
                    var employee = await _employeeRepository.GetAsync(f => f.EmployeeID == Id);
                    if (employee == null)
                    {
                        _response.statusCode = HttpStatusCode.NotFound;
                        return NotFound(_response);

                    }
                    _response.Result = _mapper.Map<EmployeeDto>(employee);
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
            public async Task<ActionResult<APIResponse>> CreateEmployee([FromBody] EmployeeCreateDto employeeCreate)
            {

                try
                {
                //Include all the required field for this validation
                    if (await _employeeRepository.GetAsync(u => u.firstName.ToLower() == employeeCreate.firstName.ToLower() && u.email == employeeCreate.email) != null)
                    {
                        ModelState.AddModelError("DuplicateError", "This record already exist!");
                        return BadRequest(ModelState);
                    }
                    if (employeeCreate == null)
                    {
                        return BadRequest(employeeCreate);

                    }

                    Employee employee = _mapper.Map<Employee>(employeeCreate);
                    await _employeeRepository.CreateAsync(employee);

                    _response.Result = _mapper.Map<EmployeeCreateDto>(employee);
                    _response.statusCode = HttpStatusCode.Created;
                    _response.IsSuccess = true;

                    return CreatedAtRoute("GetEmployee", new { Id = employee.EmployeeID }, _response);

                }
                catch (Exception ex)
                {

                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string> { ex.Message };
                }

                return _response;

            }

            [HttpDelete("{Id:int}", Name = "DeleteEmployee")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<APIResponse>> DeleteEmployee(int Id)
            {
                try
                {
                    if (Id == 0)
                    {
                        _response.statusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_response);

                    }

                    var employee = await _employeeRepository.GetAsync(u => u.EmployeeID == Id);
                    if (employee == null)
                    {
                        _response.statusCode = HttpStatusCode.NotFound;
                        return NotFound(_response);

                    }

                    await _employeeRepository.RemoveAsync(employee);

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

            [HttpPut("{Id:int}", Name = "UpdateEmployee")]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            public async Task<ActionResult<APIResponse>> UpdateEmployee(int Id, [FromBody] EmployeeUpdateDto employeeUpdateDto)
            {

                try
                {
                    if (employeeUpdateDto == null || Id != employeeUpdateDto.EmployeeID)
                    {
                        _response.statusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_response);
                    }

                    Employee model = _mapper.Map<Employee>(employeeUpdateDto);

                    await _employeeRepository.UpdateAsync(model);
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

            [HttpPatch("{Id:int}", Name = "UpdatePartialEmployee")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<APIResponse>> UpdatePartialVilla(int Id, JsonPatchDocument<EmployeeUpdateDto> jsonPatch)
            {

                try
                {

                    if (jsonPatch == null || Id == 0)
                    {
                        _response.statusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_response);
                    }

                    var employee = await _employeeRepository.GetAsync(u => u.EmployeeID == Id, tracked: false);

                    EmployeeUpdateDto employeeUpdateDto = _mapper.Map<EmployeeUpdateDto>(employee);

                    if (employee == null)
                    {
                        _response.statusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_response);

                    }

                    jsonPatch.ApplyTo(employeeUpdateDto, ModelState);

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);

                    }

                    Employee model = _mapper.Map<Employee>(employeeUpdateDto);

                    await _employeeRepository.UpdateAsync(model);
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
