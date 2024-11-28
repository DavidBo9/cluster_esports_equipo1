using Microsoft.EntityFrameworkCore;

namespace cluster.Api.Helpers
{
    public class MatchHelper : IMatchHelper
    {
        private readonly DataContext _context;

        public MatchHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CanScheduleMatchAsync(int team1Id, int team2Id, DateTime startTime)
        {
            // Check if either team has a match within 24 hours
            var endTime = startTime.AddHours(3); // Assuming matches last 3 hours
            return await IsTeamAvailableAsync(team1Id, startTime, endTime) &&
                   await IsTeamAvailableAsync(team2Id, startTime, endTime);
        }

        public async Task<bool> IsTeamAvailableAsync(int teamId, DateTime startTime, DateTime endTime)
        {
            var hasConflict = await _context.Matches
                .AnyAsync(m => (m.Team1Id == teamId || m.Team2Id == teamId) &&
                              ((m.StartTime <= startTime && m.EndTime >= startTime) ||
                               (m.StartTime <= endTime && m.EndTime >= endTime) ||
                               (m.StartTime >= startTime && m.EndTime <= endTime)));

            return !hasConflict;
        }
    }
}