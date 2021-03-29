using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CashRegister.Interfaces;
using CashRegister.Models.Domain;
using CashRegister.Models.Services;
using Microsoft.Extensions.Configuration;

namespace CashRegister.Services
{
    public class UserStorage : IUserStorage
    {
        private readonly IMapper _mapper;
        private readonly IList<User> _users;

        public UserStorage(IConfiguration configuration, IMapperProvider mapperProvider)
        {
            _mapper = mapperProvider.Mapper;
            _users = configuration.GetSection("Users").Get<List<User>>();
        }

        public UserSM GetUserByGuid(Guid guid)
        {
            var user = _users.FirstOrDefault(x => x.Guid == guid);
            var mapped = _mapper.Map<UserSM>(user);
            return mapped;
        }

        public UserSM GetUserByGuid(string guidString)
        {
            return !Guid.TryParse(guidString, out var guid) ? default : GetUserByGuid(guid);
        }

        public UserSM GetUserByCredentials(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(userName));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

            var user = _users.FirstOrDefault(x => x.UserName == userName && x.Password == password);

            var mapped = _mapper.Map<UserSM>(user);
            return mapped;
        }

        public bool Exists(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(userName));

            return _users.Any(x => x.UserName == userName);
        }
    }
}