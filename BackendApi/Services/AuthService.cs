using BackendApi.Models;
using BackendApi.Repositories;
using BackendApi.Utils;
using System.Diagnostics;

namespace BackendApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtTokenGenerator _tokengenerator;
        private readonly IAuthRepository _Repository;

        public AuthService(JwtTokenGenerator token, IAuthRepository Repository)
        {
            _tokengenerator = token;
            _Repository = Repository;
                
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                User user = await _Repository.ValidateUserAsync(request.Username, request.Password);
                if (user == null)
                {
                    return null;
                }

                string token = _tokengenerator.GenerateToken(user);
                LoginResponse response = new LoginResponse();
                response.Token = token;
                response.Role = user.Role;

                return response;    
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in AuthService LoginAsync: " + ex.ToString());
                throw;
            }
            
        }
    }
}
