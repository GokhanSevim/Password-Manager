using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Utilities;

namespace Entities
{
	[Table("SmtpSettings")]
	public class ESmtpSettings : IEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }


		private string _Host;
		[Required, MaxLength(255)]
		public string Host
		{
			get { return _Host; }
			set { _Host = value.SanitizeUrl(); }
		}

		private string _User;
		[Required, MaxLength(60), DataType(DataType.EmailAddress)]
		public string User
		{
			get { return _User; }
			set { _User = value.ToEmail(); }
		}

		private string _Password;
		[MaxLength(50), DataType(DataType.Password)]
		public string Password
		{
			get { return _Password; }
			set { _Password = value.SanitizePassword(); }
		}

		private string _From;
		[Required, MaxLength(60), DataType(DataType.EmailAddress)]
		public string From
		{
			get { return _From; }
			set { _From = value.ToEmail(); }
		}

		private string _SenderName;
		[MaxLength(255)]
		public string SenderName
		{
			get { return _SenderName; }
			set { _SenderName = value.Sanitize(); }
		}

		public int Port { get; set; }

		public bool UseSSL { get; set; }

		public bool UseHTML { get; set; }

		
		private string _DefaultTitle;
		[MaxLength(255)]
		public string DefaultTitle
		{
			get { return _DefaultTitle; }
			set { _DefaultTitle = value.Sanitize(); }
		}

		private string _Signature;
		[MaxLength(4096)]
		public string Signature
		{
			get { return _Signature; }
			set { _Signature = value.SanitizeHTML(); }
		}

		public bool UseDefaultCredentials { get; set; }

		public int TimeOut { get; set; }

		[Required]
		public DateTime CreationTime { get; set; }
	}
}
