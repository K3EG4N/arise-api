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
    }
}
