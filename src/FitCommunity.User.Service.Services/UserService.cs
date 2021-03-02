using FitCommunity.User.Service.Contracts.V1.Requests;
using FitCommunity.User.Service.Contracts.V1.Responses;
using FitCommunity.User.Service.Core;
using FitCommunity.User.Service.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace FitCommunity.User.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDbContext _dbContext;

        public UserService(IUserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> Get(UserSearchRequest search)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> Insert(UserUpsertRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> Update(Guid id, UserUpsertRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
