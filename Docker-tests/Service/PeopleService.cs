using Docker_Tests.Domain;
using Microsoft.EntityFrameworkCore;

namespace Docker_tests.Service;

public class PeopleService : IPeopleService
{
    private readonly ApplicationContext _context;
    public PeopleService(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Person>> GetFiltered(string filter)
    {
        return await _context.People
            .Where(t => t.FirstName.Contains(filter) || t.LastName.Contains(filter))
            .ToListAsync();
    }
}
