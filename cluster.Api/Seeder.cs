using cluster.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace cluster.Api
{
    public class Seeder
    {
        private readonly DataContext dataContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public Seeder(DataContext dataContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.dataContext = dataContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await dataContext.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckUsersAsync();
            await CheckTeamsAsync();
            await CheckTournamentsAsync();
            await CheckPlayersAsync();
            await CheckMatchesAsync();
        }

        private async Task CheckRolesAsync()
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!await roleManager.RoleExistsAsync("Player"))
                await roleManager.CreateAsync(new IdentityRole("Player"));
        }

        private async Task CheckUsersAsync()
        {
            if (!userManager.Users.Any())
            {
                var users = new List<(User user, string password, string role)>
                {
                    (new User { UserName = "Fong", Email = "fong@email.com" }, "123456", "Admin"),
                    (new User { UserName = "Chuby", Email = "chuby@email.com" }, "123456", "Player"),
                    (new User { UserName = "Pingul", Email = "pingul@email.com" }, "123456", "Player"),
                    (new User { UserName = "Fredos", Email = "fredos@email.com" }, "123456", "Player"),
                    (new User { UserName = "Fong mi dios", Email = "fongdios@email.com" }, "123456", "Player")
                };

                foreach (var (user, password, role) in users)
                {
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }

        private async Task CheckTeamsAsync()
        {
            if (!dataContext.Teams.Any())
            {
                var teams = new List<Team>
                {
                    new Team { TeamName = "9z Team" },
                    new Team { TeamName = "KRÜ Esports" },
                    new Team { TeamName = "Leviatán" },
                    new Team { TeamName = "INFINITY" },
                    new Team { TeamName = "Estral Esports" },
                    new Team { TeamName = "Isurus" },
                    new Team { TeamName = "All Knights" },
                    new Team { TeamName = "Maycam Evolve" }
                };

                dataContext.Teams.AddRange(teams);
                await dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckTournamentsAsync()
        {
            if (!dataContext.Tournaments.Any())
            {
                var tournaments = new List<Tournament>
                {
                    new Tournament
                    {
                        Name = "Liga Master Flow 2024",
                        StartDate = new DateTime(2024, 1, 15),
                        EndDate = new DateTime(2024, 3, 30),
                        Location = "Buenos Aires, Argentina",
                        Status = "Active"
                    },
                    new Tournament
                    {
                        Name = "LVP SuperLiga 2024",
                        StartDate = new DateTime(2024, 2, 1),
                        EndDate = new DateTime(2024, 5, 15),
                        Location = "Madrid, España",
                        Status = "Upcoming"
                    },
                    new Tournament
                    {
                        Name = "Golden League 2024",
                        StartDate = new DateTime(2024, 3, 10),
                        EndDate = new DateTime(2024, 6, 20),
                        Location = "Bogotá, Colombia",
                        Status = "Upcoming"
                    },
                    new Tournament
                    {
                        Name = "Liga Latinoamérica Clausura 2024",
                        StartDate = new DateTime(2024, 6, 1),
                        EndDate = new DateTime(2024, 8, 15),
                        Location = "Ciudad de México, México",
                        Status = "Upcoming"
                    },
                    new Tournament
                    {
                        Name = "Copa Latinoamérica Sur",
                        StartDate = new DateTime(2023, 9, 1),
                        EndDate = new DateTime(2023, 11, 30),
                        Location = "Santiago, Chile",
                        Status = "Completed"
                    }
                };

                dataContext.Tournaments.AddRange(tournaments);
                await dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckPlayersAsync()
        {
            if (!dataContext.Players.Any() && userManager.Users.Any() && dataContext.Teams.Any())
            {
                var users = await userManager.GetUsersInRoleAsync("Player");
                var teams = await dataContext.Teams.ToListAsync();
                var players = new List<Player>();

                var usersList = users.ToList();
                for (int i = 0; i < usersList.Count; i++)
                {
                    players.Add(new Player
                    {
                        UserId = int.Parse(usersList[i].Id), // Convert string ID to int
                        TeamId = teams[i % teams.Count].Id,
                        Type = "Professional"
                    });
                }

                dataContext.Players.AddRange(players);
                await dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckMatchesAsync()
        {
            if (!dataContext.Matches.Any() && dataContext.Teams.Any() && dataContext.Tournaments.Any())
            {
                var tournaments = await dataContext.Tournaments.ToListAsync();
                var teams = await dataContext.Teams.ToListAsync();
                var matches = new List<Match>();

                foreach (var tournament in tournaments)
                {
                    // Create some matches for each tournament
                    for (int i = 0; i < teams.Count; i += 2)
                    {
                        if (i + 1 < teams.Count)
                        {
                            var match = new Match
                            {
                                TournamentId = tournament.Id,
                                Team1Id = teams[i].Id,
                                Team2Id = teams[i + 1].Id,
                                StartTime = tournament.StartDate.AddDays(i),
                                EndTime = tournament.StartDate.AddDays(i).AddHours(3),
                                Score = tournament.Status == "Completed" ? "3-2" : "0-0"
                            };
                            matches.Add(match);
                        }
                    }
                }

                dataContext.Matches.AddRange(matches);
                await dataContext.SaveChangesAsync();
            }
        }
    }
}
