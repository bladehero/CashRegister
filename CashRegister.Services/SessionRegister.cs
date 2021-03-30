using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CashRegister.Interfaces;
using CashRegister.Models.Domain;
using CashRegister.Models.Exceptions.SessionExceptions;
using CashRegister.Models.Exceptions.UserStorageExceptions;
using CashRegister.Models.Services;

namespace CashRegister.Services
{
    public class SessionRegister : ISessionRegister
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserStorage _userStorage;

        public SessionRegister(IAppDbContext dbContext, IMapperProvider mapperProvider, IUserStorage userStorage)
        {
            _dbContext = dbContext;
            _mapper = mapperProvider.Mapper;
            _userStorage = userStorage;
        }

        public bool HasCurrent => Current is not null;

        public SessionSM Current
        {
            get
            {
                try
                {
                    var session = _dbContext.Sessions.SingleOrDefault(x => x.Finished.HasValue == false);
                    return _mapper.Map<SessionSM>(session);
                }
                catch (InvalidOperationException e)
                {
                    throw new MultipleSessionOpenedException("More than one sessions cannot be opened in a time.", e);
                }
            }
        }

        public async Task StartAsync(string user, decimal balance, CancellationToken cancellationToken = default)
        {
            if (_userStorage.Exists(user) == false)
            {
                throw new NoUserDefinedException($"System has no user with specified username `{user}`.");
            }
            
            if (Current is not null)
            {
                throw new MultipleSessionOpenedException("Session is already opened.");
            }
            
            var session = new Session
            {
                Started = DateTime.UtcNow,
                InitialBalance = balance,
                UserName = user
            };
            await _dbContext.Sessions.AddAsync(session, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task FinishAsync(CancellationToken cancellationToken = default)
        {
            var current = Current;
            
            if (Current is null)
            {
                throw new ClosedSessionException("No opened session exists.");
            }
            
            current.Finished = DateTime.UtcNow;
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}