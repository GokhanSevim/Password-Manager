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
	[Table("Addresses")]
	public class EAddresses : IEntity
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

		private string _Firstname;
		[MaxLength(255)]
		[DataType(DataType.Text)]
		public string Firstname
		{
			get { return _Firstname; }
			set { _Firstname = value.Sanitize().ToTitleCase(); }
		}

		private string _Lastname;
		[MaxLength(255)]
		[DataType(DataType.Text)]
		public string Lastname
		{
			get { return _Lastname; }
			set { _Lastname = value.Sanitize().ToUpper(); }
		}

		private string _Email;
		[MaxLength(60)]
		[DataType(DataType.EmailAddress)]
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

        [Required]
        public DateTime CreationTime { get; set; }
    }
}
