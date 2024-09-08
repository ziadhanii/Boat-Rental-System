namespace BoatSystem.Core.Interfaces
{
    using BoatSystem.Core.Models.BoatSystem.Application.Models;
    using System.Threading.Tasks;
    using System.IdentityModel.Tokens.Jwt;
    using BoatSystem.Core.Models;
    using BoatSystem.Core.Entities;

    public interface IAuthService
    {
        Task<AuthModel> Login(TokenRequestModel model);
        Task<AuthModel> RegisterCustomerAsync(RegisterCustomerModel model);
        Task<AuthModel> RegisterOwnerAsync(RegisterOwnerModel model);
        Task<AuthModel> RegisterAdminAsync(RegisterAdminModel model);
        Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);
        Task<OperationResult> VerifyOwnerAsync(string userId);
        Task<List<Owner>> GetUnverifiedOwnersAsync();

    }

}
