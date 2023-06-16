using Business.Abstract;
using Dto.Crypt;
using Entities;
using Filters;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PM.UI.Controllers
{
	[AuthorizeWithSession]
	public class CryptController : Controller
	{
		private readonly ICategoryService _categoriesService;
		private readonly IPasswordsService _passwordsService;
		private readonly INotesService _notesService;
		private readonly IPaymentCardsService _paymentCardsService;
		private readonly IAddressService _addressService;
		private readonly IBankAccountsService _bankAccountsService;

		public CryptController(ICategoryService categoriesService, IPasswordsService passwordsService, INotesService notesService, IPaymentCardsService paymentCardsService, IAddressService addressService, IBankAccountsService bankAccountsService)
		{
			_categoriesService = categoriesService;
			_passwordsService = passwordsService;
			_notesService = notesService;
			_paymentCardsService = paymentCardsService;
			_addressService = addressService;
			_bankAccountsService = bankAccountsService;
		}

		[BindProperty]
		public List<ECategories> CategoryList { get; set; } = new();

		[BindProperty]
		public List<PasswordDto> PasswordList { get; set; } = new();

		[BindProperty]
		public List<NoteDto> NoteList { get; set; } = new();

		[BindProperty]
		public List<PaymentCardDto> PaymentCardList { get; set; } = new();

		[BindProperty]
		public List<AddressDto> AddressList { get; set; } = new();

		[BindProperty]
        public List<BankAccountDto> BankAccountList { get; set; } = new();

        public IActionResult Index()
		{
			return Redirect("Password");
		}

		public async Task<IActionResult> Password()
		{
			string userId = HttpContext.GetCurrentUserId();

			var categoryList = await _categoriesService.FindAllAsync(x => x.Published && x.UserId == userId);

			if (categoryList.Success)
			{
				CategoryList = categoryList.Data;
			}

			var passwordList = await _passwordsService.FindAllAsync(HttpContext.GetSecretKey(), x => x.UserId == userId);

			if (passwordList.Success)
			{
				PasswordList = passwordList.Data;
			}

			return View(this);
		}

		public async Task<IActionResult> Note()
		{
			string userId = HttpContext.GetCurrentUserId();

			var categoryList = await _categoriesService.FindAllAsync(x => x.Published && x.UserId == userId);

			if (categoryList.Success)
			{
				CategoryList = categoryList.Data;
			}

			var noteList = await _notesService.FindAllAsync(x => x.UserId == userId);

			if (noteList.Success)
			{
				NoteList = noteList.Data;
			}

			return View(this);
		}

		public async Task<IActionResult> Address()
		{
			string userId = HttpContext.GetCurrentUserId();

			var categoryList = await _categoriesService.FindAllAsync(x => x.Published && x.UserId == userId);

			if (categoryList.Success)
			{
				CategoryList = categoryList.Data;
			}

			var addressList = await _addressService.FindAllAsync(x => x.UserId == userId);

			if (addressList.Success)
			{
				AddressList = addressList.Data;
			}

			return View(this);
		}

		[Route("[controller]/payment-card")]
		public async Task<IActionResult> PaymentCard()
		{
			string userId = HttpContext.GetCurrentUserId();

			var categoryList = await _categoriesService.FindAllAsync(x => x.Published && x.UserId == userId);

			if (categoryList.Success)
			{
				CategoryList = categoryList.Data;
			}

			var paymentCardList = await _paymentCardsService.FindAllAsync(HttpContext.GetSecretKey(), x => x.UserId == userId);

			if (paymentCardList.Success)
			{
				PaymentCardList = paymentCardList.Data;
			}

			return View(this);
		}

		[Route("[controller]/bank-account")]
		public async Task<IActionResult> BankAccount()
		{
			string userId = HttpContext.GetCurrentUserId();

			var categoryList = await _categoriesService.FindAllAsync(x => x.Published && x.UserId == userId);

			if (categoryList.Success)
			{
				CategoryList = categoryList.Data;
			}

			var bankAccountList = await _bankAccountsService.FindAllAsync(HttpContext.GetSecretKey(), x => x.UserId == userId);

			if (bankAccountList.Success)
			{
				BankAccountList = bankAccountList.Data;
			}

			return View(this);
		}
	}
}
