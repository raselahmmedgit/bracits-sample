using lab.WebApi19Sample.Identity;
using lab.WebApi19Sample.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace lab.WebApi19Sample.Manager
{
    public class SecurityManager : ISecurityManager
    {
        private readonly IConfiguration _iConfiguration;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<SecurityManager> _logger;
        
        public SecurityManager(IConfiguration iConfiguration,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ILogger<SecurityManager> logger)
        {
            _iConfiguration = iConfiguration;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        
        public string GenerateJsonWebToken(TokenViewModel viewModel)
        {
            try
            {
                _logger.LogInformation("GenerateJsonWebToken Called.");
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iConfiguration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_iConfiguration["Jwt:Issuer"],
                    _iConfiguration["Jwt:Issuer"],
                    null,
                    expires: DateTime.Now.AddMinutes(Convert.ToInt32(_iConfiguration["Jwt:Expire"])),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error in GenerateJsonWebToken." + ex.StackTrace);
            }
            return null;
        }

        public async Task<ApplicationUser> GetUser(string emailAddress)
        {
            try
            {
                _logger.LogInformation("AuthenticateUser Called.");

                ApplicationUser user = await _userManager.FindByNameAsync(emailAddress);

                if (user != null)
                { 
                    _logger.LogInformation("Valid User.");
                    return user;
                }
                else
                {
                    _logger.LogInformation("Invalid User.");
                    return null;
                }
                
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error in AuthenticateUser." + ex.StackTrace);
                return null;
            }
            
        }

    }

    public interface ISecurityManager
    {
        string GenerateJsonWebToken(TokenViewModel viewModel);
        Task<ApplicationUser> GetUser(string emailAddress);
    }
}
