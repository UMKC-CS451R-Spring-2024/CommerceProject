using Client.Repositories.Settings.Responses;
using Client.Repositories.Settings.Requests;

namespace Client.Repositories.Settings
{
    public interface ISettingsRepository
    {
        Task<GetSettingsResponse> GetSettings(string UserId);
        Task<UpdateSettingsResponse> UpdateSettings(string UserId, UpdateSettingsRequest request);
    }
}