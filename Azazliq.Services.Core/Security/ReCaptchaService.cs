using System.Text.Json;
using Azaliq.Services.Core.Security.Contract;
using Microsoft.Extensions.Configuration;

namespace Azaliq.Services.Core.Security
{
    public class ReCaptchaService : IReCaptchaService
    {
        private readonly string _secretKey;
        private readonly IHttpClientFactory _httpClientFactory;

        public ReCaptchaService(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _secretKey = config["GoogleReCaptcha:SecretKey"];
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> VerifyAsync(string token)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync(
                $"https://www.google.com/recaptcha/api/siteverify?secret={_secretKey}&response={token}",
                null);

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("success").GetBoolean();
        }
    }
}
