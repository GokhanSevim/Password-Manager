using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Dto.Crypt
{
	public class PaymentCardDto : IDto
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

		private string _Name;
		[MaxLength(255)]
		[Required]
		public string Name
		{
			get { return _Name; }
			set { _Name = value.Sanitize(); }
		}

		private string _CardNumberTemp;
		[MaxLength(255)]
		[Required]
		public string CardNumberTemp
		{
			get { return _CardNumberTemp; }
			set { _CardNumberTemp = value.Sanitize(); }
		}

		private string _CardOwnerTemp;
		[MaxLength(255)]
		[Required]
		public string CardOwnerTemp
		{
			get { return _CardOwnerTemp; }
			set { _CardOwnerTemp = value.Sanitize(); }
		}

		private string _SecurityCodeTemp;
		[MaxLength(255)]
		[Required]
		public string SecurityCodeTemp
		{
			get { return _SecurityCodeTemp; }
			set { _SecurityCodeTemp = value.Sanitize(); }
		}

		private string _CardTypeTemp;
		[MaxLength(255)]
		[Required]
		public string CardTypeTemp
		{
			get { return _CardTypeTemp; }
			set { _CardTypeTemp = value.Sanitize(); }
		}		

		private string _ExprationDateTemp;
		[MaxLength(255)]
		[Required]
		public string ExprationDateTemp
		{
			get { return _ExprationDateTemp; }
			set { _ExprationDateTemp = value.Sanitize(); }
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
