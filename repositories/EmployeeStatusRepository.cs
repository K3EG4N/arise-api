using arise_api.entities;
using arise_api.generic;
using arise_api.provider;

namespace arise_api.repositories
{
    public interface IEmployeeStatusRepository : IBaseRepository<EmployeeStatus> { }

    public class EmployeeStatusRepository(AriseDbContext context) : BaseRepository<EmployeeStatus>(context), IEmployeeStatusRepository { }
}
