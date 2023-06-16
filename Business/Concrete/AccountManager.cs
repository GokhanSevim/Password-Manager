using Business.Abstract;
using Entities;
using Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Notification.Abstract;
using Results.Abstract.Results;
using Results.Concrete.Results;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using Helpers;
using Dto.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Business.Concrete
{
	[TryException]
	public class AccountManager : IAccountService
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IUserStore<ApplicationUser> _userStore;
		private readonly IUserEmailStore<ApplicationUser> _emailStore;
		private readonly ILogger<AccountManager> _logger;
		private readonly IEMailServices _emailServices;
		private readonly ICategoryService _categoryService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AccountManager(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore, ILogger<AccountManager> logger, IEMailServices emailServices, IHttpContextAccessor httpContextAccessor, ICategoryService categoryService)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_userStore = userStore;
			_emailStore = GetEmailStore();
			_logger = logger;
			_emailServices = emailServices;
			_httpContextAccessor = httpContextAccessor;
			_categoryService = categoryService;
		}

		public async Task<IDataResult<string>> AddAsync(RegisterDto Input)
		{

			var result = new DataResult<string>("", false);

			if (Input is not null)
			{
				var user = Activator.CreateInstance<ApplicationUser>();

				user.PhoneNumber = "";
				user.Avatar = "";
				user.EmailConfirmed = true;
				user.Email = Input.Email;
				user.Name = Input.Name;
				user.Surname = Input.Surname;
				user.CreationDate = DateTime.Now.ToUniversalTime();
				user.UserName = Input.Email;
				user.PhoneNumberConfirmed = false;
				user.AccessFailedCount = 0;
				user.LockoutEnabled = false;
				user.AccessFailedCount = 0;
				user.NormalizedEmail = Input.Email;
				user.NormalizedUserName = Input.Email;
				user.TwoFactorEnabled = false;
				user.SecretKey = Helpers.AesCryptHelper.GenerateRandomBytes(32);

				await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
				await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

				var insert = await _userManager.CreateAsync(user, Input.Password);

				if (insert.Succeeded)
				{
					result.Success = insert.Succeeded;
					var defaultCategory = new ECategories()
					{
						UserId = user.Id,
						IsDefault = true,
						Name = "Public",
						Description = "Default Category",
						DisplayOrder = 0,
						Published = true,
						CreationTime = DateTime.Now.ToUniversalTime()
					};

					await _categoryService.AddAsync(defaultCategory);

					_logger.LogInformation("Kullanıcı şifre ile yeni bir hesap oluşturdu.");

					var send = await _emailServices.SendAsync("Hoşgeldiniz | Password Manager", $"Merhaba sayın {user.FullName},<br>Password Manager sistemini tercih ettiğiniz için teşekkür ederiz...", Input.Email);

					if (send.Success)
					{
						result.Data = "/crypt/password/";
					}
					else
					{
						result.Message = send.Message;
					}
				}
				else
				{
					var message = new StringBuilder();

					foreach (var error in insert.Errors)
					{
						message.AppendLine($"{error.Description}.");
					}

					result.Message = message.ToString();
				}
			}

			return result;
		}

		public async Task<IDataResult<string>> ForgotPasswordAsync(ForgotPasswordDto Input)
		{
			var result = new DataResult<string>("", false);

			if (Input is not null)
			{
				var user = await _userManager.FindByEmailAsync(Input.Email);
				if (user is null || !(await _userManager.IsEmailConfirmedAsync(user)))
				{
					//Kullanıcının var olmadığını veya onaylanmadığını açıklamıyoruz.
					result.Success = true;
					result.Message = "";
					return result;
				}

				var code = await _userManager.GeneratePasswordResetTokenAsync(user);
				code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

				var request = _httpContextAccessor.HttpContext.Request;

				var uriBuilder = new UriBuilder
				{
					Scheme = request.Scheme,
					Host = request.Host.Host,
					Port = request.Host.Port.GetValueOrDefault(0),
					Path = "/account/resetPassword",
					Query = $"code={code}"
				};

				var callbackUrl = uriBuilder.ToString();

				var send = await _emailServices.SendAsync(
					"Şifre Yenileme",
					$"Şifrenizi yenilemek için <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>buraya tıklayın</a>.",
					Input.Email);

				if (send.Success)
				{
					result.Success = true;
					result.Message = "";
					result.Data = "/account/login/";
				}
			}

			return result;
		}

		public async Task<IDataResult<string>> LoginAsync(LoginDto Input)
		{
			var result = new DataResult<string>("", false);

			if (Input is not null)
			{
				var userLogin = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

				if (userLogin.Succeeded)
				{

					var user = await _userManager.FindByEmailAsync(Input.Email);

					if (user is not null)
					{
						_logger.LogInformation($"{Input.Email} kullanıcısı oturum açtı.");
						result.Success = true;
						result.Data = "/";

						//Oturum bilgisi session olarak tutuluyor.
						_httpContextAccessor.HttpContext.SetCurrentUser(user);

						return result;
					}
				}

				if (userLogin.RequiresTwoFactor)
				{
					result.Success = true;
					result.Data = $"/loginWith2fa?RememberMe={Input.RememberMe}";
					return result;
				}

				if (userLogin.IsLockedOut)
				{
					_logger.LogWarning("Kullanıcı hesabı kilitlendi.");
					result.Success = true;
					result.Data = "/lockout";
					return result;
				}
				else
				{
					result.Message = "Oturum açılamadı. Lütfen bilgilerinizi kontrol edin.";
				}
			}

			return result;
		}

		public async Task<IDataResult<string>> ResetPasswordAsync(ResetPasswordDto Input)
		{
			var result = new DataResult<string>("", false);

			if (Input is not null)
			{
				Input.Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Input.Code));

				if (string.IsNullOrWhiteSpace(Input.Code))
				{
					return result;
				}

				var user = await _userManager.FindByEmailAsync(Input.Email);
				if (user is null)
				{
					//Kullanıcının var olmadığını açıklamıyoruz.
					result.Success = true;
					result.Message = "";
					return result;
				}

				var reset = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
				if (reset.Succeeded)
				{
					result.Success = true;
					result.Message = "";
					result.Data = "/account/login/";
					return result;
				}
			}

			return result;
		}

		private IUserEmailStore<ApplicationUser> GetEmailStore()
		{
			if (!_userManager.SupportsUserEmail)
			{
				throw new NotSupportedException("Varsayılan kullanıcı arabirimi, e-posta desteği olan bir kullanıcı deposu gerektirir.");
			}

			return (IUserEmailStore<ApplicationUser>)_userStore;
		}
	}
}
