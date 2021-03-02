using FitCommunity.User.Service.Contracts.V1.Requests;
using FitCommunity.User.Service.Contracts.V1.Responses;
using System;
using System.Threading.Tasks;

namespace FitCommunity.User.Service.Core.Interfaces
{
    public interface IRoleService
    {
        Task<RoleResponse> Get(RoleSearchRequest search);
        Task<RoleResponse> GetById(Guid id);
    }
}
