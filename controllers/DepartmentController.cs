using arise_api.dtos.Generics;
using arise_api.services;
using Microsoft.AspNetCore.Mvc;

namespace arise_api.controllers
{
    [Route("api/[controller]")]
    public class DepartmentController(IDepartmentService service) : Controller
    {
        private readonly IDepartmentService _service = service;

        [HttpGet]
        public async Task<ActionResult<List<SelectOption>>> GetDepartmentOptions()
        {
            var users = await _service.GetSelectOptionsAsync();
            return Ok(users);
        }
    }
}
