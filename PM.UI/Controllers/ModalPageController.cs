using Business.Abstract;
using Dto.Crypt;
using Entities;
using Filters;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace PM.UI.Controllers
{
	[AuthorizeWithSession]
	[TryException]
	public class ModalPageController : Controller
	{
		private readonly IPasswordsService _passwordsService;
		private readonly INotesService _notesService;
		private readonly IBankAccountsService _bankAccountsService;
		private readonly IAddressService _addressService;
		private readonly IPaymentCardsService _paymentCardsService;

		public ModalPageController(IPasswordsService passwordsService, INotesService notesService, IPaymentCardsService paymentCardsService, IBankAccountsService bankAccountsService, IAddressService addressService)
		{
			_passwordsService = passwordsService;
			_notesService = notesService;
			_paymentCardsService = paymentCardsService;
			_bankAccountsService = bankAccountsService;
			_addressService = addressService;
		}

		public async Task<IActionResult> Passwords(int Id = 0)
		{
			var model = new PasswordDto();

			if (Id > 0)
			{
				var secretKey = HttpContext.GetSecretKey();
				var userId = HttpContext.GetCurrentUserId();

				var row = await _passwordsService.GetAsync(x => x.Id == Id && x.UserId == userId, secretKey);

				if (row.Success)
				{
					model = row.Data;
				}
			}

			return View(model);
		}

		public async Task<IActionResult> Notes(int Id = 0)
		{
			var model = new NoteDto();

			if (Id > 0)
			{
				var userId = HttpContext.GetCurrentUserId();

				var row = await _notesService.GetAsync(x => x.Id == Id && x.UserId == userId);

				if (row.Success)
				{
					model = row.Data;
				}
			}

			return View(model);
		}

		public async Task<IActionResult> Address(int Id = 0)
		{
			var model = new AddressDto();

			if (Id > 0)
			{
				var userId = HttpContext.GetCurrentUserId();

				var row = await _addressService.GetAsync(x => x.Id == Id && x.UserId == userId);

				if (row.Success)
				{
					model = row.Data;
				}
			}

			return View(model);
		}

		public async Task<IActionResult> PaymentCard(int Id = 0)
		{
			var model = new PaymentCardDto();

			if (Id > 0)
			{
				var secretKey = HttpContext.GetSecretKey();
				var userId = HttpContext.GetCurrentUserId();

				var row = await _paymentCardsService.GetAsync(x => x.Id == Id && x.UserId == userId, secretKey);

				if (row.Success)
				{
					model = row.Data;
				}
			}

			return View(model);
		}

		public async Task<IActionResult> BankAccount(int Id = 0)
		{
			var model = new BankAccountDto();

			if (Id > 0)
			{
				var secretKey = HttpContext.GetSecretKey();
				var userId = HttpContext.GetCurrentUserId();

				var row = await _bankAccountsService.GetAsync(x => x.Id == Id && x.UserId == userId, secretKey);

				if (row.Success)
				{
					model = row.Data;
				}
			}

			return View(model);
		}
	}
}
