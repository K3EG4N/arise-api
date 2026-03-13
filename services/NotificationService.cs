using arise_api.dtos.Responses;
using arise_api.helpers;

namespace arise_api.services
{
    public interface INotificationService
    {
        Task<List<NotificationResponse>> GetNotifications(Guid userId);
    }

    public class NotificationService(IEmployeeService employeeRepository, IUserService userService) : INotificationService
    {
        public IEmployeeService _employeeRepository = employeeRepository;
        public IUserService _userService = userService;

        public async Task<List<NotificationResponse>> GetNotifications(Guid userId)
        {
            List<NotificationResponse> notifications = [];

            var user = await _userService.FindUserByIdAsync(userId);

            if (user == null)
            {
                return [];
            }

            var employee = await _employeeRepository.GetEmployeeByUserId(userId);

            if (employee == null)
            {
                notifications.Add(new()
                {
                    Title = "Complete information",
                    SubTitle = "Termine de configurar su cuenta para poder usar todas las funcionalidades",
                    Time = $"{(DateTimeHelper.GetDateTimeNow() - user.CreatedAt).Hours} hours ago"
                });
            }

            return notifications;
        }
    }
}
