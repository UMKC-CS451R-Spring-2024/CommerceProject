using Client.Repositories.Settings.Data;

namespace Client.Repositories.Settings
{
    public interface ISettingsRepository
    {
        Task<UserSettings> GetSettings(string UserId);
        Task<UserSettings> UpdateSettings(string UserId, UserSettings request);
        Task<UserSettings> CreateSettings(UserSettings request);
    }
}