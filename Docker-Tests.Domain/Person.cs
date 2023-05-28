using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docker_Tests.Domain;

public class Person
{
    public int PersonId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
