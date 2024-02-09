using Xunit;
using System.Net.Http;

namespace CustomMiddleware.Tests
{
    public class AuthenticationMiddlewareTests
    {
        private readonly HttpClient _client;

        public AuthenticationMiddlewareTests()
        {
            _client = new HttpClient();
        }

        [Fact]
        public async Task NoCredentials_Unauthorized()
        {
            var response = await _client.GetAsync("http://localhost:5069/");
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task IncorrectCredentials_Unauthorized()
        {
            var response = await _client.GetAsync("http://localhost:5069/?username=user5&password=password2");
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task CorrectCredentials_Authorized()
        {
            var response = await _client.GetAsync("http://localhost:5069/?username=user1&password=password1");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task OnlyUsername_Unauthorized()
        {
            var response = await _client.GetAsync("http://localhost:5069/?username=user1");
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}