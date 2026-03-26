using arise_api.dtos.Generics;
using arise_api.repositories;

namespace arise_api.services
{
    public interface IDepartmentService
    {
        Task<List<SelectOption>> GetSelectOptionsAsync();
    }

    public class DepartmentService(IDepartmentRepository repository) : IDepartmentService
    {
        private readonly IDepartmentRepository _repository = repository;

        public async Task<List<SelectOption>> GetSelectOptionsAsync()
        {
            var departments = await _repository.GetAllAsync(new()
            {
                OrderBy = q => q.OrderBy(d => d.Name)
            });

            return [.. departments.Select(d => new SelectOption
            {
                Value = d.DepartmentId,
                Label = d.Name
            })];
        }
    }
}
