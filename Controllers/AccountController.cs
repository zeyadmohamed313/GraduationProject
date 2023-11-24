using GraduationProject.DTO;
using GraduationProject.Models;
using GraduationProject.Serviecs.CurrentlyReadingServices;
using GraduationProject.Serviecs.FavouriteListServices;
using GraduationProject.Serviecs.ReadServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GraduationProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _usermanger;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IConfiguration _configuration;
		private readonly ICurrentlyReadingRepository _currentlyReadingRepository;
		private readonly IToReadRepository _toReadRepository;
		private readonly IReadRepository _readRepository;
		private readonly IFavouriteListRepository _favouriteListRepository;
		public AccountController(UserManager<ApplicationUser> usermanger, IConfiguration configuration
			, ICurrentlyReadingRepository currentlyReadingRepository , IToReadRepository toReadRepository
			, IReadRepository readRepository, IFavouriteListRepository favouriteListRepository,
		  SignInManager<ApplicationUser> signInManager)
		{
		    _usermanger = usermanger;
			 _configuration = configuration;
			_currentlyReadingRepository = currentlyReadingRepository;
			_toReadRepository = toReadRepository;
			_readRepository = readRepository;
			_favouriteListRepository = favouriteListRepository;
			_signInManager=signInManager;
		}
		#region Register
		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromBody] ApplicationUserDTO TempUser)
		{
			try
			{
				if (ModelState.IsValid == true)
				{
					ApplicationUser User = new ApplicationUser();
					User.Email = TempUser.Email;
					User.Major = TempUser.Major;
					User.UserName = TempUser.UserName;
					if (await _usermanger.FindByEmailAsync(User.Email) != null)
					{
						return BadRequest("This Email Already Exsists");
					}
					else if (await _usermanger.FindByNameAsync(User.UserName) != null)
					{
						return BadRequest("This Name Is already Exsists");
					}
					IdentityResult result = await _usermanger.CreateAsync(User, TempUser.Password);
					if (result.Succeeded)
					{
						_currentlyReadingRepository
							.AddCurrentlyReadingListToUser(new CurrentlyReading() { UserId = User.Id });
						_readRepository
							.AddReadToUser(new Read() { UserId = User.Id });
						_toReadRepository
							.AddToReadToUser(new ToRead() { UserId = User.Id });
						_favouriteListRepository.
							AddFavouriteToUser(new FavouriteList() { UserId = User.Id });

						return Ok("Account Add Succeeded");
					}
					else
					{
						var Errors = string.Empty;
						foreach (var error in result.Errors)
						{
							Errors += $"{error.Description}',,,,'";
						}
						return BadRequest(Errors);
					}
				}
				else
					return BadRequest(ModelState);
			}
            catch(Exception ex)
			{
				return StatusCode(500, "An unexpected error occurred during registration.");

			}
		}
		#endregion

		#region Login
		[HttpPost("login")] 
		public async Task<IActionResult> Login(LoginDTO TempUser)
		{
			try
			{
				if (ModelState.IsValid)
				{  //check + create token  
					ApplicationUser user = await _usermanger.FindByNameAsync(TempUser.UserName);
					if (user != null)
					{
						bool found = await _usermanger.CheckPasswordAsync(user, TempUser.Password);


						if (found)
						{
							//claims token
							var claims = new List<Claim>();
							claims.Add(new Claim(ClaimTypes.Name, user.UserName));
							claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
							claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

							//get role
							var roles = await _usermanger.GetRolesAsync(user);
							foreach (var item in roles)
							{
								claims.Add(new Claim(ClaimTypes.Role, item));

							}
							SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
							//    signingCredentials 
							SigningCredentials signing = new SigningCredentials(securityKey,
							SecurityAlgorithms.HmacSha256
							);


							JwtSecurityToken MyToken = new JwtSecurityToken(
							issuer: _configuration["JWT:ValidIssuer"],//url for web api << provider
							audience: _configuration["JWT:ValidAudiance"], // url consumer << angular
							claims: claims,
							expires: DateTime.Now.AddDays(30),
							signingCredentials: signing
							);

							return Ok(new
							{
								token = new JwtSecurityTokenHandler().WriteToken(MyToken),
								expiration = MyToken.ValidTo
							});
						}

					}

					return Unauthorized();
				}
				return Unauthorized();
			}
			catch(Exception ex)
			{
				return StatusCode(500, "An unexpected error occurred during registration.");

			}
		}
		#endregion

		#region ForgetPassword
		[HttpPost("ForgetPassword")]
		public async Task<IActionResult> ForgetPassword(ForgotPasswordDTO TempUser)
		{
			try
			{
				ApplicationUser User = await _usermanger.FindByNameAsync(TempUser.UserName);
				var token = await _usermanger.GeneratePasswordResetTokenAsync(User);
				if (User != null)
				{

					var result = await _usermanger.ResetPasswordAsync(User, token, TempUser.NewPassword);
					if (result.Succeeded)
					{

						return Ok("New PassWord Add Done");
					}
					else
					{
						var Errors = string.Empty;
						foreach (var error in result.Errors)
						{
							Errors += $"{error.Description}  +  ";
						}
						return BadRequest(Errors);
					}
				}
				return Unauthorized();
			}
			catch(Exception ex)
			{
				return StatusCode(500, "An unexpected error occurred during registration.");

			}
		}
		#endregion

		#region ChangePassword
		[HttpPost("changePassword")]
		public async Task<IActionResult> changePassword(ChangePasswordDTO TempUser)
		{
			try
			{
				ApplicationUser user = await _usermanger.FindByNameAsync(TempUser.UserName);
				if (user != null)
				{

					var result = await _usermanger.ChangePasswordAsync(user, TempUser.OldPassword, TempUser.NewPassword);
					//ChangePasswordAsync(user, user.PasswordHash, NewPassword);
					//  GeneratePasswordResetTokenAsync(user);
					if (result.Succeeded)
					{

						return Ok("Password  Change Succeeded");
					}
					else
					{
						var Errors = string.Empty;
						foreach (var error in result.Errors)
						{
							Errors += $"{error.Description}  +  ";
						}
						return BadRequest(Errors);
					}


				}
				return Unauthorized();
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An unexpected error occurred during registration.");

			}
		}
		#endregion

		#region GetAuthenticatedUser
		[HttpGet("GetAuthenticatedUser")]
		public IActionResult GetAuthenticatedUser()
		{
			try
			{
				if (User.Identity.IsAuthenticated)
				{
					// User is authenticated
					var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
					// Your code here
					return Ok($"Authenticated user with ID: {userId}");
				}
				else
				{
					return Unauthorized("Not authenticated");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An unexpected error occurred during registration.");

			}
		}

		#endregion

	}

}
