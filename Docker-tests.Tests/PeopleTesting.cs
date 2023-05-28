using AutoFixture;
using Docker_Tests.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Docker_tests.Tests
{
    [Collection("Sequential")]
    public class PeopleTesting : IntegrationTestBase
    {
        private readonly Fixture _fixture;
        public PeopleTesting(WebApplicationFactory<Program> factory)
            : base(factory)
        {
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task FilterReturnsCorrectData(bool findOne)
        {
            // arrange
            var generatedData =
                _fixture.Build<Person>()
                .Without(t => t.PersonId)
                .CreateMany(100)
                .ToList();

            if (findOne)
            {
                generatedData.Add(new Person()
                {
                    FirstName = "Lorem",
                    LastName = "Ipsum"
                });
            }

            await InsertData(generatedData);

            // act
            var people = await Client.GetAsync("/people?filter=Lorem");

            // assert
            people.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            
            var content = await people.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();

            var responseData = JsonConvert.DeserializeObject<IEnumerable<Person>>(content);

            responseData.Should().NotBeNull();

            if (findOne)
            {
                responseData.Count().Should().Be(1);

                responseData.First().FirstName.Should().Be("Lorem");
                responseData.First().LastName.Should().Be("Ipsum");
            }
            else
            {
                responseData.Should().BeEmpty();
            }
            
        }
    }
}