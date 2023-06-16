using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Dto.Crypt
{
	public class BankAccountDto : IDto
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

		[MaxLength(255)]
		public byte[] SecretKey { get; set; }

		
		private string _BankNameTemp;
		[Required]
		[MaxLength(255)]
		public string BankNameTemp
		{
			get { return _BankNameTemp; }
			set { _BankNameTemp = value.Sanitize(); }
		}

		private string _IbanTemp;
		[MaxLength(125)]
		[Required]
		public string IbanTemp
		{
			get { return _IbanTemp; }
			set { _IbanTemp = value.Sanitize(); }
		}

		private string _SwiftCodeTemp;
		[MaxLength(255)]
		public string SwiftCodeTemp
		{
			get { return _SwiftCodeTemp; }
			set { _SwiftCodeTemp = value.Sanitize(); }
		}

		private string _AccountNumberTemp;
		[MaxLength(255)]
		public string AccountNumberTemp
		{
			get { return _AccountNumberTemp; }
			set { _AccountNumberTemp = value.Sanitize(); }
		}

		private string _NoteTemp;
		[MaxLength(4096)]
		[Required]
		public string NoteTemp
		{
			get { return _NoteTemp; }
			set { _NoteTemp = value.Sanitize(); }
		}

		public bool PasswordReprompt { get; set; }

		public DateTime CreationTime { get; set; }
	}
}
