using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using lab.WebApi19Sample.Core;
using lab.WebApi19Sample.Helpers;
using lab.WebApi19Sample.Identity;
using lab.WebApi19Sample.ViewModels;
using System;
using System.Threading.Tasks;
using lab.WebApi19Sample.Manager;
using lab.WebApi19Sample.Exception;

namespace lab.WebApi19Sample.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        #region Global Variable Declaration
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<ApplicationRole> _roleManager;
        private ISecurityManager _iSecurityManager;
        private readonly ILogger<AccountController> _logger;
        #endregion

        #region Constructor
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            ISecurityManager iSecurityManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _iSecurityManager = iSecurityManager;
            _logger = logger;
        }
        #endregion

        #region Actions

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel viewModel)
        {
            try
            {
                _logger.LogInformation("Login Called.");

                if (ModelState.IsValid)
                {
                    var applicationUser = await _iSecurityManager.GetUser(viewModel.EmailAddress);

                    if (applicationUser != null)
                    {
                        _logger.LogInformation("Authenticated Successfully.");

                        await _signInManager.SignOutAsync();
                        // This doesn't count login failures towards account lockout
                        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                        var result = await _signInManager.PasswordSignInAsync(viewModel.EmailAddress, viewModel.Password, true, lockoutOnFailure: false);

                        if (result.Succeeded)
                        {
                            _logger.LogInformation("Login Successfully.");
                            return LoginJsonWebToken(viewModel, applicationUser);
                        }
                        else
                        {
                            _logger.LogInformation("Login Failed.");
                            _resultApi = ResultApi.Fail(MessageHelper.LoginFail);
                            return Error(_resultApi);
                        }

                    }
                    else
                    {
                        _logger.LogInformation("Authenticated Failed.");
                        _resultApi = ResultApi.Fail(MessageHelper.UnAuthenticated);
                        return Error(_resultApi);
                    }
                }
                else
                {
                    _logger.LogInformation("Model State Error.");
                    string message = ExceptionHelper.ModelStateErrorFirstFormat(ModelState);
                    _resultApi = ResultApi.Fail(message);
                    return Error(_resultApi);
                }

            }
            catch (System.Exception ex)
            {
                _logger.LogError(LogMessageHelper.FormateMessageForException(ex.Message, "Error"));
                return Error(ex);
            }
        }

        private IActionResult LoginJsonWebToken(LoginViewModel viewModel, ApplicationUser applicationUser)
        {
            var tokenViewModel = new TokenViewModel()
            {
                PinNumber = "123456",
                EmailAddress = applicationUser.Email,
                Password = viewModel.Password
            };
            var tokenString = _iSecurityManager.GenerateJsonWebToken(tokenViewModel);
            if (!string.IsNullOrEmpty(tokenString))
            {
                _logger.LogInformation("Token Generated Successfully.");
                var userModel = new UserViewModel()
                {
                    PinNumber = "123456",
                    UserName = applicationUser.UserName,
                    EmailAddress = applicationUser.Email,
                    Designation = "Manager",
                    RoleId = "Manager",
                    RoleName = "Manager",
                    Token = tokenString,
                };

                _resultApi = ResultApi.Ok(userModel);
                return Ok(_resultApi);
            }
            else
            {
                _logger.LogInformation("Token Generation Failed.");
                _resultApi = ResultApi.Fail(MessageHelper.TokenGenerationFailed);
                return Error(_resultApi);
            }
        }

        //
        // POST: /Account/Register
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel viewModel)
        {
            try
            {
                _logger.LogInformation("Register Called.");

                if (ModelState.IsValid)
                {
                    string userName = ((viewModel.EmailAddress).Split('@')[0]).Trim(); // you are get here username.

                    var user = new ApplicationUser
                    {
                        UserName = viewModel.EmailAddress,
                        Email = viewModel.EmailAddress
                    };

                    var isEmailExists = await IsEmailExists(user);
                    if (isEmailExists)
                    {
                        _logger.LogInformation("Already Exists.");
                        _resultApi = ResultApi.Fail(MessageHelper.AlreadyExists);
                        return Error(_resultApi);
                    }

                    var result = await _userManager.CreateAsync(user, viewModel.Password);
                    if (result.Succeeded)
                    {
                        var applicationUser = await _iSecurityManager.GetUser(viewModel.EmailAddress);

                        if (applicationUser != null)
                        {
                            _logger.LogInformation("Authenticated Successfully.");

                            await _signInManager.SignOutAsync();
                            // This doesn't count login failures towards account lockout
                            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                            var resultSignIn = await _signInManager.PasswordSignInAsync(viewModel.EmailAddress, viewModel.Password, true, lockoutOnFailure: false);

                            if (resultSignIn.Succeeded)
                            {
                                _logger.LogInformation("Login Successfully.");
                                return RegisterJsonWebToken(viewModel, applicationUser);
                            }
                            else
                            {
                                _logger.LogInformation("Login Failed.");
                                _resultApi = ResultApi.Fail(MessageHelper.LoginFail);
                                return Error(_resultApi);
                            }
                        }
                        else
                        {
                            _logger.LogInformation("Register Failed.");
                            _resultApi = ResultApi.Fail(MessageHelper.RegisterFail);
                            return Error(_resultApi);
                        }
                    }
                    else
                    {
                        _logger.LogInformation("Register Failed.");
                        _resultApi = ResultApi.Fail(MessageHelper.RegisterFail);
                        return Error(_resultApi);
                    }
                }
                else
                {
                    _logger.LogInformation("Model State Error.");
                    string message = ExceptionHelper.ModelStateErrorFirstFormat(ModelState);
                    _resultApi = ResultApi.Fail(message);
                    return Error(_resultApi);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(LogMessageHelper.FormateMessageForException(ex.Message, "Error"));
                return Error(ex);
            }
        }

        private IActionResult RegisterJsonWebToken(RegisterViewModel viewModel, ApplicationUser applicationUser)
        {
            var tokenViewModel = new TokenViewModel()
            {
                PinNumber = "123456",
                EmailAddress = applicationUser.Email,
                Password = viewModel.Password
            };
            var tokenString = _iSecurityManager.GenerateJsonWebToken(tokenViewModel);
            if (!string.IsNullOrEmpty(tokenString))
            {
                _logger.LogInformation("Token Generated Successfully.");
                var userModel = new UserViewModel()
                {
                    PinNumber = "123456",
                    UserName = applicationUser.UserName,
                    EmailAddress = applicationUser.Email,
                    Designation = "Manager",
                    RoleId = "Manager",
                    RoleName = "Manager",
                    Token = tokenString,
                };

                _resultApi = ResultApi.Ok(userModel);
                return Ok(_resultApi);
            }
            else
            {
                _logger.LogInformation("Token Generation Failed.");
                _resultApi = ResultApi.Fail(MessageHelper.TokenGenerationFailed);
                return Error(_resultApi);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            try
            {
                _logger.LogInformation("Logout Called.");

                await _signInManager.SignOutAsync();
                
                _logger.LogInformation("Logout Successfully.");
                _resultApi = ResultApi.Ok(MessageHelper.Logout);
                return Ok(_resultApi);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(LogMessageHelper.FormateMessageForException(ex.Message, "Error"));
                return Error(ex);
            }
        }

        private async Task<bool> IsEmailExists(ApplicationUser user)
        {
            try
            {
                var isExists = await _userManager.FindByEmailAsync(user.Email);

                if (isExists != null)
                {
                    string isEmailExistsMessage = string.Format(MessageHelper.IsEmailExists, user.Email);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, MessageHelper.Error);
                _logger.LogError(ex.Message);
                return false;
            }
        }

        #region Helpers

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        #endregion

        #endregion
    }
}