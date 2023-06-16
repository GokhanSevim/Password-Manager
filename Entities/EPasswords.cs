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
	[Table("Passwords")]
	public class EPasswords : IEntity
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

		[MaxLength(55)]
		public byte[] InitVect { get; set; }

		[Required]
        public byte[] Url { get; set; }

		[Required]
		public byte[] Name { get; set; }

		[Required]
		public byte[] UserName { get; set; }

		[Required]
		public byte[] Password { get; set; }

		public byte[] Note { get; set; }

        public bool PasswordReprompt { get; set; }

		public bool AutoLogin { get; set; }

		public bool DisableAutofill { get; set; }

		[Required]
		public DateTime CreationTime { get; set; }
	}
}
