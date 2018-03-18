using LoadUserActivity.Data.Repositories.Common;
using UserActivity.Domain.Entities;
using UserActivity.Domain.Interfaces.Repositories;

namespace LoadUserActivity.Data.Repositories
{
    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository {}
}
