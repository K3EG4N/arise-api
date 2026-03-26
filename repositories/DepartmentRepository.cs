using arise_api.entities;
using arise_api.generic;
using arise_api.provider;

namespace arise_api.repositories
{
    public interface IDepartmentRepository : IBaseRepository<Department> { }

    public class DepartmentRepository(AriseDbContext context) : BaseRepository<Department>(context), IDepartmentRepository { }
}
