using System;
using System.Threading.Tasks;
using Core.Data;

namespace Core.Services
{
    public interface IUserInfoService
    {
        BaseUserInfo SystemUser { get; }
        BaseUserInfo GetUserInfo(string userId);
        Task<BaseUserInfo> GetUserInfoAsync(string userId);

        BaseUserInfo GetCurrentUserInfo();
        Task<BaseUserInfo> GetCurrentUserInfoAsync();

        bool UpdateUserInfo(Guid userId, string accountId);
        bool UpdateCurrentUserInfo(Guid userId, string accountId, string personFullName, string organizationName);

        void DeleteCurrentUserInfo();

        void DeleteUserInfo(string userId);
    }
}
