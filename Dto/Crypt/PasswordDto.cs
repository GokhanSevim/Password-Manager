using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Dto.Crypt
{
	public class PasswordDto : IDto
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

		[MaxLength(255)]
		public byte[] SecretKey { get; set; }

		private string _UrlTemp;
		[MaxLength(1024)]
		[Required]
		[DataType(DataType.Url)]
		[RegularExpression(pattern: @"^((?:http|https):\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$", ErrorMessage ="Geçersiz Url")]
		public string UrlTemp
		{
			get { return _UrlTemp; }
			set { _UrlTemp = value.SanitizeUrl(); }
		}

		private string _NameTemp;
		[MaxLength(255)]
		[Required]
		public string NameTemp
		{
			get { return _NameTemp; }
			set { _NameTemp = value.Sanitize(); }
		}

		private string _UserNameTemp;
		[MaxLength(255)]
		[Required]
		public string UserNameTemp
		{
			get { return _UserNameTemp; }
			set { _UserNameTemp = value.Sanitize(); }
		}

		private string _PasswordTemp;
		[MaxLength(255)]
		[Required]
		public string PasswordTemp
		{
			get { return _PasswordTemp; }
			set { _PasswordTemp = value.SanitizePassword(); }
		}

		private string _NoteTemp;
		[MaxLength(4096)]
		public string NoteTemp
		{
			get { return _NoteTemp; }
			set { _NoteTemp = value.Sanitize(); }
		}

		public bool PasswordReprompt { get; set; }

		public bool AutoLogin { get; set; }

		public bool DisableAutofill { get; set; }

		public DateTime CreationTime { get; set; }
	}
}
