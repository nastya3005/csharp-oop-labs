using Lab5.Application.Interfaces.Repositories;
using Lab5.Domain.Entities;
using Lab5.Infrastructure.Data;

namespace Lab5.Infrastructure.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly InMemoryDataContext _context;

    public SessionRepository(InMemoryDataContext context)
    {
        _context = context;
    }

    public async Task<Session?> GetByIdAsync(Guid id)
    {
        return await Task.FromResult(_context.Sessions.FirstOrDefault(s => s.Id == id));
    }

    public async Task AddAsync(Session session)
    {
        _context.Sessions.Add(session);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Session session)
    {
        Session? existingSession = await GetByIdAsync(session.Id);
        if (existingSession != null)
        {
            _context.Sessions.Remove(existingSession);
            _context.Sessions.Add(session);
        }

        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        Session? session = await GetByIdAsync(id);
        if (session != null)
        {
            _context.Sessions.Remove(session);
        }

        await Task.CompletedTask;
    }

    public async Task<IEnumerable<Session>> GetExpiredSessionsAsync()
    {
        return await Task.FromResult(
            _context.Sessions.Where(s => !s.IsActive).ToList());
    }
}