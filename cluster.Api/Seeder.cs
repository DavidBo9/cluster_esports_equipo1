using cluster.Shared.Entities;

namespace cluster.Api
{
    public class Seeder
    {
        private readonly DataContext dataContext;

        public Seeder(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task SeedAsync()
        {
            await dataContext.Database.EnsureCreatedAsync();
            await CheckUsersAsync();
        }
        private async Task CheckUsersAsync()
        {
            if(!dataContext.Users.Any())
            {
                dataContext.Users.Add(new User { Username = "Fong" });
                dataContext.Users.Add(new User { Username = "Chuby" });
                dataContext.Users.Add(new User { Username = "Pingul" });
                dataContext.Users.Add(new User { Username = "Fredos" });
                dataContext.Users.Add(new User { Username = "Fong mi dios" });
            }
        }
    }
}
