namespace Azaliq.Services.Core.Security.Contract
{
    public interface IReCaptchaService
    {
        Task<bool> VerifyAsync(string token);
    }
}
