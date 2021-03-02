using FitCommunity.User.Service.Contracts.V1.Requests;
using FitCommunity.User.Service.Contracts.V1.Responses;
using FitCommunity.User.Service.Core;
using FitCommunity.User.Service.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace FitCommunity.User.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUserDbContext _dbContext;

        public RoleService(IUserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<RoleResponse> Get(RoleSearchRequest search)
        {
            throw new NotImplementedException();
        }

        public Task<RoleResponse> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
