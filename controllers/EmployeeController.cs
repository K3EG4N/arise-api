using arise_api.dtos.Generics;
using arise_api.dtos.Request;
using arise_api.dtos.Responses;
using arise_api.generic;
using arise_api.services;
using Microsoft.AspNetCore.Mvc;

namespace arise_api.controllers
{
    [Route("api/[controller]")]
    public class EmployeeController(IEmployeeService service) : Controller
    {
        private readonly IEmployeeService _service = service;

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<EmployeeByUserId>> GetEmployeByUserId(Guid userId)
        {
            var users = await _service.GetEmployeeByUserId(userId);
            return Ok(users);
        }

        [HttpGet]
        public async Task<ActionResult<DataGroup<ListEmployeeResponse>>> GetAllEmployees([FromQuery] BaseFilter filter)
        {
            var employees = await _service.GetAllEmployeesAsync(filter);
            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse>> CreateEmployee([FromBody] CreateEmployeeRequest request)
        {
            var result = await _service.CreateEmployeeAsync(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("bulk")]
        public async Task<ActionResult<BaseResponse>> CreateBulkEmployees([FromBody] FileUpload request)
        {
            var result = await _service.CreateBulkEmployeesAsync(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{employeeId}")]
        public async Task<ActionResult<BaseResponse>> UpdateEmployee(Guid employeeId, [FromBody] UpdateEmployeeRequest request)
        {
            var result = await _service.UpdateEmployeeAsync(employeeId, request);
            return StatusCode(result.StatusCode, result);
        }
    }
}
