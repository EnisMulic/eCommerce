using FitCommunity.User.Service.Contracts.V1.Requests;
using FitCommunity.User.Service.Contracts.V1.Responses;
using System;
using System.Threading.Tasks;

namespace FitCommunity.User.Service.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> Get(UserSearchRequest search);
        Task<UserResponse> GetById(Guid id);
        Task<UserResponse> Insert(UserUpsertRequest request);
        Task<UserResponse> Update(Guid id, UserUpsertRequest request);
        Task<bool> Delete(int id);
    }
}
