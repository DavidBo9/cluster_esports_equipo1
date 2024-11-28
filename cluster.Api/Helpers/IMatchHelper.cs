namespace cluster.Api.Helpers
{
    public interface IMatchHelper
    {
        Task<bool> CanScheduleMatchAsync(int team1Id, int team2Id, DateTime startTime);
        Task<bool> IsTeamAvailableAsync(int teamId, DateTime startTime, DateTime endTime);
    }
}
