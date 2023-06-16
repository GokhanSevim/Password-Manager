using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Dto.Crypt
{
	public class AddressDto : IDto
	{
		public int Id { get; set; }

		private string _UserId;
		[MaxLength(60)]
		public string UserId
		{
			get { return _UserId; }
			set { _UserId = value.SanitizeUrl(); }
		}

		public int CategoryId { get; set; }

		private string _CategoryTemp;
		[MaxLength(255)]
		public string CategoryTemp
		{
			get { return _CategoryTemp; }
			set { _CategoryTemp = value.Sanitize(); }
		}

		private string _Name;
		[MaxLength(255)]
		[Required]
		public string Name
		{
			get { return _Name; }
			set { _Name = value.Sanitize(); }
		}				

		private string _Firstname;
		[MaxLength(255)]
		public string Firstname
		{
			get { return _Firstname; }
			set { _Firstname = value.Sanitize(); }
		}

		private string _Lastname;
		[MaxLength(255)]
		public string Lastname
		{
			get { return _Lastname; }
			set { _Lastname = value.Sanitize(); }
		}

		public string FullName { get { return $"{Firstname} {Lastname}"; } }

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

		private string _Address;
		[Required]
		[MaxLength(1024)]
		[DataType(DataType.MultilineText)]
		public string Address
		{
			get { return _Address; }
			set { _Address = value.Sanitize(); }
		}

		private string _Note;
		[MaxLength(4096)]
		[DataType(DataType.MultilineText)]
		public string Note
		{
			get { return _Note; }
			set { _Note = value.Sanitize(); }
		}

		public bool PasswordReprompt { get; set; }

		public DateTime CreationTime { get; set; }
	}
}
