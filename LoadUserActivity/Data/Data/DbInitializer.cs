using System.Linq;
using UserActivity.Data.Context;

namespace LoadUserActivity.Data.Data
{
    public static class DbInitializer
    {
        public static void Initialize()
        {
            var context = new AppDbContext();
            context.Database.EnsureCreated();

            if(context.Activities.Any())
            {
                return;
            }
        }
    }
}
