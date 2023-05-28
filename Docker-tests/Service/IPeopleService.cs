using Docker_Tests.Domain;

namespace Docker_tests.Service;

public interface IPeopleService
{
    public Task<IEnumerable<Person>> GetFiltered(string filter);
}
