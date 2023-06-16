using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Entities
{
	[Table("BankAccounts")]
	public class EBankAccounts : IEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		private string _UserId;
		[Required]
		[MaxLength(60)]
		public string UserId
		{
			get { return _UserId; }
			set { _UserId = value.SanitizeUrl(); }
		}

		public int CategoryId { get; set; }

		private string _Name;
		[Required]
		[MaxLength(255)]
		[DataType(DataType.Text)]
		public string Name
		{
			get { return _Name; }
			set { _Name = value.Sanitize(); }
		}

		[MaxLength(55)]
		public byte[] InitVect { get; set; }

		[Required]
		public byte[] BankName { get; set; }

		[Required]
		public byte[] Iban { get; set; }

        public byte[] SwiftCode { get; set; }

		public byte[] AccountNumber { get; set; }

		public byte[] Note { get; set; }

		public bool PasswordReprompt { get; set; }

		[Required]
        public DateTime CreationTime { get; set; }
	}
}