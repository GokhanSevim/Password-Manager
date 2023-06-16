using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utilities;

namespace Dto.Account
{
    public class RegisterDto : IDto
    {
		private string _Email;
		[Required]
		[EmailAddress]
		[Display(Name = "E-Posta")]
		[DataType(DataType.EmailAddress), MaxLength(60, ErrorMessage = "Maksimum 60 karakter")]
		[RegularExpression(pattern: @"^(([^<>()[\]\\.,;:\s@\']+(\.[^<>()[\]\\.,;:\s@\']+)*)|(\'.+\'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Geçersiz E-Posta Adresi")]
		public string Email
		{
			get { return _Email; }
			set { _Email = value.ToEmail(); }
		}

		private string _Name;
		[Required]
		[Display(Name = "Adınız")]
		public string Name
		{
			get { return _Name; }
			set { _Name = value.Sanitize(); }
		}

		private string _Surname;
		[Required]
		[Display(Name = "Soyadınız")]
		public string Surname
		{
			get { return _Surname; }
			set { _Surname = value.Sanitize(); }
		}

		private string _Password;
		[Required]
		[StringLength(100, ErrorMessage = "{0}, en az {2} ve en fazla {1} karakter uzunluğunda olmalıdır.", MinimumLength = 12)]
		[DataType(DataType.Password)]
		[Display(Name = "Şifre")]
		public string Password
		{
			get { return _Password; }
			set { _Password = value.SanitizePassword(); }
		}

		private string _ConfirmPassword;
		[DataType(DataType.Password)]
		[Display(Name = "Şifre Tekrar")]
		[Compare("Password", ErrorMessage = "Şifre ve onay şifresi uyuşmuyor.")]
		public string ConfirmPassword
		{
			get { return _ConfirmPassword; }
			set { _ConfirmPassword = value.SanitizePassword(); }
		}
	}
}
