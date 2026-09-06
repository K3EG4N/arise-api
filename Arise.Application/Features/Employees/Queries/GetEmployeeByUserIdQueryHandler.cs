using Arise.Application.Common.Results;
using Arise.Application.Interfaces;
using Arise.Domain.Entities;
using MediatR;

namespace Arise.Application.Features.Employees.Queries
{
    public class GetEmployeeByUserIdQueryHandler : IRequestHandler<GetEmployeeByUserIdQuery, Result<GetEmployeeByUserIdResponse>>
    {
        private readonly IRepository<Employee> _employeeRepository;

        public GetEmployeeByUserIdQueryHandler(
            IRepository<Employee> employeeRepository
        )
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Result<GetEmployeeByUserIdResponse>> Handle(GetEmployeeByUserIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.FirstOrDefaultAsync(
                e => e.UserId == request.UserId && e.DeletedAt == null,
                includes: e => e.User!);

            if (employee == null)
            {
                return Result<GetEmployeeByUserIdResponse>.Failure("Empleado no encontrado.");
            }

            return Result<GetEmployeeByUserIdResponse>.Success(new GetEmployeeByUserIdResponse
            {
                EmployeeId = employee.EmployeeId,
                UserId = request.UserId,
                Name = BuildName([employee.FirstName, employee.MiddleName ?? "", employee.PaternalLastName, employee.MaternalLastName ?? ""]),
                Email = employee.User!.Email,
                Code = employee.Code,
                Photo = employee.Photo
            });
        }

        private static string BuildName(string[] names)
        {
            return string.Join(" ", names.Where(n => !string.IsNullOrWhiteSpace(n)));
        }
    }
}
